using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
public class DungeonGen : MonoBehaviour
{
    // Number of NORMAL rooms to generate. Does not include boss room and starting room.
    public int roomCount = 10;

    private List<Room> rooms = new List<Room>();
    [SerializeField] private GameObject startingRoomPrefab;
    [SerializeField] private List<GameObject> roomPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> bossRoomPrefabs = new List<GameObject>();
    public string roomPrefabsPath = "Prefabs/Rooms/General";
    public string bossPrefabPath = "Prefabs/Rooms/Boss";

    // Start is called before the first frame update
    void Start()
    {
        roomPrefabs = LoadRoomPrefabs(roomPrefabsPath);
        bossRoomPrefabs = LoadRoomPrefabs(bossPrefabPath);
        Generate();
    }

    // Update is called once per frame
    private List<GameObject> LoadRoomPrefabs(string path)
    {
        return new List<GameObject>(Resources.LoadAll<GameObject>(path));
    }

    void Generate()
    {
        Vector3 nextRoomPosition = Vector3.zero;

        // Instantiate and place the starting room
        GameObject startingRoomObject = Instantiate(startingRoomPrefab, nextRoomPosition, Quaternion.identity);
        Room startingRoom = startingRoomObject.GetComponent<Room>();
        if (startingRoom != null)
        {
            rooms.Add(startingRoom);
            nextRoomPosition = startingRoom.exitPoint.position;
        }

        // Instantiate and place the other rooms
        for (int i = 0; i < roomCount; i++)
        {
            GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            GameObject roomObject = Instantiate(roomPrefab, nextRoomPosition, Quaternion.identity);
            Room newRoom = roomObject.GetComponent<Room>();

            if (newRoom != null)
            {
                // moves to correct position, because objects are intit at the center
                roomObject.transform.position = nextRoomPosition + new Vector3(newRoom.width / 2, 0, 0);
                rooms.Add(newRoom);
                nextRoomPosition = newRoom.exitPoint.position;

                // Important: since the starting room was separate from this loop, we don't need to do i - 1
                rooms[i].nextRoom = newRoom;
                newRoom.previousRoom = rooms[i];
            }
        }

        // Instantiate and place the boss room
        GameObject bRP = bossRoomPrefabs[Random.Range(0, bossRoomPrefabs.Count)];
        GameObject bossRoomObject = Instantiate(bRP, nextRoomPosition, Quaternion.identity);
        Room bossRoom = bossRoomObject.GetComponent<Room>();
        bossRoomObject.transform.position = nextRoomPosition + new Vector3(bossRoom.width, 0, 0);
        if (bossRoom != null)
        {
            Room lastRoom = rooms.Last();
            rooms.Add(bossRoom);
            lastRoom.nextRoom = bossRoom;
            bossRoom.previousRoom = lastRoom;
        }

        // adds the next room to the valves, so when you open the door it loads the next room. 
        InitValve(rooms[0]);
        for (int i = 1; i < rooms.Count; i++)
        {
            rooms[i].SetActive(false);
            InitValve(rooms[i]);
        }
    }
    private void InitValve(Room room)
    {
        // Find the Valve object by name within the room
        Transform[] allChildTransforms = room.GetComponentsInChildren<Transform>(true);
        Transform valveTransform = null;
        foreach (Transform childTransform in allChildTransforms)
        {
            if (childTransform.name == "Valve")
            {
                valveTransform = childTransform;
                break;
            }
        }

        if (valveTransform != null)
        {
            AN_Button[] anButtons = valveTransform.GetComponents<AN_Button>();
            foreach (AN_Button anButton in anButtons)
            {
                if (anButton != null)
                {
                    anButton.nextRoom = room.nextRoom;
                }
            }
        }
    }
}
