using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            connectionModel.CreateRoom(_roomNameInput.text);
            Debug.Log("room made with name " + _roomNameInput.text);
        }

        

    }
}
