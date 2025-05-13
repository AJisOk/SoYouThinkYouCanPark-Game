using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameOverHandler : MonoBehaviour
{
    [Header("Defaults")]
    [Range(0, 1f)][SerializeField] protected float _baseAnimDuration = 0.5f;
    [SerializeField] private AnimationCurve _easingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [Range(0, 1f)][SerializeField] protected float _defaultAlphaIn = 1f;
    [Range(0, 1f)][SerializeField] protected float _defaultAlphaOut = 0f;

    [Header("Text")]
    [SerializeField] protected TextMeshProUGUI _scoreText;

    [Header("Canvas Groups")]
    [SerializeField] protected CanvasGroup _titleCG;
    [SerializeField] protected CanvasGroup _scorePromptCG;
    [SerializeField] protected CanvasGroup _scoreTextCG;
    [SerializeField] protected CanvasGroup _buttonContainerCG;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _titleCG.alpha = 0f;
        _scorePromptCG.alpha = 0f;
        _scoreTextCG.alpha = 0f;
        _buttonContainerCG.alpha = 0f;


    }

    private void Start()
    {
        SetFinalScore();
        
        StartCoroutine(GameOverAnimation());
    }

    private IEnumerator GameOverAnimation()
    {
        float elapsedTime = 0f;

        //show title
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _titleCG.alpha = Mathf.Lerp(_defaultAlphaOut, _defaultAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;

        //show score prommpt
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _scorePromptCG.alpha = Mathf.Lerp(_defaultAlphaOut, _defaultAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;

        //wait .5 sec
        yield return new WaitForSeconds(1f);

        //show score
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _scoreTextCG.alpha = Mathf.Lerp(_defaultAlphaOut, _defaultAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;

        //wait .5 sec
        yield return new WaitForSeconds(1f);

        //show buttons
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _buttonContainerCG.alpha = Mathf.Lerp(_defaultAlphaOut, _defaultAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _buttonContainerCG.alpha = 1f;
        _buttonContainerCG.enabled = false;
        elapsedTime = 0f;

        yield return null;
    }

    private void SetFinalScore()
    {
        int questions = _gameManager.CurrentQuestion.QuestionID + 1;
        int correctAnswers = _gameManager.CorrectAnswers;

        _scoreText.text = correctAnswers.ToString() + " / " + questions.ToString();
    }

    public void ResetQuestions()
    {
        _gameManager.SetCurrentQuestion(0);
        _gameManager.CorrectAnswers = 0;
    }


}
