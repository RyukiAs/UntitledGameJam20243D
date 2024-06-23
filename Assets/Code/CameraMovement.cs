using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
            return; // Exit if player is not assigned

        Vector3 currentPosition = gameObject.transform.position;
        Vector3 targetPosition = new Vector3(player.transform.position.x, currentPosition.y, player.transform.position.z);
        transform.position = targetPosition;


        //transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * 5f);
        //this.gameObject.transform.position = position;
    }
}
