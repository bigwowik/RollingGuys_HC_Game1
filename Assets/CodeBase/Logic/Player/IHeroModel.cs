using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public interface IHeroModel
    {
        void Move(float velocityX);
        event Action<Vector3> UpdatePosition;
    }
}