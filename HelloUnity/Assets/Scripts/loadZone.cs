using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadZone : MonoBehaviour
{
    public Room nextRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nextRoom != null)
            {
                nextRoom.SetActive(true);
            }
        }
    }
}
