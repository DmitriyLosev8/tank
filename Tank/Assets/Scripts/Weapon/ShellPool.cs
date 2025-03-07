using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class ShellPool 
    {
        private Shell _shellPrefab;
        private Shell[] _pool;
        private float _damage;
        private int _capacity = 5;

        public ShellPool(Shell shellPrefab, float damage)
        {
            _shellPrefab = shellPrefab;
            _damage = damage;
            _pool = CreateShells();
        }

        public Shell GetShell()
        {
            foreach (var shell in _pool)
            {
                if (shell.gameObject.activeSelf == false)
                {
                    shell.gameObject.SetActive(true);

                    return shell;
                }
            }

            throw new System.Exception("Not enough Shells in the pool!!!");
        }

        private Shell[] CreateShells()
        {
            Shell[] pool = new Shell[_capacity];

            for (int i = 0; i < _capacity; i++)
            {
                Shell shell = GameObject.Instantiate(_shellPrefab);
                shell.Init(_damage);
                shell.gameObject.SetActive(false);
                pool[i] = shell;
            }

            return pool;
        }
    }
}