using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color colRectColor = Color.blue;
    public Color colDiaColor = Color.green;

    public AudioSource audioSource;
    public AudioClip crashSFX;

    public TMP_Text winText;

    private Rigidbody2D rb;
    private PlayerMovement moveScript;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        moveScript = GetComponent<PlayerMovement>();

        if (winText != null)
            winText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rectangle"))
        {
            spriteRenderer.color = colRectColor;
            PlaySFX();
        }

        else if (other.CompareTag("Diamond"))
        {
            spriteRenderer.color = colDiaColor;
            PlaySFX();
        }

        else if (other.CompareTag("BorderLine"))
        {
            SceneManager.LoadScene("Level1");
        }

        else if (other.CompareTag("FinishSpot"))
        {
            string current = SceneManager.GetActiveScene().name;

            if (current == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }

            else if (current == "Level2")
            {
                if (winText != null)
                    winText.gameObject.SetActive(true);

                FreezePlayer();
            }
        }
    }

    void PlaySFX()
    {
        if (audioSource != null && crashSFX != null)
            audioSource.PlayOneShot(crashSFX, 1f);
    }

    void FreezePlayer()
    {
        if (moveScript != null)
            moveScript.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }
}