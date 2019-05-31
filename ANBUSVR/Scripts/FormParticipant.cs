using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class FormParticipant : MonoBehaviour
{

    public Narrador.Dialogue dialogueRec;
    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(InputAge());
    }

    public IEnumerator InputAge()
    {
        var age = GameObject.Find("FormParticipant/Edad/Slider").GetComponent<Slider>().value;
        GameObject.Find("FormParticipant/Edad/Text").GetComponent<Text>().text = age.ToString();
        yield return null;

    }

    public void RecName()
    {
        StartCoroutine(RecNameCorrutine());
    }

    public IEnumerator RecNameCorrutine()
    {
        var narrador = GameObject.Find("Narrador").GetComponent<Narrador>();

        StartCoroutine(narrador.PlayDialogue(dialogueRec));

        var ANBUSVR_microphone = GameObject.Find("Microphone").GetComponent<ANBUSVR_Microphone>();

        //inicializamos el micro
        ANBUSVR_microphone.StartMicrophone();

        yield return new WaitForSeconds(4);

        //obtenemos el input Name
        GameObject.Find("FormParticipant/Name/Text").GetComponent<Text>().text = ANBUSVR_microphone.voiceText;

        yield return null;

    }
   

    public void PostForm()
    {
        //obtenemos los datos del participante
        var ANBUSVR = GameObject.Find("ANBUSVR");
        var ANBUSVR_api = ANBUSVR.GetComponent<ANBUSVR_API>();
        var ANBUSVR_participant = ANBUSVR.GetComponent<ANBUSVR_Participant>();

        ANBUSVR_participant.participant.name = GameObject.Find("FormParticipant/Name/Text").GetComponent<Text>().text;
        ANBUSVR_participant.participant.gender = "";
        ANBUSVR_participant.participant.age = (int)GameObject.Find("FormParticipant/Edad/Slider").GetComponent<Slider>().value;

        foreach (var toggle in GameObject.Find("FormParticipant/Sexo").GetComponent<ToggleGroup>().ActiveToggles())
        {
            if (toggle.isOn)
            {
                ANBUSVR_participant.participant.gender = toggle.GetComponentInChildren<Text>().text;
            }
        }

        switch (ANBUSVR_participant.participant.gender)
        {
            case "hombre":case "Hombre":
                ANBUSVR_participant.participant.gender = "male";
                break;
            case "mujer":case "Mujer":
                ANBUSVR_participant.participant.gender = "female";
                break;
        }
     
        //activamos la coorrutina para analizar los objetos
        StartCoroutine(ANBUSVR_api.PostParticipant(ANBUSVR_participant));

        StartCoroutine(exitForm());
    }

    public IEnumerator exitForm()
    {
        //esperamos 3 segundos antes de terminar la escena
        yield return new WaitForSeconds(3);

        //terminamos la escena
        SceneManager.LoadScene("Analytics");

        yield return null;
    }


}
