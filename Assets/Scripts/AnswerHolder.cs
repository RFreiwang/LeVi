using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerHolder : MonoBehaviour
{
    [SerializeField]
    private bool answer;

    public void Answer()
    {
        QuizManager.Instance.IsItTrue(answer);
    }

    public void AbortQuiz()
    {
        UIManager.Instance.GoBackToMainMenu();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
