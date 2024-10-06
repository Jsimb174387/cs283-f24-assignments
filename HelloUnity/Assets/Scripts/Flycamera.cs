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
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //so that camera doesn't flip
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        transform.position = Vector3.Lerp(transform.position, player.position + cameraOffset, Time.deltaTime * moveSpeed);
    }
}