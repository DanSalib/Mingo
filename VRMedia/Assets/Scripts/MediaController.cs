using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaController : MonoBehaviour {

    public SimplePlayback SimplePlayback;

	// Use this for initialization
	void Start () {
        VideoItemController.OnClicked += OnVideoClick;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnVideoClick(VideoListItem video)
    {
        SimplePlayback.Play_Pause();
        SimplePlayback.videoId = video.Id;
        SimplePlayback.PlayYoutubeVideo(video.Id);
    }

    private string ParseVideoId(string url)
    {
        UnityEngine.Debug.Log(url.Substring(url.Length - 11, 11));

        return url.Substring(url.Length - 11, 11);
    }

    private void OnDestroy()
    {
        VideoItemController.OnClicked -= OnVideoClick;
    }
}
