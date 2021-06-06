using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;

namespace Hearthstone.UI
{
	public class WidgetRunner : IService
	{
		private const float MAX_FRAME_TIME_SECONDS = 5f;

		private const float CHECK_OBJECT_DISPOSED_TIME_SECONDS = 1f;

		private HashSet<WidgetTemplate> m_widgetTemplates = new HashSet<WidgetTemplate>();

		private HashSet<WidgetTemplate> m_widgetsPendingTick = new HashSet<WidgetTemplate>();

		private List<(UnityEngine.Object, AssetHandle)> m_ownedAssetHandlePairs = new List<(UnityEngine.Object, AssetHandle)>();

		private float m_lastDisposeCheckTime;

		private void Update()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			bool flag = false;
			int num = 0;
			m_widgetsPendingTick.UnionWith(m_widgetTemplates);
			while (Time.realtimeSinceStartup - realtimeSinceStartup < 5f)
			{
				WidgetTemplate[] array = m_widgetsPendingTick.ToArray();
				m_widgetsPendingTick.Clear();
				WidgetTemplate[] array2 = array;
				foreach (WidgetTemplate widgetTemplate in array2)
				{
					if (widgetTemplate != null)
					{
						widgetTemplate.Tick();
					}
				}
				if (m_widgetsPendingTick.Count == 0)
				{
					flag = true;
					break;
				}
				num++;
			}
			foreach (WidgetTemplate widgetTemplate2 in m_widgetTemplates)
			{
				widgetTemplate2.ResetUpdateTargets();
			}
			if (Time.realtimeSinceStartup - m_lastDisposeCheckTime >= 1f)
			{
				HandleDisposeUnusedAssetHandles();
			}
			if (!flag)
			{
				Log.All.PrintWarning("Resolving widget visual states timed out at {0} seconds and {1} iterations!", 5f, num);
			}
		}

		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			HandleDisposeUnusedAssetHandles();
		}

		private void HandleDisposeUnusedAssetHandles()
		{
			m_lastDisposeCheckTime = Time.realtimeSinceStartup;
			for (int num = m_ownedAssetHandlePairs.Count - 1; num >= 0; num--)
			{
				(UnityEngine.Object, AssetHandle) tuple = m_ownedAssetHandlePairs[num];
				if (!(tuple.Item1 != null))
				{
					tuple.Item2?.Dispose();
					m_ownedAssetHandlePairs.RemoveAt(num);
				}
			}
		}

		public void RegisterWidget(WidgetTemplate widget)
		{
			m_widgetTemplates.Add(widget);
			m_widgetsPendingTick.Add(widget);
		}

		public void UnregisterWidget(WidgetTemplate widget)
		{
			m_widgetTemplates.Remove(widget);
			m_widgetsPendingTick.Remove(widget);
		}

		public void RegisterAssetHandle(UnityEngine.Object owner, AssetHandle assetHandle)
		{
			m_ownedAssetHandlePairs.Add((owner, assetHandle));
		}

		public void UnregisterAssetHandle(UnityEngine.Object owner, AssetHandle assetHandle)
		{
			m_ownedAssetHandlePairs.Remove((owner, assetHandle));
		}

		public void AddWidgetPendingTick(WidgetTemplate widget)
		{
			m_widgetsPendingTick.Add(widget);
		}

		public Type[] GetDependencies()
		{
			if (Application.isPlaying)
			{
				return new Type[1] { typeof(SceneMgr) };
			}
			return null;
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			Processor.RegisterUpdateDelegate(Update);
			if (Application.isPlaying)
			{
				SceneMgr.Get().RegisterScenePreLoadEvent(OnPreLoadNextScene);
			}
			yield break;
		}

		public void Shutdown()
		{
			Processor.UnregisterUpdateDelegate(Update);
			if (Application.isPlaying)
			{
				SceneMgr.Get().UnregisterScenePreLoadEvent(OnPreLoadNextScene);
			}
		}
	}
}
