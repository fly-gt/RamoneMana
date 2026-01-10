using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Object {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>(true);
            }

            return instance;
        }
    }
}
