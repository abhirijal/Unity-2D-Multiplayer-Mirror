using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas1;// Start is called before the first frame update
    [SerializeField] private GameObject canvas2;
    
   public void ChangeScene() {
        
        Debug.Log("function running!");
        canvas2.SetActive(true);
        canvas1.SetActive(false);
    }

    public void ChangeLevel() {
        SceneManager.LoadScene("LEVELONE");
    }

}
