using UnityEngine;

namespace Scripts.Camera
{
    public class ThirdPersonCam : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _orientation;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _playerObj;
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Settings")]
        [SerializeField] private float _rotationSpeed = 7f;
        [SerializeField] private float _updateOrientationSpeed = 20f;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
            _orientation.forward = Vector3.Slerp(_orientation.forward, viewDir.normalized, Time.deltaTime * _updateOrientationSpeed);

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                _playerObj.forward = Vector3.Slerp(
                    _playerObj.forward,
                    inputDir.normalized,
                    Time.deltaTime * _rotationSpeed
                );
        }
    }
}
