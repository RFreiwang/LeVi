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

    private static UIManager uiinstance = null;

    // Game Instance Singleton
    public static UIManager UIinstance
    {
        get
        {
            return uiinstance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (uiinstance != null && uiinstance != this)
        {
            Destroy(this.gameObject);
        }

        uiinstance = this;
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

    public void LoadQuizPanel(GameObject gameObject)
    {
        Game.Instance.SetGameState(GameState.takingquiz);
        
        GameObject Artboard = Instantiate(gameObject, transform.position, transform.rotation, QuizPanel.transform);
    }

}
