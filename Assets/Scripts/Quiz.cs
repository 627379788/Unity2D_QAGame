using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questionList = new List<QuestionSO>();

    [Header("Answer")]
    [SerializeField] GameObject[] answerEumb;
    bool isEarlyAnswer = true;
    [Header("ButtonColor")]
    [SerializeField] Sprite correctSprite;
    [SerializeField] Sprite errorSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [Header("progressBar")]
    [SerializeField] Slider sliderProgress;

    public bool isComplete;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sliderProgress.maxValue = questionList.Count;
        sliderProgress.minValue = 0;
    }
    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion) {
            if(sliderProgress.value == sliderProgress.maxValue) {
                isComplete = true;
                return;
            }
            isEarlyAnswer = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }else if(!isEarlyAnswer && !timer.isAnswerQuestion) {
            DisplayCorrectAnswer(-1);
            SetButtonState(false);
        }
    }
    void DisplayCorrectAnswer(int index){
        Image buttonImage;
        if(index == currentQuestion.GetCorrectAnserIndex()) {
            buttonImage = answerEumb[index].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
            questionText.text = "correct!";
            scoreKeeper.IncrementCorrectAnswers();
        }else {
            buttonImage = answerEumb[currentQuestion.GetCorrectAnserIndex()].GetComponent<Image>();
            buttonImage.sprite = correctSprite;
            questionText.text = "Sorry, the correct answer was:\n" + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnserIndex());
        }
    }

    void initButtonText() {
        questionText.text = currentQuestion.GetQuestion();
        for(int i = 0; i < answerEumb.Length; i++) {
            answerEumb[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
        }
    }

    void GetNextQuestion() {
        if (questionList.Count > 0) {
            GetRandomQuestion();
            initButtonText();
            SetButtonState(true);
            SetDefaultSprite();
            sliderProgress.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
    }
    void GetRandomQuestion() {
        int index = Random.Range(0,questionList.Count);
        if(questionList.Contains(questionList[index])) {
            currentQuestion = questionList[index];
            questionList.Remove(currentQuestion);
        }
    }

    void SetDefaultSprite()
    {
        for(int i = 0; i < answerEumb.Length; i++) {
            answerEumb[i].GetComponent<Image>().sprite = errorSprite;
        }
    }

    public void ClickAnswerButton(int index) {
        isEarlyAnswer = true;
        DisplayCorrectAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " +  scoreKeeper.CalculateScore() + "%";
    }

    void SetButtonState(bool state) {
        for(int i = 0; i < answerEumb.Length; i++) {
            Button button = answerEumb[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
