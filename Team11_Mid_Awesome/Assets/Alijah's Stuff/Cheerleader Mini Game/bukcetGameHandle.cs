using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bukcetGameHandle : MonoBehaviour
{

    public float progresssAmount;
    public float progressBarSpeed;

    // Start is called before the first frame update
    void Start()
    {
        progresssAmount = 0f;
        progressBarSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        progresssAmount = Mathf.Lerp(0, 1f, progressBarSpeed * Time.fixedDeltaTime);
    }
}
