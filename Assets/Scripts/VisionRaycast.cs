using UnityEngine;

namespace Scripts
{
    public class VisionRaycast : MonoBehaviour
    {
        public delegate void OnPlayerSpotted(Transform playerTransform);
        public OnPlayerSpotted onPlayerSpotted;

        [SerializeField] private Transform player;
        [SerializeField] private float visionRange = 10f;
        [SerializeField] private float visionAngle = 45f;
        [SerializeField] private LayerMask _obstacleLayer;

        void Update()
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (directionToPlayer.magnitude < visionRange && angle < visionAngle)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, visionRange, _obstacleLayer))
                {
                    onPlayerSpotted?.Invoke(player);
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3 center = transform.position;
            Vector3 forward = transform.forward;

            DrawVisionCone(center, forward, visionRange, visionAngle);
        }

        void DrawVisionCone(Vector3 center, Vector3 forward, float range, float angle)
        {
            Gizmos.DrawLine(center, center + forward * range);

            int segments = 30;
            float step = angle / segments;
            for (int i = 0; i <= segments; i++)
            {
                float currentAngle = i * step - (angle / 2);
                Vector3 point1 = center + Quaternion.Euler(0, currentAngle, 0) * forward * range;
                Vector3 point2 = center + Quaternion.Euler(0, currentAngle + step, 0) * forward * range;

                Gizmos.DrawLine(point1, point2);
                Gizmos.DrawLine(center + forward * range, point1);
            }
        }
    }
}
