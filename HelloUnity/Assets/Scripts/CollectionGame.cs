using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectionGame : MonoBehaviour
{
    public UIDocument uiDocument;
    public Label scoreText;
    public Label healthText;
    public Label gameOverText;
    [SerializeField] private int health = 20;
    [SerializeField] private int score = 0;
    private bool freeze = false;
    [SerializeField] private PlayerMotionController PMC;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
        UpdateUIScore();
        healthText = uiDocument.rootVisualElement.Q<Label>("Health");
        UpdateUIHealth();
        gameOverText = uiDocument.rootVisualElement.Q<Label>("GameOverText");
        gameOverText.style.display = DisplayStyle.None; // Hide game over text
    }

    void OnTriggerEnter(Collider entity)
    {
        if (entity.gameObject.CompareTag("Collectable"))
        {
            score++;
            UpdateUIScore();

            Collectable obj = entity.gameObject.GetComponent<Collectable>();
            if (obj != null)
            {
                obj.OnCollected();
            }
        }
        if (entity.gameObject.CompareTag("EnemyWeapon"))
        {
            if (freeze == false)
            {
                health--;
                UpdateUIHealth();
                StartCoroutine(Freeze());
                if (health <= 0)
                {
                    EndGame();
                }
            }

        }
    }

    void UpdateUIHealth()
    {
        if (scoreText != null)
        {
            healthText.text = "Health: " + health;
        }
    }
    void UpdateUIScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator Freeze()
    {
        freeze = true;
        PMC.OnDamage(); // Trigger the damage animation

        // Wait for the damage animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        freeze = false;
    }

    void EndGame()
    {
        gameOverText.style.display = DisplayStyle.Flex; // Show game over text
        gameOverText.text = "Game Over! Final Score: " + score;  
        Time.timeScale = 0; // Stop the game
    }
}
