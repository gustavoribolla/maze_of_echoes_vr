using UnityEngine;
using UnityEngine.InputSystem;

public class DesktopTestMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool enableKeyboardDebug = true;

    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!enableKeyboardDebug) return;
        if (Keyboard.current == null || characterController == null) return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1f;
        if (Keyboard.current.sKey.isPressed) input.y -= 1f;
        if (Keyboard.current.dKey.isPressed) input.x += 1f;
        if (Keyboard.current.aKey.isPressed) input.x -= 1f;

        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (Keyboard.current.qKey.isPressed)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        if (Keyboard.current.eKey.isPressed)
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }

        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }
}