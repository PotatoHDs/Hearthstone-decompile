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
	// Token: 0x02001031 RID: 4145
	public class WidgetRunner : IService
	{
		// Token: 0x0600B3EA RID: 46058 RVA: 0x00375408 File Offset: 0x00373608
		private void Update()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			bool flag = false;
			int num = 0;
			this.m_widgetsPendingTick.UnionWith(this.m_widgetTemplates);
			while (Time.realtimeSinceStartup - realtimeSinceStartup < 5f)
			{
				WidgetTemplate[] array = this.m_widgetsPendingTick.ToArray<WidgetTemplate>();
				this.m_widgetsPendingTick.Clear();
				foreach (WidgetTemplate widgetTemplate in array)
				{
					if (widgetTemplate != null)
					{
						widgetTemplate.Tick();
					}
				}
				if (this.m_widgetsPendingTick.Count == 0)
				{
					flag = true;
					break;
				}
				num++;
			}
			foreach (WidgetTemplate widgetTemplate2 in this.m_widgetTemplates)
			{
				widgetTemplate2.ResetUpdateTargets();
			}
			if (Time.realtimeSinceStartup - this.m_lastDisposeCheckTime >= 1f)
			{
				this.HandleDisposeUnusedAssetHandles();
			}
			if (!flag)
			{
				Log.All.PrintWarning("Resolving widget visual states timed out at {0} seconds and {1} iterations!", new object[]
				{
					5f,
					num
				});
			}
		}

		// Token: 0x0600B3EB RID: 46059 RVA: 0x00375520 File Offset: 0x00373720
		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			this.HandleDisposeUnusedAssetHandles();
		}

		// Token: 0x0600B3EC RID: 46060 RVA: 0x00375528 File Offset: 0x00373728
		private void HandleDisposeUnusedAssetHandles()
		{
			this.m_lastDisposeCheckTime = Time.realtimeSinceStartup;
			for (int i = this.m_ownedAssetHandlePairs.Count - 1; i >= 0; i--)
			{
				ValueTuple<UnityEngine.Object, AssetHandle> valueTuple = this.m_ownedAssetHandlePairs[i];
				if (!(valueTuple.Item1 != null))
				{
					AssetHandle item = valueTuple.Item2;
					if (item != null)
					{
						item.Dispose();
					}
					this.m_ownedAssetHandlePairs.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600B3ED RID: 46061 RVA: 0x00375590 File Offset: 0x00373790
		public void RegisterWidget(WidgetTemplate widget)
		{
			this.m_widgetTemplates.Add(widget);
			this.m_widgetsPendingTick.Add(widget);
		}

		// Token: 0x0600B3EE RID: 46062 RVA: 0x003755AC File Offset: 0x003737AC
		public void UnregisterWidget(WidgetTemplate widget)
		{
			this.m_widgetTemplates.Remove(widget);
			this.m_widgetsPendingTick.Remove(widget);
		}

		// Token: 0x0600B3EF RID: 46063 RVA: 0x003755C8 File Offset: 0x003737C8
		public void RegisterAssetHandle(UnityEngine.Object owner, AssetHandle assetHandle)
		{
			this.m_ownedAssetHandlePairs.Add(new ValueTuple<UnityEngine.Object, AssetHandle>(owner, assetHandle));
		}

		// Token: 0x0600B3F0 RID: 46064 RVA: 0x003755DC File Offset: 0x003737DC
		public void UnregisterAssetHandle(UnityEngine.Object owner, AssetHandle assetHandle)
		{
			this.m_ownedAssetHandlePairs.Remove(new ValueTuple<UnityEngine.Object, AssetHandle>(owner, assetHandle));
		}

		// Token: 0x0600B3F1 RID: 46065 RVA: 0x003755F1 File Offset: 0x003737F1
		public void AddWidgetPendingTick(WidgetTemplate widget)
		{
			this.m_widgetsPendingTick.Add(widget);
		}

		// Token: 0x0600B3F2 RID: 46066 RVA: 0x00375600 File Offset: 0x00373800
		public Type[] GetDependencies()
		{
			if (Application.isPlaying)
			{
				return new Type[]
				{
					typeof(SceneMgr)
				};
			}
			return null;
		}

		// Token: 0x0600B3F3 RID: 46067 RVA: 0x0037561E File Offset: 0x0037381E
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			Processor.RegisterUpdateDelegate(new Action(this.Update));
			if (Application.isPlaying)
			{
				SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			}
			yield break;
		}

		// Token: 0x0600B3F4 RID: 46068 RVA: 0x0037562D File Offset: 0x0037382D
		public void Shutdown()
		{
			Processor.UnregisterUpdateDelegate(new Action(this.Update));
			if (Application.isPlaying)
			{
				SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			}
		}

		// Token: 0x040096AF RID: 38575
		private const float MAX_FRAME_TIME_SECONDS = 5f;

		// Token: 0x040096B0 RID: 38576
		private const float CHECK_OBJECT_DISPOSED_TIME_SECONDS = 1f;

		// Token: 0x040096B1 RID: 38577
		private HashSet<WidgetTemplate> m_widgetTemplates = new HashSet<WidgetTemplate>();

		// Token: 0x040096B2 RID: 38578
		private HashSet<WidgetTemplate> m_widgetsPendingTick = new HashSet<WidgetTemplate>();

		// Token: 0x040096B3 RID: 38579
		private List<ValueTuple<UnityEngine.Object, AssetHandle>> m_ownedAssetHandlePairs = new List<ValueTuple<UnityEngine.Object, AssetHandle>>();

		// Token: 0x040096B4 RID: 38580
		private float m_lastDisposeCheckTime;
	}
}
