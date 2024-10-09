using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class desk_cheer : MonoBehaviour {
     void OnTriggerEnter(Collider other){
            if (other.gameObject.tag == "Player") {
                SceneManager.LoadScene("Alijahs_Scene");
             }
      }
}