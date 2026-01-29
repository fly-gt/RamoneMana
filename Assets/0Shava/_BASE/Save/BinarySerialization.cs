using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinarySerialization : ISaveService {
    public void Save(SaveData data) {
        var bf = new BinaryFormatter();
        var stream = new FileStream(UnityEngine.Application.persistentDataPath, FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public SaveData Load() {
        if (File.Exists(UnityEngine.Application.persistentDataPath)) {
            var bf = new BinaryFormatter();
            var stream = new FileStream(UnityEngine.Application.persistentDataPath, FileMode.Open);
            SaveData data = bf.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else {
            var data = new SaveData();
            Save(data);
            return data;
        }
    }
}
