using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ListController : MonoBehaviour {

    public GameObject SideBar;

    public YoutubeVideoListCreator VideoListCreator;

    public VideoListItem[] VideoList;

    public Text CategoryTitle;

    private Dictionary<string, string> CategoryIdToTitle = new Dictionary<string, string>
    {
        { "music" , "Music" },
        { "relaxing%20nature", "Relaxing Nature" },
        { "car%20racing", "Car Racing" },
        { "comedy", "Comedy" },
        { "canadian%20news", "News" },
        { "basketball", "Basketball" },
        { "soccer", "Soccer" },
        { "hockey", "Hockey" },
        { "gaming", "Gaming" },
        { "cooking", "Cooking" }
    };

    // Use this for initialization
    void Start () {
        PanelController.OnClicked += RefreshList;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshList(PanelController panel)
    {
        CategoryTitle.text = CategoryIdToTitle[panel.categoryId];

        UnityEngine.Debug.Log("id = " + panel.categoryId);
        int i = 0;
        foreach (VideoListItem vid in VideoListCreator.CategoryVideos[panel.categoryId])
        {
            VideoList[i].Title = vid.Title;
            VideoList[i].Id = vid.Id;
            VideoList[i].ThumbnailUrl = vid.ThumbnailUrl;
            VideoList[i].VideoTitle.text = vid.Title;
            StartCoroutine(GetImage(VideoList[i]));
            i++;
        }
    }

    IEnumerator GetImage(VideoListItem listItem)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(listItem.ThumbnailUrl);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            listItem.Thumbnail.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    private void OnDestroy()
    {
        PanelController.OnClicked -= RefreshList;
    }
}
