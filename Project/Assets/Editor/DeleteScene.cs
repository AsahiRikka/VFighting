using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeleteScene
{
    private static List<GameObject> deleteObj=new List<GameObject>();
    
    [MenuItem("GameTools/遍历Hierarchy")]
    static void GetAllSceneObjectsWithInactive()
    {
        var allGos = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        var previousSelection = Selection.objects;
        Selection.objects = allGos;
        var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        Selection.objects = previousSelection;
        foreach(var trans in selectedTransforms)
        {
            if (trans != null)
            {
                if (trans.position.x > 50  && trans.position.z > 50)
                {
                    deleteObj.Add(trans.gameObject);
                }
            }
        }

        foreach (var v in deleteObj)
        {
            GameObject.DestroyImmediate(v);
        }
    }
    
    
}
