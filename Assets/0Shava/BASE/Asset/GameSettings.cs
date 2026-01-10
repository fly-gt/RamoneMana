using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject {
    public PlayerView player;
    [Space]
    public Color successColor, loseColor;
    [Space]
    public Material green;
    public Material red;
    public Material blue;
    [Space]
    public Material greenArrow;
    public Material redArrow;
    public Material blueArrow;
}
