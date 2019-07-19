using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using MiniJSON;

public class url_info : MonoBehaviour
{
    public TMPro.TMP_InputField url_from_menu;
    public TMPro.TMP_InputField user_from_menu;
    public TMPro.TMP_InputField pass_from_menu;

    // Start is called before the first frame update
    void Start()
    {
        url_from_menu.text = PlayerPrefs.GetString("url_before_exit");
        user_from_menu.text = PlayerPrefs.GetString("user_before_exit");
        pass_from_menu.text = PlayerPrefs.GetString("pass_before_exit");
    }
    // Update is called once per frame
    void Update()
    {
        textEditor.url = url_from_menu.text;
        textEditor.user = user_from_menu.text;
        textEditor.pass = pass_from_menu.text;
        PlayerPrefs.SetString("url_before_exit", textEditor.url);
        PlayerPrefs.SetString("user_before_exit", textEditor.user);
        PlayerPrefs.SetString("pass_before_exit", textEditor.pass);
    }
}
