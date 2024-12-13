using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int x;
    public int y;
    public int width;
    public int height;

    public Room(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public bool Overlaps(Room other)
    {
        return (x < other.x + other.width && x + width > other.x && y < other.y + other.height && y + height > other.y);
    }

}
