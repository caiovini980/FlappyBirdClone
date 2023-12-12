using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Info", menuName = "Player Info")]
    public class PlayerInfo : ScriptableObject
    {
        public float jumpForce = 5.0f;
        
        public Sprite playerSprite;
        public AudioClip hitSound;
        public AudioClip jumpSound;
        public AudioClip deathSound;
        public AudioClip scoreSound;
    }
}
