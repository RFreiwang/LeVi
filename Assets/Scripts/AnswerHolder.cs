using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerHolder : MonoBehaviour
{
    public delegate void AnswerEvent(string ans);
    public event AnswerEvent answerEv;
    public int a = 2;

    public void Answer(string answer)
    {
        answerEv?.Invoke(answer);
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
