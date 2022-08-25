using System;
using CodeBase.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class HeroView : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Move(Vector3 newPosition)
        {
            _rigidbody.MovePosition(newPosition);
        }
    }
}