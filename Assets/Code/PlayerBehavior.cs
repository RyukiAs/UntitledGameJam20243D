using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerBehavior : MonoBehaviour
{
    //never set the value of a public variable here
    public float speed;
    public GameObject bulletPrefab;

    public float secondsBetweenShots;
    float secondsSinceLastShot;

    public float secondsBetweenJumps;
    float secondsSinceLastJump;

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        References.thePlayer = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //WASD to move
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

        //look at new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        //Firing
        secondsSinceLastShot += Time.deltaTime;
        secondsSinceLastJump += Time.deltaTime;

        //Click to fire
        // if clicked, create a bullet at current position
        if (Input.GetButton("Fire1") && secondsSinceLastShot >= secondsBetweenShots)
        {
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            //transform.position += Vector3.up * 0.01f;
            secondsSinceLastShot = 0;
        }

        if (Input.GetButton("Jump") && secondsSinceLastJump >= secondsBetweenJumps)
        {
            ourRigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 10, Input.GetAxis("Vertical"));
            secondsSinceLastJump = 0;
        }

    }
}
