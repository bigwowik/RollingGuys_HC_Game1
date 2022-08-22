using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroView : MonoBehaviour, IHeroView
    {
        public event Action InputUpdateEvent;
        public void Move(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        private void Update()
        {
            InputUpdateEvent?.Invoke();
            
        }

        
    }
}