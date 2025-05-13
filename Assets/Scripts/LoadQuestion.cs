using UnityEngine;
using TMPro;

public class LoadQuestion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionNumberText;
    [SerializeField] private TextMeshProUGUI _questionNameText;
    [SerializeField] private TextMeshProUGUI _answerTextA;
    [SerializeField] private TextMeshProUGUI _answerTextB;
    [SerializeField] private TextMeshProUGUI _answerTextC;
    [SerializeField] private TextMeshProUGUI _answerTextD;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void Start()
    {
        _questionNumberText.text = "Question " + (_gameManager.CurrentQuestion.QuestionID + 1) + " / " + (_gameManager.NoOfQuestions);

        _questionNameText.text = _gameManager.CurrentQuestion.QuestionName;

        _answerTextA.text = "A. " + _gameManager.CurrentQuestion.AnswerA;
        _answerTextB.text = "B. " + _gameManager.CurrentQuestion.AnswerB;
        _answerTextC.text = "C. " + _gameManager.CurrentQuestion.AnswerC;
        _answerTextD.text = "D. " + _gameManager.CurrentQuestion.AnswerD;
    }
}
