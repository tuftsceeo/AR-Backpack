using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateStick : MonoBehaviour
{
    public GameObject stick;
    private float angleRotate;

    // Start is called before the first frame update
    void Start()
    {
        angleRotate = 2;
    }

    // Update is called once per frame
    void Update()
    {
        stick.transform.Rotate(Vector3.forward, angleRotate);
        foreach (Transform child in transform)
        {
            child.gameObject.transform.Rotate(Vector3.forward, angleRotate);
            Debug.Log("child found");
        }

    }
}
