// Lily Zhang and Mohammed Emun
// 7/19/2019
// AR Panel display for visualizing Thingworx data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using MiniJSON;

public class textEditor : MonoBehaviour
{
    public static string url;
    public static string user;
    public static string pass;

    private List<string> keyVals;
    private Dictionary<string, object> rowData;
    private bool goodDownload;
    private string panelErrorDisplay;

    public TextMeshProUGUI tmpText;
    private TMPro.TextMeshPro tmProh;

    public static object scriptReading = "hey world";

    // Start is called before the first frame update
    void Start()
    {
        tmProh = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        Debug.Log("here comes the incoming text");
        tmpText.text = "LOADING...";

        tmProh.text = tmpText.text;
        Debug.Log(tmProh.text);

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
                output = output + key + ": " + rowData[key] + "\n";

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
                var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

                // https://stackoverflow.com/questions/22739791/parse-nested-json-with-minijson-unity3d/22745634#22745634
                // data is passed as dictionary within a list
                List<object> rows = dict["rows"] as List<object>;
                rowData = rows[0] as Dictionary<string, object>;

                // put all property names into a list
                keyVals = new List<string>(rowData.Keys);
                // remove extraneous info from list; Thingworx users can't use these as n
                keyVals.Remove("name");
                keyVals.Remove("description");
                keyVals.Remove("thingTemplate");
                keyVals.Remove("tags");

                // put all property values into another list
                Debug.Log("PRINTING KEY VALUES");
                foreach (string key1 in keyVals)
                {
                    Debug.Log(key1 + ": " + rowData[key1]);
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
}

