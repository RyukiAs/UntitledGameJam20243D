using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public GameObject arenaSlicePrefab;

    private int nextSliceIndex = 0;
    private float sliceGrowthFactor = 5.0f;
    void CreateNextSlice()
    {
        GameObject slice = Instantiate(arenaSlicePrefab);

        Vector3 newScale = slice.transform.localScale;
        newScale.x *= Mathf.Pow(5.0f, nextSliceIndex);
        newScale.y *= Mathf.Pow(2.0f, nextSliceIndex);
        newScale.z *= Mathf.Pow(5.0f, nextSliceIndex);
        slice.transform.localScale = newScale;
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
