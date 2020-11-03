using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    takingquiz,
    inmainmenu
}


public class Game : MonoBehaviour
{
    [SerializeField]
    public bool cheat;
    private static Game instance = null;
    public GameState gamestate { get; private set; }
    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;
    private QuizManager quizManager;

 
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

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        quizManager = QuizManager.Instance;
    }

    public void SetGameState(GameState state)
    {
        this.gamestate = state;
        OnStateChange?.Invoke();
    }


    


}
