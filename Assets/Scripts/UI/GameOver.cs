using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class GameOver : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Health _health;

        private bool _gameOver = false;

        private void Awake()
        {
            _health.onHealthChanged += OnHealthChanged;
        }

        private void Start()
        {
            _gameOverPanel.SetActive(false);
        }

        private void OnHealthChanged(float health)
        {
            if (_health.LiveLeft() <= 0 && !_gameOver)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                _gameOver = true;
                Time.timeScale = 0f;
                _gameOverPanel.SetActive(true);
            }
        }
    }
}
