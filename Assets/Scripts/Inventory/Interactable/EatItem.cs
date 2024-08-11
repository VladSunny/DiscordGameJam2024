using DG.Tweening;
using UnityEngine;

namespace Scripts.PlayerInventory
{
    public class EatItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Vector3 _popupOffset = Vector3.up * 1.5f;
        [SerializeField] private string _dialog = "";
        [SerializeField] private string _get = "";
        [SerializeField] private float _snacks = 100f;

        public Vector3 popupOffset => _popupOffset;
        public bool CanInteract => _canInteract;

        private bool _canInteract = true;

        public AudioClip interactAudio; // Assign your AudioClip in the Inspector

        private AudioSource audioSource;

        void Awake()
        {
            // Get the AudioSource component attached to the AudioManager
            audioSource = GetComponent<AudioSource>();
        }

        public void Interact()
        {
            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            if (_dialog != "")
                GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>().OpenDialog(_dialog);

            if (_get != "")
                GameObject.FindWithTag("Player").GetComponent<Inventory>().AddItem(_get);

            playerTransform.GetComponent<Health>().AddSnacks(_snacks);

            _canInteract = false;

            Sequence sequence = DOTween.Sequence();

            if (audioSource != null && interactAudio != null)
            {
                audioSource.PlayOneShot(interactAudio);
            }

            sequence.Append(transform.DOMove(playerTransform.position, 0.5f).SetEase(Ease.InOutCubic));
            sequence.Join(transform.DOScale(0, 0.5f).SetEase(Ease.InOutCubic));

            sequence.OnComplete(() => Destroy(gameObject));

            sequence.Play();
        }
    }
}
