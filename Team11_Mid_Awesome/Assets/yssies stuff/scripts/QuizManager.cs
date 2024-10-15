using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;// For the legacy Text component

public class QuizManager : MonoBehaviour
{
    public List<questionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestionIndex;
    public Text questionText; // Legacy Text component
     private int score;
     public int scoreWin = 5;
    
    // Start is called before the first frame update
    private void Start()
    {
       generateQuestion();
    }

    public void correctAnswer(){
        score++;
        if(score == 5){
            SceneManager.LoadScene("ClassroomBook");
        }
        // Remove the answered question from the list
        QnA.RemoveAt(currentQuestionIndex);

        // If all questions are done but player hasn't won, load "nerds_talk"
        if (QnA.Count == 0) // Check if no questions remain
        {
            SceneManager.LoadScene("nerds_talk"); // Load 'nerds_talk' scene
        }
        else
        {
            generateQuestion(); // Continue to next question
        }
    }
      public void incorrectAnswer(){
        // Remove the answered question from the list
        QnA.RemoveAt(currentQuestionIndex);

        // If all questions are done but player hasn't won, load "nerds_talk"
        if (QnA.Count == 0) // Check if no questions remain
        {
            SceneManager.LoadScene("nerds_talk"); // Load 'nerds_talk' scene
        }
        else
        {
            generateQuestion(); // Continue to next question
        }
    }

    void generateQuestion(){
        currentQuestionIndex = Random.Range(0, QnA.Count);
        questionText.text = QnA[currentQuestionIndex].questions;
        setAnswers();
    }

    void setAnswers(){
        for (int i = 0; i < options.Length; i++){  // Fixed 'length' to 'Length'
            options[i].GetComponent<answerScript>().isCorrect = false;  
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestionIndex].answers[i]; // Legacy Text component

            if(QnA[currentQuestionIndex].correctAnswerIndex == i+1){
                 options[i].GetComponent<answerScript>().isCorrect = true; 
            }
        }
    }
    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}