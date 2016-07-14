using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class CardManager : MonoBehaviour {

    public GameObject characterQuad;
    public GameObject uiName;
    public GameObject uiDescription;
    public GameObject uiQuad;
    Text nameText;
    Text descriptionText;

    Texture quadTexture;
    Material quadMaterial;
    Renderer quadRenderer;
	// Use this for initialization
	void Start () {
        nameText = uiName.GetComponent<Text>();
        descriptionText = uiDescription.GetComponent<Text>();
        quadMaterial = characterQuad.GetComponent<Material>();
        quadRenderer = characterQuad.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ShowCard(string name)
    {
        uiName.SetActive(true);
        uiDescription.SetActive(true);
        uiQuad.SetActive(true);
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
        if (name.ToLower() == "raven")
        {
            nameText.text = "Raven Reyes";
        }
    }

    void SetCharacterDescription(string name)
    {
        if (name.ToLower() == "raven")
        {
            string description = "Raven was trained as a zero-g mechanic. Out of the Arkers Raven has the most powerful mind of the group. Her exit from the City of Light was a loss to be sure. \n\n" +
                                    "* Age: 19 \n" +
                                    "* Living Family: none \n" +
                                    "* Skills: Genius, Mechanic, Electronics Expert. \n" +
                                    "* Kills: -- \n It won't survive me";
            StartCoroutine(GetTexture("http://vignette4.wikia.nocookie.net/thehundred/images/2/2b/RavenS2Promo.png/revision/latest?cb=20160401040926"));
            descriptionText.text = description;
        }
        if (name.ToLower() == "clarke")
        {
            descriptionText.text = "Clarke aka Wanheda";
        }
    }

    IEnumerator GetTexture(string urlString )
    {
        using (UnityWebRequest www = UnityWebRequest.GetTexture(urlString)) 
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                quadTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                //quadMaterial.SetTexture("QuadMaterial", quadTexture);
                quadRenderer.material.mainTexture = quadTexture;
                characterQuad.SetActive(true);
                uiQuad.SetActive(false);
            }
        }
    }
}
