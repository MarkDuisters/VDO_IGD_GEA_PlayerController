using UnityEngine;
using UnityEngine.InputSystem;

public class Physics_PlayerMovement : MonoBehaviour
{
    Vector2 moveInput = new Vector2();
    Camera cam => Camera.main;
    Rigidbody rb => GetComponent<Rigidbody>();

    [SerializeField] float acceleration = 6f;
    [SerializeField] float maxSpeed = 6f;
    [SerializeField] float JumpForce = 6f;

    bool isMoving = false;
    int jumpCount = 0;
    [SerializeField] int jumpLimit = 2;

    //float usnigned { get { return usnigned; } set { usnigned = Mathf.Abs(value); } }

    void Update()
    {
        Vector3 getMoveDirection = MoveDirection();
        MovePlayer(getMoveDirection);
        if (isMoving) RotatePlayer(getMoveDirection);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        isMoving = moveInput.x != 0 || moveInput.y != 0;
        print(isMoving);

    }
    public void OnJump(InputValue value)
    {
        bool getJump = value.isPressed;
        bool isGrounded = GroundDetection.instance.IsGrounded(transform.position);
        if (isGrounded)
        {
            jumpCount = 0;
        }
        if (getJump && isGrounded || getJump && jumpCount <= jumpLimit)
        {
            jumpCount++;
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    Vector3 CameraAdjustedVector(Vector3 moveDirection)
    {
        Vector3 forward = cam.transform.forward * moveDirection.z;
        Vector3 right = cam.transform.right * moveDirection.x;
        Vector3 combinedDirection = forward + right;
        Vector3 finalDirection = new Vector3(combinedDirection.x, 0f, combinedDirection.z);
        return finalDirection;
    }
    Vector3 MoveDirection()
    {
        Vector3 adjustedMovement = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveDirection = CameraAdjustedVector(adjustedMovement);
        return moveDirection;
    }
    void MovePlayer(Vector3 direction)
    {
        if (!GroundDetection.instance.IsGrounded(transform.position)) return;

        Vector3 forceDirection = direction * acceleration * Time.deltaTime;

        if (rb.linearVelocity.magnitude > maxSpeed) return;
        rb.AddForce(forceDirection, ForceMode.VelocityChange);
    }
    void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
