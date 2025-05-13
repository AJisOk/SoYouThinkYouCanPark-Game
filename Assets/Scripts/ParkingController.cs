using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingController : MonoBehaviour
{
    [SerializeField] protected CanvasAnimationHandler _canvasAnimHandler;
    private GameManager _gameManager;
    public bool IsOffCourse { get; set; } = false;
    //public bool CanDrive { get; set; } = false;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public bool SubmitAnswer(int answerID)
    {
        if (answerID == _gameManager.CurrentQuestion.CorrectAnswerID)
        {
            CorrectAnswer();
            return true;
        }
        else
        {
            IncorrectAnswer(answerID);
            return false;
        }
    }

    private void CorrectAnswer()
    {
        //higlight correct answer
        //reset scene + load next question

        _canvasAnimHandler.OnCorrectAnswer(_gameManager.CurrentQuestion.CorrectAnswerID);

        //_gameManager.NextQuestion();
    }

    private void IncorrectAnswer(int submittedAnswer)
    {
        //higlhight both selected answer and correct answer
        //reset scene + load next question
        _gameManager.HighlightCorrectAnswer();

        _canvasAnimHandler.OnIncorrectAnswer(_gameManager.CurrentQuestion.CorrectAnswerID, submittedAnswer);
    }
}
