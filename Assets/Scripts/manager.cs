using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Option to quit the game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }  
    }
}
