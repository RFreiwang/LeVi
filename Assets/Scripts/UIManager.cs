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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
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
    }

    public void SwitchPanelAndScroll()
    {
        Transform Panel = this.transform.GetChild(0);
        Transform Quizpanel = Panel.transform.Find("Quizpanel");

        this.GetComponent<UnityEngine.UI.ScrollRect>().enabled = false;
        if (Game.Instance.gamestate == GameState.inmainmenu)
        {
            this.GetComponent<UnityEngine.UI.ScrollRect>().enabled = true;
            Quizpanel.gameObject.SetActive(false);
        }
        if (Game.Instance.gamestate == GameState.takingquiz)
        {
            this.GetComponent<UnityEngine.UI.ScrollRect>().enabled = false;
            Quizpanel.gameObject.SetActive(true);
        }
    }

    public void GoBackToMainMenu()
    {
        Game.Instance.SetGameState(GameState.inmainmenu);
    }

    public void GoBack()
    {
        int lastChildIndex = QuizPanel.transform.childCount - 1;
        Debug.Log(lastChildIndex);
        if(lastChildIndex != 0)
        {
            QuizPanel.transform.GetChild(lastChildIndex).gameObject.SetActive(false);
        }
    }

    public void LoadQuizPanel(GameObject gameObject)
    {
        Game.Instance.SetGameState(GameState.takingquiz);
        
        GameObject Artboard = Instantiate(gameObject, transform.position, transform.rotation, QuizPanel.transform);
    }

    public void ClickNext()
    {
        QuizManager.Instance.NextQuestion();
    }
}
