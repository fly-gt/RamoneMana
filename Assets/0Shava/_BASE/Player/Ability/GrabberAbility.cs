using TMPro;
using UnityEngine;

public class GrabberAbility : MonoBehaviour {
    public Camera playerCamera;
    public Transform grabPoint;
    public LayerMask grabbableLayers;

    [Header("Grab Settings")]
    public float grabRange = 3f;
    public float followSpeed = 20f;
    public float rotationLerpSpeed = 15f;

    [Header("Debug")]
    public Rigidbody grabbedRb;
    public Collider grabbedCollider;

    private Collider playerCollider;
    private Vector3 cachedGrabPos;
    private Quaternion cachedGrabRot;

    private void Awake() {
        if (playerCamera == null)
            playerCamera = Camera.main;

        playerCollider = GetComponent<Collider>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (grabbedRb == null) {
                TryGrabObject();
            } else {
                ReleaseObject();
            }
        }
    }

    void LateUpdate() {
        // Кэшируем положение и поворот точки захвата для использования в FixedUpdate
        if (grabbedRb != null) {
            cachedGrabPos = playerCamera.transform.position + playerCamera.transform.forward;
            cachedGrabRot = grabPoint.rotation;
        }
    }

    void FixedUpdate() {
        if (grabbedRb != null) {
            // Плавно тянем объект к grabPoint
            Vector3 toTarget = cachedGrabPos - grabbedRb.position;
            grabbedRb.MovePosition(grabbedRb.position + toTarget * followSpeed * Time.fixedDeltaTime);

            // Плавный поворот к grabPoint
            Quaternion smoothedRotation = Quaternion.Slerp(grabbedRb.rotation, cachedGrabRot, rotationLerpSpeed * Time.fixedDeltaTime);
            grabbedRb.MoveRotation(smoothedRotation);
        }
    }

    void TryGrabObject() {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabbableLayers)) {
            Rigidbody rb = hit.rigidbody;
            if (rb != null) {
                grabbedRb = rb;
                grabbedCollider = hit.collider;

                // Игнор коллизий
                if (playerCollider)
                    Physics.IgnoreCollision(playerCollider, grabbedCollider, true);

                // Настройки Rigidbody
                grabbedRb.useGravity = false;
                grabbedRb.linearDamping = 10f; // сглаживание движения
                grabbedRb.interpolation = RigidbodyInterpolation.Interpolate;
            }
        }
    }

    void ReleaseObject() {
        if (grabbedRb != null && grabbedCollider != null && playerCollider != null) {
            Physics.IgnoreCollision(playerCollider, grabbedCollider, false);
        }

        if (grabbedRb != null) {
            grabbedRb.useGravity = true;
            grabbedRb.linearDamping = 0f;
        }

        grabbedRb = null;
        grabbedCollider = null;
    }
}
