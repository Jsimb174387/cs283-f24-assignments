using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectionGame : MonoBehaviour
{
    public UIDocument uiDocument;
    public Label scoreText;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIScore();
        scoreText = uiDocument.rootVisualElement.Q<Label>("Score");
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
    }

    void UpdateUIScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

}
