using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

public class AutoLUISManager : MonoBehaviour {

    string requestString = "https://api.projectoxford.ai/luis/v1/application?id=a287f18f-4ae3-4346-b712-2bb9468f81c2&subscription-key=f2b59c258e5042a3b265498b92acd8a8&q=";
    public GameObject inputFieldObject;
    InputField userInput;
    string requestText = ""; 

    void Start()
    {
        //StartCoroutine(GetText());
        userInput = inputFieldObject.GetComponent<InputField>();
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.P))
        //{
        //    //send request string
        //    SetStringRequest();
        //}
    }

    public void SetStringRequest()
    {
        //parse through string for spaces
        //turn spaces into %20 
        //append to request string 
        requestText = Regex.Replace(userInput.text, @"\s+", "%20");
        Debug.Log(requestText);
        StartCoroutine(GetText());

    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(requestString+requestText);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}



