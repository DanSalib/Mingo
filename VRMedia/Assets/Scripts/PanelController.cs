using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    public delegate void PanelClick(PanelController panel);
    public static event PanelClick OnClicked;
    public string url;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPanelClick()
    {
        OnClicked(this);
    }
}
