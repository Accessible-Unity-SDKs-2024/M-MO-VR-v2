using Facebook.WitAi.TTS.Utilities;
using UnityEngine;

namespace Assets
{
    public class VisionReader
    {
        private bool speakerEnabled = true;

        private TTSSpeaker ttsSpeaker;

        private VisionObject _currentObjectInfo = null, _nextObjectInfo = null;

        private bool _isSpeaking = false;

        /// <summary>
        /// Constructor for VisionReader
        /// </summary>
        /// <param name="speaker"></param>
        public VisionReader(TTSSpeaker speaker)
        {
            ttsSpeaker = speaker;

            // Debug log for speaker events
            ttsSpeaker.Events.OnClipLoadBegin.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadBegin: {s}"));
            ttsSpeaker.Events.OnClipLoadSuccess.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadSuccess: {s}"));
            ttsSpeaker.Events.OnStartSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = true;
                Debug.Log($"OnStartSpeaking: {s}");
            });
            ttsSpeaker.Events.OnFinishedSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = false;
                Debug.Log($"OnFinishedSpeaking: {s}");
            });
            ttsSpeaker.Events.OnCancelledSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = false;
                Debug.Log($"OnCancelledSpeaking: {s}");
            });
        }

        /// <summary>
        /// Constructor for VisionReader
        /// </summary>
        /// <param name="speaker"></param>
        /// <param name="speakerEnabled"></param>
        public VisionReader(TTSSpeaker speaker, bool speakerEnabled)
        {
            ttsSpeaker = speaker;
            this.speakerEnabled = speakerEnabled;

            // Debug log for speaker events
            ttsSpeaker.Events.OnClipLoadBegin.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadBegin: {s}"));
            ttsSpeaker.Events.OnClipLoadSuccess.AddListener((_ttsSpeaker, s) => Debug.Log($"OnClipLoadSuccess: {s}"));
            ttsSpeaker.Events.OnStartSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = true;
                Debug.Log($"OnStartSpeaking: {s}");
            });
            ttsSpeaker.Events.OnFinishedSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = false;
                Debug.Log($"OnFinishedSpeaking: {s}");
            });
            ttsSpeaker.Events.OnCancelledSpeaking.AddListener((_ttsSpeaker, s) =>
            {
                _isSpeaking = false;
                Debug.Log($"OnCancelledSpeaking: {s}");
            });
        }

        /// <summary>
        /// Speak the name and description of the object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="interactable"></param>
        public void Speak(string name, string description, bool interactable = false)
        {
            if (string.IsNullOrEmpty(name)) return;
            _currentObjectInfo = new VisionObject(name, description, interactable);

            if (!speakerEnabled) return;
            
            if (_isSpeaking) ttsSpeaker.Stop();

            string speakText = "This is a " + name + ". ";
            if (!string.IsNullOrEmpty(description)) speakText += description;

            if (interactable) speakText += "This object can be interacted with.";
            // else speakText += "This object cannot be interacted with.";

            ttsSpeaker.Speak(speakText);
        }

        /// <summary>
        /// Set the speaker to enabled or disabled
        /// </summary>
        /// <param name="enabled"></param>
        public void SetSpeakerEnabled(bool enabled)
        {
            speakerEnabled = enabled;
        }

        /// <summary>
        /// Check if the speaker is enabled
        /// </summary>
        /// <returns></returns>
        public bool IsSpeakerEnabled()
        {
            return speakerEnabled;
        }

        class VisionObject
        {
            public string name;
            public string description;
            public bool interactable;

            public VisionObject()
            {
                name = "";
                description = "";
                interactable = false;
            }

            public VisionObject(string name, string description, bool interactable)
            {
                this.name = name;
                this.description = description;
                this.interactable = interactable;
            }
        }
    }
}