using UnityEngine;

namespace EnemyPatrolling
{
    public class DeathEnemy : MonoBehaviour
    {
        [SerializeField] private AudioClip Death;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ParticleSystem DeathParticle;

        private Collider2D col;
        private SpriteRenderer sprite;

        private void Awake()
        {
            // Get references to the collider and sprite renderer
            col = GetComponent<Collider2D>();
            sprite = GetComponent<SpriteRenderer>();
        }

        public void DoDeath()
        {
            if (audioSource != null && Death != null)
            {
                audioSource.PlayOneShot(Death);
            }

            // Spawn particles
            CreateDeathParticle();

            // Hide visual and disable collision
            if (col != null) col.enabled = false;
            if (sprite != null) sprite.enabled = false;

            // Destroy after 0.5 seconds
            Destroy(gameObject, 0.5f);
        }

        private void CreateDeathParticle()
        {
            if (DeathParticle != null)
            {
                Instantiate(DeathParticle, transform.position, transform.rotation).Play();
            }
        }
    }
}
