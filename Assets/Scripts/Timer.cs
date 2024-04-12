using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float completeQuestionTime = 30f;
    [SerializeField] float showCorrectAnswerTime = 10f;
    public bool isAnswerQuestion = false;
    public bool loadNextQuestion;
    public float fillFraction;
    float timerValue;
    public void CancelTimer(){
        timerValue = 0;
    }
    void Update()
    {
        UpdateTimer();
    }
    void UpdateTimer() {
        timerValue -= Time.deltaTime;
        if(timerValue <= 0) {
            if(isAnswerQuestion) {
                isAnswerQuestion = false;
                timerValue = showCorrectAnswerTime;
            }else {
                isAnswerQuestion = true;
                timerValue = completeQuestionTime;
                loadNextQuestion = true;
            }
        }else {
            fillFraction = isAnswerQuestion ? timerValue / completeQuestionTime : timerValue / showCorrectAnswerTime;
        }
    }
}
