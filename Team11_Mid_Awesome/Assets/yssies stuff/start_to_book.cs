using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_to_book : MonoBehaviour
{
    public void SwitchScenes()
    {
        SceneManager.LoadScene("ClassroomBook");
    }
}
