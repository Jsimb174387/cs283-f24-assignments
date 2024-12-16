using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject boss;
    public CollectionGame inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<CollectionGame>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            inventory.EndGame();
        }
        
    }
}
