using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 为特效进行碰撞编辑
/// </summary>
public class VFXEditor : OdinEditorWindow
{
    [MenuItem("VBattle编辑器/特效资源编辑器")]
    private static void OpenWindow()
    {
        GetWindow<VFXEditor>().Show();
    }
    
    
}
