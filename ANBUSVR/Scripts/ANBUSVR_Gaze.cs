using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANBUSVR_Gaze : MonoBehaviour {

    public ANBUSVR_API.Gaze gaze;

    //player
    private static GameObject centerEyeAnchor;

    //tiempoInicio
    public double timeStart = 0.0;
    //private bool doTime = false;

    #region variables fixation

    //tiempo en el que se considera fijacion para empezar
    public static double gazeTimeStart = 0.1;
    
    //distancia maxima
    private static float gazeMaxDistance = 5;
    
    //numero de secuencia
    private static int sequence = 1;
    
    //item fijado
    private GameObject gazeItem;

    #endregion

    private void Awake()
    {
        //definimos un nuevo item
        gaze = new ANBUSVR_API.Gaze();

        //obtenemos la camara del player
        centerEyeAnchor = GameObject.Find("CenterEyeAnchor").gameObject;

    }


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //tiempo
        TimeCount();

        //activamos la coorrutina para analizar los objetos
        StartCoroutine(gazes());
    }


    public void TimeCount()
    {
        timeStart += Time.deltaTime;
    }


    private IEnumerator gazes()
    {
        RaycastHit hit;

        //dirección
        Vector3 direction = centerEyeAnchor.transform.TransformDirection(Vector3.forward) * gazeMaxDistance;
        Vector3 origen = centerEyeAnchor.transform.position;

        //dibujamos ray
        Debug.DrawRay(origen, direction, Color.red);

        //si colisiona con un objeto
        if (Physics.Raycast(origen, direction, out hit))
        {
            if (hit.collider != false)
            {
                //obtenemos el objeto que estamos mirando
                GameObject hitItem = hit.collider.gameObject;

                //si no hay un item fijado
                if (!gazeItem)
                {
                    //obtenemos el componente AnalyticsItem
                    var ANBUSVR_item = hitItem.GetComponent<ANBUSVR_Item>();
                    //var analyticsRespondent = gameObject.GetComponent<AnalyticsRespondent>();
                    //var analyticsScene = gameObject.GetComponent<AnalyticsScene>();
                    
                    //Si es un objeto para analizar
                    if (ANBUSVR_item == true)
                    {
                        Debug.Log("se ha encontrado un item para analizar");

                        //if (analyticsRespondent.respondent._id == "") StartCoroutine(AnalyticsAPI.PostRespondent(analyticsRespondent));
                        //if (analyticsScene.scene._id == "") StartCoroutine(AnalyticsAPI.PostScene(analyticsScene));
                        //if (analyticsItem.item._id == "") StartCoroutine(AnalyticsAPI.PostItem(analyticsItem));

                        //actualizamos los datos de la nueva fijacion
                        //gaze.respondent_id = analyticsRespondent.respondent._id;
                        //gaze.scene_id = analyticsScene.scene._id;
                        
                        //actualizamos el item
                        gazeItem = hitItem;

                        gaze.item_id = ANBUSVR_item.item.id;
                        
                        gaze.sequence = sequence++;
                        gaze.timeStart = timeStart;
                        gaze.timeEnd = timeStart;

                        var ANBUSVR_participant = GameObject.Find("ANBUSVR").GetComponent<ANBUSVR_Participant>();
                        gaze.participant_id = ANBUSVR_participant.participant.id;
                    }
                }
                else
                {
                    //si estamos mirando al mismo item
                    if (gazeItem != hitItem)
                    {

                        //si la duracion de la fijacion es mayor que el umbral
                        
                        Debug.Log("Se termina la fijacion y la guardamos");

                        gaze.timeEnd = timeStart;

                        //guardamos el gaze
                        var ANBUSVR = GameObject.Find("ANBUSVR");
                        var ANBUSVR_api = ANBUSVR.GetComponent<ANBUSVR_API>();
                        StartCoroutine(ANBUSVR_api.PostGaze(this));
                        
                        //reiniciamos el item
                        gazeItem = null;
                    }
                }
            }
        }
        yield return null;
    }
}
