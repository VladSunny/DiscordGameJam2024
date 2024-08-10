using UnityEngine;

namespace Scripts.Check
{
    public class GroundCheck : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] private Vector3 boxSize = new Vector3(1f, 0.1f, 1f);
        [SerializeField] float characterHeight = 2f;
        [SerializeField] LayerMask groundLayer;

        public bool OnGround()
        {
            Vector3 position = transform.position - new Vector3(0, characterHeight / 2, 0);

            Collider[] hitColliders = Physics.OverlapBox(position, boxSize / 2, Quaternion.identity, groundLayer);

            return hitColliders.Length > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position - new Vector3(0, characterHeight / 2, 0);
            Gizmos.DrawWireCube(position, boxSize);
        }
    }
}
