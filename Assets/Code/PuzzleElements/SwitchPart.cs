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

        public MechanismPart GetPartType()
        {
            return _mySwitchPart;
        }
    }
}
