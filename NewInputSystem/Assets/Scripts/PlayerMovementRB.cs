using NewInputSystem; // Importing the NewInputSystem namespace
using UnityEngine; // Importing Unity's core engine functionality
using UnityEngine.InputSystem; // Importing Unity's new Input System

public class PlayerMovementRB : MonoBehaviour
{
    // Variables
    private CustomInputs inputActions; // Reference to the input action map CustomInputs
    private Rigidbody rb; // Reference to the Rigidbody component

    private Vector2 moveDirections; // To store movement input from the player
    public float speed = 10f; // Speed at which the player will move

    public Transform cam; // Reference to the main camera's transform for adjusting player rotation
    private float smoothTurnVelocity; // Smooth turning velocity (used for smooth rotation)
    private float turnSmoothTime = 0.1f; // The time it takes to smoothly rotate the player

    public float jumpHeight = 2f; // Height the player will jump
    public float gravityScale = 2f; // Custom gravity multiplier
    private bool jumpInput = false; // A flag to track if the player has pressed the jump button

    public Transform groundCheck; // Reference to empty GameObject to check ground
    private float groundDistance = 0.2f; // Ground check sphere radius
    public LayerMask groundMask;  // Layer mask to identify what is considered ground
    bool isGrounded; // Boolean to check if the player is currently grounded

    private Vector3 velocity;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the input action map by creating an instance of CustomInputs
        inputActions = new CustomInputs();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Enable the input system
        inputActions.Player.Movement.Enable();
        inputActions.Player.Jump.Enable();

        // Subscribe to the movement action event
        inputActions.Player.Movement.performed += OnMovePerformed;
        inputActions.Player.Movement.canceled += OnMoveCancelled;

        // Subscribe to the jump action event
        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Jump.canceled += OnJumpCanceled;
    }

    // OnDisable is called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Disable the input system
        inputActions.Player.Movement.Disable();
        inputActions.Player.Jump.Disable();

        // Unsubscribe from the movement action event
        inputActions.Player.Movement.performed -= OnMovePerformed;
        inputActions.Player.Movement.canceled -= OnMoveCancelled;

        // Unsubscribe from the jump action event
        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;
    }

    // This function is called when the movement input is performed
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // To store the movement input as a Vector2
        moveDirections = context.ReadValue<Vector2>();
    }

    // This function is called when the movement input is canceled
    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        // Reset the movement direction to zero
        moveDirections = Vector2.zero;
    }

    // This function is called when the jump button is pressed
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Jump input is true when jump button is pressed
        jumpInput = true;
    }

    // This function is called when the jump button is released
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        // Jump input is false when jump button is released
        jumpInput = false;
    }

    // FixedUpdate is called at regular intervals for physics-based updates
    private void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If the player is grounded and jump input is received, trigger the jump
        if (isGrounded && jumpInput)
        {
            Jump();
        }

        // If the player is not grounded, apply additional gravity to simulate faster falling
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);
        }

        Move(); // Call the movement function to handle horizontal movement
    }

    // Handles player movement based on input and camera direction
    private void Move()
    {
        // Convert the 2D movement input into a 3D vector
        Vector3 direction = new Vector3(moveDirections.x, 0f, moveDirections.y).normalized;

        // Check if the player is on ground and movement input is significant enough to start moving the player
        if (direction.magnitude >= 0.1f && isGrounded)
        {
            // Calculate the angle for the player's rotation based on the movement direction and camera orientation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // Smoothly rotate the player towards the desired direction
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, turnSmoothTime);

            // Apply the calculated rotation to the player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate the forward direction for movement based on the player's rotation and input
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the player in the calculated direction at the defined speed
            rb.MovePosition(transform.position + moveDir.normalized * speed * Time.fixedDeltaTime);
        }
    }

    // Method to apply a jump force using Rigidbody
    private void Jump()
    {
        // Calculate and apply vertical velocity based on the desired jump height
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
