using UnityEngine;
using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string item;
        [SerializeField] private string dialog;
        [SerializeField] private Vector3 _popupOffset = Vector3.up * 1.5f;

        public Vector3 popupOffset => _popupOffset;

        public bool CanInteract => !_interacted;

        private bool _interacted = false;

        public AudioClip interactAudio; // Assign your AudioClip in the Inspector

        private AudioSource audioSource;

        void Awake()
        {
            // Get the AudioSource component attached to the AudioManager
            audioSource = GetComponent<AudioSource>();
        }

        public async void Interact()
        {
            if (_interacted)
                return;

            _interacted = true;

            if (audioSource != null && interactAudio != null)
            {
                audioSource.PlayOneShot(interactAudio);
            }

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
