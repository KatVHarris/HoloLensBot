using HoloToolkit;
using UnityEngine;
using System.Collections;

public class ALIEController : MonoBehaviour {

    public GameObject DictationController;
    private KeywordManager keywordManager;

    void Start()
    {
        keywordManager = DictationController.GetComponent<KeywordManager>();
    }

    void GazeEntered()
    {
        keywordManager.StartKeywordRecognizer();
        
    }

    void GazeExited()
    {

        keywordManager.StopKeywordRecognizer();
    }
}
