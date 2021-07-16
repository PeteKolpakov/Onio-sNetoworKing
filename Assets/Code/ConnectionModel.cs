using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

namespace OniosNetworKing
{
    public class ConnectionModel : MonoBehaviourPunCallbacks
    {
        List<RoomInfo> availableRooms;

        public event Action<string> ConnectionError;


        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom("default Room Name");
        }

        public void CreateRoom(string name)
        {
            PhotonNetwork.CreateRoom(name);           
        }

        internal void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void JoinRoom(string name)
        {
            PhotonNetwork.JoinRoom(name);
        }

        public void RenameLocalPlayerTo(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
        }

        internal void JoinDefaultLobby()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            availableRooms = roomList;
        }

        public List<RoomInfo> GetAllRooms()
        {
            return availableRooms;
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            ConnectionError?.Invoke("Create Room Failed: " + message);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            ConnectionError?.Invoke("Join Random Room Failed: " + message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            ConnectionError?.Invoke("Join Room Failed: " + message);
        }

        public override void OnConnectedToMaster()
        {
            JoinDefaultLobby();
        }
        internal void StartGame()
        {
            if(PhotonNetwork.IsConnectedAndReady && PhotonNetwork.IsMasterClient)
            {
                //What happens to the Player who did not creat the lobby?
                PhotonNetwork.LoadLevel(2);
            }
        }
    }
}
