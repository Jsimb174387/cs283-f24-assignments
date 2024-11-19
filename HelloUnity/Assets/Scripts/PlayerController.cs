using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    public Transform player; 
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    public float moveSpeed = 20;
    public float turnSpeed = 180;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();
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
        playerRigidbody.MovePosition(player.position + moveDirection * moveSpeed * Time.deltaTime);
        playerAnimator.SetBool("isWalking", moveDirection != Vector3.zero);

        // Apply rotation
        player.Rotate(Vector3.up, turn);
    }
}
