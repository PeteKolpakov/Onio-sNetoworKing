using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using System;
using ExitGames.Client.Photon;

namespace OniosNetworKing
{
    public class PhotonChatController : MonoBehaviour, IChatClientListener
    {
        [SerializeField] private string _nickName;
        private ChatClient _chatClient;
        private void Awake()
        {
            _nickName = PlayerPrefs.GetString("USERNAME");
        }
        private void Start()
        {
            _chatClient = new ChatClient(this);
            ConnectToPhotonChat();
        }

        private void ConnectToPhotonChat()
        {
            Debug.Log("Connecting to Photon Chat");
            _chatClient.AuthValues = new Photon.Chat.AuthenticationValues(_nickName);
            //ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.ChatSettings();
            //_chatClient.ConnectUsingSettings(chatSettings);
        }

        private void Update()
        {
        
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            throw new NotImplementedException();
        }

        public void OnDisconnected()
        {
            throw new NotImplementedException();
        }

        public void OnConnected()
        {
            throw new NotImplementedException();
        }

        public void OnChatStateChange(ChatState state)
        {
            throw new NotImplementedException();
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            throw new NotImplementedException();
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            throw new NotImplementedException();
        }

        public void OnUnsubscribed(string[] channels)
        {
            throw new NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            throw new NotImplementedException();
        }

        public void OnUserSubscribed(string channel, string user)
        {
            throw new NotImplementedException();
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            throw new NotImplementedException();
        }
    }
}
