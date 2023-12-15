using UnityEngine;
using Utils.PoolingUtils;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class ObstacleSpawnController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private ObstacleHideController obstacleHideController;
        [SerializeField] private GameObject positionToEnableObstacle;
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private ObjectPool pool;   
        [SerializeField] private Transform environmentSection;
        
        [Space(5)]
        [Header("Settings")]
        [SerializeField] private int amountToSpawn = 5;
        [SerializeField] private float horizontalDistanceBetweenInstances = 1.25f;
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
                float xPosition = positionToEnableObstacle.transform.position.x + 
                                  horizontalDistanceBetweenInstances * i;
                GenerateObstacleAtPosition(new Vector3(xPosition, GetRandomYPosition(), transform.position.z));
            }
        }

        private void GenerateObstacleAtPosition(Vector3 position)
        {
            GameObject newObstacle = pool.GetObject(obstaclePrefab);
            newObstacle.transform.SetParent(environmentSection);
            newObstacle.transform.position = position;
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
            obstacleHideController.OnReturnObstacle += () =>
            {
                GenerateObstacleAtPosition(
                    new Vector3(
                        positionToEnableObstacle.transform.position.x, GetRandomYPosition(), transform.position.z)
                    );
            };
        }
    }
}
