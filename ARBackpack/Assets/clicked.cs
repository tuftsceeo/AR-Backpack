using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicked : MonoBehaviour
{
    public static bool apply; 

    public void change_apply()
    {
        apply = !apply;
    }
    public static void apply_equals_false()
    {
        apply = false; 
    }
    
}
