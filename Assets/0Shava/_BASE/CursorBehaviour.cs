using UnityEngine;

public class CursorBehaviour : Singletone<CursorBehaviour> {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Cursor.visible = !Cursor.visible;
        }
#endif
    }

    public void SetCursor(bool value, string ctx = null) {
        Debug.Log($"SetCursor : {value}");

        if (value) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (ctx != null) {
            Debug.Log(ctx);
        }
    }
}
