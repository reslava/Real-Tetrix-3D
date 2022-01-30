using UnityEngine;
using UnityEngine.SceneManagement;

namespace RafaEslava
{
    public class Scene : MonoBehaviour
    {

        public void GameLoad()
        {         
            SceneManager.LoadScene("Tetris3D");
        }
    }
}
