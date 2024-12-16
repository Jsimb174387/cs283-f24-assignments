using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRigidbody;
    public Animator playerAnimator;
    public CharacterController controller;
    public Camera playerCamera;

    public float moveSpeed = 20;
    public float turnSpeed = 60;
    public float gravity = 9.81f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;
    public Transform gunTransform;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        UpdateGunRotation();
    }

    void HandleMovement()
    {
        // WASD to move the player character
        moveDirection = Vector3.zero;

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
        Vector3 velocity = moveDirection * moveSpeed;

        // Handle jumping
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
        playerAnimator.SetBool("isWalking", moveDirection != Vector3.zero);
    }

    void HandleCameraRotation()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player
        player.Rotate(Vector3.up * mouseX);

        // Rotate camera
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;
    }

    void UpdateGunRotation()
    {
        gunTransform.rotation = cameraTransform.rotation;
    }

    public void OnDamage()
    {
        playerAnimator.SetTrigger("Damage");
    }
    public void OnFootstep()
    {
    }
}