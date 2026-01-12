using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject {
    public List<NumberData> numberData;
}

[Serializable]
public class NumberData {
    public int Value;
    public Sprite Sprite;
}
