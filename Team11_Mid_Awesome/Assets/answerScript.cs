using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerScript : MonoBehaviour
{
    public bool isCorrect;
    public QuizManager quizmanager;
    // Start is called before the first frame update
    public void Answer(){
        if(isCorrect){
            Debug.Log("Correct Answer");
            quizmanager.correct();

        }
        else{
            Debug.Log("Incorrect Answer");
            quizmanager.correct();
        }
    }
    

}
