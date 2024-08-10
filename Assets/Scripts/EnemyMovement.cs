using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Movement
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _patrolPointsParent;

        [Header("Movement Settings")]
        [SerializeField] private float _waitTime = 0.5f;

        private NavMeshAgent _agent;
        private Transform[] _points;
        private int _currentPoint = 0;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _points = new Transform[_patrolPointsParent.childCount];
            for (int i = 0; i < _patrolPointsParent.childCount; i++)
            {
                _points[i] = _patrolPointsParent.GetChild(i);
            }
        }

        private void Start()
        {
            _currentPoint = Random.Range(0, _points.Length);
            _agent.SetDestination(_points[_currentPoint].position);
        }

        private void Update()
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _currentPoint = Random.Range(0, _points.Length);
                _agent.SetDestination(_points[_currentPoint].position);
                _agent.isStopped = true;
                Invoke(nameof(ResetPath), _waitTime);
            }
        }

        private void ResetPath()
        {
            _agent.isStopped = false;
        }
    }
}
