using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code
{
    class SwitchPart : MonoBehaviour
    {
        [SerializeField]
        protected SwitchPartType _mySwitchPart;
        [SerializeField]
        protected SwitchColor _myColor;

        public SwitchPartType GetPartType()
        {
            return _mySwitchPart;
        }
        public SwitchColor GetPartColor()
        {
            return _myColor;
        }
    }
}
