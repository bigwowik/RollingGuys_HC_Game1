using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Inputs;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendMovement : MonoBehaviour
    {
        private IFriend _friend;
        
        public float CriticalDistance = 1f;
        public float Speed = 1f;

        
        private Vector3 _lastNextFriendPosition;
        
        private IInputService _inputService;
        private Rigidbody _rigidbody;


        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _friend = GetComponent<IFriend>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if(!_inputService.isActive) return;
            
            //Debug.Log(name + " t.pos: " + transform.position + " rb pos: " + _rigidbody.position);
            Debug.Log(name + " pos delta: " + (transform.position -_rigidbody.position));
            
            if (Vector3.Distance(_friend.NextFriend.Position, _rigidbody.position) > CriticalDistance)
            {
                _lastNextFriendPosition = _friend.NextFriend.Position;
            }
            
            _rigidbody.position = Vector3.Lerp(_rigidbody.position, _lastNextFriendPosition, Speed * Time.fixedDeltaTime);
        }
    }
}