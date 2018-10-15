using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MainUIController : MonoBehaviour {

    public GameObject MediaUI;

    public GameObject ListUI;

    public delegate void PanelsLoaded(PanelController panel);
    public static event PanelsLoaded OnLoaded;

    long duration = 2000;


    // Use this for initialization
    void Start () {
        PanelController.OnClicked += ShowListUI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowListUI(PanelController panel)
    {
        StartCoroutine(MoveBarIn());
    }
    private IEnumerator MoveBarIn()
    {
        int rate = 3;
        float t = 0;

        Vector3 originalLocation = this.ListUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(375, originalLocation.y, originalLocation.z);
        
        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.ListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }
}
