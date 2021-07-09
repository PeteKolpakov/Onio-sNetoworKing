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
        [SerializeField] private List<Door> _targetDoors;
        private Vector3 _initialPosition;
        [SerializeField] [EnableIf("_myType", SwitchType.Timed)]
        private float _resetTime = 3f;
        [SerializeField]
        protected SwitchType _myType;

        private bool _isPressed = false;
        private List<Collider> _thingsStandingOnMe;

        private void Awake()
        {
            _thingsStandingOnMe = new List<Collider>();
            _initialPosition = transform.position;
            if (_targetDoors.Count == 0)
            {
                Debug.LogWarning($"Warning: Switch {gameObject} has no Target Doors");
            }
        }

        private void Update()
        {
            CheckPressedStatus();
            Debug.Log($"is pressed is {_isPressed}, there are {_thingsStandingOnMe.Count} things on the switch");
        }
        private bool CheckPressedStatus()
        {
            if (!_isPressed)
                return false;

            if (_thingsStandingOnMe.Count > 0)
            {
                _isPressed = true;
            }
            else if(_thingsStandingOnMe.Count <= 0)
            {
                _isPressed = false;
            }
            return _isPressed;
        }

        private void OnTriggerEnter(Collider other)
        {
            _isPressed = true;

            StartCoroutine(DUtils.SlideDown(gameObject));
            if (!_thingsStandingOnMe.Contains(other))
            {
                _thingsStandingOnMe.Add(other);
            }
            switch (_myType)
            {
                case SwitchType.Timed:
                    StartCoroutine(TimedSwitchAction());
                    break;
                default:
                    OpenDoors();
                    break;
            }            
        }
        private void OnTriggerExit(Collider other)
        {
            if (_thingsStandingOnMe.Contains(other))
            {
                _thingsStandingOnMe.Remove(other);
                CheckPressedStatus();
            }
            if (_isPressed)
            {
                return;
            }
            switch (_myType)
            {
                case SwitchType.Momentary:
                    StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .3f));
                    CloseDoors();
                    break;
            }
        }

        private void OpenDoors()
        {
            if (_isPressed)
            {
                foreach (Door door in _targetDoors)
                {
                    door.Open();
                }
            }
        }

        private void CloseDoors()
        {
            if (!_isPressed)
            {
                foreach (Door door in _targetDoors)
                {
                    door.Close();
                }
            }
        }

        private IEnumerator TimedSwitchAction()
        {
            OpenDoors();
            yield return new WaitUntil(() => _isPressed == false);
            yield return new WaitForSeconds(_resetTime);
            StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .3f));
            CloseDoors();
        }
    }
}
