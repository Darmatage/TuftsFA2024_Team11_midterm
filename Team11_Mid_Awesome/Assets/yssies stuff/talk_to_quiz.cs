using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class talk_to_quiz : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("nerds");
    }
}
