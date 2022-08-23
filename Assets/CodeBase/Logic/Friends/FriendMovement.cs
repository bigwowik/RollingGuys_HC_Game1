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
        public Vector3 LastNextFriendPosition;

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

        private void OnEnable()
        {
            _rigidbody.isKinematic = true;
        }


        private void Update()
        {
            if(!_inputService.isActive) return;

            if (Vector3.Distance(_friend.NextFriend.Position, transform.position) > CriticalDistance)
            {
                LastNextFriendPosition = _friend.NextFriend.Position;
            }
            
        }

        private void FixedUpdate()
        {
            if(!_inputService.isActive) return;
            _rigidbody.position = Vector3.Lerp(transform.position, LastNextFriendPosition, Speed * Time.deltaTime);
        }
    }
}