using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadQuizScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void activateQuizpanel(int index)
    {
        //go.tag = "Kapitel";
        UIManager.Instance.LoadQuizPanel(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
