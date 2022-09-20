using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Friends;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class CharacterAnimations : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _rb;

        private bool _isAlive;
        private FriendChain _friendChain;
        private ILevelProgressService _levelProgressService;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }

        private void Start()
        {
            _levelProgressService.OnLevelWin += WinHandle;
        }

        private void WinHandle()
        {
            _levelProgressService.OnLevelWin -= WinHandle;
            _animator.SetTrigger("Dance");
            _animator.transform.rotation = quaternion.Euler(0,180, 0);
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponentInChildren<Rigidbody>();

            _isAlive = true;

            _friendChain = GetComponent<FriendChain>();
            _friendChain.OnDeath += OnDeathHandle;
        }

        private void OnDeathHandle()
        {
            _friendChain.OnDeath -= OnDeathHandle;
            
            _animator.SetTrigger("Death");
            
            _isAlive = false;
        }

        public void Update()
        {
            if (!_isAlive) return;
            
            if(_rb.velocity.magnitude > 0.1)
                _animator.SetBool("isRun", true);
            else
                _animator.SetBool("isRun", false);
        }
    }
}