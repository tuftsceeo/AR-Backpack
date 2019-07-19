using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPanel : MonoBehaviour
{
    public GameObject panel;
     
    public void openPane()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
