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
    public TextMeshProUGUI tmpText;
    //public GameObject forward_line;
    //public GameObject right_line;
    //public GameObject left_line;
    //public GameObject needle_for_angle; 
    //public GameObject cone_for_distance;
    //public GameObject sphere_for_color;
    //public GameObject sphere_for_touch;
    //public GameObject angle_visual;
    //public GameObject all_data_panel;
    //public GameObject all_arrows; 
    Vector3 temp;
    Vector3 temp_2;
    Vector3 temp_3;
    Vector3 temp_4;
    Vector3 temp_5;
    private TMPro.TextMeshPro tmProh;
    //private object distReading = null;
    //private object colorReading = null;
    //private object angleReading = null;
    //private object touchReading = null;
    //private object forwardReading = null;
    //private object rightReading = null;
    //private object leftReading = null;
    //private object batteryReading = null; 
    public static object scriptReading = "hey world";
    //public SimpleHealthBar BatteryPower;
    // Dropdown variables
    //public TMPro.TMP_Dropdown select_visualization;
    //public TMPro.TMP_InputField x_input;
    //public TMPro.TMP_InputField y_input;
    //public TMPro.TMP_InputField z_input;
    //static Vector3 color_save;
    //static Vector3 ultrasonic_save;
    //static Vector3 touch_save;
    //static Vector3 gyro_save;
    //static Vector3 data_save;
    //static Vector3 movement_save;
    // END of dropdown variables


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
        //// Battery Power
        //float battery_power;
        //battery_power = float.Parse((string)batteryReading);
        //if (battery_power < 1 && battery_power > .50)
        //{
        //    BatteryPower.UpdateColor(Color.green);
        //}
        //else if (battery_power < .50 && battery_power > .25)
        //{
        //    BatteryPower.UpdateColor(Color.yellow);
        //}
        //else if (battery_power < .25 && battery_power > 0)
        //{
        //    BatteryPower.UpdateColor(Color.red);
        //}
        //BatteryPower.UpdateBar(battery_power, 1);


        //string touch_output = (string)touchReading;
        //if ((string)touchReading == "1")
        //    touch_output = "True";
        //else if ((string)touchReading == "0")
        //    touch_output = "False";

        // DISPLAY THINGWORX VALUES IN AR PANEL
        string output = "";
        foreach (string key in keyVals)
        {
            output = output + key + ": " + rowData[key] + "\n";

        }

        tmpText.text = output;
        tmProh.text = output;
        //string color = (string)colorReading;
        //Color32 brown = new Color32(114, 96, 96, 255);
        //// sets text bubble to corresponding color reading
        //switch (color)
        //{
        //    case "white":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.white;
        //        break;
        //    case "red":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.red;
        //        break;
        //    case "yellow":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.yellow;
        //        break;
        //    case "green":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.green;
        //        break;
        //    case "blue":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.blue;
        //        break;
        //    case "brown":
        //        sphere_for_color.GetComponent<Renderer>().material.color = brown;
        //        break;
        //    case "black":
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.black;
        //        break;
        //    default:
        //        sphere_for_color.GetComponent<Renderer>().material.color = Color.grey;
        //        break;
        //}
        //// This is where the cone updates
        //double size_cone = double.Parse((string)distReading);
        //// Convert to inches
        //size_cone = size_cone/3.2;
        //// Convert to cm
        //size_cone = size_cone / 2.54;
        //float size_cone_new = (float)size_cone;
        //temp = cone_for_distance.transform.localScale;
        //temp.z = size_cone_new;
        //temp.y = size_cone_new/3;
        //temp.x = size_cone_new/3;
        //cone_for_distance.transform.localScale = temp;

        //// This is where the sphere for touch updates
        //float sphere_touch = float.Parse((string)touchReading);
        //temp_2 = sphere_for_touch.transform.localScale;
        ////Debug.Log("the touch is-----" + sphere_touch);
        //temp_2.z = sphere_touch/4;
        //temp_2.y = sphere_touch/4;
        //temp_2.x = sphere_touch/4;
        //sphere_for_touch.transform.localScale = temp_2;

        //// This is where the angle is measured
        //float angle = float.Parse((string)angleReading);
        //needle_for_angle.transform.rotation = Quaternion.Euler(0, 0, angle);

        //// This is where the forward is shown
        //float line_forward = float.Parse((string)forwardReading);
        //temp_3 = forward_line.transform.localScale;
        //temp_3.z = line_forward;
        //temp_3.y = line_forward;
        //temp_3.x = line_forward;
        //forward_line.transform.localScale = temp_3;

        //// This is where the right is shown
        //float line_right = float.Parse((string)rightReading);
        //temp_4 = right_line.transform.localScale;
        //temp_4.z = line_right;
        //temp_4.y = line_right;
        //temp_4.x = line_right;
        //right_line.transform.localScale = temp_4;

        //// This is where the left is shown
        //float line_left = float.Parse((string)leftReading);
        //temp_5 = left_line.transform.localScale;
        //temp_5.z = line_left;
        //temp_5.y = line_left;
        //temp_5.x = line_left;
        //left_line.transform.localScale = temp_5;

        //// CODE FOR DROPDOWN
        //switch (select_visualization.value)
        //{
        //    case 0:
        //        Debug.Log("Color Sensor");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_color", 1);
        //            sphere_for_color.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("color_transform_x", sphere_for_color.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("color_transform_y", sphere_for_color.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("color_transform_z", sphere_for_color.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //    case 1:
        //        Debug.Log("Ultrasonic Sensor");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_cone", 1);
        //            cone_for_distance.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("cone_transform_x", cone_for_distance.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("cone_transform_y", cone_for_distance.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("cone_transform_z", cone_for_distance.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //    case 2:
        //        Debug.Log("Touch Sensor");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_touch", 1);
        //            sphere_for_touch.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("touch_transform_x", sphere_for_touch.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("touch_transform_y", sphere_for_touch.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("touch_transform_z", sphere_for_touch.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //    case 3:
        //        Debug.Log("Gyroscopic Sensor");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_gyro", 1);
        //            angle_visual.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("gyro_transform_x", angle_visual.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("gyro_transform_y", angle_visual.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("gyro_transform_z", angle_visual.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //    case 4:
        //        Debug.Log("Data Panel");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_data", 1);
        //            all_data_panel.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("data_transform_x", all_data_panel.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("data_transform_y", all_data_panel.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("data_transform_z", all_data_panel.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //    case 5:
        //        Debug.Log("Movement");
        //        if (clicked.apply == true)
        //        {
        //            PlayerPrefs.SetInt("player_prefs_arrows", 1);
        //            all_arrows.transform.localPosition = new Vector3(float.Parse(x_input.text), float.Parse(y_input.text), float.Parse(z_input.text));
        //            PlayerPrefs.SetFloat("arrows_transform_x", all_arrows.transform.localPosition.x);
        //            PlayerPrefs.SetFloat("arrows_transform_y", all_arrows.transform.localPosition.y);
        //            PlayerPrefs.SetFloat("arrows_transform_z", all_arrows.transform.localPosition.z);
        //            clicked.apply_equals_false();
        //            break;
        //        }
        //        else
        //            break;
        //}

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

            if (request.isNetworkError)
            {
                Debug.Log("Network Error");
            }
            if (request.isHttpError)
            {
                Debug.Log("HTTP error :(");
            }
            else if (!request.downloadHandler.isDone)
            {
                Debug.Log("didn't download properly");
            }
            else
            {
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

