using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class displaySize : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerSize();
    }

    public void getPlayerSize()
    {
        if(player != null)
        {
            PlayerBehavior playerScript = player.GetComponent<PlayerBehavior>();
            float size = playerScript.playerSize;
            text.text = "Size: " + size;
        }
        
    }
}
