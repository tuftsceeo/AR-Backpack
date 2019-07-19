using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helloPanel : MonoBehaviour
{
    public GameObject thisPanel;
    bool state;

    void Start()
    {
        state = true;
    }
    public void showPanel()
    {
        state = !state;
        thisPanel.SetActive(state);
    }
}
