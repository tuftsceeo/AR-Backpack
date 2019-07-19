using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info_panel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    bool state;

    public void panel_for_info()
    {
        state = !state;
        panel.SetActive(state);
    }
}
