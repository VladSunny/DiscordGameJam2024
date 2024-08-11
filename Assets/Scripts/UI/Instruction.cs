using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Instruction : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 0f;
        }
        void Update()
        {
            if (Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
        }
    }
}
