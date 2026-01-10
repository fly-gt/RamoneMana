using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Localization : Singletone<Localization> {
    public LocalizationAsset asset;
    public Dictionary<string, LocData> data;
    public string enStr = "en";
    public string ruStr = "ru";

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        data = asset.locDatas.ToDictionary(x => x.Key);
    }

    public string Get(string key) {
        if (!data.ContainsKey(key)) {
            Debug.LogError($"{nameof(Localization)} {nameof(Get)} {key} : didnt find");
            return key;
        }

        //return data[key].en;

        if (GetLanguage() == ruStr) {
            return data[key].ru;
        }

        return data[key].en;
    }

    private string GetLanguage() {
        return ruStr;
        //return YandexGeneral.Instance.Lang;
    }

#if UNITY_EDITOR
    public static LocData GetData_DEBUG(string key) {
        var asset = Utility.Find<LocalizationAsset>();
        var found = asset.locDatas.FirstOrDefault(x => x.Key == key);

        if (found == null) {
            return null;
        }

        return found;
    }
#endif
}
