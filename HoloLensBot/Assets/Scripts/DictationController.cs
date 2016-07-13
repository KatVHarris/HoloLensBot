using HoloToolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// Bassed of the HoloAcademy Communicator. This keeps track of the various parts of the recording and text display process.
/// </summary>

[RequireComponent(typeof(AudioSource), typeof(MicrophoneManager), typeof(KeywordManager))]
public class DictationController : MonoBehaviour {

    [Tooltip("The sound to be played when the recording session starts.")]
    public AudioClip StartListeningSound;
    [Tooltip("The sound to be played when the recording session ends.")]
    public AudioClip StopListeningSound;

    [Tooltip("The icon to be displayed while recording is happening.")]
    public GameObject MicIcon;

    [Tooltip("A message to help the user understand what to do next.")]
    public Renderer MessageUIRenderer;

    [Tooltip("The waveform animation to be played while the microphone is recording.")]
    public Transform Waveform;

    public GameObject startRecordingObject;
    public GameObject stopRecordingObject;

    private AutoLUISManager autoLUISManager; 

    public Button startRecordingButton;
    public Button stopRecordingButton; 


    private AudioSource dictationAudio;
    private AudioSource startAudio;
    private AudioSource stopAudio;

    private float origLocalScale;
    //private bool animateWaveform;
    private bool recording = false; 

    public enum Message
    {
        StartRec, //PressMic,
        StopRec, //PressStop,
        SendMessage
    };

    private MicrophoneManager microphoneManager;

    void Start()
    {
        dictationAudio = gameObject.GetComponent<AudioSource>();

        startAudio = gameObject.AddComponent<AudioSource>();
        stopAudio = gameObject.AddComponent<AudioSource>();

        startAudio.playOnAwake = false;
        startAudio.clip = StartListeningSound;
        stopAudio.playOnAwake = false;
        stopAudio.clip = StopListeningSound;

        microphoneManager = GetComponent<MicrophoneManager>();

        startRecordingButton = startRecordingObject.GetComponent<Button>();
        stopRecordingButton = stopRecordingObject.GetComponent<Button>();

        autoLUISManager = GameObject.Find("LUISManager").GetComponent<AutoLUISManager>();

        //origLocalScale = Waveform.localScale.y;
        //animateWaveform = false;
    }

    void Update()
    {
        // If the audio has stopped playing and the PlayStop button is still active,  reset the UI.
        if (!dictationAudio.isPlaying )
        {
            //PlayStop();
        }
    }

    public void Record()
    {
        if (!recording)
        {
            recording = true; 
            // Turn the microphone on, which returns the recorded audio.
            dictationAudio.clip = microphoneManager.StartRecording();

            startRecordingObject.SetActive(false);
            stopRecordingObject.SetActive(true);
        }
    }

    public void RecordStop()
    {
        if (recording)
        {

            // Turn off the microphone.
            microphoneManager.StopRecording();
            SendMessage();

            // Restart the PhraseRecognitionSystem and KeywordRecognizer
            microphoneManager.StartCoroutine("RestartSpeechSystem", GetComponent<KeywordManager>());


            startRecordingObject.SetActive(true);
            stopRecordingObject.SetActive(false);

        }
    }

    public void SendMessage()
    {
        //SEND TO LUIS -- time out
        string completedString = microphoneManager.getCompletedString();
        autoLUISManager.setDictationRequest(completedString);
        
    }

    void ResetAfterTimeout()
    {
        //Send Message to LUIS Controller
        stopRecordingObject.gameObject.SetActive(false);
        startRecordingObject.gameObject.SetActive(true);
    }

}
