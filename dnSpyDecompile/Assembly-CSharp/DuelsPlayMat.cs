using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200062A RID: 1578
public class DuelsPlayMat : MonoBehaviour
{
	// Token: 0x06005851 RID: 22609 RVA: 0x001CD948 File Offset: 0x001CBB48
	public void Start()
	{
		this.m_livesReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnLivesWidgetReady));
		this.m_vaultReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnVaultWidgetReady));
		this.m_vaultLeverReference.RegisterReadyListener<Clickable>(new Action<Clickable>(this.OnLeverClickableReady));
		this.m_vaultDoorOpenedListeners = new List<Action>();
		this.m_vaultDoorClickedListeners = new List<Action>();
	}

	// Token: 0x06005852 RID: 22610 RVA: 0x001CD9B0 File Offset: 0x001CBBB0
	public bool IsReady()
	{
		return this.m_livesWidgetLoaded && this.m_vaultWidgetLoaded;
	}

	// Token: 0x06005853 RID: 22611 RVA: 0x001CD9C2 File Offset: 0x001CBBC2
	public void SetLeverButtonEnabled(bool enabled)
	{
		this.m_leverButton.enabled = enabled;
		if (enabled)
		{
			this.m_leverButton.GetComponent<VisualController>().SetState(DuelsConfig.LEVER_GLOW_STATE);
		}
	}

	// Token: 0x06005854 RID: 22612 RVA: 0x001CD9E9 File Offset: 0x001CBBE9
	public void RegisterVaultDoorOpenedListener(Action a)
	{
		if (this.m_vaultDoorOpenedListeners.Contains(a))
		{
			return;
		}
		this.m_vaultDoorOpenedListeners.Add(a);
	}

	// Token: 0x06005855 RID: 22613 RVA: 0x001CDA06 File Offset: 0x001CBC06
	public void RemoveVaultDoorOpenedListener(Action a)
	{
		this.m_vaultDoorOpenedListeners.Remove(a);
	}

	// Token: 0x06005856 RID: 22614 RVA: 0x001CDA18 File Offset: 0x001CBC18
	public void OnVaultDoorOpened()
	{
		for (int i = 0; i < this.m_vaultDoorOpenedListeners.Count; i++)
		{
			this.m_vaultDoorOpenedListeners[i]();
		}
	}

	// Token: 0x06005857 RID: 22615 RVA: 0x001CDA4C File Offset: 0x001CBC4C
	public void RegisterVaultDoorClickedListener(Action a)
	{
		if (this.m_vaultDoorClickedListeners.Contains(a))
		{
			return;
		}
		this.m_vaultDoorClickedListeners.Add(a);
	}

	// Token: 0x06005858 RID: 22616 RVA: 0x001CDA69 File Offset: 0x001CBC69
	public void RemoveVaultDoorClickedListener(Action a)
	{
		this.m_vaultDoorClickedListeners.Remove(a);
	}

	// Token: 0x06005859 RID: 22617 RVA: 0x001CDA78 File Offset: 0x001CBC78
	public void OnVaultDoorClicked()
	{
		for (int i = 0; i < this.m_vaultDoorClickedListeners.Count; i++)
		{
			this.m_vaultDoorClickedListeners[i]();
		}
	}

	// Token: 0x0600585A RID: 22618 RVA: 0x001CDAAC File Offset: 0x001CBCAC
	private void OnLivesWidgetReady(Widget w)
	{
		this.m_livesWidget = w;
		this.m_livesWidget.RegisterDoneChangingStatesListener(new Action<object>(this.OnLivesWidgetDoneChangingStates), null, true, false);
	}

	// Token: 0x0600585B RID: 22619 RVA: 0x001CDACF File Offset: 0x001CBCCF
	private void OnLivesWidgetDoneChangingStates(object obj)
	{
		this.m_livesWidgetLoaded = true;
		this.m_livesWidget.RemoveStartChangingStatesListener(new Action<object>(this.OnLivesWidgetDoneChangingStates));
	}

	// Token: 0x0600585C RID: 22620 RVA: 0x001CDAEF File Offset: 0x001CBCEF
	private void OnVaultWidgetReady(Widget w)
	{
		this.m_vaultWidget = w;
		this.m_vaultWidget.RegisterDoneChangingStatesListener(new Action<object>(this.OnLivesWidgetDoneChangingStates), null, true, false);
		this.m_vaultWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnVaultEvent));
	}

	// Token: 0x0600585D RID: 22621 RVA: 0x001CDB29 File Offset: 0x001CBD29
	private void OnVaultWidgetDoneChangingStates(object obj)
	{
		this.m_vaultWidgetLoaded = true;
		this.m_vaultWidget.RemoveStartChangingStatesListener(new Action<object>(this.OnLivesWidgetDoneChangingStates));
	}

	// Token: 0x0600585E RID: 22622 RVA: 0x001CDB49 File Offset: 0x001CBD49
	private void OnVaultEvent(string eventName)
	{
		if (eventName == DuelsConfig.DOOR_OPENED_EVENT)
		{
			this.OnVaultDoorOpened();
			return;
		}
		if (eventName == DuelsConfig.DOOR_LEVEL_CLICKED)
		{
			this.OnVaultDoorClicked();
		}
	}

	// Token: 0x0600585F RID: 22623 RVA: 0x001CDB72 File Offset: 0x001CBD72
	private void OnLeverClickableReady(Clickable c)
	{
		this.m_leverButton = c;
	}

	// Token: 0x04004BB4 RID: 19380
	public AsyncReference m_livesReference;

	// Token: 0x04004BB5 RID: 19381
	public AsyncReference m_vaultReference;

	// Token: 0x04004BB6 RID: 19382
	public AsyncReference m_vaultLeverReference;

	// Token: 0x04004BB7 RID: 19383
	private Clickable m_leverButton;

	// Token: 0x04004BB8 RID: 19384
	private Widget m_livesWidget;

	// Token: 0x04004BB9 RID: 19385
	private Widget m_vaultWidget;

	// Token: 0x04004BBA RID: 19386
	private bool m_livesWidgetLoaded;

	// Token: 0x04004BBB RID: 19387
	private bool m_vaultWidgetLoaded;

	// Token: 0x04004BBC RID: 19388
	private List<Action> m_vaultDoorOpenedListeners;

	// Token: 0x04004BBD RID: 19389
	private List<Action> m_vaultDoorClickedListeners;
}
