using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class RoomListing : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        public void SetText(RoomInfo roomInfo)
        {
            _text.text = roomInfo.Name + ": Players" + roomInfo.PlayerCount;
        }
        public void SetText(string roomInfo)
        {
            _text.text = roomInfo;
        }
    }
}
