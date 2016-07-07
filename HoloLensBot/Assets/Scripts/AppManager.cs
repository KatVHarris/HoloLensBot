using UnityEngine;
using System.Collections;

public class AppManager : MonoBehaviour {
    int totalbubbles = 0;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(totalbubbles== 4)
        {
            //SHOW UI
        }
    }

    void OnSelect()
    {
        string FocuseObjectName = GazeGestureManager.Instance.FocusedObject.name;
        switch (FocuseObjectName)
        {
            case "Sphere":
                GameObject go = GazeGestureManager.Instance.FocusedObject;
                go.GetComponent<BubbleBehavior>().PopBubble(go);
                totalbubbles++; 
                break;
            default:
                break; 
        }
    }
}
