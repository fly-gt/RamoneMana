using UnityEngine;

public class Granbber1 : MonoBehaviour {
    public Camera playerCamera;
    public Transform grabPoint;
    public LayerMask grabbableLayers;

    [Header("Grab Settings")]
    public float grabRange = 3f;
    public float followSpeed = 20f;
    public float rotationLerpSpeed = 15f;

    [Header("Debug")]
    //public Rigidbody grabbedRb;
    public GameObject grabbedGo;
    public Collider grabbedCollider;

    private Collider playerCollider;

    private void Awake() {
        if (playerCamera == null)
            playerCamera = Camera.main;

        playerCollider = GetComponent<Collider>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (grabbedGo == null) {
                TryGrabObject();
            } else {
                ReleaseObject();
            }
        }

        if (grabbedGo != null) {
            // Плавно тянем объект к grabPoint
            Vector3 toTarget = grabPoint.position - grabbedGo.transform.position;
            //grabbedRb.MovePosition();
            //grabbedRb.transform.position = grabbedRb.position * followSpeed * Time.deltaTime;
            grabbedGo.transform.position = grabPoint.position;
            // Плавный поворот к grabPoint
            Quaternion smoothedRotation = Quaternion.Slerp(grabbedGo.transform.rotation, grabPoint.rotation, rotationLerpSpeed * Time.deltaTime);
            //grabbedRb.MoveRotation(smoothedRotation);
            grabbedGo.transform.rotation = grabPoint.rotation;
        }
    }

    void LateUpdate() {
        // Кэшируем положение и поворот точки захвата для использования в FixedUpdate

    }

    void FixedUpdate() {

    }

    void TryGrabObject() {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabbableLayers)) {
            Rigidbody rb = hit.rigidbody;

            if (rb != null) {
                grabbedGo = rb.gameObject;
                grabbedCollider = hit.collider;

                // Игнор коллизий
                if (playerCollider)
                    Physics.IgnoreCollision(playerCollider, grabbedCollider, true);

                // Настройки Rigidbody
                //grabbedRb.useGravity = false;
                //grabbedRb.linearDamping = 10f; // сглаживание движения
                //grabbedRb.interpolation = RigidbodyInterpolation.Interpolate;

                grabbedGo.transform.parent = transform;
                GameObject.Destroy(rb);
            }
        }
    }

    void ReleaseObject() {
        if (grabbedGo != null && grabbedCollider != null && playerCollider != null) {
            Physics.IgnoreCollision(playerCollider, grabbedCollider, false);
        }

        if (grabbedGo != null) {
            Rigidbody rb = grabbedGo.AddComponent<Rigidbody>();

            rb.useGravity = true;
            rb.linearDamping = 0f;
            rb.transform.parent = null;
        }

        grabbedGo = null;
        grabbedCollider = null;
    }
}
