using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UsernameInput : MonoBehaviour
{
    public string userName;

    private GameObject inputFieldGameObject;
    private InputField inputField;

    public void Start()
    {
        inputFieldGameObject = GameObject.Find("InputField");
        // Debug.Log(inputFieldGameObject);
        inputField = inputFieldGameObject.GetComponent<InputField>();
        // Debug.Log(inputField);
        userName = PlayerPrefs.GetString("playerName", "Player");
        inputField.text = userName;
    }
    public void SetUserName(string text)
    {
        userName = text;
        PlayerPrefs.SetString("playerName", userName);
    }


}
