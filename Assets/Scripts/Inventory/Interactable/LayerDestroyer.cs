using UnityEngine;
using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class LayerDestroyer : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _destroyTag = "Electrical";
        [SerializeField] private string _dialog;
        [SerializeField] private Vector3 _popupOffset = Vector3.forward * 1.5f;

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

            if (_dialog != "")
                GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>().OpenDialog(_dialog);

            if (audioSource != null && interactAudio != null)
            {
                audioSource.PlayOneShot(interactAudio);
            }

            var objects = GameObject.FindGameObjectsWithTag(_destroyTag);

            Debug.Log(objects.Length);

            foreach (var obj in objects)
            {
                Destroy(obj);
            }

            await gameObject.transform.DOScale(0, 1f).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
            Destroy(gameObject);

        }
    }
}
