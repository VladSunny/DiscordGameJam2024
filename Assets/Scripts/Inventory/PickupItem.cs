using UnityEngine;
using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string item;

        private bool interacted = false;

        public async void Interact()
        {
            if (interacted)
                return;

            interacted = true;

            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            Debug.Log(inventory);
            inventory.AddItem(item);

            await gameObject.transform.DOScale(0, 0.5f).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();

            Destroy(gameObject);
        }
    }
}
