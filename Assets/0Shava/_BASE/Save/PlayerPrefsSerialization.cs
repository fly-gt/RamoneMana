using Newtonsoft.Json;

public class PlayerPrefsSerialization : ISaveService {
    private const string key = "save_data";
    public void Save(SaveData data) {
        string json = JsonConvert.SerializeObject(data);
        UnityEngine.PlayerPrefs.SetString(key, json);
        UnityEngine.PlayerPrefs.Save();
    }

    public SaveData Load() {
        if (UnityEngine.PlayerPrefs.HasKey(key)) {
            string json = UnityEngine.PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }

        var newData = new SaveData();
        Save(newData);
        return newData;
    }
}