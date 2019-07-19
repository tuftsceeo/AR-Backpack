using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class pause_screen : MonoBehaviour
{
    bool freezeToggle;
    // Start is called before the first frame update
    void Start()
    {
        VuforiaRenderer.Instance.Pause(false);
    }

    // Update is called once per frame
    public void Freeze()
    {
        freezeToggle = !freezeToggle;
        VuforiaRenderer.Instance.Pause(freezeToggle);

    }
}