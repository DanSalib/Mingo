/*****************************************************************************
Copyright © 2015 SDKBOX.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*****************************************************************************/

using UnityEngine;
using UnityEngine.Events;
using Sdkbox;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace Sdkbox
{
	[Serializable]
	public class GoogleAnalytics : PluginBase<GoogleAnalytics>
	{
		public string iOSTrackingCode;
		public string AndroidTrackingCode;

		protected string buildConfiguration()
		{
			Json config = newJsonObject();
			Json cur;

			cur = config;
			cur["ios"] = newJsonObject(); cur = cur["ios"];
			cur["GoogleAnalytics"] = newJsonObject(); cur = cur["GoogleAnalytics"];
			cur["trackingCode"] = new Json(iOSTrackingCode);

			cur = config;
			cur["android"] = newJsonObject(); cur = cur["android"];
			cur["GoogleAnalytics"] = newJsonObject(); cur = cur["GoogleAnalytics"];
			cur["trackingCode"] = new Json(AndroidTrackingCode);

			return config.dump();
		}

		protected override void init()
		{
			Debug.Log("SDKBOX GoogleAnalytics starting.");

			SDKBOX.Instance.init(); // reference the SDKBOX singleton to ensure shared init.

			#if !UNITY_EDITOR
			string config = buildConfiguration();
			Debug.Log("configuration: " + config);

			#if UNITY_ANDROID
			GoogleAnalytics._player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				// call GoogleAnalytics::init()
				sdkbox_googleanalytics_init(config);
				Debug.Log("SDKBOX GoogleAnalytics Initialized.");
			}));
			#else
			// call GoogleAnalytics::init()
			sdkbox_googleanalytics_init(config);
			Debug.Log("SDKBOX GoogleAnalytics Initialized.");
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * The analytics session is being explicitly started at plugin initialization time.
         */
        public void startSession()
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_startSession();}));
			#else
			sdkbox_googleanalytics_startSession();
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * You normally will never stop a session manually.
         */
        public void stopSession()
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_stopSession();}));
			#else
			sdkbox_googleanalytics_stopSession();
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Manually request dispatch of hits. By default, data is dispatched from the
         * Google Analytics SDK for Android every 5 minutes.
         */
        public void dispatchHits()
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_dispatchHits();}));
			#else
			sdkbox_googleanalytics_dispatchHits();
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Change the dispatch info time period to the desired amount of seconds.
         */
        public void dispatchPeriodically( int seconds )
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_dispatchPeriodically(seconds);}));
			#else
			sdkbox_googleanalytics_dispatchPeriodically(seconds);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Stop periodically sending info. Then manually the <code>dispatchPeridically</code>
         * or <code>dispatchHits</code> should be called.
         */
        public void stopPeriodicalDispatch()
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_stopPeriodicalDispatch();}));
			#else
			sdkbox_googleanalytics_stopPeriodicalDispatch();
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Set user ID for this tracking session
         */
        public void setUser(string userID)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_setUser(userID);}));
			#else
			sdkbox_googleanalytics_setUser(userID);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Set value to custom dimension
         */
        public void setDimension(int index, string value)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_setDimension(index, value);}));
			#else
			sdkbox_googleanalytics_setDimension(index, value);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Set value to custom metric
         */
        public void setMetric(int index, string value)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_setMetric(index, value);}));
			#else
			sdkbox_googleanalytics_setMetric(index, value);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Log screen info. title is the title of a screen. Screens are logical units
         * inside your app you'd like to identify at analytics panel.
         */
        public void logScreen(string title)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_logScreen(title);}));
			#else
			sdkbox_googleanalytics_logScreen(title);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * logEvent("Achievement", "Unlocked", "Slay 10 dragons", 5);
         */
        public void logEvent(string eventCategory, string eventAction, string eventLabel, int value)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_logEvent(eventCategory, eventAction, eventLabel, value);}));
			#else
			sdkbox_googleanalytics_logEvent(eventCategory, eventAction, eventLabel, value);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Log an exception. It is a basic support for in-app events.
         */
        public void logException(string exceptionDescription, bool isFatal)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_logException(exceptionDescription, isFatal);}));
			#else
			sdkbox_googleanalytics_logException(exceptionDescription, isFatal);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Measure a time inside the application.
         */
        public void logTiming(string timingCategory, int timingInterval, string timingName, string timingLabel)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_logTiming(timingCategory, timingInterval, timingName, timingLabel);}));
			#else
			sdkbox_googleanalytics_logTiming(timingCategory, timingInterval, timingName, timingLabel);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Log a social event.
         */
        public void logSocial(string socialNetwork, string socialAction, string socialTarget)
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_logSocial(socialNetwork, socialAction, socialTarget);}));
			#else
			sdkbox_googleanalytics_logSocial(socialNetwork, socialAction, socialTarget);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * While running on dry run, the tracked events won't be sent to the actual
         * analytics account.
         */
        public void setDryRun( bool enable )
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_setDryRun(enable);}));
			#else
			sdkbox_googleanalytics_setDryRun(enable);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Enable advertising tracking when in google's ad vendors.
         */
        public void enableAdvertisingTracking( bool enable )
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_enableAdvertisingTracking(enable);}));
			#else
			sdkbox_googleanalytics_enableAdvertisingTracking(enable);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Create a tracker identified by the google analytics tracker id XX-YYYYYYYY-Z.
         * If the tracker already existed, no new tracker will be created. In any case, the
         * tracker associated with tracker id will be set as default tracker for  analytics
         * operations.
         */
        public void createTracker( string trackerId )
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_createTracker(trackerId);}));
			#else
			sdkbox_googleanalytics_createTracker(trackerId);
			#endif
			#endif // !UNITY_EDITOR
		}

        /*!
         * Enable a tracker identified by a trackerId. If the tracker does not exist,
         * nothing will happen.
         */
        public void enableTracker( string trackerId )
		{
			#if !UNITY_EDITOR
            _lazy_init();
			#if UNITY_ANDROID
			AndroidJavaObject activity = GoogleAnalytics._player.GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {sdkbox_googleanalytics_enableTracker(trackerId);}));
			#else
			sdkbox_googleanalytics_enableTracker(trackerId);
			#endif
			#endif // !UNITY_EDITOR
		}

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_init(string config);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_startSession();

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_stopSession();

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_dispatchHits();

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_dispatchPeriodically(int seconds);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_stopPeriodicalDispatch();

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_setUser(string userID);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_setDimension(int index, string value);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_setMetric(int index, string value);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_logScreen(string title);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_logEvent(string eventCategory, string eventAction, string eventLabel, int value);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_logException(string exceptionDescription, bool isFatal);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_logTiming(string timingCategory, int timingInterval, string timingName, string timingLabel);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_logSocial(string socialNetwork, string socialAction, string socialTarget);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_setDryRun(bool enable);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_enableAdvertisingTracking(bool enable);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_createTracker(string trackerId);

		#if UNITY_IOS
		[DllImport("__Internal")]
		#else
		[DllImport("googleanalytics")]
		#endif
		public static extern void sdkbox_googleanalytics_enableTracker(string trackerId);
	}
}
