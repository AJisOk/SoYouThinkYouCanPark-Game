using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<QuestionData> _questionsList;
    [SerializeField] private List<ParkingSpace> _parkingSpaceList;


    public int CorrectAnswers = 0;
    private GameObject[] _gameObjects;
    private ParkingSpace _ps;
    public QuestionData CurrentQuestion;
    public int NoOfQuestions;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (CurrentQuestion == null) CurrentQuestion = _questionsList[0];

        NoOfQuestions = _questionsList.Count;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //populates parking spot list each time scene reloads THIS DOESNT WORK - DOESNT POPULATE IN HEIRARCHRICAL ORDER
    //I can sort these because the parkingspace class has answer id variable
    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        List<ParkingSpace> tempPSList = new List<ParkingSpace>(4);

        if (_parkingSpaceList.Count == 4) _parkingSpaceList.Clear();

        _gameObjects = GameObject.FindGameObjectsWithTag("Parking Space");
        if (_gameObjects.Length == 0) return;


        foreach (GameObject go in _gameObjects)
        {
            _ps = go.GetComponent<ParkingSpace>();
            tempPSList.Add(_ps);
            _parkingSpaceList.Add(null);
        }

        foreach (ParkingSpace ps in tempPSList)
        {
            _parkingSpaceList[ps.AnswerID] = ps;
        }
    }

    public void SetCurrentQuestion(int questionId)
    {
        CurrentQuestion = _questionsList[questionId];
    }

    public void NextQuestion(bool correct)
    {
        if (correct) CorrectAnswers += 1;

        if (_questionsList.Count == CurrentQuestion.QuestionID + 1)
        {
            SceneManager.LoadScene("GameOverScene");
            return;
        }

        CurrentQuestion = _questionsList[CurrentQuestion.QuestionID + 1];
        SceneManager.LoadScene("GameScene");
    }

    public void HighlightCorrectAnswer()
    {
        //print("HighlightCorrectAnswer() - CorrectAnswerID: " + CurrentQuestion.QuestionID);
        _parkingSpaceList[CurrentQuestion.CorrectAnswerID].ThisAnswerIsCorrect();

    }
}
