using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class debugText : MonoBehaviour
{
    private string testMessage;
    private string tempMessage;
    public TextMeshProUGUI scrollInfo;

    // Start is called before the first frame update
    void Start()
    {
        testMessage = (string)textEditor.scriptReading;
        tempMessage = testMessage;
    }

    // Update is called once per frame
    void Update()
    {
        string message = (string)textEditor.scriptReading;
        if (message != tempMessage)
        {
            scrollInfo.text += ("\n" + message);
            tempMessage = message;
        }



    }
}
