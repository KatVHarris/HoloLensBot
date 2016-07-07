using UnityEngine;
using System.Collections;

public class BubbleBehavior : MonoBehaviour {
    public GameObject bubbleSystemObject;
    public GameObject Sphere; 

    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.G))
        {
            //PopBubble(this.gameObject);
        }
	}


    public void PopBubble(GameObject go)
    {
        if(this.gameObject == go)
        {
            GameObject bubbleParent = go.transform.parent.gameObject;
            bubbleParent.GetComponent<ParticleSystem>().Play();     
            Destroy(go);
        }



    }
}
