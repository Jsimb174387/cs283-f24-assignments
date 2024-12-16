using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public AN_DoorScript doorScript; // Reference to AN_DoorScript

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the reference is set
        if (doorScript == null)
        {
            Debug.LogError("DoorScript reference is not set in DoorKey");
        }
    }

    // Detect when the item is collected
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            CollectItem();
        }
    }

    // Method to handle item collection
    void CollectItem()
    {
        Debug.Log("Item collected");
        doorScript.Action(); // Call the Action method in AN_DoorScript
        Destroy(gameObject); // Optionally destroy the key object
    }
}