using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public Transform player; 
    public float sensitivity = 500;
    public float moveSpeed = 10;
    public Vector3 cameraOffset = new Vector3(0, 5, -3);

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; 
        //so that camera doesn't flip
        rotationY = Mathf.Clamp(rotationY, -45, 45);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = player.position + rotation * cameraOffset;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * moveSpeed);
        transform.LookAt(player.position);
    }
}