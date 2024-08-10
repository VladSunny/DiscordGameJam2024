using UnityEngine;

namespace Scripts.PlayerInventory
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private string item;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered by " + other.gameObject.name);

            if (other.CompareTag("Player"))
            {
                Inventory inventory = other.GetComponent<Inventory>();
                inventory.AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}
