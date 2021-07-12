using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using System;
using ExitGames.Client.Photon;

namespace OniosNetworKing
{
    public class PhotonChatController : MonoBehaviourPun, IChatClientListener
    {
        [SerializeField] private string _nickName;
        [SerializeField] private string _useerID;
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
            ChatAppSettings chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
            _chatClient.ConnectUsingSettings(chatAppSettings);
        }
        public void SendDirectMessage(string recipient, string message)
        {
            _chatClient.SendPrivateMessage(recipient, message);
        }

        private void Update()
        {
            _chatClient.Service();
        }

        public void DebugReturn(DebugLevel level, string message)
        {

        }

        public void OnDisconnected()
        {
            Debug.Log("You have disconnected to the photon Chat");
        }

        public void OnConnected()
        {
            Debug.Log("You have connected to the photon Chat");
            SendDirectMessage("Brilliath", "Hi Brilliath");
        }

        public void OnChatStateChange(ChatState state)
        {
            
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
           
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            if (!string.IsNullOrEmpty(message.ToString()))
            {
                //Chanel Name format [Sender:Recipient]
                string[] splatnames = channelName.Split(new char[] {':'});
                string senderName = splatnames[0];
                if (!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log($"{ sender}:{ message}");
                }
            }
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            
        }

        public void OnUnsubscribed(string[] channels)
        {
            
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            
        }

        public void OnUserSubscribed(string channel, string user)
        {
            
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            
        }
    }
}
