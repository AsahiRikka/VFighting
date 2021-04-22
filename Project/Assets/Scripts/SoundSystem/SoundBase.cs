using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 播放音效的基本单元
/// </summary>
public class SoundBase : MonoBehaviour
{
    /// <summary>
    /// 声音播放器
    /// </summary>
    [SerializeField]
    private AudioSource _audioSource;

    /// <summary>
    /// 被使用时初始化工作
    /// </summary>
    public void SoundPlay(AudioClip audioClip,bool isLoop,AudioMixerGroup mixerGroup)
    {
        //音效设置
        _audioSource.clip = audioClip;
        _audioSource.loop = isLoop;
        _audioSource.outputAudioMixerGroup = mixerGroup;

        audioClipTime = audioClip.length;
        initTime = Time.time;
        currentTime = 0;
        
        //播放
        _audioSource.Play();
    }

    private float audioClipTime;

    private float currentTime;

    private float initTime;

    private void Update()
    {
        if (currentTime - initTime >= audioClipTime)
        {
            SoundStop();
        }
        currentTime = Time.time;
    }

    public void SoundStop()
    {
        _audioSource.Stop();
        gameObject.SetActive(false);
    }
}
