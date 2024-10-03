using UnityEngine;

public class SlidingBlock : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 10f; 

    private Collider2D blockCollider;

    void Start()
    {
        blockCollider = GetComponent<Collider2D>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Block hit the player!");
            
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, blockCollider);
            Debug.Log("Block phased through a non-player object: " + collision.gameObject.tag);
        }
    }
}
