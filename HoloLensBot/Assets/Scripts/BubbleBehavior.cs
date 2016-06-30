using UnityEngine;
using System.Collections;

public class BubbleBehavior : MonoBehaviour {
    public GameObject bubbleSystemObject;
    private ParticleSystem bubbleSystem;
    public GameObject Sphere; 

    // Use this for initialization
	void Start () {
        bubbleSystem = bubbleSystemObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.G))
        {
            PopBubble();
        }
	}

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            PopBubble();
        }
    }

    public void PopBubble()
    {
        Destroy(Sphere);
        
        bubbleSystem.Play();
    }
}
