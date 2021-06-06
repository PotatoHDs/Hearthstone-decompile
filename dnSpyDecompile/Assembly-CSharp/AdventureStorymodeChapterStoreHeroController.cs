using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class AdventureStorymodeChapterStoreHeroController : MonoBehaviour
{
	// Token: 0x0600046C RID: 1132 RVA: 0x0001ACD8 File Offset: 0x00018ED8
	private void Start()
	{
		this.m_visualController = base.GetComponent<VisualController>();
		if (this.m_visualController == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.Start: visual controller does not exist!", Array.Empty<object>());
			return;
		}
		this.m_visualController.Owner.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "HERO_BUTTONS_CHANGED")
			{
				base.StopCoroutine("RefreshHeroButtonsWhenReady");
				base.StartCoroutine("RefreshHeroButtonsWhenReady");
			}
		});
		this.m_widgetReferencesToLoad = this.m_storeHeroButtonReferences.Length;
		AsyncReference[] storeHeroButtonReferences = this.m_storeHeroButtonReferences;
		for (int i = 0; i < storeHeroButtonReferences.Length; i++)
		{
			storeHeroButtonReferences[i].RegisterReadyListener<Widget>(new Action<Widget>(this.OnHeroPickerButtonWidgetReady));
		}
		base.StartCoroutine("RefreshHeroButtonsWhenReady");
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001AD73 File Offset: 0x00018F73
	private void OnDestroy()
	{
		this.ReleaseHeroDefs();
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0001AD7C File Offset: 0x00018F7C
	private void OnHeroPickerButtonWidgetReady(Widget widget)
	{
		if (widget == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.OnHeroPickerButtonWidgetReady: the Widget was null!", Array.Empty<object>());
			return;
		}
		this.m_storeHeroButtons.Add(widget);
		GuestHeroPickerButton componentInChildren = widget.GetComponentInChildren<GuestHeroPickerButton>();
		if (componentInChildren == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.OnHeroPickerButtonWidgetReady: the Widget did not have the hero picker button component!", Array.Empty<object>());
			return;
		}
		this.m_heroPickerButtons.Add(componentInChildren);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001ADE4 File Offset: 0x00018FE4
	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		this.m_heroFullDefs.Add(fullDef);
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001ADF2 File Offset: 0x00018FF2
	private IEnumerator RefreshHeroButtonsWhenReady()
	{
		this.LoadHeroFullDefs();
		while (this.m_widgetReferencesToLoad > this.m_storeHeroButtons.Count || this.m_heroesToLoad > this.m_heroFullDefs.Count)
		{
			yield return null;
		}
		this.SetupHeroPickerButtons();
		yield break;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001AE04 File Offset: 0x00019004
	private void LoadHeroFullDefs()
	{
		AdventureBookPageDataModel adventureBookPageDataModel = this.GetAdventureBookPageDataModel();
		WingDbId wingId = (WingDbId)((adventureBookPageDataModel != null) ? adventureBookPageDataModel.ChapterData.WingId : 0);
		AdventureDataDbfRecord record = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		List<AdventureGuestHeroesDbfRecord> list = GameDbf.AdventureGuestHeroes.GetRecords().FindAll((AdventureGuestHeroesDbfRecord x) => x.AdventureId == record.AdventureId && (this.m_useHeroesFromManyWings || x.WingId == (int)wingId));
		this.m_heroesToLoad = 0;
		this.ReleaseHeroDefs();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in list)
		{
			GuestHeroDbfRecord record2 = GameDbf.GuestHero.GetRecord(adventureGuestHeroesDbfRecord.GuestHeroId);
			if (record2 != null)
			{
				string cardId = GameUtils.TranslateDbIdToCardId(record2.CardId, false);
				DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
				this.m_heroesToLoad++;
			}
			else
			{
				Log.Adventures.Print("AdventureStoreModeChapterStoreHeroController: Guest Hero with ID={0} not found!", new object[]
				{
					adventureGuestHeroesDbfRecord.GuestHeroId
				});
			}
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001AF24 File Offset: 0x00019124
	private void SetupHeroPickerButtons()
	{
		if (this.m_heroFullDefs.Count > this.m_heroPickerButtons.Count)
		{
			Log.Adventures.Print("AdventureStoreModeChapterStoreHeroController: More hero defs than buttons, only the first {0} heroes will be displayed", new object[]
			{
				this.m_heroPickerButtons.Count
			});
		}
		for (int i = 0; i < this.m_heroPickerButtons.Count; i++)
		{
			this.m_heroPickerButtons[i].SetDivotVisible(false);
			if (i < this.m_heroFullDefs.Count)
			{
				this.UpdateHeroData(i);
			}
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001AFB0 File Offset: 0x000191B0
	private AdventureBookPageDataModel GetAdventureBookPageDataModel()
	{
		if (this.m_visualController == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.GetDataModel: visual controller does not exist!", Array.Empty<object>());
			return null;
		}
		IDataModel dataModel;
		this.m_visualController.GetDataModel(2, out dataModel);
		return dataModel as AdventureBookPageDataModel;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001AFF8 File Offset: 0x000191F8
	private void UpdateHeroData(int index)
	{
		GuestHeroPickerButton guestHeroPickerButton = this.m_heroPickerButtons[index];
		if (guestHeroPickerButton == null)
		{
			Debug.LogErrorFormat("AdventureStorymodeChapterStoreHeroController.UpdateHeroData - HeroPickerButton at index {0} is null!", new object[]
			{
				index
			});
			return;
		}
		DefLoader.DisposableFullDef disposableFullDef = this.m_heroFullDefs[index];
		if (disposableFullDef == null)
		{
			Debug.LogErrorFormat("AdventureStorymodeChapterStoreHeroController.UpdateHeroData - HeroPickerButton at index {0} is null!", new object[]
			{
				index
			});
			return;
		}
		EntityDef entityDef = disposableFullDef.EntityDef;
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureStorymodeChapterStoreHeroController.UpdateSelectedHeroClasses - button did not contain an entity def!");
			return;
		}
		guestHeroPickerButton.SetGuestHero(GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == GameUtils.TranslateCardIdToDbId(entityDef.GetCardId(), false)));
		guestHeroPickerButton.UpdateDisplay(disposableFullDef, TAG_PREMIUM.NORMAL);
		guestHeroPickerButton.HideTextAndGradient();
		Widget widget = this.m_storeHeroButtons[index];
		if (widget == null)
		{
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS item in entityDef.GetClasses(null))
		{
			heroClassIconsDataModel.Classes.Add(item);
		}
		widget.BindDataModel(heroClassIconsDataModel, false);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001B134 File Offset: 0x00019334
	private void ReleaseHeroDefs()
	{
		this.m_heroFullDefs.DisposeValuesAndClear<DefLoader.DisposableFullDef>();
	}

	// Token: 0x04000313 RID: 787
	private const string HERO_BUTTONS_CHANGED_EVENT_NAME = "HERO_BUTTONS_CHANGED";

	// Token: 0x04000314 RID: 788
	public bool m_useHeroesFromManyWings;

	// Token: 0x04000315 RID: 789
	public AsyncReference[] m_storeHeroButtonReferences;

	// Token: 0x04000316 RID: 790
	private VisualController m_visualController;

	// Token: 0x04000317 RID: 791
	private int m_widgetReferencesToLoad;

	// Token: 0x04000318 RID: 792
	private List<GuestHeroPickerButton> m_heroPickerButtons = new List<GuestHeroPickerButton>();

	// Token: 0x04000319 RID: 793
	private List<Widget> m_storeHeroButtons = new List<Widget>();

	// Token: 0x0400031A RID: 794
	private List<DefLoader.DisposableFullDef> m_heroFullDefs = new List<DefLoader.DisposableFullDef>();

	// Token: 0x0400031B RID: 795
	private int m_heroesToLoad;
}
