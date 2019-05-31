using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ANBUSVR_End : MonoBehaviour
{

    public bool isEnd = false;

    private GameObject leftHand;
    private GameObject rightHand;

    // Use this for initialization
    void Start()
    {
        //obtenemos los controles izquierdos y derechos
        leftHand = GameObject.Find("LeftHandAnchor");
        rightHand = GameObject.Find("RightHandAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        //si se coge un objeto
        var rGrabbable = rightHand.GetComponent<OVRGrabber>().grabbedObject;
        var lGrabbable = leftHand.GetComponent<OVRGrabber>().grabbedObject;

        if ((rGrabbable != null || lGrabbable != null ) && isEnd == false)
        {
            StartCoroutine(EndScene());
        }
        
    }

    IEnumerator EndScene()
    {
        isEnd = true;

        //obtenemos las escenas
        Scenes scenes = GameObject.Find("StaticScene").GetComponent<Scenes>();

        //mensaje de voz del narrador

        //esperamos 10 segundos
        yield return new WaitForSeconds(5);

        //si hay escenas disponibles las carga, si no hemos terminado
        if (scenes.scenes.Count > 0)
        {
            isEnd = false;
            scenes.nextscene();
        }
        else
        {
            Debug.Log("se han terminado las escenas");
            SceneManager.LoadScene("End");
        }
    }
}