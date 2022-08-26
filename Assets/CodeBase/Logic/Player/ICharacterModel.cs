using UniRx;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public interface ICharacterModel
    {
        ReactiveProperty<Vector3> Position { get; }
        void FixedUpdateHandle();
        void SetGameObject(GameObject gameObjectModel);
    }
}