using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{
    [SerializeField] public int Width;
    public int Height;
    public Room nextRoom;

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Width = (int)renderer.bounds.size.x;
            Height = (int)renderer.bounds.size.y;
        }
    }

    public bool Overlaps(Room other)
    {
        return transform.position.x < other.transform.position.x + other.Width &&
               transform.position.x + Width > other.transform.position.x;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

