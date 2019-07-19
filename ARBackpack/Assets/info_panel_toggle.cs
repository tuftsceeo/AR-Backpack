using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_panel_toggle : MonoBehaviour
{
	public GameObject infoPanel;
    public GameObject debugPanel;
    bool toggle;
 
	public void show_info_panel()
	{
		toggle = !toggle;
        debugPanel.SetActive(!toggle);
        infoPanel.SetActive(toggle);
        //debugPanel.SetActive(toggle);
}
}
