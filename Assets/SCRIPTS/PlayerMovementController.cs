using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float jogSpeed = 6f;
    public float rotationSpeed = 180f;

    private CharacterController characterController;
    private bool isTurningAround = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component missing from the player object.");
        }
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Get input for rotation and forward movement
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys for rotation
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys for forward/backward movement

        // Determine movement speed
        bool isJogging = Input.GetKey(KeyCode.LeftShift); // Hold Shift to jog
        float currentSpeed = isJogging ? jogSpeed : walkSpeed;

        // Handle 180° turn
        if (verticalInput < 0 && isJogging && !isTurningAround)
        {
            StartCoroutine(PerformTurnAround());
            return; // Skip other movement during the turn-around animation
        }

        // Rotate the player
        if (horizontalInput != 0)
        {
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }

        // Move the player forward/backward
        if (verticalInput != 0)
        {
            Vector3 moveDirection = transform.forward * verticalInput;
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
    }

    private System.Collections.IEnumerator PerformTurnAround()
    {
        isTurningAround = true;

        // Perform a smooth 180° rotation
        float totalRotation = 0f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        while (totalRotation < 180f)
        {
            float rotationThisFrame = Mathf.Min(rotationStep, 180f - totalRotation);
            transform.Rotate(Vector3.up, rotationThisFrame);
            totalRotation += rotationThisFrame;
            yield return null; // Wait for the next frame
        }

        isTurningAround = false;
    }
}
