using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CorrectAnswerAnimation : MonoBehaviour
{
    //plays a coroutine when an answer is submitted
    //coroutine animates correct/incorrect answer icon

    [SerializeField] protected Image _correctImage;
    [SerializeField] protected Image _incorrectImage;

    [Header("Anim Settings")]
    [Range(0, 1f)][SerializeField] protected float _animDuration = 0.5f;
    [SerializeField] protected AnimationCurve _animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    protected IEnumerator OnAnswer(bool correct)
    {




        yield return null;
    }



}
