using UnityEngine;

using Scripts.Attributes;
using System.Linq;

namespace Scripts.PlayerInventory
{
    public class Inventory : MonoBehaviour
    {
        public delegate void OnInventoryChanged(string[] items);
        public OnInventoryChanged onInventoryChanged;

        [SerializeField, ReadOnly] private string[] items;

        public string[] GetItems() => items;

        private void Awake()
        {
            items = new string[] { };
        }

        private void Start()
        {
            onInventoryChanged?.Invoke(items);
        }

        public void AddItem(string item)
        {
            items = items.Concat(new string[] { item }).ToArray();

            Debug.Log(string.Join(", ", items));

            onInventoryChanged?.Invoke(items);
        }


        public void RemoveItem(string item)
        {
            items = items.Where(i => i != item).ToArray();

            Debug.Log(string.Join(", ", items));

            onInventoryChanged?.Invoke(items);
        }

        public bool HasItem(string item)
        {
            return items.Contains(item);
        }

    }
}
