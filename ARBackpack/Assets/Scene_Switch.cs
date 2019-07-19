using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // https://loekvandenouweland.com/content/use-unity-button-to-change-between-scenes.html

    public void GotoMainScene()
    {
        SceneManager.LoadScene("thoughtBubble");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
