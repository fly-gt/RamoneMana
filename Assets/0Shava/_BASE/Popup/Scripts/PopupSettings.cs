using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupSettings", menuName = "ScriptableObjects/PopupSettings", order = 2)]
public class PopupSettings : ScriptableObject {
    public float FadeOnDuration = 0.3f;
    public float FadeOffDuration = 0.2f;
}
