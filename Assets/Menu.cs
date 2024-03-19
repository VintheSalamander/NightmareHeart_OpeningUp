using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public GameObject gameController;
    public GameObject spellCanvas;
    // Start is called before the first frame update
    void Start()
    {
        spellCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            spellCanvas.SetActive(true);
            gameController.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
