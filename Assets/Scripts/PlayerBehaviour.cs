using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerBehaviour : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;

    [SerializeField]
    private Animator _animator;

    private InputAction _moveAction;
    private InputAction _lookAction;


    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

        _moveAction = _input.actions["Move"];
        _lookAction = _input.actions["Look"];
        
    }

    void OnEnable()
    {
        _moveAction.started += OnMoveStart;
        _moveAction.performed += OnMoving;
        _moveAction.canceled += OnMoveEnd;

        _lookAction.started += OnLook;
        _lookAction.performed += OnLook;
        _lookAction.canceled += OnLook;

    }

    void OnDisable()
    {
        _moveAction.started -= OnMoveStart;
        _moveAction.performed -= OnMoving;
        _moveAction.canceled -= OnMoveEnd;

        _lookAction.started -= OnLook;
        _lookAction.performed -= OnLook;
        _lookAction.canceled -= OnLook;
    }

#region Movement Event Methods
    void OnMoveStart(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsRunning", true);
        Vector2 input = context.ReadValue<Vector2>();
        _movement.MovementVector = new Vector3(input.x, 0.0f, input.y);
    }
    void OnMoving(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _movement.MovementVector = new Vector3(input.x, 0.0f, input.y);
    }
    void OnMoveEnd(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsRunning", false);
        Vector2 input = context.ReadValue<Vector2>();
        _movement.MovementVector = new Vector3(input.x, 0.0f, input.y);
    }
#endregion

    void OnLook(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _movement.LookVector = new Vector3(input.x, 0.0f, input.y);
    }
}
