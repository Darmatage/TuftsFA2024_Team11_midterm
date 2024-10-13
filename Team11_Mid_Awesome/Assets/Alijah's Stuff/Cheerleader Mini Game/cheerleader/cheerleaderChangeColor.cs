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
        float progressValue = CheerleaderGameVariables.Progress;
        material.SetFloat("_progress", progressValue);
    }

}
