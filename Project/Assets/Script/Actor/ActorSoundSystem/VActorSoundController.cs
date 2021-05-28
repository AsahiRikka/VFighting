using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色声音控制器
/// </summary>
public class VActorSoundController:VSkillEventBase
{
    public VActorSoundController(VActorEvent actorEvent,VActorInfo actorInfo) : base(actorEvent)
    {
        _soundManager = GameFramework.instance._service.SoundManager;
        _animationInfo = actorInfo.animationInfo;
    }

    private SoundManager _soundManager;
    private VActorAnimationInfo _animationInfo;

    private List<SoundBase> updateSoundList = new List<SoundBase>();
    public void SkillStartEvent(VSkillAction skillAction)
    {
        tempSound.Clear();
        updateSoundList.Clear();
        
        foreach (var sound in skillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.skillAction)
            {
                _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
            }
        }
        
        foreach (var sound in skillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.skillUpdate)
            {
                SoundBase s = _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
                updateSoundList.Add(s);
            }
        }
    }

    protected override void ActorAttackEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.ActorAttackEvent(activeSkillAction, passiveSkillAction);

        foreach (var sound in activeSkillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.skillHit)
            {
                _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
            }
        }
    }


    private List<VSkillAction_Sound> tempSound = new List<VSkillAction_Sound>();
    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        base.SkillUpdateEvent(skillAction);
        
        foreach (var sound in skillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.keyFrame)
            {
                if (_animationInfo.currentFrame > sound.keyFrame && !tempSound.Contains(sound))
                {
                    _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
                    tempSound.Add(sound);
                    
                }
            }
        }
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        base.SkillEndEvent(currentSkill, nextSkill);

        foreach (var sound in updateSoundList)
        {
            _soundManager.ActorSoundStop(sound);
        }
    }

    protected override void DefenceEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.DefenceEvent(activeSkillAction, passiveSkillAction);
        
        foreach (var sound in activeSkillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.successDefence)
            {
                _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
            }
        }
    }
}
