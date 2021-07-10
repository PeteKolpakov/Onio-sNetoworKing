using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing
{
    public class NicknameDisplay : MonoBehaviour
    {
        public TMP_Text Nickname;

        private void Start() {
            Nickname.text = PhotonNetwork.LocalPlayer.NickName;
        }

        private void Update() {
            transform.LookAt(Camera.main.transform);
            transform.localEulerAngles = new Vector3(90, 0, 0);
        }
    }
}
