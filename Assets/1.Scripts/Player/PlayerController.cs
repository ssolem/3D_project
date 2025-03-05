using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    [Header("Look")]
    public Transform mainCamera;
    public float sensitivity;
    private Vector2 mouseInput;
    private float currentXRot;
    private float currentYRot;

    private Rigidbody _rigidbody;
    private Animator animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        Look();
    }

    private void Move()
    {
        moveDirection = (transform.forward * moveInput.y + transform.right * moveInput.x) * moveSpeed;
        moveDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    private void Look()
    {
        currentYRot += mouseInput.x * sensitivity;
        currentXRot += mouseInput.y * sensitivity;
        currentXRot = Mathf.Clamp(currentXRot, -80, 60);
        mainCamera.localEulerAngles = new Vector3(-currentXRot, 0, 0);
        transform.eulerAngles = new Vector3(0, currentYRot, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
