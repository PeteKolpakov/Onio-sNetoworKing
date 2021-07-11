using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OniosNetworKing
{
    public class ChatManager : MonoBehaviour, IPunObservable
    {
        [SerializeField] private string _nickName;
        [SerializeField][Range(0,25)] private int _maxMessageCount = 25;
        [SerializeField] private List<Message> _messageList = new List<Message>();
        [SerializeField] private GameObject _chatPannel, _textObject;
        [SerializeField] private TMP_InputField _chatBox;
        [SerializeField] private Color _playerMessage, _info;
        private void Start()
        {
            _nickName = PhotonNetwork.LocalPlayer.NickName;
        }
        void Update()
        {
            if(_chatBox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SendMessageToChat(_nickName+": "+_chatBox.text,Message.MessageType.PlayerMesage);
                    _chatBox.text = "";
                }
            }
            else if(!_chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                _chatBox.ActivateInputField();
            }
            if (!_chatBox.isFocused)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SendMessageToChat("You pressed the space key!", Message.MessageType.Info);
                }
            }
        }
        [PunRPC]
        private void RPC_SendMessageToChat()
        {

        }
        public void SendMessageToChat(string message, Message.MessageType messageType)
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
            newMessage.TextObject.color = MessageTypeColor(messageType);

            _messageList.Add(newMessage);
        }
        private Color MessageTypeColor(Message.MessageType messageType)
        {
            Color color = _info;
            switch (messageType)
            {
                case Message.MessageType.PlayerMesage:
                    color = _playerMessage;
                    break;
                case Message.MessageType.Info:
                    color = _info;
                    break;
                default:
                    break;
            }
            return color;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
    [System.Serializable]
    public class Message
    {
        public string Text;
        public TextMeshProUGUI TextObject;
        public MessageType Type;

        public enum MessageType
        {
            PlayerMesage,
            Info
        }
    }
}
