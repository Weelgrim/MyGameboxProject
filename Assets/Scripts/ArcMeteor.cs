using UnityEngine;

public class ArcMeteor : Meteor
{
    public float explosionRadius = 1.0f; 
    public GameObject explosionEffectPrefab;
    private Vector2 _initialDirection;
    private bool _hasExploded = false;
    public static float TimeToSelfDestruct = 3.0f;
    private GameObject _currentExplosionEffect;

    protected override void Start()
    {
        base.Start();
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        _initialDirection = directionToPlayer * speed;
        Rb.velocity = _initialDirection;

        Invoke(nameof(SelfDestruct), TimeToSelfDestruct);
    }

    private void FixedUpdate()
    {
        if (!_hasExploded)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            Rb.velocity = directionToPlayer * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
        else if (other.CompareTag("Bullet"))
        {
            OnHitByBullet();
            Destroy(other.gameObject);
        }
    }

    protected override void OnHitByBullet()
    {
        base.OnHitByBullet();
        Explode();
    }

    private void Explode()
    {
        if (_hasExploded)
            return;

        _hasExploded = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                GameManager.Instance.LoseLife();
            }
        }
        
        if (explosionEffectPrefab != null)
        {
            _currentExplosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void SelfDestruct()
    {
        if (!_hasExploded)
        {
            Explode();
        }
    }

    private void OnDestroy()
    {
        if (_currentExplosionEffect != null)
        {
            Destroy(_currentExplosionEffect, 0.6f);
        }
    }
}