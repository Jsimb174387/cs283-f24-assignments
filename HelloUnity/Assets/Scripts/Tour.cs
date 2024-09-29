using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public Transform player;
    public Transform[] POIs;
    public float cameraMoveSpeed = 50;
    private int currentPOI = 0;
    private float progress = 1;

    // Start is called before the first frame update
    void Start()
    {
        player.position = POIs[0].position;
        player.rotation = POIs[0].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentPOI++;
            if (currentPOI >= POIs.Length)
            {
                currentPOI = 0;
            }
            progress = 0;
        }
        // if in progress, then we need to move the camera to the next POI
        if (progress < 1)
        {
            progress += Time.deltaTime / cameraMoveSpeed;
            player.position = Vector3.Lerp(POIs[currentPOI].position, POIs[(currentPOI + 1) % POIs.Length].position, progress);
            player.rotation = Quaternion.Slerp(POIs[currentPOI].rotation, POIs[(currentPOI + 1) % POIs.Length].rotation, progress);
        }
    }
}
