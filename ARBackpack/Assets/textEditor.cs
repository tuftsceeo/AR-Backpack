// Lily Zhang and Mohammed Emun
// 8/2/2019
// AR Panel display for visualizing Thingworx data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using MiniJSON;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using System.Net.Security;

//THIS CURRENTLY USES API REQUESTS 
// MQTT CURRENTLY CAUSES UNITY TO CRASH ~60 SECONDS AFTER PLAY IS CLICKED
//TO USE MQTT:
//    uncomment lines 51-69 and comment out line 71
//TO USE API REQUESTS:
//   comment out lines 51-69 and uncomment line 71

public class textEditor : MonoBehaviour
{ 
    public static string url;
    public static string user;
    public static string pass;

    private List<string> keyVals;
    private Dictionary<string, object> dict;

    private bool goodDownload;
    private string panelErrorDisplay = "some Error";

    public TextMeshProUGUI tmpText;
    private TMPro.TextMeshPro tmProh;

    private MqttClient client;
    private string broker = "iot.eclipse.org";

    public static object scriptReading = "hey world";

    // Start is called before the first frame update
    void Start()
    {
        tmProh = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        tmpText.text = "LOADING...";

        tmProh.text = tmpText.text;
        Debug.Log(tmProh.text);

        //// MQTT stuff
        //client = new MqttClient(broker);
        //// register to message received 
        //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        //string clientId = System.Guid.NewGuid().ToString();
        //goodDownload = false;
        //try
        //{
        //    client.Connect(clientId);
        //    Debug.Log("CONNECTED!!");
        //    goodDownload = true;
        
        //}
        //catch (System.Exception e)
        //{
        //    Debug.LogError("Connection error: " + e);
        //}
        //// subscribe to the topic
        //client.Subscribe(new string[] { "topic/EV3ARProject" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        StartCoroutine(GetRequest());
    }


    // Update is called once per frame
    void Update()
    {
        // DISPLAY THINGWORX VALUES IN AR PANEL
        string output = "";
        if (goodDownload)
        {
            foreach (string key in keyVals)
            {
                output = output + key + ": " + dict[key] + "\n";

            }
        }
        else
        {
            output = panelErrorDisplay;
        }
        

        tmpText.text = output;
        tmProh.text = output;

    }


    // parse data from Thingworx 
    IEnumerator GetRequest()
    {
        for (; ; )
        {
            Debug.Log("INFORMATION: ");
            Debug.Log(url);
            Debug.Log(user);
            Debug.Log(pass);

            UnityWebRequest request = new UnityWebRequest(url);
            request.SetRequestHeader("AUTHORIZATION", authenticate(user, pass));
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            goodDownload = false;
            if (request.isNetworkError)
            {
                Debug.Log("Network Error");
                panelErrorDisplay = "NETWORK ERROR (THINGWORX ISSUE)";

            }
            if (request.isHttpError)
            {
                Debug.Log("HTTP error :(");
                panelErrorDisplay = "HTTP ERROR (URL ISSUE)";
            }
            else if (!request.downloadHandler.isDone)
            {
                Debug.Log("didn't download properly");
                panelErrorDisplay = "DATA DID NOT DOWNLOAD CORRECTLY";
            }
            else
            {
                goodDownload = true;
                // Show results as text
                // Debug.Log("now trying to display downloaded info");
                Debug.Log(request.downloadHandler.text);

                // deserialize json?
                // https://gist.github.com/darktable/1411710
                var dictAll = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

                // https://stackoverflow.com/questions/22739791/parse-nested-json-with-minijson-unity3d/22745634#22745634
                // data is passed as dictionary within a list
                List<object> rows = dictAll["rows"] as List<object>;
                dict = rows[0] as Dictionary<string, object>;

                // put all property names into a list
                keyVals = new List<string>(dict.Keys);
                // remove extraneous info from list; Thingworx users can't use these as n
                keyVals.Remove("name");
                keyVals.Remove("description");
                keyVals.Remove("thingTemplate");
                keyVals.Remove("tags");

                // put all property values into another list
                Debug.Log("PRINTING KEY VALUES");
                foreach (string key1 in keyVals)
                {
                    Debug.Log(key1 + ": " + dict[key1]);
                }

            }

        }

    }

    // create authentication setRequestHeader for password-protected site
    // from https://stackoverflow.com/questions/39482954/unitywebrequest-embedding-user-password-data-for-http-basic-authentication-not
    string authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    // for MQTT connection
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // handle message received 
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message from " + e.Topic + " : " + msg);

        // deserialize json?
        // https://gist.github.com/darktable/1411710
        dict = Json.Deserialize(msg) as Dictionary<string, object>;

        // put all property names into a list
        keyVals = new List<string>(dict.Keys);

        // put all property values into another list
        Debug.Log("PRINTING KEY VALUES");
        foreach (string key1 in keyVals)
        {
            Debug.Log(key1 + ": " + dict[key1]);
        }
    }
}

