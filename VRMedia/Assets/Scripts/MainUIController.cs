﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class MainUIController : MonoBehaviour {

    public GameObject MediaUI;

    public GameObject VideoListUI;

    public GameObject CategoryUI;

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
        VideoListUI.SetActive(true);
        StartCoroutine(ShowVideos());
        StartCoroutine(HideCategories());
    }

    public void HideListUI(VideoListItem vid)
    {
        StartCoroutine(ShowCategories());
        StartCoroutine(HideVideos());
    }
   

    private IEnumerator HideCategories()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.CategoryUI.transform.localPosition;
        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(-820, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

           this.CategoryUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            foreach(var button in buttons)
            {
                var c = button.targetGraphic.color;
                button.targetGraphic.color = new Color(c.r, c.g, c.b, 1-t);
            }

            yield return null;
        }
        CategoryUI.SetActive(false);
    }

    private IEnumerator HideVideos()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.VideoListUI.transform.localPosition;
        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(1540, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.VideoListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
        VideoListUI.SetActive(false);
    }

    private IEnumerator ShowVideos()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.VideoListUI.transform.localPosition;
        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(625, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.VideoListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
    }

    private IEnumerator ShowCategories()
    {
        CategoryUI.SetActive(true);

        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.CategoryUI.transform.localPosition;
        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(-4, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

           this.CategoryUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            foreach (var button in buttons)
            {
                var c = button.targetGraphic.color;
                button.targetGraphic.color = new Color(c.r, c.g, c.b, t);
            }
            yield return null;
        }
    }
}