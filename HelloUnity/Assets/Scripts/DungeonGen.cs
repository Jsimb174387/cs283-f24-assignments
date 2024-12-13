using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    public int dungeonWidth = 100;
    public int dungeonHeight = 100;
    public int roomCount = 10;

    private List<Room> rooms = new List<Room>();
    [SerializeField] private List<GameObject> roomPrefabs = new List<GameObject>();
    [SerializeField] private GameObject startingRoomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        int currentX = 0;

        // Instantiate and place the starting room
        GameObject startingRoomObject = Instantiate(startingRoomPrefab, new Vector3(currentX, 0, 0), Quaternion.identity);
        Room startingRoom = startingRoomObject.GetComponent<Room>();
        if (startingRoom != null)
        {
            currentX += startingRoom.Width;
            rooms.Add(startingRoom);
        }

        // Instantiate and place the other rooms
        for (int i = 0; i < roomCount; i++)
        {
            GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            GameObject roomObject = Instantiate(roomPrefab, new Vector3(currentX, 0, 0), Quaternion.identity);
            Room newRoom = roomObject.GetComponent<Room>();

            if (newRoom != null)
            {
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
                    currentX += newRoom.Width; // Update currentX after adding the room
                }
                else
                {
                    // Destroy the room if it overlaps
                    Destroy(roomObject);
                }
            }
        }
    }
}