using UnityEngine;

using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class OpeningDoor : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private string _cantOpenMessage = "The door is locked.";
        [SerializeField] private string _openedMessage = "The door is now open.";
        [SerializeField] private string _key = "key";
        [SerializeField] private Vector3 _popupOffset = Vector3.forward * 1.5f;
        public Vector3 popupOffset => _popupOffset;

        public async void Interact()
        {
            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            InteractionManager interactionManager = GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>();

            if (!inventory.HasItem(_key))
            {
                interactionManager.GetComponent<InteractionManager>().OpenDialog(_cantOpenMessage);
            }
            else
            {
                inventory.RemoveItem(_key);
                interactionManager.GetComponent<InteractionManager>().OpenDialog(_openedMessage);
                await transform.DOScale(0, 1f).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
                Destroy(gameObject);
            }
        }
    }
}
