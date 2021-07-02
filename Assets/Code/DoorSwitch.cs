using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    class DoorSwitch : MonoBehaviour
    {
        [SerializeField]
        private Door _targetDoor;

        private void OnTriggerEnter(Collider other)
        {
            _targetDoor.Open();
        }
    }
}
