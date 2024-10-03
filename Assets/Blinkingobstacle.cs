using UnityEngine;

public class BlinkingObstacle : MonoBehaviour
{
    public float minBlinkInterval = 3f;  // Minimum time between blinks
    public float maxBlinkInterval = 5f;  // Maximum time between blinks
    public Color color1 = Color.red;  // First color (red)
    public Color color2 = Color.blue;  // Second color (blue)
    public float oscillationSpeed = 2f;  // Speed of the oscillation
    public float oscillationHeight = 1f;  // Height of the oscillation

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer
    private Collider2D objectCollider;  // Reference to the object's Collider2D
    private bool isColor1 = true;  // Track which color is currently displayed
    private Vector3 originalPosition;  // Store the object's original position for oscillation

    void Start()
    {
        // Get the SpriteRenderer and Collider2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();

        // Save the original position to oscillate around
        originalPosition = transform.position;

        // Start the blinking coroutine
        StartCoroutine(Blink());
    }

    void Update()
    {
        // Apply vertical oscillation using a sine wave for smooth movement
        float newY = originalPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }

    // Coroutine to handle blinking with random intervals
    private System.Collections.IEnumerator Blink()
    {
        while (true)
        {
            // Wait for a random time between minBlinkInterval and maxBlinkInterval
            float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(waitTime);

            // Toggle the color between red and blue
            isColor1 = !isColor1;
            spriteRenderer.color = isColor1 ? color1 : color2;  // Set the sprite color to red or blue
        }
    }

}
