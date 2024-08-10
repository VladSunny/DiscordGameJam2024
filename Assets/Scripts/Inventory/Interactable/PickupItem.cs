using UnityEngine;
using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string item;
        [SerializeField] private string dialog;
        [SerializeField] private Vector3 _popupOffset = Vector3.up * 1.5f;

        private bool interacted = false;

        public Vector3 popupOffset => _popupOffset;

        public async void Interact()
        {
            if (interacted)
                return;

            interacted = true;

            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            Debug.Log(inventory);
            inventory.AddItem(item);

            if (dialog != "")
                GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>().OpenDialog(dialog);

            await gameObject.transform.DOScale(0, 0.5f).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();

            Destroy(gameObject);
        }
    }
}
