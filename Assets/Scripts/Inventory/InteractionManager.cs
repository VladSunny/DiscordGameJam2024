using UnityEngine;
using DG.Tweening;

namespace Scripts.PlayerInventory
{
    public class InteractionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _interactionPopup;

        [Header("UI Settings")]
        [SerializeField] private float _popupOffset = 1.5f;

        private GameObject _currentInteractable;

        private void Awake()
        {
            _currentInteractable = null;
        }

        public void SeeItem(GameObject interactable)
        {
            Debug.Log("See item: " + interactable);

            if (interactable == null)
            {
                _interactionPopup.SetActive(false);
                _currentInteractable = null;
                return;
            }

            if (_currentInteractable != interactable)
            {
                _currentInteractable = interactable;
                _interactionPopup.SetActive(true);
                _interactionPopup.transform.position = interactable.transform.position;
                _interactionPopup.transform.DOMoveY(interactable.transform.position.y + _popupOffset, 0.5f).SetEase(Ease.OutQuad);
            }
        }

        public void Interact()
        {
            if (_currentInteractable != null)
                _currentInteractable.GetComponent<IInteractable>().Interact();
        }
    }
}
