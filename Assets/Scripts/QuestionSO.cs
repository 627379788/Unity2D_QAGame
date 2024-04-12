using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "请输入问题!";
    [SerializeField] string[] answerEnum = new string[4];
    [SerializeField] int correctAnserIndex;

    public int GetCorrectAnserIndex(){
       return correctAnserIndex >= 0 ? correctAnserIndex : -1;
    }
    public string GetAnswer(int index) {
        return answerEnum[index];
    }
    public string GetQuestion(){
        return question;
    }
}
  