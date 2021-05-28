using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class KeySettingDictionary:SerializedMonoBehaviour
{
    public Dictionary<MyKeyCode, TMP_Dropdown> KeyDropDown;

    public Dictionary<MyKeyCode, TMP_Dropdown> KeyDropDown2;
}
