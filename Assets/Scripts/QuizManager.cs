using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[SerializeField]
public class QuizManager : MonoBehaviour
{
    [SerializeField]
    public Quiz[] QuizArray;


    [SerializeField]
    GameObject GamePanel;

    [SerializeField]
    Slider slider;

    Queue<GameObject> Fragen;
    public Quiz currentQuiz = null;
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
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        for(int i = 0; i < QuizArray.Length; i++)
        {
            QuizArray[i].quizIndex = i;
        }

        
    }

    private void updateLocalSlider()
    {
        UIManager.Instance.TweenProgressSlider(localPoints, slider);
    }

    public void IsItTrue(bool answer)
    {
        AudioManager.Instance.Stop();
        if (answer || Game.Instance.cheat)
        {
            Debug.Log("Question was answered");
            
            if(Fragen.Count > 0)
            {
                UIManager.Instance.WinPanel.SetActive(true);
                UIManager.Instance.TweenLoseAndWinPanel(UIManager.Instance.WinPanel);
                UIManager.Instance.WinPanel.transform.SetAsLastSibling();
            }
            else
            {

                Game.Instance.SetFinished(currentQuiz.quizIndex, true);
                if (currentQuiz.Fach == Fächer.Mathe)
                {
                    if (!currentQuiz.isFinished)
                    {
                        Game.Instance.IncrementMathPoints();
                    }
                }
                else if (QuizManager.Instance.currentQuiz.Fach == Fächer.Sachkunde)
                {
                    if (!currentQuiz.isFinished)
                    {
                        Game.Instance.IncrementSachPoints();
                    }
                }
                currentQuiz.isFinished = true;
                AllQuestionFinished?.Invoke();
            }

        }
        else
        {
            Debug.Log("Answer is wrong");
            UIManager.Instance.LosePanel.SetActive(true);
            UIManager.Instance.TweenLoseAndWinPanel(UIManager.Instance.LosePanel);
            UIManager.Instance.LosePanel.transform.SetAsLastSibling();

        }
    }

    public void NextQuestion()
    {
        localPoints++;
        Debug.Log("next q");
        AnswerHolder ans = currentQuestion.GetComponent<AnswerHolder>();
        Transform Kapitelauswahl = currentQuestion.transform.parent;
        Destroy(currentQuestion.transform.gameObject);
        UIManager.Instance.CloseLoseAndWinPanel(UIManager.Instance.WinPanel);
      

        Debug.Log(Fragen.Count);
        if(Fragen.Count > 0)
        {
            GameObject go = Fragen.Dequeue();
            Debug.Log(go);
            if (go != null)
            {
                currentQuestion = Instantiate(go, currentQuiz.transform.position, currentQuiz.transform.rotation, currentQuiz.transform);
                updateLocalSlider();
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
        localPoints = 0;
        updateLocalSlider();
        if(currentQuiz != null)
        {
            Destroy(currentQuiz.gameObject);
        }
        Game.Instance.SetGameState(GameState.inmainmenu);
    }

    public void StartQuiz(GameObject[] fragen)
    {
        EnqueueQuestions(fragen);
        GameObject go = Fragen.Dequeue();
        currentQuestion = Instantiate(go, currentQuiz.transform.position, currentQuiz.transform.rotation, currentQuiz.transform);
        AnswerHolder ans = currentQuestion.GetComponent<AnswerHolder>();
    }
}


public enum Fächer
{
    Sachkunde,
    Mathe
}