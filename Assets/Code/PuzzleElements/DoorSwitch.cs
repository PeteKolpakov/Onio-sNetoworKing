using NaughtyAttributes;
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
        private Vector3 _initialPosition;
        [SerializeField][EnableIf("_myColor", SwitchColor.Green)]
        private float _resetTime = 3f;

        private void Awake()
        {
            _targetDoors = new List<Door>();
            _initialPosition = transform.position;
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
            switch (_myColor)
            {
                case SwitchColor.Green:
                    StartCoroutine(CloseDoorsWithDelay());
                    break;
                default:
                    OpenDoors();
                    break;
            }            
        }
        private void OnTriggerExit(Collider other)
        {
            StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition));
            switch (_myColor)
            {
                case SwitchColor.Blue:
                    CloseDoors();
                    break;
                default:
                    break;
            }
        }

        private void OpenDoors()
        {
            foreach (Door door in _targetDoors)
            {
                door.Open();
            }
        }

        private void CloseDoors()
        {
            foreach (Door door in _targetDoors)
            {
                door.Close();
            }
        }

        private IEnumerator CloseDoorsWithDelay()
        {
            OpenDoors();
            yield return new WaitForSeconds(_resetTime);
            CloseDoors();
        }
    }
}
