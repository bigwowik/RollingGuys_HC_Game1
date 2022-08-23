using System;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendInteractive : MonoBehaviour
    {
        private bool _wasActivated = false;
        
        public FriendMovement FriendMovement;
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision- "+collision.collider);
            if(collision.collider.gameObject.TryGetComponent<Hero>(out var hero))
            {
                if(_wasActivated) return;
                
                
                FriendMovement.enabled = true;
                hero.GetComponent<IFriend>().AddBackFriend(GetComponent<IFriend>());
                _wasActivated = true;
            }
        }
    }
}