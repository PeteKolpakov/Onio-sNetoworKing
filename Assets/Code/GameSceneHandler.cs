using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace OniosNetworKing
{
    public class GameSceneHandler : MonoBehaviourPunCallbacks
    {
        [SerializeField] string playerPrefabName;
        public Transform FirstPlayerPosition;
        public Transform SecondPlayerPosition;

        private void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
            }
            else
            {
                CreatePlayerPrefab();
            }
        }

        private void CreatePlayerPrefab()
        {
            if (PhotonNetwork.IsConnected)
            {
                if(PhotonNetwork.LocalPlayer.IsMasterClient){
                    PhotonNetwork.Instantiate(playerPrefabName, FirstPlayerPosition.position, Quaternion.identity);
                }else{
                    PhotonNetwork.Instantiate(playerPrefabName, SecondPlayerPosition.position, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("Not connected to photon network");
            }
        }

        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.OfflineMode)
            {
                PhotonNetwork.CreateRoom("OfflineMode123");
            }
        }

        public override void OnCreatedRoom()
        {
            if (PhotonNetwork.OfflineMode)
            {
                CreatePlayerPrefab();
            }
        }
    }
}
