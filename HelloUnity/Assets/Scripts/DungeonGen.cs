using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    public int dungeonWidth = 100;
    public int dungeonHeight = 100;
    public int maxRooms = 10;
    public int minRoomSize = 3;
    public int maxRoomSize = 15;

    private list<Room> rooms = new list<Room>();

    // Start is called before the first frame update
    void Start()
    {
        GenDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < roomCount; i++)
        {
            int width = Random.Range(minRoomSize, maxRoomSize);
            int height = Random.Range(minRoomSize, maxRoomSize);
            int x = Random.Range(0, dungeonWidth - width - 1);
            int y = Random.Range(0, dungeonHeight - height - 1);

            roomCount newRoom = new Room(x, y, width, height);

            bool overlaps = false;
            foreach (Room gRoom in rooms)
            {
                if (newRoom.Overlaps(gRoom))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                rooms.Add(newRoom);
            }
        }
    }

    void ConnectRooms()
    {
        
    }
}
