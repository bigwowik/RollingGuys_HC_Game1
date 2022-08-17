using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;
        private void Awake()
        {
            var bootStrapper = FindObjectOfType<GameBootstrapper>();
            if (bootStrapper == null)
                Instantiate(BootstrapperPrefab);

        }
    }
}