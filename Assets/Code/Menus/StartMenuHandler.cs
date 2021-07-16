using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OniosNetworKing.Assets
{
    class StartMenuHandler : MonoBehaviour
    {
        public void SetScene(int target)
        {
            SceneManager.LoadScene(target);
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
