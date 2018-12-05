using UnityEngine;
using System.Collections;
using Sdkbox;

public class SampleScript : MonoBehaviour
{
	void Start ()
	{
		Sdkbox.GoogleAnalytics ga = FindObjectOfType<Sdkbox.GoogleAnalytics>();
		if (ga != null)
		{
			ga.startSession();
		}
	}
}
