using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadQuizScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void activateQuizpanel(GameObject go)
    {
        UIManager.UIinstance.LoadQuizPanel(go);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
