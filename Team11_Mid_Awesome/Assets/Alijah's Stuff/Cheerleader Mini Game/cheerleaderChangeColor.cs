using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheerleaderChangeColor : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Use Time.time for continuous updates
        float progressValue = Mathf.Sin(Time.time * 0.2f); // Adjust the multiplier as needed
        material.SetFloat("_progress", progressValue);
        
        Debug.Log("progress bar: " + material.GetFloat("_progress"));
    }
}
