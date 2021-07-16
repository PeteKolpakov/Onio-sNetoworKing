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
        private float _originalAlpha = 100;
        private float _timer = 3;

        [SerializeField] private GameObject _chatPannel, _textObject;
        [SerializeField] private string _nickName;
        [SerializeField] private TMP_InputField _chatBox;
        [SerializeField] private List<Message> _messageList = new List<Message>();
        [SerializeField] [Range(0, 25)] private int _maxMessageCount = 25;
        [SerializeField] private Color _whitePlayerMessage, _info, _greenPlayerMessage, _errorColor, _alphaFade, _oldTextColor;
        private void Start()
        {
            //_nickName = PhotonNetwork.LocalPlayer.NickName;
            _nickName = photonView.Owner.NickName;
            SendMessageToChat($"{_nickName}: has joined!", Message.MessageType.Info);
            SendMessageToChat("Welcome! Click on the input field or press the Enter key to use the chat", Message.MessageType.Info);
            SendMessageToChat("Write /h to see available commands", Message.MessageType.Info);
            if (!PhotonNetwork.IsConnected)
            {
                RPC_SendMessageToChat($"Not connected to Photon Server", Message.MessageType.Error);
            }
            SendMessageToChat($"{_nickName}: has joined!", Message.MessageType.Info);
        }
        void Update()
        {

            CheckMessagesOcclussion();

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

        private void CheckMessagesOcclussion()
        {
            if (!IsChatting && _alphaFade.a != (byte)0)
            {
                for (int i = 0; i < _messageList.Count; i++)
                {
                    _alphaFade = _chatBox.GetComponent<Image>().color;
                    _alphaFade = new Color32((byte)_alphaFade.r, (byte)_alphaFade.g, (byte)_alphaFade.b, (byte)0);
                    _oldTextColor = _messageList[i].TextObject.faceColor;
                    _oldTextColor = new Color32((byte)_oldTextColor.r, (byte)_oldTextColor.g, (byte)_oldTextColor.b, (byte)0);

                    _chatBox.GetComponent<Image>().color = _alphaFade;
                    _messageList[i].TextObject.faceColor = _oldTextColor;
                }
            }
            else if (IsChatting && _alphaFade.a != (byte)0)
            {
                for (int i = 0; i < _messageList.Count; i++)
                {
                    _alphaFade = _chatBox.GetComponent<Image>().color;
                    _alphaFade = new Color32((byte)_alphaFade.r, (byte)_alphaFade.g, (byte)_alphaFade.b, (byte)_originalAlpha);
                    _oldTextColor = _messageList[i].TextObject.faceColor;
                    _oldTextColor = new Color32((byte)_oldTextColor.r, (byte)_oldTextColor.g, (byte)_oldTextColor.b, (byte)_originalAlpha);

                    _chatBox.GetComponent<Image>().color = _alphaFade;
                    _messageList[i].TextObject.faceColor = _oldTextColor;
                }
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
                if (_chatBox.text.Substring(1, 1) == "h")
                {
                    RPC_SendMessageToChat("There are currently 2 commands. Write /c to change the color of your text. Write /h to see possible commands", Message.MessageType.Info);
                }
                else if (_chatBox.text.Substring(1, 1) == "c")
                {
                    RPC_SendMessageToChat("You have changed your text color! To go back to the old color, write /c in a new line", Message.MessageType.Info);
                    _changedColor = !_changedColor;
                }
                else if ( _chatBox.text.Substring(1, 1) != "c" || _chatBox.text.Substring(1, 1) != "h")
                {
                    RPC_SendMessageToChat("There was an error when inputing the commands. Please try again", Message.MessageType.Error);
                }
            }
            else
            {
                SendMessageToChat($"{_nickName}:" + _chatBox.text, Message.MessageType.PlayerMesage);
            }
        }
        public void RenameLocalPlayerTo(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
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
