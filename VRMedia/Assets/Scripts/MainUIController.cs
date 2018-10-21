using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MainUIController : MonoBehaviour {

    public GameObject MediaUI;

    public GameObject ListUI;

    public GameObject CategoryListUI;

    public delegate void PanelsLoaded(PanelController panel);
    public static event PanelsLoaded OnLoaded;

    long duration = 2000;


    // Use this for initialization
    void Start () {
        PanelController.OnClicked += ShowListUI;
        VideoItemController.OnClicked += HideListUI;
    }

    private void OnDestroy()
    {
        PanelController.OnClicked -= ShowListUI;
        VideoItemController.OnClicked -= HideListUI;
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void ShowListUI(PanelController panel)
    {
        StartCoroutine(HideCategories());
        StartCoroutine(MoveVideoLeft());
        StartCoroutine(MoveBarIn());
    }

    public void HideListUI(VideoListItem vid)
    {
        CategoryListUI.SetActive(true);
        StartCoroutine(ShowCategories());
        StartCoroutine(MoveVideoCenter());
        StartCoroutine(MoveBarOut());
    }

    private IEnumerator MoveVideoLeft()
    {
        int rate = 3;
        float t = 0;

        Vector3 originalLocation = this.MediaUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(-280, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.MediaUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }

    private IEnumerator MoveVideoCenter()
    {
        int rate = 3;
        float t = 0;

        Vector3 originalLocation = this.MediaUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(-110, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.MediaUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }

    private IEnumerator MoveBarIn()
    {
        int rate = 3;
        float t = 0;

        Vector3 originalLocation = this.ListUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(-355, originalLocation.y, originalLocation.z);
        
        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.ListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }

    private IEnumerator HideCategories()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.CategoryListUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(812, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.CategoryListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }

        CategoryListUI.SetActive(false);
    }

    private IEnumerator ShowCategories()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.CategoryListUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(662, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.CategoryListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }

    private IEnumerator MoveBarOut()
    {
        int rate = 3;
        float t = 0;

        Vector3 originalLocation = this.ListUI.transform.localPosition;
        UnityEngine.Debug.Log(originalLocation.x);
        Vector3 destination = new Vector3(0, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.ListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);
            yield return null;
        }
    }
}
