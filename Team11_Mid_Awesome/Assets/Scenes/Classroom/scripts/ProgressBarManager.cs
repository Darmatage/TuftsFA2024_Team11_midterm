using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager instance;
    public float progressAmount;
    public GameObject progressBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update(){
        //progressbar.SetActive(SceneManager.GetActiveScene().name == "ClassroomBook");
        progressBar.GetComponent<Renderer>().material.SetFloat("_progress", progressAmount);
    }

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
        {
            progressAmount += progress;
        }
    }
}

