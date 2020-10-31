using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject Startseite;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void LoadPrefab(GameObject gameObject)
    {
        Transform Panel = this.transform.GetChild(0);
        GameObject Artboard = Instantiate(gameObject, transform.position, transform.rotation, Panel);
    }

}
