using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200003B RID: 59
[CustomEditClass]
public class AdventureDungeonCrawlHeroPowerOption : AdventureOptionWidget
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x0600029A RID: 666 RVA: 0x00010AB7 File Offset: 0x0000ECB7
	public override bool IsReady
	{
		get
		{
			return base.IsReady && this.m_isDefLoaded;
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00010ACC File Offset: 0x0000ECCC
	protected override void OnWidgetInstanceReady(WidgetInstance widgetInstance)
	{
		base.OnWidgetInstanceReady(widgetInstance);
		if (this.m_widgetInstance == null)
		{
			return;
		}
		if (this.m_databaseId != 0L)
		{
			this.m_isDefLoaded = false;
			string cardId = GameUtils.TranslateDbIdToCardId((int)this.m_databaseId, false);
			DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
		}
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00010B25 File Offset: 0x0000ED25
	protected override void OnClickableReady(Clickable clickable)
	{
		base.OnClickableReady(clickable);
		if (this.m_clickable == null)
		{
			return;
		}
		this.m_clickable.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Select();
		});
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00010B58 File Offset: 0x0000ED58
	protected override void Rollover()
	{
		base.Rollover();
		AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback heroPowerHoverOptionCallback = this.m_rolloverCallback as AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback;
		if (heroPowerHoverOptionCallback == null)
		{
			Log.Adventures.PrintError("rollover callback was null or was not a HeroPowerHoverOptionCallback!", Array.Empty<object>());
			return;
		}
		if (this.m_dataModel.Locked)
		{
			heroPowerHoverOptionCallback(this.m_databaseId, this.m_BigCardBone);
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x00010BB0 File Offset: 0x0000EDB0
	protected override void Rollout()
	{
		base.Rollout();
		AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback heroPowerHoverOptionCallback = this.m_rolloutCallback as AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback;
		if (heroPowerHoverOptionCallback == null)
		{
			Log.Adventures.PrintError("rollout callback was null or was not a HeroPowerHoverOptionCallback!", Array.Empty<object>());
			return;
		}
		if (this.m_dataModel.Locked)
		{
			heroPowerHoverOptionCallback(this.m_databaseId, this.m_BigCardBone);
		}
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00010C08 File Offset: 0x0000EE08
	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		try
		{
			if (def == null)
			{
				Debug.LogErrorFormat("Unable to load FullDef for cardID={0}", new object[]
				{
					cardID
				});
			}
			else
			{
				this.m_isDefLoaded = true;
				Actor componentInChildren = this.m_widgetInstance.GetComponentInChildren<Actor>(true);
				componentInChildren.SetFullDef(def);
				componentInChildren.UpdateAllComponents();
				if (this.m_widgetInstance)
				{
					this.m_widgetInstance.TriggerEvent("SetUpState", default(Widget.TriggerEventParameters));
				}
			}
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00010C94 File Offset: 0x0000EE94
	public void Init(long heroPowerDbId, bool locked, string lockedText, bool completed, bool newlyUnlocked, AdventureOptionWidget.OptionAcknowledgedCallback acknowledgedCallback)
	{
		this.m_databaseId = heroPowerDbId;
		string name = null;
		base.InitWidget(name, locked, lockedText, false, completed, newlyUnlocked, acknowledgedCallback);
		if (this.m_widgetInstance != null)
		{
			string cardId = GameUtils.TranslateDbIdToCardId((int)this.m_databaseId, false);
			DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
		}
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00010CF0 File Offset: 0x0000EEF0
	public override void Select()
	{
		base.Select();
		if (this.m_dataModel == null)
		{
			Log.Adventures.PrintError("Attempting to set deck pouch option clickable events but data model was null!", Array.Empty<object>());
			return;
		}
		AdventureDungeonCrawlHeroPowerOption.HeroPowerSelectedOptionCallback heroPowerSelectedOptionCallback = this.m_selectedCallback as AdventureDungeonCrawlHeroPowerOption.HeroPowerSelectedOptionCallback;
		if (heroPowerSelectedOptionCallback == null)
		{
			Log.Adventures.PrintError("Attempting to set a callback for the AdventureDungeonCrawlHeroPowerOption, but no callback was provided!", Array.Empty<object>());
			return;
		}
		heroPowerSelectedOptionCallback(this.m_databaseId, this.m_dataModel.Locked);
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00010D5C File Offset: 0x0000EF5C
	public override void SetVisible(bool isVisible)
	{
		if (isVisible == this.m_isVisible)
		{
			return;
		}
		base.SetVisible(isVisible);
		Actor componentInChildren = this.m_widgetInstance.GetComponentInChildren<Actor>(true);
		if (componentInChildren == null)
		{
			Log.Adventures.PrintError("Tried to set hero power actor visibility but hero power actor was not found!", Array.Empty<object>());
			return;
		}
		if (isVisible)
		{
			this.m_widgetInstance.Show();
			componentInChildren.Show();
			return;
		}
		this.m_widgetInstance.Hide();
		componentInChildren.Hide();
	}

	// Token: 0x040001C5 RID: 453
	[CustomEditField(Sections = "Bones")]
	public GameObject m_BigCardBone;

	// Token: 0x040001C6 RID: 454
	private bool m_isDefLoaded;

	// Token: 0x020012C8 RID: 4808
	// (Invoke) Token: 0x0600D564 RID: 54628
	public delegate void HeroPowerSelectedOptionCallback(long heroPowerDbId, bool isLocked);

	// Token: 0x020012C9 RID: 4809
	// (Invoke) Token: 0x0600D568 RID: 54632
	public delegate void HeroPowerHoverOptionCallback(long heroPowerDbId, GameObject bigCardBone);
}
