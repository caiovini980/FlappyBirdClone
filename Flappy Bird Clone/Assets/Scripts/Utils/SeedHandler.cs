using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public class SeedHandler : MonoBehaviour
    {
        [SerializeField] private int seed;
        [SerializeField] private bool useRandomSeed;

        private void Awake()
        {
            if (useRandomSeed)
            {
                seed = Random.Range(0, 10000);
            }
            
            Random.InitState(seed);
        }
    }
}
