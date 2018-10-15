using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class YoutubeVideoListCreator : MonoBehaviour
{
    public delegate void LoadCategory(string categoryId);
    public static event LoadCategory OnLoad;

    private string apiKey = "AIzaSyCOu6VAoXIymLoI-5U5CWh3LFOAoVGXvIQ";
    public Dictionary<string, List<VideoListItem>> CategoryVideos = new Dictionary<string, List<VideoListItem>>
    {
        { "music" , new List<VideoListItem>() },
        { "relaxing%20nature" , new List<VideoListItem>() },
        { "news" , new List<VideoListItem>() },
        { "comedy" , new List<VideoListItem>() },
        { "automotive" , new List<VideoListItem>() },
    };

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {

        foreach (string key in CategoryVideos.Keys)
        {
            Debug.Log(key);
            UnityWebRequest www = UnityWebRequest.Get("https://www.googleapis.com/youtube/v3/search?q=" + key + "&maxResults=10&part=snippet&type=video&key=AIzaSyCOu6VAoXIymLoI-5U5CWh3LFOAoVGXvIQ");
            //UnityWebRequest www = UnityWebRequest.Get("https://www.googleapis.com/youtube/v3/videos?part=snippet&chart=mostpopular&videoCategoryId=" + key + "&maxResults=10&key=" + apiKey);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonData = "";
                jsonData = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data, 3, www.downloadHandler.data.Length - 3);  // Skip thr first 3 bytes (i.e. the UTF8 BOM)
                JSONObject json = new JSONObject(jsonData);
                // Or retrieve results as binary data
                //Debug.Log(www.downloadHandler.text);
                YoutubeResponseData responseData = JsonUtility.FromJson<YoutubeResponseData>(www.downloadHandler.text);
                foreach(var videoItem in responseData.items)
                {
                    VideoListItem video = new VideoListItem
                    {
                        Title = videoItem.snippet.title,
                        Id = videoItem.id.videoId,
                        ThumbnailUrl = videoItem.snippet.thumbnails.medium.url
                    };
                    Debug.Log(video.Title + " " + video.Id + " " + video.ThumbnailUrl);
                    CategoryVideos[key].Add(video);
                }
                if(key == "music")
                {
                    //OnLoad(key);
                }
            }
        }
    }
}
