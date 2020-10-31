using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SerializeField]
public class QuizManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] FragenArray;
    Queue<GameObject> Fragen;
    static GameObject currentQuestion;
    private static int localPoints;
    private static QuizManager instance = null;
    UnityEvent QuestionAnswer;
    AnswerHolder answerHolder;

    public static QuizManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        if(answerHolder == null)
        {
            answerHolder = new AnswerHolder();
        }

        answerHolder.answerEv += IsItTrue;
        Debug.Log(answerHolder.a);
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        Fragen = new Queue<GameObject>();
        GameObject quizstartenRect = GameObject.Find("QuizstartenRect");
        if (quizstartenRect != null)
        {
           foreach(GameObject Frage in FragenArray)
            {
                Fragen.Enqueue(Frage);
            }
        }
    }


    public void IsItTrue(string answer)
    {
        if (answer == "y")
        {
            Debug.Log("Question was answered");
            Destroy(currentQuestion);
            currentQuestion = Fragen.Dequeue();
            localPoints++;
            Instantiate(currentQuestion, transform.position, transform.rotation, this.transform);

        }
        else
        {
            //TODO implement Lose Screen
     
        }
    }

    public void starteQuiz()
    {
        currentQuestion = Fragen.Dequeue();
        Instantiate(currentQuestion, transform.position, transform.rotation, this.transform);

    }
}
