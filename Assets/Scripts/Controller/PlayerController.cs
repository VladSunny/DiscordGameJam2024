using UnityEngine;

using Scripts.Movement;

namespace Scripts.Controller
{
    [RequireComponent(typeof(CharacterMovement))]

    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform _orientation;

        private PlayerControls _playerControls;
        private CharacterMovement _characterMovement;

        private Vector2 _moveInput;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        private void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = _orientation.forward * _moveInput.y + _orientation.right * _moveInput.x;

            _characterMovement.Move(moveDirection.normalized);
        }

        private void PlayerInput()
        {
            _moveInput = _playerControls.Player.Movement.ReadValue<Vector2>();
        }
    }
}
