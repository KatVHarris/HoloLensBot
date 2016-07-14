using HoloToolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// This keeps track of the various parts of the recording and text display process.
/// </summary>

[RequireComponent(typeof(AudioSource), typeof(MicrophoneManager), typeof(KeywordManager))]
public class Communicator : MonoBehaviour
{
    // This enumeration gives the manager two different ways to handle how to send messages to LUIS. Both will
    // interact with LUIS. The first causes automatic sending to LUIS on timeout.
    // The second allows the dictation phrase to be manually sent.
    public enum DictationToLUISType { OnPause, OnClickSend };

    [Tooltip("Determines whether automatic or manual select will ping LUIS")]
    public DictationToLUISType DictationTypeLUIS;

    AutoLUISManager LUISController; 

    [Tooltip("The button to be selected when the user wants to record audio and dictation.")]
    public GameObject RecordButtonObject;
    [Tooltip("The button to be selected when the user wants to stop recording.")]
    public GameObject RecordStopButtonObject;
    [Tooltip("The button to be selected when the user wants to play audio.")]
    public GameObject PlayButtonObject;
    [Tooltip("The button to be selected when the user wants to stop playing.")]
    public GameObject PlayStopButtonObject;

    private Button RecordButton;
    private Button RecordStopButton;
    private Button PlayButton;
    private Button PlayStopButton;

    bool recording = false;
    bool playing = false; 

    [Tooltip("The sound to be played when the recording session starts.")]
    public AudioClip StartListeningSound;
    [Tooltip("The sound to be played when the recording session ends.")]
    public AudioClip StopListeningSound;

    [Tooltip("The icon to be displayed while recording is happening.")]
    public GameObject MicIcon;

    [Tooltip("A message to help the user understand what to do next.")]
    public Renderer MessageUIRenderer;

    //[Tooltip("The waveform animation to be played while the microphone is recording.")]
    //public Transform Waveform;
    //[Tooltip("The meter animation to be played while the microphone is recording.")]
    //public MovieTexturePlayer SoundMeter;

    private AudioSource dictationAudio;
    private AudioSource startAudio;
    private AudioSource stopAudio;

    private float origLocalScale;
    private bool animateWaveform;

    public enum Message
    {
        PressMic,
        PressStop,
        SendMessage
    };

    private MicrophoneManager microphoneManager;

    void Start()
    {
        RecordButton = RecordButtonObject.GetComponent<Button>();
        RecordStopButton = RecordStopButtonObject.GetComponent<Button>(); 
        PlayButton = PlayButtonObject.GetComponent<Button>(); 
        PlayStopButton = PlayStopButtonObject.GetComponent<Button>();

        dictationAudio = gameObject.GetComponent<AudioSource>();

        startAudio = gameObject.AddComponent<AudioSource>();
        stopAudio = gameObject.AddComponent<AudioSource>();

        startAudio.playOnAwake = false;
        startAudio.clip = StartListeningSound;
        stopAudio.playOnAwake = false;
        stopAudio.clip = StopListeningSound;

        microphoneManager = GetComponent<MicrophoneManager>();

        LUISController = GameObject.Find("LUISManager").GetComponent<AutoLUISManager>();

        //origLocalScale = Waveform.localScale.y;
        //animateWaveform = false;
    }

    void Update()
    {
        //Need to implement waveform animation

        // If the audio has stopped playing and the PlayStop button is still active,  reset the UI.
        if (!dictationAudio.isPlaying && PlayStopButton.enabled)
        {
            PlayStop();
        }
    }

    public void Record()
    {
        if (!recording)
        {
            // Turn the microphone on, which returns the recorded audio.
            dictationAudio.clip = microphoneManager.StartRecording();

            // Set proper UI state and play a sound.
            SetUI(true, Message.PressStop, startAudio);

            //STOP the PhraseRecognizer!!

            RecordButton.gameObject.SetActive(false);
            RecordStopButton.gameObject.SetActive(true);
            recording = true; 
        }
    }

    public void RecordStop()
    {
        if (recording)
        {
            // Turn off the microphone.
            microphoneManager.StopRecording();
            // Restart the PhraseRecognitionSystem and KeywordRecognizer
            microphoneManager.StartCoroutine("RestartSpeechSystem", GetComponent<KeywordManager>());

            if(DictationTypeLUIS == DictationToLUISType.OnPause)
            {
                SendToLuis();
            }

            // Set proper UI state and play a sound.
            SetUI(false, Message.SendMessage, stopAudio);

            PlayButtonObject.SetActive(true);
            RecordStopButtonObject.SetActive(false);
            RecordButtonObject.SetActive(true);
            recording = false; 
        }
    }

    public void Play()
    {
        if (!playing)
        {
            PlayButton.gameObject.SetActive(false);
            PlayStopButton.gameObject.SetActive(true);

            dictationAudio.Play();
            playing = true;
        }
    }

    public void PlayStop()
    {
        if (playing)
        {
            PlayStopButton.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(true);

            dictationAudio.Stop();

            playing = false; 
        }
    }

    public void SendCommunicatorMessage()
    {
        AstronautWatch.Instance.CloseCommunicator();
    }

    void ResetAfterTimeout()
    {
        // Set proper UI state and play a sound.
        SetUI(false, Message.PressMic, stopAudio);

        RecordStopButtonObject.SetActive(false);
        RecordButtonObject.SetActive(true);
    }

    private void SetUI(bool enabled, Message newMessage, AudioSource soundToPlay)
    {
        //animateWaveform = enabled;
        //SoundMeter.gameObject.SetActive(enabled);
        MicIcon.SetActive(enabled);

        //FStartCoroutine(ChangeLabel(newMessage));

        soundToPlay.Play();
    }

    private IEnumerator ChangeLabel(Message newMessage)
    {
        switch (newMessage)
        {
            case Message.PressMic:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex01", Mathf.Lerp(1.0f, 0.0f, i));
                    yield return null;
                }
                break;
            case Message.PressStop:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex01", Mathf.Lerp(0.0f, 1.0f, i));
                    yield return null;
                }
                break;
            case Message.SendMessage:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex02", Mathf.Lerp(0.0f, 1.0f, i));
                    yield return null;
                }
                break;
        }
    }

    void SendToLuis()
    {
        string luisRequest = microphoneManager.GetDictation();
        LUISController.setDictationRequest(luisRequest);
        
    }
}
