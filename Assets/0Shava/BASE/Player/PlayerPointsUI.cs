using TMPro;
using UnityEngine;

public class PlayerPointsUI : MonoBehaviour {
    public TMP_Text pointsTxt;

    public void SetPoints(int points) {
        //pointsTxt.text = points.ToString();
        pointsTxt.SetText("{0}", points);
    }
}
