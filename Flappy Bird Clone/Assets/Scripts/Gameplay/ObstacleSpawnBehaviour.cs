using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private ObjectPoolHandler pool;   
        [SerializeField] private Transform environmentSection;
        [SerializeField] private GameController gameController;
        
        [Space(5)]
        [Header("Settings")]
        [SerializeField] private int amountToSpawn = 5;
        [SerializeField] private float horizontalDistanceBetweenInstances = 1.25f;
        [SerializeField] private float minVerticalPositionToSpawnObstacles = 0.5f;
        [SerializeField] private float maxVerticalPositionToSpawnObstacles = 2.3f;

        private List<GameObject> _obstaclesSpawned = new List<GameObject>();
        
        private readonly float _timeToSpawnFirstObstacles = 2.0f;
        
        private float _initialOffset;
        private float _respawnOffset;
        private float _worldWidth;
        
        
        private void Awake()
        {
            float aspect = (float) Screen.width / Screen.height;
            
            if (Camera.main != null)
            {
                float worldHeight = Camera.main.orthographicSize * 2;
                _worldWidth = worldHeight * aspect;
                _initialOffset = _worldWidth * 0.3f;
                _respawnOffset = _initialOffset * 1.4f;
            }
        }

        private void Start()
        {
            Vector3 position = positionToEnableObstacle.transform.position;
            position.x = (_worldWidth) - _initialOffset;
            positionToEnableObstacle.transform.position = position;
            
            SetupEvents();
        }

        private void CreateInitialObstacles()
        {
            ClearObstacles();
            
            for (int i = 0; i < amountToSpawn; i++)
            {
                float xPosition = positionToEnableObstacle.transform.position.x + 
                                  horizontalDistanceBetweenInstances * i;
                GameObject obstacle = GenerateObstacleAtPosition(
                    new Vector3(xPosition, GetRandomYPosition(), transform.position.z));
                
                _obstaclesSpawned.Add(obstacle);
            }
        }

        private GameObject GenerateObstacleAtPosition(Vector3 position)
        {
            GameObject newObstacle = pool.GetObject(obstaclePrefab);
            newObstacle.transform.SetParent(environmentSection);
            newObstacle.transform.position = position;

            return newObstacle;
        }

        private float GetRandomYPosition()
        {
            float positionY = positionToEnableObstacle.transform.position.y;
            return Random.Range(
                positionY + minVerticalPositionToSpawnObstacles, 
                positionY + maxVerticalPositionToSpawnObstacles
                );
        }

        // WHEN CLICK ON PLAY AGAIN
        private void ClearObstacles()
        {
            foreach (GameObject obstacle in _obstaclesSpawned)
            {
                pool.AddToThePool(obstacle);
            }
            
            _obstaclesSpawned.Clear();
        }

        private void SetNewPositionToObstacle()
        {
            float distanceForPipes = amountToSpawn * horizontalDistanceBetweenInstances;
            float initialSpace = _worldWidth + _respawnOffset;
            float currentPosition = positionToEnableObstacle.transform.position.x;
            float distanceToTheFinalPipe = distanceForPipes + currentPosition;
                
            GenerateObstacleAtPosition(
                new Vector3(
                    distanceToTheFinalPipe - initialSpace, 
                    GetRandomYPosition(), 
                    transform.position.z)
            );
        }

        // EVENTS
        private void SetupEvents()
        {
            gameController.OnGameStarted += (() =>
            {
                Debug.Log("Game starting... Spawning obstacles");
                StartCoroutine(WaitToSpawnInitialObstacles());
            });
            
            obstacleHideController.OnReturnObstacle += SetNewPositionToObstacle;
        }
        
        // COROUTINES
        IEnumerator WaitToSpawnInitialObstacles()
        {
            yield return new WaitForSeconds(_timeToSpawnFirstObstacles);
            CreateInitialObstacles();
        }
    }
}
