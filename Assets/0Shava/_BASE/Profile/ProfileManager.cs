using Newtonsoft.Json;
using UnityEngine;

public class ProfileManager : Singletone<ProfileManager> {
    private SaveData data;
    private SaveSystem save;

    public int Score => data.Score;

    private void Awake() {
        save = new SaveSystem(new PlayerPrefsSerialization());
        data = save.Load();
#if UNITY_EDITOR
        Debug.Log(JsonConvert.SerializeObject(data));
#endif
    }

    public void SetScore(int score) {
        data.Score = score;
        save.Save(data);
    }

    public void Clear() {
        data = new();
        save.Save(data);
    }
}
