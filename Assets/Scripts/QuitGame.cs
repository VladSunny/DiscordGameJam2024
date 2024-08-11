using UnityEngine;

namespace Scripts
{
    public class QuitGame : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("Quitting Game");
                Application.Quit();
            }
        }
    }
}
