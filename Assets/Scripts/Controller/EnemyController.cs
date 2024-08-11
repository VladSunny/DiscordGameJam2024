using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Movement
{
    public class EnemyController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _patrolPointsParent;
        [SerializeField] private Transform _playerTransform;

        [Header("Attack")]
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _punchCooldown = 1f;

        [Header("Movement Settings")]
        [SerializeField] private float _waitTime = 0.5f;
        [SerializeField] private float _chaseRange = 1f;
        [SerializeField] private float _lostPlayerWaitTime = 3f;
        [SerializeField] private float _patrolSpeed = 3.5f;
        [SerializeField] private float _hardChaseDuration = 2f;
        [SerializeField] private float _chaseSpeed = 5.5f;
        [SerializeField] private float _lostPlayerRadius = 1f;
        [SerializeField] private float _chasingPlayerVisionAngle = 145f;
        [SerializeField] private float _patrolVisionAngle = 90f;

        public enum EnemyState
        {
            Patrolling,
            Chasing,
            LostPlayer
        }


        private NavMeshAgent _agent;
        private Animator _animator;
        private VisionRaycast _visionRaycast;
        private Transform[] _points;
        private int _currentPoint = 0;
        private EnemyState _currentState;
        private float _lostPlayerTime;
        private float _punchCooldownTimer;
        private float _hardChaseDurationTimer;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _visionRaycast = GetComponent<VisionRaycast>();
            _animator = GetComponentInChildren<Animator>();

            _points = new Transform[_patrolPointsParent.childCount];
            for (int i = 0; i < _patrolPointsParent.childCount; i++)
            {
                _points[i] = _patrolPointsParent.GetChild(i);
            }

            _currentState = EnemyState.Patrolling;
            SetNextPoint();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case EnemyState.Patrolling:
                    HandlePatrolling();
                    break;
                case EnemyState.Chasing:
                    HandleChasing();
                    break;
                case EnemyState.LostPlayer:
                    HandleLostPlayer();
                    break;
            }

            if (_hardChaseDurationTimer > 0)
                _hardChaseDurationTimer -= Time.deltaTime;

            if (_punchCooldownTimer > 0)
                _punchCooldownTimer -= Time.deltaTime;

            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }

        private void HandlePatrolling()
        {
            _animator.SetBool("Attacking", false);

            _visionRaycast.visionAngle = _patrolVisionAngle;

            float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);

            if (_visionRaycast.OnPlayerSpotted() || distanceToPlayer <= _chaseRange)
            {
                _hardChaseDurationTimer = _hardChaseDuration;
                SwitchToChasing();
                return;
            }

            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _agent.isStopped = true;
                Invoke(nameof(SetNextPoint), _waitTime);
            }
        }

        private void HandleChasing()
        {
            _visionRaycast.visionAngle = _chasingPlayerVisionAngle;

            float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);

            if (!_visionRaycast.OnPlayerSpotted() && distanceToPlayer > _chaseRange && _hardChaseDurationTimer <= 0)
            {
                SwitchToLostPlayer();
                return;
            }

            if (distanceToPlayer <= 1f)
            {
                if (_punchCooldownTimer <= 0)
                {
                    _punchCooldownTimer = _punchCooldown;
                    _animator.SetBool("Attacking", true);
                    _playerTransform.GetComponent<Health>().TakeDamageToHealth(_damage);
                }
            }
            else
            {
                _animator.SetBool("Attacking", false);
            }

            _agent.SetDestination(_playerTransform.position);
            _agent.speed = _chaseSpeed;
        }

        private void HandleLostPlayer()
        {
            _animator.SetBool("Attacking", false);

            if (Time.time - _lostPlayerTime > _lostPlayerWaitTime)
            {
                SwitchToPatrolling();
                return;
            }

            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _agent.isStopped = true;
                // Move around the last point in a small radius
                Invoke(nameof(MoveAroundLastPoint), 0.1f);
            }
        }

        private void SetNextPoint()
        {
            _currentPoint = Random.Range(0, _points.Length);
            _agent.SetDestination(_points[_currentPoint].position);
            _agent.isStopped = false;
            _agent.speed = _patrolSpeed;
        }

        private void MoveAroundLastPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _lostPlayerRadius; // Используем настраиваемый радиус
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_points[_currentPoint].position + randomDirection, out hit, _lostPlayerRadius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
            _agent.isStopped = false;
        }

        private void SwitchToChasing()
        {
            _currentState = EnemyState.Chasing;
            _agent.isStopped = false;
            _agent.speed = _chaseSpeed;
            _agent.SetDestination(_playerTransform.position);
        }

        private void SwitchToLostPlayer()
        {
            _currentState = EnemyState.LostPlayer;
            _lostPlayerTime = Time.time;
            _agent.isStopped = false;
            _agent.speed = _patrolSpeed;

            _agent.SetDestination(_playerTransform.position);
        }

        private void SwitchToPatrolling()
        {
            _currentState = EnemyState.Patrolling;
            SetNextPoint();
        }
    }
}
