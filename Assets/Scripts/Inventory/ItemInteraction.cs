using UnityEngine;

namespace Scripts.PlayerInventory
{
    public interface IInteractable
    {
        public void Interact();
        public Vector3 popupOffset { get; }
        public bool CanInteract { get; }
    }

    public class ItemInteraction : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InteractionManager _interactionManager;

        [Header("Settings")]
        [SerializeField] private float sphereRadius = 5f;
        [SerializeField] private LayerMask interactableLayer;

        private void Update()
        {
            Vector3 position = transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(position, sphereRadius, interactableLayer);

            if (hitColliders.Length > 0)
            {
                Collider closestCollider = null;
                float closestDistance = Mathf.Infinity;

                foreach (var collider in hitColliders)
                {
                    float distance = Vector3.Distance(position, collider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = collider;
                    }
                }

                var inter = closestCollider.TryGetComponent(out IInteractable interactable);
                if (inter)
                    _interactionManager.SeeItem(closestCollider.gameObject);
            }

            else { _interactionManager.SeeItem(null); }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }
    }
}
