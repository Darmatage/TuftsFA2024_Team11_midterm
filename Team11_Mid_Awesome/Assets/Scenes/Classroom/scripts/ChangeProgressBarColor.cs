using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeProgressBarColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.SetFloat("_progress", ProgressBarManager.progressAmount);
    }
}
