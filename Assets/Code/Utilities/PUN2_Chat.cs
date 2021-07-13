using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PUN2_Chat : MonoBehaviourPun
{
    private bool _isChatting = false;
    private string _chatInput = "";

    public int FontSize = 25; public int YOffSet = 25;
    public int XOffset = 60;

    [System.Serializable]
    public class ChatMessage
    {
        public string Sender = "";
        public string Message = "";
        public float Timer = 0;
    }

    private List<ChatMessage> _chatMessages = new List<ChatMessage>();

    void Start()
    {
        //Initialize Photon View
        if (gameObject.GetComponent<PhotonView>() == null)
        {
            PhotonView photonView = gameObject.AddComponent<PhotonView>();
            photonView.ViewID = 1;
        }
        else
        {
            photonView.ViewID = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !_isChatting)
        {
            _isChatting = true;
            _chatInput = "";
        }

        //Hide messages after timer is expired
        for (int i = 0; i < _chatMessages.Count; i++)
        {
            if (_chatMessages[i].Timer > 0)
            {
                _chatMessages[i].Timer -= Time.deltaTime;
            }
        }
    }

    void OnGUI()
    {
        if (!_isChatting)
        {
            GUI.Label(new Rect(5, Screen.height - 50, 200, 50), "Press 'T' to chat");
        }
        else
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                _isChatting = false;
                if (_chatInput.Replace(" ", "") != "")
                {
                    //Send message
                    photonView.RPC("SendChat", RpcTarget.All, PhotonNetwork.LocalPlayer, _chatInput);
                }
                _chatInput = "";
            }

            GUI.SetNextControlName("ChatField");
            GUI.Label(new Rect(5, Screen.height - YOffSet, 200, 50), "Say:");
            GUIStyle inputStyle = GUI.skin.GetStyle("box");
            inputStyle.fontSize = FontSize;
            inputStyle.alignment = TextAnchor.MiddleLeft;
            _chatInput = GUI.TextField(new Rect(XOffset, Screen.height - 50, 400, 50), _chatInput, 60, inputStyle);

            GUI.FocusControl("ChatField");
        }

        //Show messages
        for (int i = 0; i < _chatMessages.Count; i++)
        {
            if (_chatMessages[i].Timer > 0 || _isChatting)
            {
                GUI.Label(new Rect(5, Screen.height - 100 - 40 * i, 500, 50), _chatMessages[i].Sender + ": " + _chatMessages[i].Message);
            }
        }
    }

    [PunRPC]
    void SendChat(Player sender, string message)
    {
        ChatMessage m = new ChatMessage();
        m.Sender = sender.NickName;
        m.Message = message;
        m.Timer = 15.0f;

        _chatMessages.Insert(0, m);
        if (_chatMessages.Count > 8)
        {
            _chatMessages.RemoveAt(_chatMessages.Count - 1);
        }
    }
}
