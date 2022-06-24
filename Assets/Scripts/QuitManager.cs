using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas1;
    [SerializeField] private GameObject canvas2;// Start is called before the first frame update
    public void Quit() {
        Application.Quit();
    }

    public void goBack() {
        canvas2.SetActive(false);
        canvas1.SetActive(true);
    }
}
