using System.Collections;
using System.Threading;
using LightGive.ManagerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LightGive.DebugManager
{
    public class DebugManager : SingletonMonoBehaviour<DebugManager>
	{
		[SerializeField]
		private Text showText;
		[SerializeField]
		private bool isShow = true;
		[SerializeField]
		private float updateInterval = 0.1f;
		[SerializeField]
		private int targetFrameRate = 60;
		[SerializeField]
		private int warningFrameRate = 40;
		[SerializeField]
		private int cautionFrameRate = 20;

		private int frames = 0;
		private float timeCnt = 0.0f;
		private float timeMin = 0.0f;
		private float fps = 0.0f;

		protected override void Awake()
		{
			isDontDestroy = true;
			base.Awake();
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = targetFrameRate;
		}

		private void Update()
		{
			if(isShow != showText.gameObject.activeSelf)
            {
				showText.gameObject.SetActive(isShow);
            }

			timeMin -= Time.deltaTime;
			timeCnt += Time.timeScale / Time.deltaTime;
			frames++;

			if (0 < timeMin)
			{
				return;
			}

			fps = timeCnt / frames;
			timeMin = updateInterval;
			timeCnt = 0;
			frames = 0;

			if (fps < cautionFrameRate)
			{
				showText.color = Color.red;
			}
			else if (fps < warningFrameRate)
			{
				showText.color = Color.yellow;
			}
			else
			{
				showText.color = Color.white;
			}

			showText.text = "FPS: " + fps.ToString("F2");
		}
	}
}