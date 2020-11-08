using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField]
    Text punkte;
    [SerializeField]
    GameObject StartNav;
    [SerializeField]
    Image upperImage;
    [SerializeField]
    Image lowerImage;
    [SerializeField]
    GameObject Eule;
    [SerializeField]
    Sprite correct;
    [SerializeField]
    GameObject erfolge;
    [SerializeField]
    Slider matheSlider;
    [SerializeField]
    Slider sachkSlider;
    [SerializeField]
    Text matheText;
    [SerializeField]
    Text sachkText;

    Tween childTween;

    private Button[] buttons;
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
        else
        {
            instance = this;
        }

        buttons = gameObject.GetComponentsInChildren<Button>();

        QuizPanel.SetActive(false);
  
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startSzene());

        Game.Instance.OnStateChange += SwitchPanelAndScroll;

    }
    

    IEnumerator startSzene()
    {
        foreach (Button but in buttons)
        {
            but.transform.localPosition = new Vector3(0, but.transform.localPosition.y - 1000, 0);
        }

  
        tweenImageLeft(upperImage);
        tweenImageLeft(lowerImage);
        StartCoroutine(tweenStartButtons(buttons));

        
        yield return null;
    }

    public IEnumerator tweenStartButtons(Button[] buttonArray)
    {
        for(int i = 0; i < buttonArray.Length; i++)
        {
            Tween buttonTween = buttonArray[i].gameObject.transform.DOLocalMoveY(buttonArray[i].transform.localPosition.y + 1000, 1f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);

        }
    }

    public IEnumerator TweenAllChildren(Transform go, float duration, float waitTime, Ease easeType)
    {
        foreach (Transform child in go)
        {
            child.localPosition = new Vector3(child.localPosition.x, child.localPosition.y - 1000, 0);
        }
        foreach (Transform child in go)
        {
            childTween = child.gameObject.transform.DOLocalMoveY(child.transform.localPosition.y + 1000, duration).SetEase(easeType);
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    public IEnumerator TweenErfolge()
    {
        matheSlider.value = 0;
        sachkSlider.value = 0;
        matheText.text = "0";
        sachkText.text = "0";
        int mathPoints = Game.Instance.GetMathPoints();
        int sachPoints = Game.Instance.GetSachPoints();
        float math = Mathf.Clamp01((float)mathPoints/4f);
        float sach = Mathf.Clamp01((float)sachPoints/4f);
        sachkText.text = sachPoints.ToString();
        matheText.text = mathPoints.ToString();

        while (childTween.IsActive())
        {
            yield return 0;
        }

    
        Tween mathTween = matheSlider.DOValue(math, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => { 
            Tween sachTween = sachkSlider.DOValue(sach, 0.5f).SetEase(Ease.OutCirc); });

    }


    public void TweenProgressSlider(int i, Slider slider)
    {
        float points = Mathf.Clamp01(i / 10f);
        slider.DOValue(points, 0.5f).SetEase(Ease.OutQuad);
        updatePunkte(i);
    }

    public void updatePunkte(int i)
    {
        punkte.text = "Punkte " + i + "/10";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMainMenu();
        }
    }


    private void tweenImageLeft(Image img)
    {
        img.fillAmount = 0;
        img.DOFillAmount(1f, 1.5f).SetDelay(0.5f).SetEase(Ease.InOutQuad);
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
        Game.Instance.Load();
        Transform Panel = this.transform.GetChild(0);
        loadQuizScript[] quizarray = gameObject.GetComponentsInChildren<loadQuizScript>();
        StartNav.SetActive(false);
        if (gameObject.name == "Kapitelauswahl Mathematik")
        {
            for (int i = 0; i < quizarray.Length; i++)
            {
                Quiz quiz = QuizManager.Instance.QuizArray[i].GetComponent<Quiz>();
                if (quiz.isFinished)
                {
                    quizarray[i].gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    quizarray[i].gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        if (gameObject.name == "Kapitelauswahl Sachkunde")
        {
            for (int i = 0; i < quizarray.Length; i++)
            {
                Quiz quiz = QuizManager.Instance.QuizArray[i+4].GetComponent<Quiz>();
                if (quiz.isFinished)
                {
                    quizarray[i].gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    quizarray[i].gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        GameObject artboard = Instantiate(gameObject, Panel.position, Panel.rotation, Startseite.transform);
        FailNavBar.SetActive(true);
        FailNavBar.transform.SetAsLastSibling();


        StartCoroutine(TweenAllChildren(artboard.transform, 1f, 0.2f, Ease.OutBack));
    }

    public void ResetSaveButton()
    {
        Game.Instance.ResetSaveData();
    }

    public void SetToDone(Quiz[] quizArray)
    {
        foreach(Quiz quiz in quizArray)
        {

        }
    }

    public void TweenLoseAndWinPanel(GameObject go)
    {
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.DOScale(0, 0.1f).From().SetEase(Ease.InOutQuad);
    }

    public void CloseLoseAndWinPanel(GameObject go)
    {
        go.transform.DOScale(0, 0.1f).SetEase(Ease.InOutQuad).OnComplete(() => { go.SetActive(false); });
    }

    public void SwitchPanelAndScroll()
    {
      //  Transform Panel = this.transform.GetChild(0);
        Transform Quizpanel = Panel.transform.Find("Quizpanel");

        this.GetComponent<UnityEngine.UI.ScrollRect>().normalizedPosition = new Vector2(0, 1);
        if (Game.Instance.gamestate == GameState.inmainmenu)
        {
            erfolge.gameObject.SetActive(false);
            Quizpanel.gameObject.SetActive(false);
            this.GetComponent<UnityEngine.UI.ScrollRect>().content = Panel.GetComponent<RectTransform>();
            Startseite.SetActive(true);

        }
        if (Game.Instance.gamestate == GameState.takingquiz)
        {
            Startseite.SetActive(false);
            erfolge.gameObject.SetActive(false);
            this.GetComponent<UnityEngine.UI.ScrollRect>().content = QuizPanel.GetComponent<RectTransform>();
            Quizpanel.gameObject.SetActive(true);
        }
        if(Game.Instance.gamestate == GameState.achievement)
        {
            Startseite.SetActive(false);
            Quizpanel.gameObject.SetActive(false);
            this.GetComponent<UnityEngine.UI.ScrollRect>().content = erfolge.GetComponent<RectTransform>();
            erfolge.SetActive(true);
        }
    }

    public void GoBackToMainMenu()
    {
        Quiz quiz = QuizManager.Instance.currentQuiz;
        if(quiz == null)
        {
            TryMainMenu();
        }
        else
        {
            AreYouSure.SetActive(true);
            TweenLoseAndWinPanel(AreYouSure);
        }
    }

    public void ShowErfolge()
    {
        Game.Instance.Load();
        Game.Instance.SetGameState(GameState.achievement);
        StartCoroutine(TweenAllChildren(erfolge.transform.GetChild(0), 1f, 0.2f, Ease.OutBack));
        StartCoroutine(TweenErfolge());
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
        StartNav.SetActive(true);
        FailNavBar.SetActive(false);
        Game.Instance.SetGameState(GameState.inmainmenu);
        StartCoroutine(startSzene());
    }

    public void ForceGoBackToMainMenu()
    {
        TryMainMenu();
        AreYouSure.SetActive(false);
    }

    public void GoBackAchievements(GameObject go)
    {
        Game.Instance.SetGameState(GameState.inmainmenu);
    }

    public void GoBack(GameObject go)
    {
        CloseLoseAndWinPanel(go);
        //int lastChildIndex = QuizPanel.transform.childCount - 1;
        //Debug.Log(lastChildIndex);
        //if(lastChildIndex != 0)
        //{
        //    QuizPanel.transform.GetChild(lastChildIndex).gameObject.SetActive(false);
        //}
    }

    public void LoadQuizPanel(int index)
    {
        Game.Instance.SetGameState(GameState.takingquiz);
        GameObject artboard = Instantiate(QuizManager.Instance.QuizArray[index].gameObject, GamePanel.transform);
        QuizManager.Instance.currentQuiz = artboard.GetComponent<Quiz>();
        QuizManager.Instance.AllQuestionFinished += ShowKapitelAbgeschlossen;
        StartCoroutine(TweenAllChildren(artboard.transform, 1f, 0.2f, Ease.OutBack));
    }


    public void ClickNext()
    {
        QuizManager.Instance.NextQuestion();
    }

    public void ShowKapitelAbgeschlossen()
    {
        KapitelAbgeschlossenPanel.SetActive(true);
        TweenLoseAndWinPanel(KapitelAbgeschlossenPanel);
    }

    public void FinishChapter()
    {
        KapitelAbgeschlossenPanel.SetActive(false);
        Debug.Log(GameObject.FindGameObjectWithTag("Kapitel"));
        FailNavBar.SetActive(false);
        QuizManager.Instance.ShutDownQuiz();
        Destroy(GameObject.FindGameObjectWithTag("Kapitel"));
        Destroy(GameObject.FindGameObjectWithTag("Quiz"));
        StartNav.SetActive(true);
        StartCoroutine(startSzene());
        Game.Instance.Save();
    }


    public void ShowPanel(bool show, GameObject gameObject)
    {
        gameObject.SetActive(show);
    }
}
