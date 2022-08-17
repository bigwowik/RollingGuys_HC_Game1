using System;
using UnityEngine;

namespace CodeBase.Sounds
{
    public interface IAudioPlayable
    {
        event Action AudioEvent;
    }
}