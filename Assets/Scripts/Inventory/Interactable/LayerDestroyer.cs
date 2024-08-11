using UnityEngine;

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

        public void Interact()
        {
            if (_interacted)
                return;

            _interacted = true;

            if (_dialog != "")
                GameObject.FindWithTag("InteractionManager").GetComponent<InteractionManager>().OpenDialog(_dialog);


            var objects = GameObject.FindGameObjectsWithTag(_destroyTag);

            Debug.Log(objects.Length);

            foreach (var obj in objects)
            {
                Destroy(obj);
            }

        }
    }
}
