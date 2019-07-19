using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeObject : MonoBehaviour
{
    public GameObject AR_panel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting stabilizer code");

    }

    // Update is called once per frame
    void Update()
    {
        AR_panel.transform.LookAt(Camera.main.transform.position, Vector3.up);

    }

}
