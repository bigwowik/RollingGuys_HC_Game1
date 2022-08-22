using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public interface IHeroView
    {
        void Move(Vector3 newPosition);
        event Action InputUpdateEvent;
    }
}