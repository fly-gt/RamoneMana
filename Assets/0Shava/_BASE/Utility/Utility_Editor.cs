#if UNITY_EDITOR
using UnityEditor;

public static partial class Utility {
    public static T Find<T>() where T : class {
        var guid = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        var path = AssetDatabase.GUIDToAssetPath(guid[0]);
        var obj = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;

        return obj;
    }
}
#endif