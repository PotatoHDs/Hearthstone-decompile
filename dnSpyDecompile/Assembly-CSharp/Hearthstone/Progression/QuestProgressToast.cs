using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001119 RID: 4377
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestProgressToast : MonoBehaviour
	{
		// Token: 0x0600BFC8 RID: 49096 RVA: 0x003A70DA File Offset: 0x003A52DA
		private void Awake()
		{
			this.m_toast = base.GetComponent<WidgetTemplate>();
			this.m_toast.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					this.Hide();
				}
			});
		}

		// Token: 0x0600BFC9 RID: 49097 RVA: 0x003A70FF File Offset: 0x003A52FF
		public void Initialize(QuestDataModel questDataModel)
		{
			this.m_toast.BindDataModel(questDataModel, false);
		}

		// Token: 0x0600BFCA RID: 49098 RVA: 0x003A710E File Offset: 0x003A530E
		public void Show()
		{
			if (this.m_toast == null)
			{
				return;
			}
			OverlayUI.Get().AddGameObject(base.gameObject.transform.parent.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			this.m_toast.Show();
		}

		// Token: 0x0600BFCB RID: 49099 RVA: 0x003A714C File Offset: 0x003A534C
		public void Hide()
		{
			if (this.m_toast == null)
			{
				return;
			}
			this.m_toast.Hide();
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		// Token: 0x0600BFCC RID: 49100 RVA: 0x003A717D File Offset: 0x003A537D
		public Vector3 GetOffset()
		{
			return this.MULTIPLE_TOAST_OFFSET;
		}

		// Token: 0x0600BFCD RID: 49101 RVA: 0x003A718C File Offset: 0x003A538C
		public static void DebugShowFake(QuestDataModel questDataModel)
		{
			Widget fakeToast = WidgetInstance.Create(QuestToastManager.QUEST_PROGRESS_TOAST_PREFAB, false);
			fakeToast.RegisterReadyListener(delegate(object _)
			{
				QuestProgressToast componentInChildren = fakeToast.GetComponentInChildren<QuestProgressToast>();
				componentInChildren.Initialize(questDataModel);
				OverlayUI.Get().AddGameObject(componentInChildren.transform.parent.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
				componentInChildren.Show();
			}, null, true);
		}

		// Token: 0x04009B9A RID: 39834
		private WidgetTemplate m_toast;

		// Token: 0x04009B9B RID: 39835
		public PlatformDependentVector3 MULTIPLE_TOAST_OFFSET = new PlatformDependentVector3(PlatformCategory.Screen)
		{
			PC = new Vector3(0f, 0f, 35f),
			Phone = new Vector3(0f, 0f, 35f)
		};

		// Token: 0x04009B9C RID: 39836
		private const string CODE_HIDE = "CODE_HIDE";
	}
}
