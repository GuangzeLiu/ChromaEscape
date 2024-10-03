using UnityEngine;

public class ColorPickup : MonoBehaviour
{
    public Color newColor = Color.white;  // Set the color to white when picked up

    // Detect collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the pickup is the player
        if (collision.CompareTag("Player"))
        {
            // Get the player's SpriteRenderer
            SpriteRenderer playerSprite = collision.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                // Ensure the color is set to fully opaque white (R: 1, G: 1, B: 1, A: 1)
                playerSprite.color = new Color(1f, 1f, 1f, 1f);  // Full white, fully opaque
            }

            // Destroy the pickup box after it has been collected
            Destroy(gameObject);
        }
    }
}
