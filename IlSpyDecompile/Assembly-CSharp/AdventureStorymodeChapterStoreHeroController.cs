using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class AdventureStorymodeChapterStoreHeroController : MonoBehaviour
{
	private const string HERO_BUTTONS_CHANGED_EVENT_NAME = "HERO_BUTTONS_CHANGED";

	public bool m_useHeroesFromManyWings;

	public AsyncReference[] m_storeHeroButtonReferences;

	private VisualController m_visualController;

	private int m_widgetReferencesToLoad;

	private List<GuestHeroPickerButton> m_heroPickerButtons = new List<GuestHeroPickerButton>();

	private List<Widget> m_storeHeroButtons = new List<Widget>();

	private List<DefLoader.DisposableFullDef> m_heroFullDefs = new List<DefLoader.DisposableFullDef>();

	private int m_heroesToLoad;

	private void Start()
	{
		m_visualController = GetComponent<VisualController>();
		if (m_visualController == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.Start: visual controller does not exist!");
			return;
		}
		m_visualController.Owner.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "HERO_BUTTONS_CHANGED")
			{
				StopCoroutine("RefreshHeroButtonsWhenReady");
				StartCoroutine("RefreshHeroButtonsWhenReady");
			}
		});
		m_widgetReferencesToLoad = m_storeHeroButtonReferences.Length;
		AsyncReference[] storeHeroButtonReferences = m_storeHeroButtonReferences;
		for (int i = 0; i < storeHeroButtonReferences.Length; i++)
		{
			storeHeroButtonReferences[i].RegisterReadyListener<Widget>(OnHeroPickerButtonWidgetReady);
		}
		StartCoroutine("RefreshHeroButtonsWhenReady");
	}

	private void OnDestroy()
	{
		ReleaseHeroDefs();
	}

	private void OnHeroPickerButtonWidgetReady(Widget widget)
	{
		if (widget == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.OnHeroPickerButtonWidgetReady: the Widget was null!");
			return;
		}
		m_storeHeroButtons.Add(widget);
		GuestHeroPickerButton componentInChildren = widget.GetComponentInChildren<GuestHeroPickerButton>();
		if (componentInChildren == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.OnHeroPickerButtonWidgetReady: the Widget did not have the hero picker button component!");
		}
		else
		{
			m_heroPickerButtons.Add(componentInChildren);
		}
	}

	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		m_heroFullDefs.Add(fullDef);
	}

	private IEnumerator RefreshHeroButtonsWhenReady()
	{
		LoadHeroFullDefs();
		while (m_widgetReferencesToLoad > m_storeHeroButtons.Count || m_heroesToLoad > m_heroFullDefs.Count)
		{
			yield return null;
		}
		SetupHeroPickerButtons();
	}

	private void LoadHeroFullDefs()
	{
		WingDbId wingId = (WingDbId)(GetAdventureBookPageDataModel()?.ChapterData.WingId ?? 0);
		AdventureDataDbfRecord record = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		List<AdventureGuestHeroesDbfRecord> list = GameDbf.AdventureGuestHeroes.GetRecords().FindAll((AdventureGuestHeroesDbfRecord x) => x.AdventureId == record.AdventureId && (m_useHeroesFromManyWings || x.WingId == (int)wingId));
		m_heroesToLoad = 0;
		ReleaseHeroDefs();
		foreach (AdventureGuestHeroesDbfRecord item in list)
		{
			GuestHeroDbfRecord record2 = GameDbf.GuestHero.GetRecord(item.GuestHeroId);
			if (record2 != null)
			{
				string cardId = GameUtils.TranslateDbIdToCardId(record2.CardId);
				DefLoader.Get().LoadFullDef(cardId, OnFullDefLoaded);
				m_heroesToLoad++;
			}
			else
			{
				Log.Adventures.Print("AdventureStoreModeChapterStoreHeroController: Guest Hero with ID={0} not found!", item.GuestHeroId);
			}
		}
	}

	private void SetupHeroPickerButtons()
	{
		if (m_heroFullDefs.Count > m_heroPickerButtons.Count)
		{
			Log.Adventures.Print("AdventureStoreModeChapterStoreHeroController: More hero defs than buttons, only the first {0} heroes will be displayed", m_heroPickerButtons.Count);
		}
		for (int i = 0; i < m_heroPickerButtons.Count; i++)
		{
			m_heroPickerButtons[i].SetDivotVisible(visible: false);
			if (i < m_heroFullDefs.Count)
			{
				UpdateHeroData(i);
			}
		}
	}

	private AdventureBookPageDataModel GetAdventureBookPageDataModel()
	{
		if (m_visualController == null)
		{
			Log.Adventures.PrintError("AdventureStorymodeChapterStoreHeroController.GetDataModel: visual controller does not exist!");
			return null;
		}
		m_visualController.GetDataModel(2, out var dataModel);
		return dataModel as AdventureBookPageDataModel;
	}

	private void UpdateHeroData(int index)
	{
		GuestHeroPickerButton guestHeroPickerButton = m_heroPickerButtons[index];
		if (guestHeroPickerButton == null)
		{
			Debug.LogErrorFormat("AdventureStorymodeChapterStoreHeroController.UpdateHeroData - HeroPickerButton at index {0} is null!", index);
			return;
		}
		DefLoader.DisposableFullDef disposableFullDef = m_heroFullDefs[index];
		if (disposableFullDef == null)
		{
			Debug.LogErrorFormat("AdventureStorymodeChapterStoreHeroController.UpdateHeroData - HeroPickerButton at index {0} is null!", index);
			return;
		}
		EntityDef entityDef = disposableFullDef.EntityDef;
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureStorymodeChapterStoreHeroController.UpdateSelectedHeroClasses - button did not contain an entity def!");
			return;
		}
		guestHeroPickerButton.SetGuestHero(GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == GameUtils.TranslateCardIdToDbId(entityDef.GetCardId())));
		guestHeroPickerButton.UpdateDisplay(disposableFullDef, TAG_PREMIUM.NORMAL);
		guestHeroPickerButton.HideTextAndGradient();
		Widget widget = m_storeHeroButtons[index];
		if (widget == null)
		{
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS @class in entityDef.GetClasses())
		{
			heroClassIconsDataModel.Classes.Add(@class);
		}
		widget.BindDataModel(heroClassIconsDataModel);
	}

	private void ReleaseHeroDefs()
	{
		m_heroFullDefs.DisposeValuesAndClear();
	}
}
