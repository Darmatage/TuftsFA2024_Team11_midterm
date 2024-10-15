using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBarManager : MonoBehaviour
{
    public static ProgressBarManager instance;
    public static float progressAmount;
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
        //progressBar.GetComponent<Renderer>().material.SetFloat("_progress", progressAmount);
    }

    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
}

