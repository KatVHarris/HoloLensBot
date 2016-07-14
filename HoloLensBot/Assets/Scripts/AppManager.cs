using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour {

    CardManager cardManager;
    public GameObject dictationCanvas;

    string currentRequestName; 
	// Use this for initialization
	void Start () {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Shows Quad/Card and Info on Character
    public void CharacterCardRequest(string requestName)
    {
        // Erase Canvas
        //dictationCanvas.SetActive(false);
        currentRequestName = requestName;

        // DO things
        cardManager.ShowCard(requestName);
    }
}
