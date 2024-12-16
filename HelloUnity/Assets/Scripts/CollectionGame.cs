using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CollectionGame : MonoBehaviour
{
    public UIDocument uiDocument;
    public Label scoreText;
    public Label healthText;
    public Label moneyText;
    public Label gameOverText;
    public Label shopText;
    public int health = 20;
    [SerializeField] private int score = 0;
    public int money = 5;
    private bool freeze = false;
    [SerializeField] private PlayerMotionController PMC;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
        healthText = uiDocument.rootVisualElement.Q<Label>("Health");
        gameOverText = uiDocument.rootVisualElement.Q<Label>("GameOverText");
        gameOverText.style.display = DisplayStyle.None;
        moneyText = uiDocument.rootVisualElement.Q<Label>("Money");
        shopText = uiDocument.rootVisualElement.Q<Label>("ShopText");
        shopText.style.display = DisplayStyle.None;
        UpdateUI();
        
    }

    void OnTriggerEnter(Collider entity)
    {
        Debug.Log("Player hit by " + entity.gameObject.name);
        if (entity.gameObject.CompareTag("EnemyWeapon"))
        {
            Debug.Log("Player hit by enemy weapon");
            if (freeze == false)
            {
                health--;
                UpdateUI();
                StartCoroutine(Freeze());
                if (health <= 0)
                {
                    EndGame();
                }
            }
        }
        if (entity.gameObject.CompareTag("Money"))
        {
            score++;
            money += 5;

            Collectable obj = entity.gameObject.GetComponent<Collectable>();
            if (obj != null)
            {
                obj.OnCollected();
            }
        }

        if (entity.gameObject.CompareTag("Win"))
        {
            score += 100;
            EndGame();
        }
        UpdateUI();
    }

    public void UpdateUI(){
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money;
        }
    }

    public void ShopText(string text, bool show)
    {
        if (show)
        {
            moneyText.style.display = DisplayStyle.Flex;
        }
        else
        {
            moneyText.style.display = DisplayStyle.None;
        }
        moneyText.text = text;
    }

    IEnumerator Freeze()
    {
        Debug.Log("Freezing player");
        freeze = true;
        PMC.OnDamage(); // Trigger the damage animation

        // Wait for the damage animation to complete
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        freeze = false;
    }

    public void EndGame()
    {
        gameOverText.style.display = DisplayStyle.Flex; // Show game over text
        gameOverText.text = "Game Over! Final Score: " + score;  
        Time.timeScale = 0; // Stop the game
    }

    public Label GetShopText()
    {
        return uiDocument.rootVisualElement.Q<Label>("ShopText");  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
