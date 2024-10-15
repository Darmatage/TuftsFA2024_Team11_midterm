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
    
    // Start is called before the first frame update
    private void Start()
    {
       generateQuestion();
    }

    public void correct(){
        QnA.RemoveAt(currentQuestionIndex);
        if (QnA.Count == 0 || currentQuestionIndex == 4) // When all questions are done or index reaches 4
        {
            SceneManager.LoadScene("ClassroomBook"); // Switch to your scene
        }
        else
        {
            generateQuestion();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}