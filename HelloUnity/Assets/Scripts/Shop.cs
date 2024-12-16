using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigRookGames.Weapons;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopItem : MonoBehaviour
{
    public int cost;
    public string itemName;
    public string description;
    public GunfireController weapon;
    public PlayerMotionController PMC;

    public CollectionGame inventory;

    public float buyingDelay = 0.3f;
    public float lastBuyTime = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Near())
        {
            Buy();
        }
    }

    void Awake()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<CollectionGame>();
        PMC = GameObject.FindWithTag("Player").GetComponent<PlayerMotionController>();
        weapon = GameObject.FindWithTag("Player").GetComponentInChildren<GunfireController>();
    }


    void Buy()
    {
        if (inventory.money >= cost && Time.time - lastBuyTime > buyingDelay)
        {
            lastBuyTime = Time.time;
            inventory.money -= cost;
            buff();
        }
    }

    void buff()
    {
        if (itemName == "Health")
        {
            inventory.health += 2;
        }
        else if (itemName == "Speed")
        {
            PMC.moveSpeed += 2;
        }
        else if (itemName == "FireRate")
        {
            // reduces fire delay by 15%
            weapon.shotDelay *= 0.85f;
        }

        inventory.UpdateUI();
    }

    bool Near()
    {
        float distance = Vector3.Distance(transform.position, PMC.player.position);
        Vector3 direction = transform.position - PMC.player.position;
        float angle = Vector3.Angle(direction, PMC.player.forward);
        return distance < 2 && angle < 45;
    }
}
