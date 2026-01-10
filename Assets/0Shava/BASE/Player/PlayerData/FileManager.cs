using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager {
    private FileManager() { }

    public static void Save<T>(T data, string path) where T : class {
        var bf = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static T Load<T>(string path) where T : class, new() {
        if (File.Exists(path)) {
            var bf = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            T data = bf.Deserialize(stream) as T;
            stream.Close();

            return data as T; 
        } else {
            var data = new T();
            Save(data, path);

            return data as T;
        }
    }
}
