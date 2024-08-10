using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Scripts.PlayerInventory
{
    public class InteractionManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _interactionPopup;
        [SerializeField] private GameObject _dialogWindow;

        private GameObject _currentInteractable;

        private void Awake()
        {
            _currentInteractable = null;
            _dialogWindow.SetActive(false);
            _interactionPopup.SetActive(false);
        }

        public void SeeItem(GameObject interactable)
        {

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
                _interactionPopup.transform.DOMove(interactable.transform.position + interactable.GetComponent<IInteractable>().popupOffset, 0.5f).SetEase(Ease.OutQuad);
            }
        }

        public void Interact()
        {
            if (_currentInteractable != null)
                _currentInteractable.GetComponent<IInteractable>().Interact();
        }

        public void OpenDialog(string text)
        {
            _dialogWindow.SetActive(true);
            _dialogWindow.GetComponentInChildren<TextMeshProUGUI>().text = text;
            _dialogWindow.GetComponentInChildren<TypingEffect>().StartTypingEffect();
        }

        public void CloseDialog()
        {
            _dialogWindow.SetActive(false);
        }
    }
}
