using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    string MyName;
    [SerializeField]
    GameObject[] FragenArray;
    bool isFinished;

    public void GetAnswer()
    {

    }

    public void StartQuiz()
    {
        QuizManager.Instance.StartQuiz(this, FragenArray);
    }
}
