using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code
{
    class SwitchPart : MonoBehaviour
    {
        [SerializeField]
        protected MechanismPart _mySwitchPart;
        [SerializeField]
        protected SwitchType _myColor;

        public MechanismPart GetPartType()
        {
            return _mySwitchPart;
        }
        public SwitchType GetPartColor()
        {
            return _myColor;
        }
    }
}
