using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code
{
    class Player : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private int _playerID = 0;

        public int GetPlayerID()
        {
            return _playerID;
        }
    }
}
