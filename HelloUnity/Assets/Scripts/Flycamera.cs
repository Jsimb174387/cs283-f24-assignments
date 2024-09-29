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
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //so that camera doesn't flip
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        // WASD to move the player character
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += player.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= player.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= player.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += player.right;
        }

        // Apply movement
        playerRigidbody.MovePosition(player.position + moveDirection * moveSpeed * Time.deltaTime);
        playerAnimator.SetBool("isWalking", moveDirection != Vector3.zero);
        Debug.Log(playerAnimator.GetBool("isWalking"));

        transform.position = Vector3.Lerp(transform.position, player.position + cameraOffset, Time.deltaTime * moveSpeed);
    }
}