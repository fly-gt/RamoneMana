using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AE", menuName = "ScriptableObjects/AudioEventAsset", order = 1)]
public class AudioEventAsset : ScriptableObject {
    public List<AudioClip> clip;
    public float volume = 1f;
    public Vector2 pitch = Vector2.one;
}
