using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000622 RID: 1570
[RequireComponent(typeof(WidgetTemplate))]
public class DoneChangingEventHandler : MonoBehaviour
{
	// Token: 0x0600580F RID: 22543 RVA: 0x001CCACA File Offset: 0x001CACCA
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_WAIT_FOR_DONE_CHANGING")
			{
				this.ShowWhenDone();
				return;
			}
			if (!(eventName == "CODE_DISMISS_FROM_FOREGROUND"))
			{
				return;
			}
			this.DismissFromForeground();
		});
	}

	// Token: 0x06005810 RID: 22544 RVA: 0x001CCAEF File Offset: 0x001CACEF
	private void ShowWhenDone()
	{
		if (this.m_isWaiting)
		{
			return;
		}
		this.m_isWaiting = true;
		if (this.m_isInvisibleWhileChanging)
		{
			this.m_widget.Hide();
		}
		this.m_widget.RegisterDoneChangingStatesListener(delegate(object payload)
		{
			this.m_widget.TriggerEvent("CODE_DONE_CHANGING", new Widget.TriggerEventParameters
			{
				NoDownwardPropagation = true
			});
			if (this.m_isInvisibleWhileChanging)
			{
				this.m_widget.Show();
			}
			if (this.m_useBackgroundBlur)
			{
				UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.Standard);
			}
			this.m_isWaiting = false;
		}, null, true, true);
	}

	// Token: 0x06005811 RID: 22545 RVA: 0x001CCB2E File Offset: 0x001CAD2E
	private void DismissFromForeground()
	{
		if (this.m_useBackgroundBlur)
		{
			UIContext.GetRoot().DismissPopup(base.gameObject);
		}
	}

	// Token: 0x04004B87 RID: 19335
	public const string WAIT_FOR_DONE_CHANGING = "CODE_WAIT_FOR_DONE_CHANGING";

	// Token: 0x04004B88 RID: 19336
	public const string DISMISS_FROM_FOREGROUND = "CODE_DISMISS_FROM_FOREGROUND";

	// Token: 0x04004B89 RID: 19337
	public const string FINISHED_CHANGING = "CODE_DONE_CHANGING";

	// Token: 0x04004B8A RID: 19338
	[Tooltip("Hide this widget visually until it is done changing states.")]
	[SerializeField]
	private bool m_isInvisibleWhileChanging = true;

	// Token: 0x04004B8B RID: 19339
	[Tooltip("Blur the background behind this widget.")]
	[SerializeField]
	private bool m_useBackgroundBlur = true;

	// Token: 0x04004B8C RID: 19340
	private WidgetTemplate m_widget;

	// Token: 0x04004B8D RID: 19341
	private bool m_isWaiting;
}
