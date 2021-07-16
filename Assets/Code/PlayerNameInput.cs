using UnityEngine;
using TMPro;

namespace OniosNetworKing.Assets.Code
{
    class PlayerNameInput : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _nameInput;
        private string _defaultName = "Player";
        [SerializeField]
        private ConnectionModel _connectionModel;

        public void SetPlayerName()
        {
            _connectionModel.RenameLocalPlayerTo(_nameInput.text);       
        }
    }
}
