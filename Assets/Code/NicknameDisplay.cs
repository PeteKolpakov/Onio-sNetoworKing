using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing
{
    public class NicknameDisplay : MonoBehaviour
    {
        // rotating the text to face the camera
        private void Update() {
            transform.LookAt(Camera.main.transform);
            transform.localEulerAngles = new Vector3(90, 0, 0);
        }
    }
}
