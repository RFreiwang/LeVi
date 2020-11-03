using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject Startseite;
    [SerializeField]
    public GameObject Panel;
    [SerializeField]
    public GameObject QuizPanel;
    [SerializeField]
    public GameObject WinPanel;
    [SerializeField]
    public GameObject LosePanel;
    [SerializeField]
    public GameObject GamePanel;
    [SerializeField]
    GameObject KapitelAbgeschlossenPanel;
    [SerializeField]
    GameObject AreYouSure;
    [SerializeField]
    GameObject FailNavBar;

    private static UIManager instance = null;

    // Game Instance Singleton
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Game.Instance.OnStateChange += SwitchPanelAndScroll;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GoBack();
        //}
    }

    

    public void SetParent(Transform go, Transform goparent)
    {
        go.transform.parent = goparent;
    }

    public void InstantiateGameObject(Transform go, Transform goparent)
    {
        Instantiate(go, goparent.position, goparent.rotation, goparent);
    }

    public void LoadPrefabScrollable(GameObject gameObject)
    {
        Transform Panel = this.transform.GetChild(0);
        GameObject Artboard = Instantiate(gameObject, Panel.position, Panel.rotation, Startseite.transform);
        FailNavBar.SetActive(true);
        FailNavBar.transform.SetAsLastSibling();
    }

    public void SwitchPanelAndScroll()
    {
      //  Transform Panel = this.transform.GetChild(0);
        Transform Quizpanel = Panel.transform.Find("Quizpanel");

        this.GetComponent<UnityEngine.UI.ScrollRect>().normalizedPosition = new Vector2(0, 1);
        if (Game.Instance.gamestate == GameState.inmainmenu)
        {
            Startseite.SetActive(true);
            this.GetComponent<UnityEngine.UI.ScrollRect>().content = Panel.GetComponent<RectTransform>();
            Quizpanel.gameObject.SetActive(false);
        }
        if (Game.Instance.gamestate == GameState.takingquiz)
        {
            Startseite.SetActive(false);
            this.GetComponent<UnityEngine.UI.ScrollRect>().content = QuizPanel.GetComponent<RectTransform>();
            Quizpanel.gameObject.SetActive(true);
        }
    }

    public void GoBackToMainMenu()
    {
        Quiz quiz = QuizManager.currentQuiz;
        if(quiz == null)
        {
            TryMainMenu();
        }
        else
        {
            AreYouSure.SetActive(true);
        }
    }

    public void TryMainMenu()
    {
        if(QuizManager.Instance != null)
        {
            QuizManager.Instance.ShutDownQuiz();
        }
        GameObject kapP;
        GameObject quizP;
        if (kapP = GameObject.FindGameObjectWithTag("Kapitel"))
        {
            Destroy(kapP);
        }
        if (quizP = GameObject.FindGameObjectWithTag("Quiz"))
        {
            Destroy(quizP);
        }
        FailNavBar.SetActive(false);
        Game.Instance.SetGameState(GameState.inmainmenu);
    }

    public void ForceGoBackToMainMenu()
    {
        TryMainMenu();
        AreYouSure.SetActive(false);
    }

    public void GoBack(GameObject go)
    {
        go.SetActive(false);
        //int lastChildIndex = QuizPanel.transform.childCount - 1;
        //Debug.Log(lastChildIndex);
        //if(lastChildIndex != 0)
        //{
        //    QuizPanel.transform.GetChild(lastChildIndex).gameObject.SetActive(false);
        //}
    }

    public void LoadQuizPanel(GameObject gameObject)
    {
        Game.Instance.SetGameState(GameState.takingquiz);
        GameObject Artboard = Instantiate(gameObject, GamePanel.transform);
        QuizManager.Instance.AllQuestionFinished += ShowKapitelAbgeschlossen;
    }

    public void ClickNext()
    {
        QuizManager.Instance.NextQuestion();
    }

    public void ShowKapitelAbgeschlossen()
    {
        KapitelAbgeschlossenPanel.SetActive(true);
    }

    public void FinishChapter()
    {
        KapitelAbgeschlossenPanel.SetActive(false);
        Debug.Log(GameObject.FindGameObjectWithTag("Kapitel"));
        FailNavBar.SetActive(false);
        QuizManager.Instance.ShutDownQuiz();
        Destroy(GameObject.FindGameObjectWithTag("Kapitel"));
        Destroy(GameObject.FindGameObjectWithTag("Quiz"));
    }


    public void ShowPanel(bool show, GameObject gameObject)
    {
        gameObject.SetActive(show);
    }
}
