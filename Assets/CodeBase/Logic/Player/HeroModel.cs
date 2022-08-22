using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroModel : IHeroModel
    {
        public event Action<Vector3> UpdatePosition;
        
        public Vector3 Position = Vector3.zero;
        public float Speed = 1f;


        public void Move(float velocityX)
        {
            Position += Vector3.forward * velocityX * Speed * Time.deltaTime;
            UpdatePosition?.Invoke(Position);
        }
    }
}