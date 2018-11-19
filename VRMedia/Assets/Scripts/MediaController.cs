using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;


public class MediaController : MonoBehaviour {

    public SimplePlayback SimplePlayback;
    public GameObject loadingImage;
    public GameObject loadingIndicator;
    public AudioSource videoPlayer;
    private Coroutine curCoroutine;

    private int qSamples = 1024;  // array size
    private float refValue = 0.1f; // RMS value for 0 dB
    private float rmsValue = 0;   // sound level - RMS
    private float dbValue = 0;    // sound level - dB
    private float volume = 2; // set how much the scale will vary
    private float[] samples; // audio samples'

    private bool videoReady = false;

    // Use this for initialization
    void Start () {
        VideoItemController.OnClicked += OnVideoClick;
        samples = new float[qSamples];
        SimplePlayback.OnReady += VideoPrepared;

    }

    private void VideoPrepared()
    {
        videoReady = true;
    }

    private void Update()
    {
        if(videoReady && loadingImage.activeInHierarchy)
        {
            GetVolume();
            if (dbValue > -160)
            {
                VideoReady();
            }
        }
    }

    private void GetVolume()
    {
        videoPlayer.GetOutputData(samples, 0); // fill array with samples
        float sum = 0;
        for (int i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
    }

    private void VideoReady()
    {
        StopCoroutine(curCoroutine);
        loadingImage.SetActive(false);
        videoReady = false;
    }

    public void OnVideoClick(VideoListItem video)
    {
        if (video != null && video.Id != string.Empty)
        {
            SimplePlayback.Play_Pause();
            SimplePlayback.videoId = video.Id;
            SimplePlayback.PlayYoutubeVideo(video.Id);
            if (curCoroutine != null)
            {
                StopCoroutine(curCoroutine);
            }
            curCoroutine = StartCoroutine(VideoLoading());
        }
    }

    private IEnumerator VideoLoading()
    {
        loadingImage.SetActive(true);
        loadingIndicator.SetActive(true);
        while (true)
        {
            loadingIndicator.transform.Rotate(Vector3.forward,-2f);
            yield return null;
        }
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
