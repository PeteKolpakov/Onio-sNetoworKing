using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    class DoorSwitch : SwitchPart
    {
        private List<Door> _targetDoors;

        private void Awake()
        {
            _targetDoors = new List<Door>();
            Door[] tempList = FindObjectsOfType<Door>();
            foreach (var door in tempList)
            {
                if (door.GetPartColor() == _myColor)
                {
                    _targetDoors.Add(door);
                }
            }
            if (_targetDoors.Count == 0)
            {
                Debug.LogWarning($"Warning: Switch {gameObject} has no Target Doors");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(DUtils.SlideDown(gameObject));
            foreach (Door door in _targetDoors)
            {
                door.Open();
            }
        }
    }
}
