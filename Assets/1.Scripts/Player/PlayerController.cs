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
    public LayerMask groundLayer;
    public float jumpStamina;
    public float moveStamina;

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
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
        Move();
        Look();    
        }
    }

    public void EnableMouseControl()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableMouseControl()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Move()
    {
        Vector3 moveDirection = (transform.forward * moveInput.y + transform.right * moveInput.x) * moveSpeed;
        moveDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;

        if (Mathf.Abs(_rigidbody.velocity.x) > 1 || Mathf.Abs(_rigidbody.velocity.z) > 1)
        {
            GameManager.Instance.Player.condition.LoseStamina(moveStamina * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
            animator.SetBool("Move", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            animator.SetBool("Move", false);
        }
    }

    private void Look()
    {
        currentYRot += mouseInput.x * sensitivity;
        currentXRot += mouseInput.y * sensitivity;
        currentXRot = Mathf.Clamp(currentXRot, -74, 70);
        mainCamera.localEulerAngles = new Vector3(-currentXRot, 0, 0);
        transform.eulerAngles = new Vector3(0, currentYRot, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && CanJump())
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            GameManager.Instance.Player.condition.LoseStamina(jumpStamina);
        }
    }

    private bool CanJump()
    {
        if (GameManager.Instance.Player.condition.EnoughStamina(jumpStamina))
        {
            Ray[] rays = new Ray[4]
            {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
            };

            foreach (Ray r in rays)
            {
                Debug.DrawRay(r.origin, r.direction, Color.red, 1f);
                if (Physics.Raycast(r, 0.1f, groundLayer))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            GameManager.Instance.Player.interact.Invoke();
        }
    }

    public void OnView(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            GameManager.Instance.Player.camControl.ChangePlayerPer();
        }
    }
}
