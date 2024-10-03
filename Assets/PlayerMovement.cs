using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject playerDeadUI;
    public GameObject levelCompleteUI;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private bool isLevelComplete = false;

    private enum ColorType { Warm, Cold }
    private ColorType playerColorType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerDeadUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        playerColorType = GetColorType(spriteRenderer.color);
    }

    void Update()
    {
        if (isDead || isLevelComplete) return;

        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveInputX * speed, moveInputY * speed);

        if (moveInputX > 0) spriteRenderer.flipX = false;
        else if (moveInputX < 0) spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blocks"))
        {
            HandleBlockCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Phasable"))
        {
            HandlePhasableCollision(collision);
        }
        else if (collision.gameObject.CompareTag("FinishLine"))
        {
            HandleFinishLineCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Light"))
        {
            HandleLightCollision(collision); 
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))  // Checkpoint collision
        {
            HandleCheckpointCollision(collision);
        }
    }

    private void HandleBlockCollision(Collision2D collision)
    {
        SpriteRenderer blockRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
        if (blockRenderer != null)
        {
            if (IsSameColorType(blockRenderer.color))
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
                Debug.Log("Phased through block!");
            }
            else
            {
                Die();
                Debug.Log("Player died due to color mismatch with block!");
            }
        }
    }

    private void HandleLightCollision(Collision2D collision)
    {
        SpriteRenderer lightRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
        if (lightRenderer != null)
        {
            // Change player color to match the light's color
            spriteRenderer.color = lightRenderer.color;
            playerColorType = GetColorType(lightRenderer.color); // Update the player color type
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            Debug.Log("Player color changed to match the light!");
        }
    }

    private void HandleObstacleCollision(Collision2D collision)
    {
        SpriteRenderer obstacleRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
        if (obstacleRenderer != null)
        {
            ColorType obstacleColorType = GetColorType(obstacleRenderer.color);
            if (IsCounterColor(playerColorType, obstacleColorType))
            {
                Die();
            }
        }
    }

    private void HandlePhasableCollision(Collision2D collision)
    {
        SpriteRenderer phasableRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
        if (phasableRenderer != null)
        {
            ColorType phasableColorType = GetColorType(phasableRenderer.color);
            if (playerColorType == phasableColorType)
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
                Debug.Log("Player phased through the phasable obstacle!");
            }
            else
            {
                Die();
            }
        }
    }

    private void HandleFinishLineCollision(Collision2D collision)
    {
        DisplayLevelCompleteUI();

        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);

        Debug.Log("Player phased through the finish line and completed the level!");
     }

    private void HandleCheckpointCollision(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        IncomingBlocksSpawner spawner = FindObjectOfType<IncomingBlocksSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        Debug.Log("Checkpoint reached, spawner stopped.");
    }

    private bool IsSameColorType(Color blockColor)
    {
        if (blockColor.r > blockColor.b && playerColorType == ColorType.Warm)
        {
            return true;
        }
        else if (blockColor.b > blockColor.r && playerColorType == ColorType.Cold)
        {
            return true;
        }
        return false;
    }

    private ColorType GetColorType(Color color)
    {
        return color.r > color.b ? ColorType.Warm : ColorType.Cold;
    }

    private bool IsCounterColor(ColorType color1, ColorType color2)
    {
        return color1 != color2;
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            playerDeadUI.SetActive(true);
            Debug.Log("Player Died!");
        }
    }

    private void DisplayLevelCompleteUI()
    {
        if (!isLevelComplete)
        {
            isLevelComplete = true;
            rb.velocity = Vector2.zero;
            levelCompleteUI.SetActive(true);
            Debug.Log("Level Complete!");
        }
    }
}
