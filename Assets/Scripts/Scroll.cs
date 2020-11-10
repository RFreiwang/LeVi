using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    GameObject panel;
    float offset;
    float startPos;

    private void Start()
    {
        startPos = this.transform.localPosition.y;
        panel = GameObject.Find("Panel");
    }

    // Update is called once per frame
    void Update()
    {
        offset = startPos - panel.transform.localPosition.y;
       this.transform.localPosition = new Vector3(this.transform.localPosition.x, offset, 0);
    }
}
