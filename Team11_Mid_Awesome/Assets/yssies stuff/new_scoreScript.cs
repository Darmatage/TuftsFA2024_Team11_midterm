using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

     public GameObject textGameObject;
     private int score;

     void Start () {
          score = 0;
          UpdateScore();
     }

     public void AddScore (int newScoreValue) {
          score += newScoreValue;
          UpdateScore ();
     }

     void UpdateScore () {
          Text scoreTextB = textGameObject.GetComponent<Text>();
          scoreTextB.text = "Score: " + score;
     }
}