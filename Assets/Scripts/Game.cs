using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    takingquiz,
    inmainmenu,
    achievement
}


public class Game : MonoBehaviour
{
    int mathpoints;
    int sachpoints;
    [SerializeField]
    bool[] finished;

    
    [SerializeField]
    public bool cheat;
    private static Game instance = null;
    public GameState gamestate { get; private set; }
    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;
    QuizManager q;
 
    // Game Instance Singleton
    public static Game Instance
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


        QuizManager q = FindObjectOfType<QuizManager>();

    }

    private void Start()
    {
        Load();
    }

    public int GetMathPoints()
    {
        return mathpoints;
    }

    public int GetSachPoints()
    {
        return sachpoints;
    }

    public bool[] GetFinished()
    {
        return finished;
    }

    public void SetGameState(GameState state)
    {
        this.gamestate = state;
        OnStateChange?.Invoke();
    }

    public void IncrementMathPoints()
    {
        mathpoints++;
    }

    public void IncrementSachPoints()
    {
        sachpoints++;
    }

    public void SetFinished(int index, bool isFinished)
    {
        finished[index] = isFinished;
    }


    public void Save()
    {
        SaveData saveData = new SaveData(mathpoints, sachpoints, finished);
        SaveSystem.Save(saveData);
    }

    public void ResetSaveData()
    {
        SaveSystem.ResetSaveData();
    }

    public void Load()
    {
        SaveData data = SaveSystem.Load();
        if(data != null)
        {
            mathpoints = data.mathpoints;
            sachpoints = data.sachpoints;
            finished = data.finished;
        }
        populateQuizes();
    }


    private void populateQuizes()
    {
        for(int i= 0; i < QuizManager.Instance.QuizArray.Length; i++)
        {
            QuizManager.Instance.QuizArray[i].isFinished = finished[i];
        }
    }

}
