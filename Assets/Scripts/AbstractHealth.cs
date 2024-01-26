using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractHealth : MonoBehaviour
    {
        [SerializeField] private float _health;

        private float _maxHealth;
        private float _minHealth = 0;

        public float Health => _health;

        public void TakeDamage(float damage)
        {
            if (damage < _minHealth)
                return;

            if (_health < _minHealth)
                return;

            _health -= damage;

            if(_health < _minHealth)
            {
                _health = _minHealth;
                Die();
            }
        }

        protected abstract void Die();
    }
}