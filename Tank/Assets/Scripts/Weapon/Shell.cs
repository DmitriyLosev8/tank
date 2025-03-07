using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitEffect;

        private float _damage;
        private Rigidbody _rigidbody;

        private void Awake()
        {
           _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out ShotableSurface shootableSurface))
            {
                if (other.gameObject.TryGetComponent(out EnemyTank target))
                {
                    target.TakeDamage(_damage);
                }

                Debug.Log(other.name);
                gameObject.SetActive(false);
            }
        }

        public void Init(float damage)
        {
            _damage = damage;
        }
    }
}