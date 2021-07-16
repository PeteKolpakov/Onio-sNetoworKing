using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OniosNetworKing
{
    public class ChatManager : MonoBehaviourPun, IPunObservable
    {
        public static bool IsChatting = false;

        private bool _changedColor = false;

        [SerializeField] private GameObject _chatPannel, _textObject;
        [SerializeField] private string _nickName;
        [SerializeField] private TMP_InputField _chatBox;
        [SerializeField] private List<Message> _messageList = new List<Message>();
        [SerializeField] [Range(0, 25)] private int _maxMessageCount = 25;
        [SerializeField] private Color _whitePlayerMessage, _info, _greenPlayerMessage, _errorColor;
        private void Start()
        {
            //_nickName = PhotonNetwork.LocalPlayer.NickName;
            _nickName = photonView.Owner.NickName;
            RPC_SendMessageToChat($"{_nickName}: has joined!", Message.MessageType.Info);
            RPC_SendMessageToChat("Welcome! Click on the input field or press the Enter key to use the chat", Message.MessageType.Info);
            RPC_SendMessageToChat("You can change your text color by writting '/c'", Message.MessageType.Info);
            //RPC_SendMessageToChat("You can change your text color by writting ")
            if (!PhotonNetwork.IsConnected)
            {
                RPC_SendMessageToChat($"Not connected to Photon Server", Message.MessageType.Error);
            }
            RPC_SendMessageToChat($"{_nickName}: has joined!", Message.MessageType.Info);
        }
        void Update()
        {
            //This To Freeze the player's movement, based on whether the input field is selected or not.
            CheckInputFieldFocus();

            if (_chatBox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    CheckForTypeOfInput();
                    _chatBox.text = "";
                }
            }
            else if (!_chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                _chatBox.ActivateInputField();
            }
            else if (_chatBox.isFocused && _chatBox.text == "" && Input.GetKeyDown(KeyCode.Return))
            {
                _chatBox.DeactivateInputField();
            }
        }

        private void CheckInputFieldFocus()
        {
            if (_chatBox.isFocused)
                IsChatting = true;
            else
                IsChatting = false;
        }

        private void CheckForTypeOfInput()
        {
            if (_chatBox.text.Substring(0, 1) == "/")
            {
                if(_chatBox.text.Substring(1,1)== "h")
                {
                    RPC_SendMessageToChat("There are currently 2 commands. Write /c to change the color of your text. Write /n to change your nickname", Message.MessageType.Info);
                }
                if(_chatBox.text.Substring(1, 1) == "c")
                {
                    RPC_SendMessageToChat("You have changed your text color! To go back to the old color, write /c in a new line", Message.MessageType.Info);
                    _changedColor = !_changedColor;
                }
                else if (_chatBox.text.Substring(1,1) == "n")
                {
                    RPC_SendMessageToChat("Please write your new nickname", Message.MessageType.Info);
                }
            }
            else
            {
                SendMessageToChat($"{_nickName}:" + _chatBox.text, Message.MessageType.PlayerMesage);
            }
        }

        public void SendMessageToChat(string message, Message.MessageType messageType)
        {
            photonView.RPC(nameof(RPC_SendMessageToChat), RpcTarget.All, message, messageType);
        }

        [PunRPC]
        public void RPC_SendMessageToChat(string message, Message.MessageType messageType)
        {

            if (_messageList.Count >= _maxMessageCount)
            {
                Destroy(_messageList[0].TextObject.gameObject);
                _messageList.Remove(_messageList[0]);
            }
            Message newMessage = new Message();

            newMessage.Text = message;

            GameObject newText = Instantiate(_textObject, _chatPannel.transform);

            newMessage.TextObject = newText.GetComponent<TextMeshProUGUI>();

            newMessage.TextObject.text = newMessage.Text;

            if (_changedColor == false || messageType != Message.MessageType.PlayerMesage)
            {
                newMessage.TextObject.color = MessageTypeColor(messageType);
            }
            else if (_changedColor && messageType == Message.MessageType.PlayerMesage)
            {
                //I also don't like this, but got a bit to messy to keep adding things for the sake of it without having them working correctly
                newMessage.TextObject.color = _greenPlayerMessage;
            }

            _messageList.Add(newMessage);
        }
        private Color MessageTypeColor(Message.MessageType messageType)
        {
            Color color = _info;
            switch (messageType)
            {
                case Message.MessageType.PlayerMesage:
                    color = _whitePlayerMessage;
                    break;
                case Message.MessageType.Info:
                    color = _info;
                    break;
                case Message.MessageType.Error:
                    color = _errorColor;
                    break;
                default:
                    break;
            }
            return color;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //throw new NotImplementedException();
        }
    }
    [Serializable]
    public class Message
    {
        public string Text;
        public TextMeshProUGUI TextObject;
        public MessageType Type;

        public enum MessageType
        {
            PlayerMesage,
            Info,
            Error
        }
    }
}
