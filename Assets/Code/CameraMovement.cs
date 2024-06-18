using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        Vector3 position = new Vector3(x, this.gameObject.transform.position.y, z);
        this.gameObject.transform.position = position;
    }
}
