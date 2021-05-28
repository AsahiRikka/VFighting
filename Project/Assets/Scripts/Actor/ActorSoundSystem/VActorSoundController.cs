using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色声音控制器
/// </summary>
public class VActorSoundController:VSkillEventBase
{
    public VActorSoundController(VActorEvent actorEvent) : base(actorEvent)
    {
        _soundManager = GameFramework.instance._service.SoundManager;
    }

    private SoundManager _soundManager;
    
    public void SkillStartEvent(VSkillAction skillAction)
    {
        foreach (var sound in skillAction.ActionSounds)
        {
            if (sound.skillActionType == SkillActionEnum.skillAction)
            {
                _soundManager.ActorSoundGetAndPlay(sound.audioClip, sound.isLoop, sound.soundType);
            }
        }
    }
}
