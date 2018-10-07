using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaController : MonoBehaviour {

    public SimplePlayback SimplePlayback;

	// Use this for initialization
	void Start () {
        PanelController.OnClicked += OnPanelClick;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnPanelClick(PanelController panel)
    {
        SimplePlayback.Play_Pause();
        SimplePlayback.PlayYoutubeVideo(panel.url);
    }

    private string ParseVideoId(string url)
    {
        UnityEngine.Debug.Log(url.Substring(url.Length - 11, 11));

        return url.Substring(url.Length - 11, 11);
    }
}
