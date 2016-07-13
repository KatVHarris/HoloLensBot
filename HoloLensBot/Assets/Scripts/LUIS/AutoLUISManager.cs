using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

public class AutoLUISManager : MonoBehaviour {

    string requestString = "https://api.projectoxford.ai/luis/v1/application?id=a287f18f-4ae3-4346-b712-2bb9468f81c2&subscription-key=f2b59c258e5042a3b265498b92acd8a8&q=";
    public GameObject inputFieldObject;
    InputField userInput;
    
    string requestText = "";
    string luisValue = "";
    JObject luisReturnQuery;
    GameObject appManager; 

    void Start()
    {
        appManager = GameObject.Find("AppManager");
        userInput = inputFieldObject.GetComponent<InputField>();
    }

    void Update()
    {

    }

    public void SetStringRequest()
    {
        //parse through string for spaces
        //turn spaces into %20 
        //append to request string 
        requestText = Regex.Replace(userInput.text, @"\s+", "%20");
        StartCoroutine(GetText());

    }

    public void setDictationRequest(string dictationString)
    {
        requestText = Regex.Replace(dictationString, @"\s+", "%20");
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
            luisValue = www.downloadHandler.text;
            luisReturnQuery = JObject.Parse(luisValue);
            string luisIntent = (luisReturnQuery.SelectToken("intents[0].intent").ToString()); //the accurate intent
            IntentParser(luisIntent);

        }
    }

    void IntentParser(string intentName)
    {
        switch (intentName)
        {
            case "NameQuery":
                //CallCardManager with Character Name?
                string characterName = (luisReturnQuery.SelectToken("entities[0].entity").ToString());
                //mainLuisAppManager.CharacterCardRequest(characterName);
                Debug.Log("worked");
                break;
            default:
                break; 

        }
    }
}

/*
 * {
  "query": "tell me about clarke",
  "intents": [
    {
      "intent": "NameQuery",
      "score": 0.994736,
      "actions": [
        {
          "triggered": true,
          "name": "NameQuery",
          "parameters": [
            {
              "name": "XName",
              "required": true,
              "value": [
                {
                  "entity": "clarke",
                  "type": "Character",
                  "score": 0.9995919
                }
              ]
            }
          ]
        }
      ]
    },
    {
      "intent": "None",
      "score": 0.261976063
    },
    {
      "intent": "LocateX",
      "score": 0.0114280116,
      "actions": [
        {
          "triggered": true,
          "name": "LocateX",
          "parameters": []
        }
      ]
    },
    {
      "intent": "SortCharacter",
      "score": 0.007171331,
      "actions": [
        {
          "triggered": true,
          "name": "SortCharacter",
          "parameters": [
            {
              "name": "XName",
              "required": false,
              "value": [
                {
                  "entity": "clarke",
                  "type": "Character",
                  "score": 0.9995919
                }
              ]
            }
          ]
        }
      ]
    },
    {
      "intent": "JoinCOL",
      "score": 0.00281716418
    },
    {
      "intent": "Greeting",
      "score": 0.00267843017,
      "actions": [
        {
          "triggered": true,
          "name": "Greeting",
          "parameters": []
        }
      ]
    },
    {
      "intent": "CoreCommand",
      "score": 1.72579857E-05
    },
    {
      "intent": "Upgrade",
      "score": 1.57786126E-05
    },
    {
      "intent": "Help",
      "score": 8.302972E-14
    }
  ],
  "entities": [
    {
      "entity": "clarke",
      "type": "Character",
      "startIndex": 14,
      "endIndex": 19,
      "score": 0.9995919
    }
  ]
}

*/

