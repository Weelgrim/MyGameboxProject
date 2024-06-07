using UnityEngine;

public class Shard : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseLife();
            Destroy(gameObject);
        }
    }
}