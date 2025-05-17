using UnityEngine;
using Baracuda.Monitoring;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private float _maxMovementSpeed = 1.0f;

    [SerializeField]
    private float _sprintSpeed = 1.0f;

    private float _movementSpeed;
    private float _currentVelocity;

    [Monitor]
    private Vector3 _movementVector;
    [Monitor]
    private Vector3 _lookVector;
    
    public float MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = Mathf.Clamp(value, 0.0f, _maxMovementSpeed); }
    }

    public Vector3 MovementVector 
    {  get { return _movementVector; } 
       set { _movementVector = value.normalized; } 
    }

    public Vector3 LookVector
    {
        get { return _lookVector; }
        set { _lookVector = value.normalized; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Monitor.StartMonitoring(this);
        _controller = GetComponent<CharacterController>();
        _movementSpeed = _maxMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 look = Vector3.zero;
        look +=  transform.InverseTransformDirection(transform.forward) * _lookVector.z;
        look += transform.InverseTransformDirection(transform.right) * _lookVector.x;
 
        
        //var targetAngle = Mathf.Atan2(look.x, look.z) * Mathf.Rad2Deg;

        var targetAngle = Vector3.Angle(transform.forward, look);
        var angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _currentVelocity, 0.05f);
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        //transform.Rotate(this.transform.up * Time.deltaTime, angle);
        

        //Move character based on local space
        Vector3 move = Vector3.zero;
        move += this.transform.forward * _movementVector.z;
        move += this.transform.right * _movementVector.x;
        _controller.Move(move * _movementSpeed * Time.deltaTime);

    }




}
