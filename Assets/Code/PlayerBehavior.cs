using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    public float dashCooldown;
    private float dashCooldownTimer;

    public float secondsBetweenShots;
    private float secondsSinceLastShot;

    public float secondsBetweenJumps;
    private float secondsSinceLastJump;

    public Vector3 playerSize;

    public float jumpHeight;
    public float timeInAir;
    private float trackTimeInAir;
    private bool isJumping;

    private Rigidbody rb;
    private Transform playerTransform;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        secondsSinceLastShot = secondsBetweenShots;
        References.thePlayer = gameObject;
        timeInAir = 1f;
        isGrounded = true;
        dashCooldownTimer = dashCooldown;
    }

    private void Update()
    {
        UpdatePlayerSize();
        HandleMovement();
        HandleRotation();
        HandleJumping();
        HandleDash();
    }

    private void UpdatePlayerSize()
    {
        // Assuming playerSize is used for some purpose not shown in the provided code
        playerSize = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        jumpHeight = playerTransform.localScale.y;
    }

    private void HandleMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = inputVector * speed;
    }

    private void HandleRotation()
    {
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, playerTransform.position);
        if (playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera))
        {
            Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
            // Rotate player to face cursor position
            playerTransform.LookAt(cursorPosition);
        }
    }

    private void HandleJumping()
    {
        secondsSinceLastJump += Time.deltaTime;

        if (Input.GetButton("Jump") && secondsSinceLastJump >= secondsBetweenJumps && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
            trackTimeInAir = 0;
            secondsSinceLastJump = 0;
        }

        if (isJumping)
        {
            if (timeInAir > 0)
            {
                float jumpIncrement = (jumpHeight / timeInAir) * Time.deltaTime;
                Vector3 newPosition = playerTransform.position + Vector3.up * jumpIncrement;
                playerTransform.position = newPosition;

                trackTimeInAir += Time.deltaTime;

                if (trackTimeInAir >= timeInAir)
                {
                    isJumping = false;
                }
            }
            else
            {
                Debug.LogError("timeInAir is zero or negative, setting to default 1 second.");
                timeInAir = 1;
            }
        }

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        float groundLevel = 0.55f;

        if (!isJumping && playerTransform.position.y > groundLevel)
        {
            float gravity = 10f;
            playerTransform.position += Vector3.down * gravity * Time.deltaTime;
        }
        else if (playerTransform.position.y <= groundLevel)
        {
            playerTransform.position = new Vector3(playerTransform.position.x, groundLevel, playerTransform.position.z);
            isGrounded = true;
        }
    }

    private void HandleDash()
    {
        dashCooldownTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && dashCooldownTimer >= dashCooldown)
        {
            Vector3 dashDirection = playerTransform.forward;
            rb.MovePosition(playerTransform.position + dashDirection * dashDistance);
            dashCooldownTimer = 0;
        }
    }
}
