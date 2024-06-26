using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerBehavior : MonoBehaviour
{
    public Transform arena;

    public Material normalMaterial;
    public Material dashMaterial;

    public float speed;
    public float dashDistance;
    public float dashCooldown;
    private float dashCooldownTimer;

    public float playerSize;
    public float jumpHeight;
    public float gravity;

    private Rigidbody rb;
    private Transform playerTransform;
    private bool isGrounded;

    public bool attacking;
    public float attackTimer = 0.5f;
    private float trackAttackTimer = 0f;

    public AudioSource whooshSound;
    public AudioSource thudSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        References.thePlayer = gameObject;
        isGrounded = true;
        gravity = 15.0f;
        dashCooldown = 1.0f;
        dashCooldownTimer = dashCooldown;
        attacking = false;
        UpdatePlayerSize();
    }

    private void FixedUpdate()
    {
        //UpdatePlayerSize();
        HandleMovement();
        HandleRotation();
        HandleJumping();
        HandleDash();
        handleAttack();
    }

    private void UpdatePlayerSize()
    {
        playerSize = 1.0f;
        jumpHeight = 10.0f * transform.localScale.x;
    }


    private void HandleMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y / speed, Input.GetAxis("Vertical"));

        float newX = rb.velocity.x;
        if (Mathf.Abs(newX) < Mathf.Abs(inputVector.x * speed)) newX = inputVector.x * speed;

        float newZ = rb.velocity.z;
        if (Mathf.Abs(newZ) < Mathf.Abs(inputVector.z * speed)) newZ = inputVector.z * speed;

        rb.velocity = new Vector3(newX, inputVector.y * speed, newZ);
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
        RaycastHit hit;

        Vector3 rayStart = transform.position;
        rayStart.y -= transform.localScale.y / 2.0f - 0.01f;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, out hit, 0.1f);

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
            attacking = true;
            Vector3 dashDirection = playerTransform.forward;
            rb.AddForce(dashDirection * 15.0f + Vector3.up * 1.0f, ForceMode.Impulse);
            dashCooldownTimer = 0;

            whooshSound.pitch = Random.Range(0.9f, 1.4f);
            whooshSound.Play();
        }
    }

    private void handleAttack()
    {
        if(attacking == true)
        {
            Renderer renderer= gameObject.GetComponent<Renderer>();
            renderer.material = dashMaterial;
            trackAttackTimer += Time.deltaTime;
            if(trackAttackTimer >= attackTimer)
            {
                attacking = false;
                trackAttackTimer = 0;
                renderer.material = normalMaterial;
            }
        }
    }

    public void changeSize()
    {
        arena.localScale *= 0.97f;
        if(gameObject.transform.localScale.x < 3f)
        {
            gameObject.transform.localScale += Vector3.one * 0.02f;
        }
        
    }

    public void playThud()
    { 
        thudSound.Play();
    }
}
