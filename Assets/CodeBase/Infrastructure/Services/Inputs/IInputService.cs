using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Inputs
{
    public interface IInputService : IService
    {
        bool isBlocked { get; set; } 
        bool isActive { get; }
        float VelocityX { get; }
    }
}