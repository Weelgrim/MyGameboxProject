using UnityEngine;

public class ExplodingMeteor : Meteor
{
    public GameObject shardPrefab;
    public int numberOfShards = 3;

    protected override void Start()
    {
        base.Start();
        Vector2 playerInitialPosition = player.position;
        Vector2 directionToPlayer = (playerInitialPosition - (Vector2)transform.position).normalized;
        Rb.velocity = directionToPlayer * speed;
    }


    protected override void OnHitByBullet()
    {
        base.OnHitByBullet();
        Explode();
    }

    private void Explode()
    {
        float angleStep = 360f / numberOfShards;
        float angle = 0f;

        for (int i = 0; i < numberOfShards; i++)
        {
            float shardDirX = Mathf.Sin((angle * Mathf.PI) / 180);
            float shardDirY = Mathf.Cos((angle * Mathf.PI) / 180);
            Vector3 shardVector = new Vector3(shardDirX, shardDirY, 0);
            Vector2 shardMoveDirection = shardVector.normalized * speed * 1.5f;

            GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
            shard.GetComponent<Rigidbody2D>().velocity = new Vector2(shardMoveDirection.x, shardMoveDirection.y);
            shard.transform.rotation = Quaternion.LookRotation(Vector3.forward, shardMoveDirection) * Quaternion.Euler(0, 0, 0);

            angle += angleStep;
        }

        Destroy(gameObject);
    }
}