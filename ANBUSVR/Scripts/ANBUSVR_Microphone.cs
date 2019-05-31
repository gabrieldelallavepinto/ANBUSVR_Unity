using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class ANBUSVR_Microphone : MonoBehaviour {

    private DictationRecognizer dictationRecognizer;
    public string voiceText = "";

    // Use this for initialization
    void Start () {
        //inicializamos el microfono
        InitializeMicrophone();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeMicrophone()
    {
        //funciones para el microfono
        dictationRecognizer = new DictationRecognizer();
        //dictationRecognizer.InitialSilenceTimeoutSeconds = 5;  //este es el tiempo de comienzo
        dictationRecognizer.AutoSilenceTimeoutSeconds = 5;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;
    }
    public void StartMicrophone()
    {
        //activamos el microfono 
        dictationRecognizer.Start();
    }

    public void StopMicrophone()
    {
        //desactivamos el microfono 
        dictationRecognizer.Stop();
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)   //aqui va todo lo que se maneje con resultado de voz... osea lo convertido a "texto"
    {
        voiceText = text;
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {

    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)  //aqui va cuando ya todo haya acabado.. osea cuando dejes de hablar y pase el tiempo de reconocimiento
    {
        Debug.Log("micro apagado");
        //dictationRecognizer.Stop();
    }

    private void DictationRecognizer_DictationError(string error, int hresult)
    {

    }

}
