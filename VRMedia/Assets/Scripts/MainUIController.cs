using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class MainUIController : MonoBehaviour {

    public GameObject MediaUI;

    public GameObject VideoListUI;

    public GameObject CategoryUI;

    public ListController ListController;

    public NavigationController NavController;

    public delegate void PanelsLoaded(PanelController panel);
    public static event PanelsLoaded OnLoaded;

    public Stopwatch SleepTimer = null;

    long duration = 2000;

    public bool disableButtons = false;


    // Use this for initialization
    void Start ()
    {
        PanelController.OnClicked += ShowListUI;
        VideoItemController.OnClicked += HideListUI;
        NavigationController.OnKeyPress += ActivateUI;
    }

    private void OnDestroy()
    {
        PanelController.OnClicked -= ShowListUI;
        VideoItemController.OnClicked -= HideListUI;
        NavigationController.OnKeyPress -= ActivateUI;
    }
    // Update is called once per frame
    void Update () {
		if(SleepTimer?.ElapsedMilliseconds > 3500f)
        {
            NavController.indicator.SetActive(false);
            StartCoroutine(CenterVideo());
            StartCoroutine(FadeCategories());
            SleepTimer.Stop();
            SleepTimer.Reset();
        }
	}

    public void ActivateUI(KeyCode key)
    {
        if(CategoryUI.activeInHierarchy)
        {
            SleepTimer?.Reset();
            SleepTimer?.Start();
            NavController.indicator.SetActive(true);
            StartCoroutine(MoveVidToTop());
            StartCoroutine(UnfadeCategories());
        }
    }

    public void ShowListUI(PanelController panel)
    {
        if(SleepTimer == null)
        {
            SleepTimer = new Stopwatch();
        }
        SleepTimer?.Stop();
        SleepTimer?.Reset();
        disableButtons = true;
        NavController.curPanel = ListController.VideoList[0].gameObject.GetComponent<NavObject>();
        VideoListUI.SetActive(true);
        StartCoroutine(ShowVideos());
        StartCoroutine(HideCategories());
    }

    public void HideListUI(VideoListItem vid)
    {
        disableButtons = true;
        NavController.prevPanel = null;
        NavController.curPanel = ListController.curCategoryPanel.gameObject.GetComponent<NavObject>();
        StartCoroutine(HideVideos());
        if(vid != null)
        {
            StartCoroutine(ShowFadedCategories());
            StartCoroutine(CenterVideo());
        }
        else
        {
            StartCoroutine(ShowCategories());
        }

    }

    private IEnumerator CenterVideo()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.MediaUI.transform.localPosition;
        Button[] buttons = this.MediaUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(originalLocation.x, -160f, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.MediaUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
        disableButtons = false;
        NavController.indicator.SetActive(false);
    }

    private IEnumerator MoveVidToTop()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.MediaUI.transform.localPosition;
        Button[] buttons = this.MediaUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(originalLocation.x, 0, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.MediaUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
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

            foreach (var button in buttons)
            {
                var c = button.targetGraphic.color;
                button.targetGraphic.color = new Color(c.r, c.g, c.b, 1 - t);
            }

            yield return null;
        }
        CategoryUI.SetActive(false);
        NavController.moveIndicator();
        NavController.indicator.transform.localScale = new Vector3(1.95f, 2.4f);
        disableButtons = false;
    }

    private IEnumerator HideVideos()
    {
        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.VideoListUI.transform.localPosition;
        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        Vector3 destination = new Vector3(1800, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.VideoListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
        VideoListUI.SetActive(false);
        NavController.moveIndicator();
        NavController.indicator.transform.localScale = new Vector3(1.5f, 1.5f);
    }

    private IEnumerator ShowVideos()
    {
        var curPos = NavController.viewPort.transform.localPosition;
        NavController.viewPort.transform.localPosition = new Vector3(curPos.x, 24.6f, curPos.z);

        int rate = 4;
        float t = 0;

        Vector3 originalLocation = this.VideoListUI.transform.localPosition;
        Vector3 destination = new Vector3(650, originalLocation.y, originalLocation.z);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.VideoListUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }
        NavController.indicator.SetActive(true);
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
        NavController.indicator.SetActive(true);
        disableButtons = false;
    }

    private IEnumerator UnfadeCategories()
    {
        CategoryUI.SetActive(true);

        int rate = 4;
        float t = 0;

        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();

        while (t < 0.75)
        {
            t += Time.deltaTime * rate;

            foreach (var button in buttons)
            {
                var c = button.targetGraphic.color;
                button.targetGraphic.color = new Color(c.r, c.g, c.b, c.a + t);
            }
            yield return null;
        }
    }

    private IEnumerator ShowFadedCategories()
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
                button.targetGraphic.color = new Color(c.r, c.g, c.b, t*0.25f);
            }
            yield return null;
        }
        NavController.indicator.SetActive(true);
        disableButtons = false;
    }

    private IEnumerator FadeCategories()
    {
        CategoryUI.SetActive(true);

        int rate = 4;
        float t = 0;

        Button[] buttons = this.CategoryUI.GetComponentsInChildren<Button>();
        while (t < 1)
        {
            t += Time.deltaTime * rate;

            foreach (var button in buttons)
            {
                var c = button.targetGraphic.color;
                button.targetGraphic.color = new Color(c.r, c.g, c.b, t * 0.25f);
            }
            yield return null;
        }
    }
}
