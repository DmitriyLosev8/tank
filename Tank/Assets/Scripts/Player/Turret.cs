using Assets.Scripts.Weapon;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _damage;
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private float _shellSpeed;

    private float _rotationSpeed = 10f;
    private ShellPool _shellPool;
    private float _coolDown = 3f;
    private float _currentCoolDown = 0f;

    private void Awake()
    {
        _shellPool = new ShellPool(_shellPrefab, _damage);
    }


    private void Update()
    {
        Rotate();
        TryToShot();
    }

    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetDirection = hitInfo.point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
           
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void TryToShot()
    {
        if(Input.GetMouseButton(0) && _currentCoolDown <= 0)
        {
            Shoot();
            _currentCoolDown = _coolDown;
        }

        _currentCoolDown -= Time.deltaTime;
    }

    private void Shoot()
    {     
        Shell shell = _shellPool.GetShell();
        shell.transform.position = _shotPoint.position;
        Rigidbody shellRigidbody = shell.GetComponent<Rigidbody>();
        shellRigidbody.AddForce(_shotPoint.forward * _shellSpeed, ForceMode.Impulse);
    }
}
