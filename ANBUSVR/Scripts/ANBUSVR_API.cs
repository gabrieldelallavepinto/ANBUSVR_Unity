using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;


public class ANBUSVR_API : MonoBehaviour
{   

    //private static GameObject _isCreated;

    //url de la api
    public static string urlBase = "http://localhost/ANBUSVR/public/api";

    //Proyecto
    public string token_key;

    private static GameObject instance = null;

    private void Awake()
    {

        //si es la escena principal lo guardamos, si no se destruye porque ya existe
        if (instance == null)
        {
            instance = this.gameObject;
            Debug.Log("guardamos ANBUSVR");

            //el objeto se mantiene en las siguientes escenas
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            if (instance != this.gameObject)
            {
                Debug.Log("destruimos la copia de ANBUSVR");
                Destroy(this.gameObject);
                return;
            }
        }
    }


    #region project
    [System.Serializable]
    public class Project
    {
        public string id = "";
        public string name = "";
        public string token_key = "";
    }

    public IEnumerator GetProject(ANBUSVR_Project ANBUSVR_project)
    {
        string url = urlBase + "/project/" + token_key;
        Debug.Log(url);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Project: " + webRequest.downloadHandler.text);

                if (webRequest.downloadHandler.text == "")
                {
                    #if UNITY_EDITOR
                        EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                }

                ANBUSVR_project.project = JsonUtility.FromJson<Project>(webRequest.downloadHandler.text);
                
                
                
            }
        }
    }

    #endregion

    #region item
    [System.Serializable]
    public class Item
    {
        public string id = "";
        public string project_id = "";
        public string name = "";
    }

    public IEnumerator PostItem(ANBUSVR_Item ANBUSVR_item)
    {
        var ANBUSVR_project = GetComponent<ANBUSVR_Project>();

        while (ANBUSVR_project.project.id == "")
        {
            yield return new WaitForSeconds(1f);
        }

        string url = urlBase + "/item";
        WWWForm form = new WWWForm();
        form.AddField("project_id", ANBUSVR_project.project.id);
        form.AddField("name", ANBUSVR_item.item.name);

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("El item " + ANBUSVR_item.item.name + " se ha creado correctamente " + www.downloadHandler.text);
            ANBUSVR_item.item = JsonUtility.FromJson<Item>(www.downloadHandler.text);
        }
    }

    #endregion

    #region gaze
    [System.Serializable]
    public class Gaze
    {
        public string id = "";
        public string project_id = "";
        public string participant_id = "";
        public string item_id = "";
        public int sequence = 0;
        public double timeStart = 0.0;
        public double timeEnd = 0.0;
    }

    public IEnumerator PostGaze(ANBUSVR_Gaze ANBUSVR_gaze)
    {
        var ANBUSVR_project = GetComponent<ANBUSVR_Project>();
        var ANBUSVR_participant = GetComponent<ANBUSVR_Participant>();

        while (ANBUSVR_project.project.id == "")
        {
            yield return new WaitForSeconds(1f);
        }

        string url = urlBase + "/gaze";
        WWWForm form = new WWWForm();

        //'project_id', 'participant_id', 'item_id', 'sequence', 'timeStart', 'timeEnd'
        form.AddField("project_id", ANBUSVR_project.project.id);
        form.AddField("participant_id", ANBUSVR_participant.participant.id);
        form.AddField("item_id", ANBUSVR_gaze.gaze.item_id);
        form.AddField("sequence", ANBUSVR_gaze.gaze.sequence);
        form.AddField("timeStart", ANBUSVR_gaze.gaze.timeStart.ToString());
        form.AddField("timeEnd", ANBUSVR_gaze.gaze.timeEnd.ToString());


        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("La fijacion se ha creado con exito");
            Debug.Log(www.downloadHandler.text);
            ANBUSVR_gaze.gaze = JsonUtility.FromJson<Gaze>(www.downloadHandler.text);

        }
    }
    #endregion

    #region grab
    [System.Serializable]
    public class Grab
    {
        public string id = "";
        public string project_id = "";
        public string participant_id = "";
        public string item_id = "";
        public int sequence = 0;
        public double timeStart = 0.0;
        public double timeEnd = 0.0;
    }

    public IEnumerator PostGrab(ANBUSVR_Grab ANBUSVR_grab)
    {
        var ANBUSVR_project = GetComponent<ANBUSVR_Project>();
        var ANBUSVR_participant = GetComponent<ANBUSVR_Participant>();

        while (ANBUSVR_project.project.id == "")
        {
            yield return new WaitForSeconds(1f);
        }

        string url = urlBase + "/grab";
        WWWForm form = new WWWForm();

        //'project_id', 'participant_id', 'item_id', 'sequence', 'timeStart', 'timeEnd'
        form.AddField("project_id", ANBUSVR_project.project.id);
        form.AddField("participant_id", ANBUSVR_participant.participant.id);
        form.AddField("item_id", ANBUSVR_grab.grab.item_id);
        form.AddField("sequence", ANBUSVR_grab.grab.sequence);
        form.AddField("timeStart", ANBUSVR_grab.grab.timeStart.ToString());
        form.AddField("timeEnd", ANBUSVR_grab.grab.timeEnd.ToString());

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("El agarre se ha creado con exito");
            Debug.Log(www.downloadHandler.text);
            ANBUSVR_grab.grab = JsonUtility.FromJson<Grab>(www.downloadHandler.text);

        }
    }
    #endregion

    #region participant
    [System.Serializable]
    public class Participant
    {
        public string id = "";
        public string project_id = "";
        public string name = "";
        public int age = 18;
        public string gender = "";
    }

    public IEnumerator PostParticipant(ANBUSVR_Participant ANBUSVR_participant)
    {
        var ANBUSVR_project = GetComponent<ANBUSVR_Project>();

        while (ANBUSVR_project.project.id == "")
        {
            yield return new WaitForSeconds(1f);
        }

        string url = urlBase + "/participant";
        WWWForm form = new WWWForm();

        //'project_id', 'participant_id', 'item_id', 'sequence', 'timeStart', 'timeEnd'
        form.AddField("project_id", ANBUSVR_project.project.id);
        form.AddField("name", ANBUSVR_participant.participant.name);
        form.AddField("age", ANBUSVR_participant.participant.age.ToString());
        form.AddField("gender", ANBUSVR_participant.participant.gender);

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("El participante se ha creado con exito");
            Debug.Log(www.downloadHandler.text);
            ANBUSVR_participant.participant = JsonUtility.FromJson<Participant>(www.downloadHandler.text);
        }
    }

    #endregion



}
