using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipSettings", menuName = "ScriptableObjects/EquipSettings", order = 1)]
public class EquipSettings : ScriptableObject {
    public List<EquipData> equipData;
}

[Serializable]
public class EquipData {
    public string key;
    public Sprite icon;
    public GameObject gameObject;
}
