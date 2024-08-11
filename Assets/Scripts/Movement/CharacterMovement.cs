using UnityEngine;

using Scripts.Check;

namespace Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    // [RequireComponent(typeof(GroundCheck))]

    public class CharacterMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _groundDrag = 4;

        // [Header("Jump")]
        // [SerializeField] private float _jumpForce = 12;
        // [SerializeField] private float _jumpCooldown = 0.25f;
        // [SerializeField] private float _airMultiplier = 0.4f;

        // private bool canJump = true;
        // private bool _grounded;

        // private GroundCheck _groundCheck;
        private Rigidbody _rb;
        private Animator _animator;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            // _groundCheck = GetComponent<GroundCheck>();

            _rb.freezeRotation = true;

            // Without jumps & ground check
            _rb.drag = _groundDrag;
        }

        private void Update()
        {
            // _grounded = _groundCheck.OnGround();

            SpeedControl();

            _animator.SetFloat("Speed", _rb.velocity.magnitude);

            // if (_grounded)
            //     _rb.drag = _groundDrag;
            // else
            //     _rb.drag = 0;
        }

        public void Move(Vector3 movement)
        {
            Vector3 move = movement * _moveSpeed * 10f;

            // if (!_grounded)
            //     move *= _airMultiplier;

            _rb.AddForce(move, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            if (flatVelocity.magnitude > _moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _moveSpeed;
                _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);
            }
        }

        // public void Jump()
        // {
        //     if (!canJump || !_groundCheck.OnGround()) return;

        //     canJump = false;

        //     Invoke(nameof(ResetJump), _jumpCooldown);

        //     _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        //     _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        // }

        // private void ResetJump()
        // {
        //     canJump = true;
        // }
    }
}
