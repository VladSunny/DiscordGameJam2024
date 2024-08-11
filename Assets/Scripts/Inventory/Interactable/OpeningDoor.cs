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
        [SerializeField] private bool _usingKey = true;
        [SerializeField] private bool _doorDestroying = true;
        [SerializeField] private string _get = "";

        public Vector3 popupOffset => _popupOffset;

        public bool CanInteract => _canInteract;

        private bool _canInteract = true;

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
                _canInteract = false;

                if (_usingKey)
                    inventory.RemoveItem(_key);

                if (_get != "")
                {
                    inventory.AddItem(_get);
                }

                interactionManager.GetComponent<InteractionManager>().OpenDialog(_openedMessage);

                if (_doorDestroying)
                {
                    await transform.DOScale(0, 1f).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
                    Destroy(gameObject);
                }
            }
        }
    }
}
