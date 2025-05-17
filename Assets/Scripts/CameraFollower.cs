using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _objectToFollow;

    private Vector3 _orignalVector;
    private float _originalDistance;
    private Camera _camera;

    private void Awake()
    { 
        _camera = GetComponent<Camera>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _orignalVector = this.transform.position - _objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _objectToFollow.position + _orignalVector;
    }
}
