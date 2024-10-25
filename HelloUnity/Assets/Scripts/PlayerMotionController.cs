using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public Transform player; 
    public Rigidbody playerRigidbody;
    public Animator playerAnimator;
    public CharacterController controller;


    public float moveSpeed = 20;
    public float turnSpeed = 60;

    public float gravity = 9.81f; // I added gravity so my character won't float

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

        // get Turn
        float turn = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            turn = -turnSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            turn = turnSpeed * Time.deltaTime;
        }

        // Apply movement
        Vector3 velocity = moveDirection * moveSpeed * Time.deltaTime;
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity);
        playerAnimator.SetBool("isWalking", moveDirection != Vector3.zero);

        // Apply rotation
        player.Rotate(Vector3.up, turn);
    }

    public void OnFootstep()
    {
        Debug.Log("Footstep event");
    }
}
