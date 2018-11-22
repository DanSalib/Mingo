using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FtueController : MonoBehaviour {

    public GameObject FtueGameObject;
    public NavigationController NavController;
    public GameObject MainUI;
    public NavObject startPanel;
    public Image FtueImage;
    public Image FtueIndicator;
    public Button StartButton;
    private Coroutine curCoroutine = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator FadeInMainUI()
    {
        int rate = 2;
        float t = 0;

        Vector3 originalLocation = this.MainUI.transform.localPosition;
        Vector3 destination = new Vector3(originalLocation.x, originalLocation.y, 0);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.MainUI.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            yield return null;
        }

        FtueGameObject.SetActive(false);
    }

    private IEnumerator FadeOutFtue()
    {
        int rate = 2;
        float t = 0;

        Vector3 originalLocation = this.FtueGameObject.transform.localPosition;
        Vector3 destination = new Vector3(originalLocation.x, originalLocation.y,-400);

        while (t < 1)
        {
            t += Time.deltaTime * rate;

            this.FtueGameObject.transform.localPosition = Vector3.Lerp(originalLocation, destination, t);

            var c1 = StartButton.targetGraphic.color;
            StartButton.targetGraphic.color = new Color(c1.r, c1.g, c1.b, 1 - t * 0.75f);
            var c2 = FtueImage.color;
            FtueImage.color = new Color(c2.r, c2.g, c2.b, 1 - t);
            var c3 = FtueIndicator.color;
            FtueIndicator.color = new Color(c3.r, c3.g, c3.b, 1 - t);

            if(curCoroutine == null && t > 0.5)
            {
                MainUI.SetActive(true);
                curCoroutine = StartCoroutine(FadeInMainUI());
            }

            yield return null;
        }
        NavController.curPanel = startPanel;
        NavController.moveIndicator();
    }

    public void OnStartClick()
    {
        StartCoroutine(FadeOutFtue());
    }
}
