//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerCameraAbility : BaseAbility {
//    public PlayerCameraManager cameraManager;
//    public Rigidbody rb;
//    public float mouseMultiplier = 0.3f;
//    public float clampY = 60;
//    public float lerpSpeed = 1000;
//    public float rotationSpeed = 5f;
//    public Vector3 offset;
//    public Vector3 mouse;

//    public bool block;
//    public Vector2 rotateEuler;

//    private float yaw; // Вращение по оси Y (влево/вправо)
//    private Quaternion targetRotation;

//    private void Start() {
//        cameraManager = PlayerCameraManager.Instance;
//        rb.freezeRotation = true;
//        yaw = transform.eulerAngles.y;
//        targetRotation = transform.rotation;

//#if !UNITY_EDITOR && UNITY_WEBGL
//        mouseMultiplier *= 4f;
//#endif
//    }

//    private void Update() {
//        if (block) {

//            //Debug.Log($"xxx 2 {GameController.Instance.IsPause}  {LevelManager.Instance.IsSuccess}  {LevelManager.Instance.IsLose} {block}" +
//            //    $" {PlayerBehaviour.Instance.block}");
//            return;
//        }

//        mouse = Mouse.current.delta.ReadValue();
//        yaw += mouse.x * rotationSpeed * Time.deltaTime;
//        targetRotation = Quaternion.Euler(0f, yaw, 0f);

//        var pos = transform.position + (transform.rotation * offset);
//        cameraManager.transform.position = Vector3.Lerp(cameraManager.transform.position, pos, Time.deltaTime * lerpSpeed);

//        //if (mouse != default) {
//            rotateEuler.x += mouse.x * Time.deltaTime * mouseMultiplier;
//            rotateEuler.y += -mouse.y * Time.deltaTime * mouseMultiplier;

//            rotateEuler.y = Mathf.Clamp(rotateEuler.y, -clampY, clampY);

//            cameraManager.transform.eulerAngles = new Vector3(rotateEuler.y, rotateEuler.x, 0);
//            //transform.eulerAngles = new Vector3(0, rotateEuler.x, 0);

//            targetRotation = Quaternion.Euler(new Vector3(0, rotateEuler.x, 0));
//        //}
//    }

//    private void FixedUpdate() {
//        if (rb && mouse != default) {
//            Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
//            rb.MoveRotation(smoothedRotation);
//        }
//    }

//    public void Init(Vector3 dir) {
//        var rotate = Quaternion.LookRotation(dir);
//        transform.rotation = rotate;

//        rotateEuler.x = transform.eulerAngles.y;
//        rotateEuler.y = transform.eulerAngles.x;

//        PlayerCameraManager.Instance.transform.position = transform.position + (transform.rotation * offset);
//        PlayerCameraManager.Instance.transform.forward = dir;
//    }
//}
