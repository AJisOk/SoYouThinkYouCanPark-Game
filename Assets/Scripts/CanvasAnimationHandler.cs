using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CanvasAnimationHandler : MonoBehaviour
{
    [Header("Defaults")]
    [Range(0, 1f)][SerializeField] protected float _baseAnimDuration = 0.5f;
    [Range(0, 1f)][SerializeField] protected float _answerAnimDuration = 0.3f;
    [SerializeField] private AnimationCurve _easingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("BG Image")]
    [SerializeField] protected Image _backgroundImage;
    [Range(0, 1f)][SerializeField] protected float _bgFillIn;
    [Range(0, 1f)][SerializeField] protected float _bgFillOut;

    [Header("Question Number Text")]
    [SerializeField] protected TextMeshProUGUI _questionNumberTMP;
    [SerializeField] protected CanvasGroup _qNumCanvasGroup;
    [Range(0, 1f)][SerializeField] protected float _qNumAlphaIn;
    [Range(0, 1f)][SerializeField] protected float _qNumAlphaOut;

    [Header("Question Text")]
    [SerializeField] protected TextMeshProUGUI _questionTMP;
    [SerializeField] protected Image _questionBoxImage;
    [SerializeField] protected CanvasGroup _questionCanvasGroup;
    [Range(0, 1f)][SerializeField] protected float _qAlphaIn;
    [Range(0, 1f)][SerializeField] protected float _qAlphaOut;

    [Header("Answer Text")]
    [SerializeField] protected List<CanvasGroup> _answerCanvasGroupList;

    [Range(0, 1f)][SerializeField] protected float _answerAlphaIn;
    [Range(0, 1f)][SerializeField] protected float _answerAlphaOut;

    [Header("PauseButton")]
    [SerializeField] protected CanvasGroup _pauseCG;

    [Header("Tutrorial Screen")]
    [SerializeField] protected Canvas _tutorialCanvas;
    [SerializeField] protected CanvasGroup _tutorialAllTextCG;
    [SerializeField] protected List<CanvasGroup> _tutorialCGList;
    [SerializeField] protected CanvasGroup _tutorialButtonCG;
    [Range(0, 1f)][SerializeField] protected float _tutorialAlphaIn;
    [Range(0, 1f)][SerializeField] protected float _tutorialAlphaOut;

    private bool _isUserCorrect = false;
    private CanvasGroup _correctAnswerCG;
    private CanvasGroup _incorrectAnswerCG;
    private GameManager _gameManager;
    private bool _tutorialButtonPressed = false;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _backgroundImage.fillAmount = _bgFillOut;
        _qNumCanvasGroup.alpha = _qNumAlphaOut;
        _questionCanvasGroup.alpha = _qAlphaOut;
        _pauseCG.alpha = _qAlphaOut;
        _tutorialButtonCG.alpha = _tutorialAlphaOut;

        foreach (CanvasGroup cg in _answerCanvasGroupList)
        {
            cg.alpha = _answerAlphaOut;
        }
    }

    private void Start()
    {
        if(_gameManager.CurrentQuestion.QuestionID == 0)
        {
            print("Tutorial Started");

            //start tutorial coroutine
            _tutorialCanvas.gameObject.SetActive(true);
            StartCoroutine(Tutorial());
        }
        else
        {
            print("Question Started");
            //when the scene starts, begin bg animations
            StartCoroutine(StartQuestion());
        }

    }

    protected IEnumerator Tutorial()
    {
        float elapsedTime = 0f;

        yield return new WaitForSeconds(1f);

        //reveal tutorial texts 1 by 1

        foreach (CanvasGroup cg in _tutorialCGList)
        {
            while (elapsedTime < _baseAnimDuration)
            {
                float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

                cg.alpha = Mathf.Lerp(_tutorialAlphaOut, _tutorialAlphaIn, evaluationAtTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            cg.alpha = _tutorialAlphaIn;
            elapsedTime = 0f;
        }

        yield return new WaitForSeconds(1f);

        //reveal "I understand button"
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _tutorialButtonCG.alpha = Mathf.Lerp(_tutorialAlphaOut, _tutorialAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _tutorialButtonCG.enabled = false;
        
        elapsedTime = 0f;

        //wait for player to click button

        while (!_tutorialButtonPressed) yield return null;

        //fade out texts and button

        _tutorialButtonCG.enabled = true;
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _tutorialButtonCG.alpha = Mathf.Lerp(_tutorialAlphaIn, _tutorialAlphaOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        _tutorialAllTextCG.enabled = true;
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _tutorialAllTextCG.alpha = Mathf.Lerp(_tutorialAlphaIn, _tutorialAlphaOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //run start questions coroutine
        StartCoroutine(StartQuestion());
        _tutorialCanvas.gameObject.SetActive(false);

        yield return null;
    }

    protected IEnumerator StartQuestion()
    {
        float elapsedTime = 0f;
        
        yield return new WaitForSeconds(1f);

        //show question number
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime/_baseAnimDuration);

            _qNumCanvasGroup.alpha = Mathf.Lerp(_qNumAlphaOut, _qNumAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;


        //show question box
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _questionCanvasGroup.alpha = Mathf.Lerp(_qAlphaOut, _qAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;

        foreach (CanvasGroup cg in _answerCanvasGroupList)
        {
            while (elapsedTime < _baseAnimDuration)
            {
                float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

                cg.alpha = Mathf.Lerp(_answerAlphaOut, _answerAlphaIn, evaluationAtTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
        }


        //move background
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime/_baseAnimDuration);

            _backgroundImage.fillAmount = Mathf.Lerp(_bgFillOut, _bgFillIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;

        //show pause button
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _pauseCG.alpha = Mathf.Lerp(_answerAlphaOut, _answerAlphaIn, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pauseCG.enabled = false;

        //reveal current question and each answer 1 at a time
        //fade out bg
        //re-enable controls and start

        yield return null;
    }

    protected IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        _pauseCG.enabled = true;

        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _pauseCG.alpha = Mathf.Lerp(_answerAlphaIn, _answerAlphaOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        //question fade out
        foreach (CanvasGroup cg in _answerCanvasGroupList)
        {
            while (elapsedTime < _baseAnimDuration)
            {
                float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

                cg.alpha = Mathf.Lerp(_answerAlphaIn, _answerAlphaOut, evaluationAtTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
        }

        //bg fill
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _backgroundImage.fillAmount = Mathf.Lerp(_bgFillIn, _bgFillOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;


        //hide question number
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _qNumCanvasGroup.alpha = Mathf.Lerp(_qNumAlphaIn, _qNumAlphaOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;


        //hide question box
        while (elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _questionCanvasGroup.alpha = Mathf.Lerp(_qAlphaIn, _qAlphaOut, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        //fade out incorrect answer
        if(_incorrectAnswerCG != null)
        {
            while (elapsedTime < _baseAnimDuration)
            {
                float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

                _incorrectAnswerCG.alpha = Mathf.Lerp(_answerAlphaIn, _answerAlphaOut, evaluationAtTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        elapsedTime = 0f;

        //fade out correct answer
        while(elapsedTime < _baseAnimDuration)
        {
            float evaluationAtTime = _easingCurve.Evaluate(elapsedTime / _baseAnimDuration);

            _correctAnswerCG.alpha = Mathf.Lerp(_answerAlphaIn, _answerAlphaOut, evaluationAtTime) ;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gameManager.NextQuestion(_isUserCorrect);

        yield return null;
    }

    public void TutorialButtonPressed()
    {
        _tutorialButtonPressed = true;
    }



    public void OnCorrectAnswer(int correctID)
    {
        _isUserCorrect = true;

        _correctAnswerCG = _answerCanvasGroupList[correctID];
        _answerCanvasGroupList.RemoveAt(correctID);
        StartCoroutine(FadeOut());
    }

    public void OnIncorrectAnswer(int correctID, int incorrectID)
    {
        _correctAnswerCG = _answerCanvasGroupList[correctID];
        _incorrectAnswerCG = _answerCanvasGroupList[incorrectID];

        //this doesnt work because the first removeat reduces list length
        _answerCanvasGroupList.RemoveAt(correctID);

        int i = _answerCanvasGroupList.IndexOf(_incorrectAnswerCG);
        _answerCanvasGroupList.RemoveAt(i);


        StartCoroutine(FadeOut());
    }

    public void OnReturnToMainMenu()
    {
        _gameManager.SetCurrentQuestion(0);
        _gameManager.CorrectAnswers = 0;
    }
}
