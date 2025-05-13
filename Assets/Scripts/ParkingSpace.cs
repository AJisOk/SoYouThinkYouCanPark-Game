using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParkingSpace : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    [field: SerializeField] protected Material activationMaterial;

    [Header("UI")]
    [SerializeField] protected Canvas _parkingSpaceCanvas;
    [SerializeField] protected Image _parkingLockInImage;
    //[SerializeField] protected Image _submittedAnswerImage;
    [SerializeField] protected Image _correctAnswerImage;
    [SerializeField] protected Image _incorrectAnswerImage;
    [SerializeField] protected Image _selectedAnswerImage;

    [Header("Time")]
    [SerializeField] protected float _answerTime;

    public int AnswerID;
    protected Color baseColor;
    protected float _time = 0f;
    private bool _answerSubmitted = false;
    public virtual bool IsInSpot { get; protected set; } = false;
    private ParkingController _playerParkingController;
    private PrometeoCarController playerCarController;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ForceTextFaceCamera();
    }

    private void FixedUpdate()
    {
        //if(playerCarController) print(playerCarController.carSpeed);

        if (_time >= _answerTime)
        {
            _answerSubmitted = true;
            //_selectedAnswerImage.fillAmount = 0f;
            //_parkingLockInImage.enabled = false;
            _time = 0f;
            _selectedAnswerImage.fillAmount = 0f;

            //submit answer, if true, higlight this option as correct
            //if false, highlight this option as incorrect

            if (_playerParkingController.SubmitAnswer(AnswerID))
            {
                _correctAnswerImage.fillAmount = 1f;

                //tell canvas handler to highlight selected answer as correct
            }
            else
            {
                _incorrectAnswerImage.fillAmount = 1f;

                //tell canvas hadndler to highlight selected answer as incorrect
            }

            //_playerParkingController.SubmitAnswer(AnswerID);
            //_submittedAnswerImage.enabled = true;
        }

        //if these are all true, run a countdown to lock in your answer
        if (!_answerSubmitted && IsInSpot && playerCarController.carSpeed <= 0.1 && playerCarController.carSpeed >= -0.1)
        {
            //print("The car is stationary and in a spot");

            _parkingLockInImage.fillAmount = _time / _answerTime;

            _time += Time.deltaTime;
            return;
        }

        _time = 0f;
        if (!_answerSubmitted) _parkingLockInImage.fillAmount = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerParkingController = other.GetComponent<ParkingController>();
        playerCarController = other.GetComponentInParent<PrometeoCarController>();
        baseColor = meshRenderer.materials[0].color;

        _selectedAnswerImage.fillAmount = 1;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (!_playerParkingController.IsOffCourse) 
        {
            IsInSpot = true;

            return;
        }

        IsInSpot = false;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerParkingController = null;

        IsInSpot = false;

        _selectedAnswerImage.fillAmount = 0;

        meshRenderer.materials[0].color = baseColor;
    }

    private void ForceTextFaceCamera()
    {
        _parkingSpaceCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void ThisAnswerIsCorrect()
    {
        _correctAnswerImage.fillAmount = 1f;
    }

}
