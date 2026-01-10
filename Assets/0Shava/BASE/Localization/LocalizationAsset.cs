using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationAsset", menuName = "ScriptableObjects/LocalizationAsset")]
public class LocalizationAsset : ScriptableObject {
    public LocData[] locDatas;
}

[Serializable]
public class LocData {
    public string Key;
    public string ru;
    public string en;
}
