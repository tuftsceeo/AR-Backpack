using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailSaver : MonoBehaviour
{
    public TrailRenderer trail;
    private Vector3[] trailPositions = new Vector3[200];
    private int numPositions;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            Vector3 p = trailPositions[i];
        }
        numPositions = trail.positionCount;
        Debug.Log("INITIAL NUMBER OF POSITIONS IN TRAIL IS " + numPositions);
        // int numPositions = trail.GetPositions(trailPositions);
    }

    // Update is called once per frame
    void Update()
    {
        numPositions = trail.GetPositions(trailPositions);
        Debug.Log("NOW NUMBER OF POSITIONS IN TRAIL IS " + numPositions);

    }
}
