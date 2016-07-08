using UnityEngine;
using System.Collections;

public class LUISAppManager : MonoBehaviour {
    public CardManager cardManager; 
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CharacterCardRequest(string Character)
    {
        cardManager.ShowCard(Character);
    }
}
