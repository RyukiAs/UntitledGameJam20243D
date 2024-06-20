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

    public float playerSize;
    public float jumpHeight;
    public float gravity;

    private Rigidbody rb;
    private Transform playerTransform;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        secondsSinceLastShot = secondsBetweenShots;
        References.thePlayer = gameObject;
        isGrounded = true;
        gravity = 15.0f;
        dashCooldownTimer = dashCooldown;
    }

    private void FixedUpdate()
    {
        UpdatePlayerSize();
        HandleMovement();
        HandleRotation();
        HandleJumping();
        HandleDash();
    }

    private void UpdatePlayerSize()
    {
        playerSize = 1.0f;
        jumpHeight = 10.0f * transform.localScale.x;
    }

    private void HandleMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y / speed, Input.GetAxis("Vertical"));
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

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f);

        if (Input.GetButton("Jump") && isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(2.0f * gravity * jumpHeight);
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration); // Gravity
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
