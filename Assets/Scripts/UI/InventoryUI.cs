using UnityEngine;

using Scripts.PlayerInventory;
using UnityEngine.UI;
using TMPro;

namespace Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Inventory _inventory;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();

            _inventory.onInventoryChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(string[] items)
        {
            _text.text = string.Join(", ", items);
        }
    }
}
