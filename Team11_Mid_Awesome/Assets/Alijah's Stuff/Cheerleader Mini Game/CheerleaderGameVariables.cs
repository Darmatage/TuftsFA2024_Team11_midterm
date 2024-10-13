using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerleaderGameVariables : MonoBehaviour
{

    public static float Progress;
    public static float Difficulty;

    public float debugDifficulty;

    // Start is called before the first frame update
    void Start()
    {   
        Progress = 0f;
        Difficulty = 0.0f;

        debugDifficulty = Difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        Difficulty = debugDifficulty;
    }
}
