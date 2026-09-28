using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] GameObject Pack;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;
    [SerializeField] float minSpawnTime = 25.0f;
    [SerializeField] float maxSpawnTime = 45.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Debug.Log("HealthPack collision: " + collision.ToString());
        Debug.Log("HealthPack name: " + collision.gameObject.name);
        Debug.Log("HealthPack tag: " + collision.gameObject.tag);
        Debug.Log("HealthPack Pack: " + Pack);
        Debug.Log("HealthPack playerHealth: " + player.health);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HealthPack Collision");
            if (player.health < 5)
            {
                player.health++;
                Debug.Log("HealthPack Heal!");
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                Debug.Log("Spawning in " + spawnTime);
                Invoke("SpawnHealthPack", spawnTime);
            }
            else
            {
                Debug.Log("HealthPack Already Full: " + player.health);
            }
        }
        else
        {
            Debug.Log("HealthPack goof");
        }

        Debug.Log("HealthPack playerHealth: " + player.health);
    }

    void SpawnHealthPack()
    {
        Debug.Log("HealthPack Spawning!");
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        transform.position = new Vector2(Random.Range(0, screenBounds.x), Random.Range(0, screenBounds.y));

        //Instantiate(healthPackPrefab, spawnPos, transform.rotation);


    }
}
