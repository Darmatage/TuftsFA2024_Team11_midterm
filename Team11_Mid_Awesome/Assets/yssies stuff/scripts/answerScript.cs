using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerScript : MonoBehaviour
{
    public bool isCorrect;
    public QuizManager quizmanager;
    private GameController gameController; // Correct variable name

    void Start() {
        // Assign the GameController reference correctly
        GameObject controllerObject = GameObject.FindWithTag("GameController");
        if (controllerObject != null) {
            gameController = controllerObject.GetComponent<GameController>();
        }
    }    
    public void Answer(){
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            gameController.AddScore(1); 
            quizmanager.correctAnswer();  // Call correctAnswer() to handle logic
        }
        else
        {
            Debug.Log("Incorrect Answer");
            quizmanager.incorrectAnswer();  // Even on incorrect, move to next question or scene
        }

    }
    

}
