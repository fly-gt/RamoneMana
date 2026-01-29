using Newtonsoft.Json;
using UnityEngine;

public static class PlayerPrefsStorage<T> where T : class, new() {
    /// <summary>
    /// Сохраняет объект в PlayerPrefs в формате JSON.
    /// </summary>
    /// <param name="key">Ключ для хранения.</param>
    /// <param name="data">Объект для сохранения.</param>
    public static void Save(string key, T data) {
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Загружает объект из PlayerPrefs. Если ключ не найден, возвращает новый экземпляр T.
    /// </summary>
    /// <param name="key">Ключ для загрузки.</param>
    /// <returns>Объект типа T.</returns>
    public static T Load(string key) {
        if (PlayerPrefs.HasKey(key)) {
            string json = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(json);
        }
        return new T();
    }

    /// <summary>
    /// Удаляет данные по ключу.
    /// </summary>
    public static void Delete(string key) {
        if (PlayerPrefs.HasKey(key)) {
            PlayerPrefs.DeleteKey(key);
        }
    }
}