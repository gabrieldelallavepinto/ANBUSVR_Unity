using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANBUSVR_Grab : MonoBehaviour {

    public ANBUSVR_API.Grab grab;

    public enum Hand{Left, Right};
    public Hand hand;

    public static OVRGrabber grabberLeft;
    public static OVRGrabber grabberRight;

    #region variables grab

    //tiempoInicio
    public double timeStart = 0.0;

    //numero de secuencia
    private static int sequence = 1;
    
    //item fijado
    private OVRGrabbable grabItem;

    #endregion

    private void Awake()
    {
        //definimos un nuevo item
        grab = new ANBUSVR_API.Grab();

        grabberLeft = GameObject.Find("AvatarGrabberLeft").gameObject.GetComponent<OVRGrabber>();
        grabberRight = GameObject.Find("AvatarGrabberRight").gameObject.GetComponent<OVRGrabber>();

    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        //tiempo
        TimeCount();

        //activamos la coorrutina para analizar los objetos
        StartCoroutine(grabbs());
    }


    public void TimeCount()
    {
        timeStart += Time.deltaTime;
    }


    private IEnumerator grabbs()
    {

        OVRGrabbable ovr_grabble = null;
        switch (hand)
        {
            case Hand.Left: ovr_grabble = grabberLeft.grabbedObject; break;
            case Hand.Right: ovr_grabble = grabberRight.grabbedObject; break;
        }

        //para la mano
        if (!grabItem)
        {

            if (ovr_grabble)
            {
                var ANBUSVR_item = ovr_grabble.GetComponent<ANBUSVR_Item>();
                if (ANBUSVR_item)
                {
                    grabItem = ovr_grabble;

                    grab.item_id = ANBUSVR_item.item.id;
                    grab.sequence = sequence++;
                    grab.timeStart = timeStart;
                    grab.timeEnd = timeStart;

                    var ANBUSVR_participant = GameObject.Find("ANBUSVR").GetComponent<ANBUSVR_Participant>();
                    grab.participant_id = ANBUSVR_participant.participant.id;
                }
            }
            
        }
        else
        {

            if(grabItem != ovr_grabble)
            {
                Debug.Log("Se termina el agarre del objeto");
                grab.timeEnd = timeStart;

                //guardamos el grab
                var ANBUSVR = GameObject.Find("ANBUSVR");
                var ANBUSVR_api = ANBUSVR.GetComponent<ANBUSVR_API>();
                StartCoroutine(ANBUSVR_api.PostGrab(this));
               

                grabItem = null;
            }
        }
        
        yield return null;
    }
}
