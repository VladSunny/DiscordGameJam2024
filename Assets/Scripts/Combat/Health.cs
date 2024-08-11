using UnityEngine;

using Scripts.Attributes;

namespace Scripts
{
    public class Health : MonoBehaviour
    {
        public delegate void OnHealthChanged(float health);
        public OnHealthChanged onHealthChanged;

        public delegate void OnSnacksChanged(float snacks);
        public OnSnacksChanged onSnacksChanged;

        [SerializeField, ReadOnly] private float _health = 0f;
        [SerializeField, ReadOnly] private float _snacks = 0f;

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
            onSnacksChanged?.Invoke(_snacks);
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
            if (_snacks >= damage)
            {
                _snacks -= damage;
                onSnacksChanged?.Invoke(_snacks);
                return;
            }
            else if (_snacks > 0)
            {
                damage -= _snacks;
                _snacks = 0;
                onSnacksChanged?.Invoke(_snacks);
                _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
                onHealthChanged?.Invoke(_health);
                return;
            }

            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            onHealthChanged?.Invoke(_health);
        }

        public void Heal(float heal)
        {
            _health = Mathf.Clamp(_health + heal, 0, _maxHealth);
            onHealthChanged?.Invoke(_health);
        }

        public void AddSnacks(float snacks)
        {
            _snacks += snacks;
            onSnacksChanged?.Invoke(_snacks);
        }

        public float LiveLeft()
        {
            return _health + _snacks;
        }
    }
}
