using UnityEngine;

public class PlayerView : MonoBehaviour {
    public void SetRotate(Vector3 direction, float speed) {
        // плавный переход
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            speed * Time.deltaTime
        );
    }
}
