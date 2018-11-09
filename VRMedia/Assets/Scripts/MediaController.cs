using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaController : MonoBehaviour {

    public SimplePlayback SimplePlayback;
    public Image loadingImage;

	// Use this for initialization
	void Start () {
        VideoItemController.OnClicked += OnVideoClick;
        SimplePlayback.OnReady += VideoReady;
    }

    // Update is called once per frame
    void Update () {

    }

    private void VideoReady()
    {
        StopCoroutine(VideoLoading());
        loadingImage.gameObject.SetActive(false);
    }

    public void OnVideoClick(VideoListItem video)
    {
        if (video != null && video.Id != string.Empty)
        {
            StartCoroutine(VideoLoading());
            SimplePlayback.Play_Pause();
            SimplePlayback.videoId = video.Id;
            SimplePlayback.PlayYoutubeVideo(video.Id);
        }
    }

    private IEnumerator VideoLoading()
    {
        loadingImage.gameObject.SetActive(true);
        yield return null;
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
