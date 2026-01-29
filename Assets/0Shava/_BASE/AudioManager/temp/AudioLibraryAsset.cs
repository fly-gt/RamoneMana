using UnityEngine;

//can be rework
[CreateAssetMenu(fileName = "AudioLibraryAsset", menuName = "ScriptableObjects/AudioLibraryAsset", order = 1)]
public class AudioLibraryAsset : ScriptableObject {
    public AudioEventAsset clickNumber;
    public AudioEventAsset successNumber;
    public AudioEventAsset failureNumber;
    public AudioEventAsset addScore;
}
