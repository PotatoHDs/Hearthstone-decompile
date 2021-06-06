using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureDungeonCrawlDeckOption : AdventureOptionWidget
{
	public delegate void DeckOptionSelectedCallback(int deckID, List<long> deckContents, bool deckIsLocked);

	private AdventureDeckDbfRecord m_deckRecord;

	private List<long> m_deckContents;

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public long DeckId => m_databaseId;

	protected override void OnClickableReady(Clickable clickable)
	{
		base.OnClickableReady(clickable);
		if (!(m_clickable == null))
		{
			m_clickable.AddEventListener(UIEventType.RELEASE, delegate
			{
				Select();
			});
		}
	}

	private void InitDataModel(AdventureDeckDbfRecord deckRecord)
	{
		if (deckRecord == null)
		{
			Log.Adventures.PrintError("DeckPouch tried to setup its data model with a null deck record!");
			return;
		}
		if (string.IsNullOrEmpty(deckRecord.DisplayTexture))
		{
			m_dataModel.DisplayTexture = null;
		}
		else
		{
			AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, Object materialObj, object data)
			{
				m_dataModel.DisplayTexture = materialObj as Material;
			};
			AssetLoader.Get().LoadMaterial(deckRecord.DisplayTexture, callback);
		}
		m_dataModel.DisplayColor = CollectionPageManager.ColorForClass((TAG_CLASS)deckRecord.ClassId);
	}

	public void Init(AdventureDeckDbfRecord deckRecord, bool locked, string lockedText, bool completed, bool newlyUnlocked, OptionAcknowledgedCallback acknowledgedCallback)
	{
		m_deckRecord = deckRecord;
		if (m_deckRecord == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckOption.Init() called with a null AdventureDeckDbfRecord!");
			return;
		}
		m_deckContents = CollectionManager.Get().LoadDeckFromDBF(m_deckRecord.DeckId, out var deckName, out var _);
		m_databaseId = deckRecord.DeckId;
		InitWidget(deckName, locked, lockedText, upgraded: false, completed, newlyUnlocked, acknowledgedCallback);
		InitDataModel(deckRecord);
	}

	public override void Select()
	{
		base.Select();
		if (m_dataModel == null)
		{
			Log.Adventures.PrintError("Attempting to set deck pouch option selected but data model was null!");
			return;
		}
		DeckOptionSelectedCallback deckOptionSelectedCallback = m_selectedCallback as DeckOptionSelectedCallback;
		if (deckOptionSelectedCallback == null)
		{
			Log.Adventures.PrintError("Attempting to execute a callback for the AdventureDungeonCrawlDeckOption, but no callback was provided!");
		}
		else
		{
			deckOptionSelectedCallback(m_deckRecord.DeckId, m_deckContents, m_dataModel.Locked);
		}
	}
}
