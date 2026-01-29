//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerMovementAbility : BaseAbility {
//    public Rigidbody rb;
//    public Animator animator;
//    public InputActionReference input;
//    public float moveMultiplier = 1f;
//    [Space]
//    public Vector2 inputDirection;
//    public Vector3 velocity = default;

//    public bool moving;
//    public bool block;

//    private string
//        x = "x",
//        y = "y";

//    private void Update() {
//        inputDirection = input.action.ReadValue<Vector2>();
//        Vector3 velocity = rb.linearVelocity == default ? Vector3.zero : rb.linearVelocity;
//        moving = velocity != default;

//        //Debug.Log($"{velocity.magnitude}  {moving}");
//    }

//    private void FixedUpdate() {
//        if (block) {

//            rb.linearVelocity = default;
//            animator.SetFloat(x, 0);
//            animator.SetFloat(y, 0);
//            moving = false;

//            //Debug.Log($"xxx  {GameController.Instance.IsPause}  {LevelManager.Instance.IsSuccess}  {LevelManager.Instance.IsLose} {block}" +
//            //    $" {PlayerBehaviour.Instance.block}");
//            return;
//        }

//        Move();
//    }

//    private void Move() {
//        velocity = default;

//        if (!Mathf.Approximately(inputDirection.y, 0)) {
//            velocity += transform.forward.normalized * Mathf.Sign(inputDirection.y);
//        }

//        if (!Mathf.Approximately(inputDirection.x, 0)) {
//            velocity += transform.right.normalized * Mathf.Sign(inputDirection.x);
//        }

//        rb.linearVelocity = velocity * moveMultiplier + new Vector3(0, rb.linearVelocity.y, 0);

//        animator.SetFloat(x, inputDirection.normalized.x);
//        animator.SetFloat(y, inputDirection.normalized.y);
//    }
//}
