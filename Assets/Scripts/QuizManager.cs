using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SerializeField]
public class QuizManager : MonoBehaviour
{
    [SerializeField]
    Quiz[] QuizArray;

    [SerializeField]
    GameObject GamePanel;

    Queue<GameObject> Fragen;
    public static Quiz currentQuiz = null;
    static GameObject currentQuestion = null;
    private static int localPoints;
    private static QuizManager instance = null;
    public delegate void OnQuestionFinishedHandler();
    public event OnQuestionFinishedHandler AllQuestionFinished;

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


        instance = this;
        DontDestroyOnLoad(this.gameObject);

        currentQuestion = new GameObject();
        currentQuiz = new Quiz();
    }



    public void IsItTrue(bool answer)
    {
        if (answer || Game.Instance.cheat)
        {
            Debug.Log("Question was answered");
            localPoints++;
            if(Fragen.Count > 0)
            {
                UIManager.Instance.WinPanel.SetActive(true);
                UIManager.Instance.WinPanel.transform.SetAsLastSibling();
            }
            else
            {
                AllQuestionFinished?.Invoke();
            }

        }
        else
        {
            Debug.Log("Answer is wrong");
            UIManager.Instance.LosePanel.SetActive(true);
            UIManager.Instance.LosePanel.transform.SetAsLastSibling();
            //TODO implement Lose Screen

        }
    }

    public void NextQuestion()
    {
        Debug.Log("next q");
        AnswerHolder ans = currentQuestion.GetComponent<AnswerHolder>();
        Transform Kapitelauswahl = currentQuestion.transform.parent;
        Destroy(currentQuestion.transform.gameObject);
        UIManager.Instance.WinPanel.SetActive(false);

        Debug.Log(Fragen.Count);
        if(Fragen.Count > 0)
        {
            GameObject go = Fragen.Dequeue();
            Debug.Log(go);
            if (go != null)
            {
                currentQuestion = (GameObject)Instantiate(go, currentQuiz.transform.position, currentQuiz.transform.rotation, currentQuiz.transform) as GameObject;
                ans = currentQuestion.GetComponent<AnswerHolder>();
            }
        }

       

        else
        {
            //Debug.Log("All Questions were correctly answered");
            //Debug.Log(GameObject.FindGameObjectWithTag("Kapitel"));
            //Destroy(GameObject.FindGameObjectWithTag("Kapitel"));
            //Destroy(currentQuiz.gameObject);
            //Game.Instance.SetGameState(GameState.inmainmenu);

        }

    }

    private void EnqueueQuestions(GameObject[] fragen)
    {
        Fragen = new Queue<GameObject>();
        GameObject quizstartenRect = GameObject.Find("QuizstartenRect");
        if (quizstartenRect != null)
        {
            foreach (GameObject Frage in fragen)
            {
                Fragen.Enqueue(Frage);
            }
        }
    }

    public void ShutDownQuiz()
    {
        if(currentQuiz != null)
        {
            Destroy(currentQuiz.gameObject);
        }
        Game.Instance.SetGameState(GameState.inmainmenu);
    }

    public void StartQuiz(Quiz quiz, GameObject[] fragen)
    {
        EnqueueQuestions(fragen);
        GameObject go = Fragen.Dequeue();
        currentQuestion = (GameObject) Instantiate(go, quiz.transform.position, quiz.transform.rotation, quiz.transform) as GameObject;
        currentQuiz = quiz;
        AnswerHolder ans = currentQuestion.GetComponent<AnswerHolder>();
    }
}
