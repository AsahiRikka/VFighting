using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MackySoft.Pooling;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 声音管理，存储不同类型的声音单元
/// </summary>
public class SoundManager
{
    private AudioMixer _masterMixer;
    private GameObject _soundPrefab;
    private SoundSetting _soundSetting;
    
    public SoundManager()
    {
        
    }

    public async UniTask SoundInit()
    {
        await LoadSound();
        
        //根据设置调节音量
        _soundSetting = GameFramework.instance._service.SettingManager.GameCommonSetting.SoundSetting;
        VolumeSetting(this._soundSetting);
    }

    private async UniTask LoadSound()
    {
        //加载声音单元预设
        _soundPrefab = await AddressableResources.instance.soundPrefab.LoadAssetAsync();
        
        //建立声音单元对象池
        Pool pool = PoolManager.AddPool(_soundPrefab);
        
        //加载混音器
        _masterMixer = AddressableResources.instance.AudioMixer;
    }

    private string GroupMaster = "Master";
    private string GroupBGM = "Master/BGM";
    private string GroupEffect = "Master/Effect";
    private string GroundActorVoice = "Master/ActorVoice";

    private string MasterVolume = "MasterVolume";
    private string BGMVolume = "BGMVolume";
    private string EffectVolume = "EffectVolume";
    private string ActorVoiceVolume = "ActorVoiceVolume";
    
    public SoundBase ActorSoundGetAndPlay(AudioClip audioClip, bool isLoop,SoundTypEnum e)
    {
        //获取声音单元，修改属性并播放
        Pool pool = PoolManager.GetPoolSafe(_soundPrefab);
        SoundBase sound = pool.Get<SoundBase>();

        AudioMixerGroup mixerGroup = _masterMixer.FindMatchingGroups(GroupMaster)[0];

        switch (e)
        {
            case SoundTypEnum.BGM:
                mixerGroup = _masterMixer.FindMatchingGroups(GroupBGM)[0];
                break;
            case SoundTypEnum.Effect:
                mixerGroup = _masterMixer.FindMatchingGroups(GroupEffect)[0];
                break;
            case SoundTypEnum.ActorVoice: 
                mixerGroup = _masterMixer.FindMatchingGroups(GroundActorVoice)[0];
                break;
        }
        sound.SoundPlay(audioClip, isLoop, mixerGroup);

        return sound;
    }

    public void ActorSoundStop(SoundBase sound)
    {
        sound.SoundStop();
    }

    public void VolumeSetting(SoundSetting setting)
    {
        _soundSetting = setting;
        
        _masterMixer.SetFloat(BGMVolume, (float)setting.musicSound);
        _masterMixer.SetFloat(EffectVolume, (float) setting.effectSound);
        _masterMixer.SetFloat(ActorVoiceVolume, (float) setting.characterSound);
    }
}
