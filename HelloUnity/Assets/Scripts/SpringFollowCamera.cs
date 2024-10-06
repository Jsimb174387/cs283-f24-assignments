using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    public Transform target;
    public int hDist = 1;
    public int vDist = 2;
    public float dampConstant = 1;
    public float springConstant = 1;
    private Vector3 actualPosition;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        actualPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;


        // Camera position is offset from the target position
        Vector3 idealEye = tPos - tForward * hDist + tUp * vDist;
        // The direction the camera should point is from the target to the camera position
        Vector3 cameraForward = tPos - actualPosition;

        // Compute the acceleration of the spring, and then integrate
        Vector3 displacement = actualPosition - idealEye;
        Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

        // Update the camera's velocity based on the spring acceleration
        float deltaTime = Time.deltaTime;
        velocity += springAccel * deltaTime;
        actualPosition += velocity * deltaTime;

        // Set the camera's position and rotation with the new values
        // This code assumes that this code runs in a script attached to the camera
        transform.position = actualPosition;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
