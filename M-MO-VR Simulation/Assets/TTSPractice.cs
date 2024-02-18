using Facebook.WitAi.TTS.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TTSPractice : MonoBehaviour
{
    [SerializeField]
    private TTSSpeaker ttsSpeaker;

    [Header("UI")]
    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Button speakButton;

    [SerializeField]
    private Button stopButton;

    // Start is called before the first frame update
    private void Awake()
    {
        ttsSpeaker.Events.OnClipLoadBegin.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadBegin: {s}"));
        ttsSpeaker.Events.OnClipLoadSuccess.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadSuccess: {s}"));
        ttsSpeaker.Events.OnStartSpeaking.AddListener((_ttsSpeaker, s) => Debug.Log($"OnStartSpeaking: {s}"));
        ttsSpeaker.Events.OnFinishedSpeaking.AddListener((_ttsSpeaker, s) => Debug.Log($"OnFinishedSpeaking: {s}"));
        ttsSpeaker.Events.OnCancelledSpeaking.AddListener((_ttsSpeaker, s) => Debug.Log($"OnCancelledSpeaking: {s}"));
        
        stopButton.onClick.AddListener(() =>
        {
            stopButton.interactable = false;
            ttsSpeaker.Stop();
        });

        speakButton.onClick.AddListener(() =>
        {
            stopButton.interactable = true;
            Speak();
        });
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Speak();
        }
    }

    public void Speak()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            Debug.LogWarning("Input field is empty");
            return;
        }

        ttsSpeaker.Speak(inputField.text);
    }
}
