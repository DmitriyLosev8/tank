using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankMover : MonoBehaviour
{
    [SerializeField] private float _speed = 7.2f;
    [SerializeField] private float _rotationSensetivity = 8f;

    private PlayerInput _playerInput;
    private Vector2 _moveInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();   
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _moveInput = _playerInput.Movement.Move.ReadValue<Vector2>();
        _playerInput.Movement.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            Quaternion rotationAngle = Quaternion.LookRotation(moveDirection);
            _rigidbody.rotation = Quaternion.Lerp(transform.rotation, rotationAngle, Time.deltaTime * _rotationSensetivity);
            Vector3 velocity = moveDirection * _speed;
            _rigidbody.velocity = velocity;
        }
    }
}