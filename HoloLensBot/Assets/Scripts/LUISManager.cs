using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LUISManager : MonoBehaviour {

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.projectoxford.ai/luis/v1/application?id=a287f18f-4ae3-4346-b712-2bb9468f81c2&subscription-key=f2b59c258e5042a3b265498b92acd8a8&q=tell%20me%20about%20Clarke");
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



