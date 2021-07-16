using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class CreateRoomButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _roomNameInput;
        [SerializeField]
        private ConnectionModel connectionModel;

        public void CreateRoomWithNameFromInput()  
        {
            if (!PhotonNetwork.IsConnected)
                return;

            if (connectionModel.GetAllRooms().Count > 1)
            {
                Debug.Log("youve already got a room fool, don't get greedy");
                return;
            }

            connectionModel.CreateRoom(_roomNameInput.text);            
        }
    }
}
