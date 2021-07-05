using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    class Door : SwitchPart
    {
        public void Open()
        {
            StartCoroutine(DUtils.SlideDown(gameObject));
        }
    }
}
