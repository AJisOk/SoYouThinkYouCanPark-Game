using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Scriptable Objects/Question Data")]
public class QuestionData : ScriptableObject
{
    [Header("Question Data")]
    public int QuestionID;
    public string QuestionName;
    public string AnswerA;
    public string AnswerB;
    public string AnswerC;
    public string AnswerD;
    public int CorrectAnswerID;

    
}
