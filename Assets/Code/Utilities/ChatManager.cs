using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OniosNetworKing
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private int _maxMessageCount = 25;
        [SerializeField] private List<Message> _messageList = new List<Message>();
        [SerializeField] private GameObject _chatPannel, _textObject;
        [SerializeField] private TMP_InputField _chatBox;
        void Update()
        {
            if(_chatBox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SendMessageToChat(_chatBox.text);
                    _chatBox.text = "";
                }
            }
            if (!_chatBox.isFocused)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SendMessageToChat("You pressed the space key!");
                    Debug.Log("Debug Filler yay");
                }
            }
            Debug.Log(_chatBox.isFocused);
        }
        public void SendMessageToChat(string message)
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

            _messageList.Add(newMessage);
        }
    }
    [System.Serializable]
    public class Message
    {
        public string Text;
        public TextMeshProUGUI TextObject;
    }
}
