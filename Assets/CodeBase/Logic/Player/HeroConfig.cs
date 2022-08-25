using UnityEngine;

namespace CodeBase.Logic.Player
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Configs/HeroConfig", order = 0)]
    public class HeroConfig : ScriptableObject
    {
        public float ForwardSpeed = 1;
        public float HorizontalSpeed = 1;
    }
}