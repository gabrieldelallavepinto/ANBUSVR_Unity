using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class Narrador : MonoBehaviour {

    [System.Serializable]
    public class Dialogue
    {
        public string text;
        public AudioClip audio;
    }

    public Dialogue conversationStart;
    public List<Dialogue> conversation;

    public static AudioSource audioSource;

    private int numChat = 0;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {

        //reproducimos el primer texto del narrador
        StartCoroutine(PlayDialogue(conversationStart));
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public IEnumerator DialogueAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
        yield return null;
    }

    public IEnumerator DialogueText(string text)
    {
        var narradorText = this.GetComponentInChildren<Text>();
        //si está hablando mostramos el texto
        
        narradorText.text = text;
        yield return new WaitForSeconds(3);

        //while (audioSource.isPlaying)
        //{
        //    yield return new WaitForSeconds(1);
        //}

        narradorText.text = "";

        yield return null;
    }

    public IEnumerator PlayDialogue(Dialogue dialogue)
    {
        if(dialogue.audio) StartCoroutine(DialogueAudio(dialogue.audio));
        if(dialogue.text != "") StartCoroutine(DialogueText(dialogue.text));
        yield return null;
    }

    

}
