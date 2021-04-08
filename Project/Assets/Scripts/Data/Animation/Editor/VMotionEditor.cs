using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// 动画资源编辑器
/// </summary>
public class VMotionEditor : OdinEditorWindow
{
    [MenuItem("VBattle编辑器/动画资源编辑器")]
    private static void OpenWindow()
    {
        GetWindow<VMotionEditor>().Show();
    }
    
    [ShowIf("motionState")]
    [ReadOnly]
    public VMotion motion;

    [ShowIf("motionState")] 
    [ReadOnly] 
    public string motionID;

    private VMotionDataGizmos vGizmos;

    private bool motionState = false;

    private GameObject prefab;
    private BoxCollider collider;

    [VerticalGroup("ModelSelect")]
    [Required]
    [InfoBox("选择模型：")]
    public GameObject modelPrefab;

    [ButtonGroup("loadSave")]
    [InfoBox("Asset中选中Motion资源",InfoMessageType.Warning)] 
    [Button(ButtonSizes.Large, ButtonStyle.CompactBox)]
    private void Load()
    {
        if (Selection.objects.Length == 0 || Selection.objects[0].name.Substring(0, 7) != "Motion_")  
        {
            DebugHelper.LogError("未选中Motion配置资源");
            motionState = false;
            return;
        }

        //打开场景
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/AnimationScene.unity");
        
        //获取选中m的motion
        motionState = true;
        motion = (VMotion)Selection.objects[0];
        
        AssetLoad();

        if (modelPrefab == null)
        {
            DebugHelper.LogError("未选择角色模型");
            return;
        }
        
        //在场景中生成与动画名同名物体（重复不添加）
        if (GameObject.Find(modelPrefab.name) != null)
            DestroyImmediate(GameObject.Find(modelPrefab.name));

        prefab = Instantiate(modelPrefab,Vector3.zero,new Quaternion(0,0,0,0));
        collider = prefab.AddComponent<BoxCollider>();

        //Gizmos
        vGizmos = prefab.AddComponent<VMotionDataGizmos>();
        vGizmos.Init(motion);
    }

    private void AssetLoad()
    {
        ///数据初始化
        motion.motionName = motion.name.Substring(7, motion.name.Length - 7);
        animationClip = motion.animationClip;
        motionID = motion.motionID;
        if (animationClip != null) 
            motionFrameMax = (int)(animationClip.frameRate * animationClip.length);

        //碰撞信息
        passiveBoxes = motion.passiveBoxes;
        hitBoxes = motion.hitBoxes;
        defenseBoxes = motion.defenseBoxes;
        animationStraights = motion.animationStraight;
    }

    private void AssetSave()
    {
        //数据保存
        motion.animationClip = animationClip;
    }
    
    [ButtonGroup("loadSave")]
    [InfoBox("点击保存", InfoMessageType.Warning)]
    [Button(ButtonSizes.Large, ButtonStyle.CompactBox)]
    private void Save()
    {
        AssetSave();
        
        //标记资源被修改并保存
        EditorUtility.SetDirty(motion);
        AssetDatabase.SaveAssets();
    }

    [Space(40)]
    [VerticalGroup("animationSelect",11)]
    [ShowIf("motionState")]
    [OnValueChanged("AnimationClipChange")]
    [Required]
    [InfoBox("选择动画片段：")] 
    public AnimationClip animationClip;

    [VerticalGroup("modelFrame",12)]
    [ShowIf("motionState")]
    [OnValueChanged("MotionFrameChange")]
    [PropertyRange(0, "motionFrameMax")] 
    public int currentFrame;
    private int motionFrameMax;
    
    private void AnimationClipChange()
    {
        motionFrameMax = (int)(animationClip.frameRate * animationClip.length);
    }

    private void MotionFrameChange()
    {
        animationClip.SampleAnimation(prefab, currentFrame / animationClip.frameRate);
        vGizmos.CurrentFrameRefrsh(currentFrame);

        //prefab.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) + prefab.transform.localPosition);
    }
    
    [Space(40)]
    [VerticalGroup("passiveBox",21)]
    [ShowIf("motionState")]
    [InfoBox("角色受击框：")]
    public List<VActorPassiveBox> passiveBoxes;

    [VerticalGroup("hitBox",31)]
    [ShowIf("motionState")]
    [InfoBox("角色攻击框：")] 
    public List<VActorHitBox> hitBoxes;
    
    [VerticalGroup("defenseBox",32)]
    [ShowIf("motionState")]
    [InfoBox("角色防御框：")] 
    public List<VActorDefenseBox> defenseBoxes;
    
    [VerticalGroup("animationStraight",33)]

    [ShowIf("motionState")] [InfoBox("动画硬直，一些无法被打断的片段")]
    public List<VAnimationStraight> animationStraights;
}


 
