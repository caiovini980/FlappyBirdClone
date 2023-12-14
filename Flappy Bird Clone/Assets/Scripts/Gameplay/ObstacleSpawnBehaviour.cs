using Base.Controllers;
using Obstacles;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class ObstacleSpawnController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject positionToEnableObstacle;
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private ObjectPool pool;   
        [SerializeField] private Transform environmentSection;
        
        [Space(5)]
        [Header("Settings")]
        [SerializeField] private int amountToSpawn = 5;
        // [SerializeField] private float horizontalDistanceBetweenInstances = 1.25f;
        [SerializeField] private float minVerticalPositionToSpawnObstacles = 0.5f;
        [SerializeField] private float maxVerticalPositionToSpawnObstacles = 2.3f;

        private void Start()
        {
            SetupEvents();
            CreateInitialObstacles();
        }

        private void CreateInitialObstacles()
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject newObstacle = pool.GetObject(obstaclePrefab);
                newObstacle.transform.SetParent(environmentSection);
                newObstacle.transform.position = new Vector3(
                    positionToEnableObstacle.transform.position.x,
                    GetRandomYPosition(),
                    transform.position.z);
            }
        }

        private float GetRandomYPosition()
        {
            float positionY = positionToEnableObstacle.transform.position.y;
            return Random.Range(
                positionY + minVerticalPositionToSpawnObstacles, 
                positionY + maxVerticalPositionToSpawnObstacles
                );
        }

        // EVENTS
        private void SetupEvents()
        {
            
        }
    }
}
