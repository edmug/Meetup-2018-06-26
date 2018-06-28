using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer recognizer;

    private Dictionary<string, Action> keywords;

    void Start ()
    {
        // TODO: Setup a KeywordRecognizer that let's us use voice commands to place the hoop
    }
}
