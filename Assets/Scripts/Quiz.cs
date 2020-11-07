using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quiz : MonoBehaviour
{
    [SerializeField]
    public string MyName;
    [SerializeField]
    GameObject[] FragenArray;
    [SerializeField]
    public bool isFinished;

    public int quizIndex;

    public void StartQuiz()
    {
        QuizManager.Instance.StartQuiz(FragenArray);
    }
}
