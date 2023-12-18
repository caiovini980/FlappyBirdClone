using UnityEngine;

namespace Gameplay
{
    public class BackgroundBehaviour : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            Camera gameCamera = Camera.main;
            if (gameCamera == null || _renderer == null) return;

            Transform backgroundTransform = gameObject.transform;
            backgroundTransform.localScale = Vector3.one;
            
            Sprite sprite = _renderer.sprite;
            float width = sprite.bounds.size.x;
            float height = sprite.bounds.size.y;

            float worldScreenHeight = gameCamera.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            backgroundTransform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
        }
    }
}
