using UnityEngine;

using Scripts.Attributes;

namespace Scripts
{
    public class Health : MonoBehaviour
    {
        public delegate void OnHealthChanged(float health);
        public OnHealthChanged onHealthChanged;

        [SerializeField, ReadOnly] private float _health = 0f;

        [Header("Health Settings")]
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _decayRate = 1f;
        [SerializeField] private float _decayDamage = 1f;

        private float _decayTimer = 0f;

        public float GetHealth() => _health;
        public float GetMaxHealth() => _maxHealth;

        private void Awake()
        {
            _health = _maxHealth;
        }

        private void Start()
        {
            onHealthChanged?.Invoke(_health);
        }

        private void Update()
        {
            _decayTimer += Time.deltaTime;

            if (_decayTimer >= _decayRate)
            {
                _decayTimer = 0f;
                TakeDamage(_decayDamage);
            }
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            onHealthChanged?.Invoke(_health);
        }

        public void Heal(float heal)
        {
            _health = Mathf.Clamp(_health + heal, 0, _maxHealth);
            onHealthChanged?.Invoke(_health);
        }
    }
}
