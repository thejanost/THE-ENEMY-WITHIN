using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public float rotationSpeed = 120f;
    public float quickTurnSpeed = 200f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isQuickTurning = false;
    private float targetRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (vertical < 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isQuickTurning)
        {
            isQuickTurning = true;
            targetRotation = transform.eulerAngles.y + 180;
        }

        if (isQuickTurning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targetRotation, 0), quickTurnSpeed * Time.deltaTime);
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetRotation)) < 1f)
            {
                isQuickTurning = false;
            }
        }
        else
        {
            float speed = isRunning && vertical > 0 ? runSpeed : moveSpeed;
            moveDirection = transform.forward * vertical * speed;
            controller.SimpleMove(moveDirection);
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
