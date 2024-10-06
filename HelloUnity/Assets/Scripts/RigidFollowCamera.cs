using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target; 
    public int hDist = 1;
    public float vDist = 1.5f;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;

        Vector3 eye = tPos - tForward * hDist + tUp * vDist;

        Vector3 cameraForward = tPos - eye;

        // Set the camera's position and rotation with the new values
        // This code assumes that this code runs in a script attached to the camera
        transform.position = eye;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
