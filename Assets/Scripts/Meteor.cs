using UnityEngine;
using UnityEngine.Serialization;

public class Meteor : MonoBehaviour
{
    public float speed = 1.5f;
    protected Rigidbody2D Rb;
    [FormerlySerializedAs("Player")] public Transform player;

    protected virtual void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (transform.position.y < -6.0f || transform.position.x < -10.0f || transform.position.x > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            OnHitByBullet();
            Destroy(other.gameObject);
        }
    }

    protected virtual void OnHitByBullet()
    {
        GameManager.Instance.AddScore(10);
        Destroy(gameObject);
    }
}