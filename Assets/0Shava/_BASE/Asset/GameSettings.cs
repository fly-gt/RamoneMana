using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject {
    public List<NumberData> numberData;
    public List<ProgressNumberData> progressNumberData;
}

[Serializable]
public class NumberData {
    public int Value;
    public Sprite Sprite;
}

[Serializable]
public class ProgressNumberData {
    public int Value;
    public Sprite Sprite;
}
