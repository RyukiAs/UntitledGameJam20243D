using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public GameObject arenaSlicePrefab;
    public GameObject arenaFloor;
    public float heightGrowthFactor = 5.0f;

    private int nextSliceIndex = 0;
    
    void CreateNextSlice()
    {
        GameObject slice = Instantiate(arenaSlicePrefab);


        float wallWidth = slice.transform.GetChild(0).transform.GetChild(0).localScale.z;
        float growthFactor = 1.0f / (1.0f - 2.0f * wallWidth);

        Vector3 newScale = slice.transform.localScale;
        newScale.x = arenaFloor.transform.localScale.x * Mathf.Pow(growthFactor, nextSliceIndex + 1);
        newScale.y = Mathf.Pow(heightGrowthFactor, nextSliceIndex + 1);
        newScale.z = arenaFloor.transform.localScale.z * Mathf.Pow(growthFactor, nextSliceIndex + 1);
        slice.transform.localScale = newScale;

        slice.transform.position = new Vector3(slice.transform.position.x, newScale.y / 2.0f, slice.transform.position.z);

        nextSliceIndex++;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateNextSlice();
        for (int i = 0; i < 3; i++)
        {
            CreateNextSlice();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
