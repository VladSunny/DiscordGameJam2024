using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class SnacksUI : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _health.onSnacksChanged += OnSnacksChanged;
        }

        private void OnDisable()
        {
            _health.onSnacksChanged -= OnSnacksChanged;
        }

        private void OnSnacksChanged(float snacks)
        {
            _text.text = "Snacks: " + snacks.ToString();
        }
    }
}
