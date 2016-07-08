using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardManager : MonoBehaviour {

    public GameObject characterQuad;
    public GameObject uiName;
    public GameObject uiDescription;
    Text nameText;
    Text descriptionText;
	// Use this for initialization
	void Start () {
        nameText = uiName.GetComponent<Text>();
        descriptionText = uiDescription.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ShowCard(string name)
    {
        uiName.SetActive(true);
        uiDescription.SetActive(true);
        characterQuad.SetActive(true);
        SetQuadImage(name);
        SetCharacterName(name);
        SetCharacterDescription(name);
    }

    void DisableCard()
    {

    }

    void SetQuadImage(string name)
    {

    }

    void SetCharacterName(string name)
    {
        if(name.ToLower() == "clarke")
        {
            nameText.text = "Clarke Griffin";
        }
    }

    void SetCharacterDescription(string name)
    {
        if (name.ToLower() == "clarke")
        {
            descriptionText.text = "Clarke aka Wanheda";
        }
    }
}
