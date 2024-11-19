using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject collectionEffectPrefab; 
    public void OnCollected()
    {
        if (collectionEffectPrefab != null)
        {
            // Instantiate the prefab at the position of the collectable object
            GameObject effectInstance = Instantiate(collectionEffectPrefab, transform.position, transform.rotation);
        }
        gameObject.SetActive(false);
    }
}