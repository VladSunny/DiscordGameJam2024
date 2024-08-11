using TMPro;
using UnityEngine;

namespace Scripts.PlayerInventory
{
    public class FinishDoor : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private GameObject _endScene;
        [SerializeField] private TextMeshProUGUI _score;

        [Header("Settings")]
        [SerializeField] private Vector3 _popupOffset = Vector3.forward * 1.5f;
        [SerializeField] private string _key;
        [SerializeField] private string _cantOpenText;

        private bool _canInteract = true;

        public Vector3 popupOffset => _popupOffset;

        public bool CanInteract => _canInteract;

        public void Interact()
        {
            Transform player = GameObject.FindWithTag("Player").transform;

            if (_canInteract && player.GetComponent<Inventory>().HasItem(_key))
            {
                _score.text = "Score: " + player.GetComponent<Health>().LiveLeft().ToString();

                _canInteract = false;
                player.GetComponent<Inventory>().RemoveItem(_key);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0f;
                _endScene.SetActive(true);
            }
            else
            {
                GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>().OpenDialog(_cantOpenText);
            }
        }
    }
}
