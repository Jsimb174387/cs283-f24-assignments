using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{
    [SerializeField] public int width;
    public int height;
    public Room nextRoom;
    public Room previousRoom;
    public Transform exitPoint;
    public Transform entryPoint;
    private GameObject[] enemies;

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            width = (int)renderer.bounds.size.x;
            height = (int)renderer.bounds.size.y;
        }
    }

    public bool Overlaps(Room other)
    {
        return transform.position.x < other.transform.position.x + other.width &&
               transform.position.x + width > other.transform.position.x;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    
}