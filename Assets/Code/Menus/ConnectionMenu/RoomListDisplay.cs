using OniosNetworKing.Assets.Code.Menus.ConnectionMenu;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.ConnectionMenu
{
    class RoomListDisplay : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private ConnectionModel _connectionModel;
        [SerializeField]
        private GameObject _roomListingPrefab;
        [SerializeField]
        private Transform _content;

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (var room in roomList)
            {
                RoomListing listing = Instantiate(_roomListingPrefab, _content).GetComponent<RoomListing>();
                listing.SetText(room);
            }
        }
    }
}
