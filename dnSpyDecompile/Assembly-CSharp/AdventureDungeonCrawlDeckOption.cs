using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000038 RID: 56
[CustomEditClass]
public class AdventureDungeonCrawlDeckOption : AdventureOptionWidget
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060001FC RID: 508 RVA: 0x0000B5EC File Offset: 0x000097EC
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public long DeckId
	{
		get
		{
			return this.m_databaseId;
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000B5F4 File Offset: 0x000097F4
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

	// Token: 0x060001FE RID: 510 RVA: 0x0000B628 File Offset: 0x00009828
	private void InitDataModel(AdventureDeckDbfRecord deckRecord)
	{
		if (deckRecord == null)
		{
			Log.Adventures.PrintError("DeckPouch tried to setup its data model with a null deck record!", Array.Empty<object>());
			return;
		}
		if (string.IsNullOrEmpty(deckRecord.DisplayTexture))
		{
			this.m_dataModel.DisplayTexture = null;
		}
		else
		{
			AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, UnityEngine.Object materialObj, object data)
			{
				this.m_dataModel.DisplayTexture = (materialObj as Material);
			};
			AssetLoader.Get().LoadMaterial(deckRecord.DisplayTexture, callback, null, false, false);
		}
		this.m_dataModel.DisplayColor = CollectionPageManager.ColorForClass((TAG_CLASS)deckRecord.ClassId);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000B6A8 File Offset: 0x000098A8
	public void Init(AdventureDeckDbfRecord deckRecord, bool locked, string lockedText, bool completed, bool newlyUnlocked, AdventureOptionWidget.OptionAcknowledgedCallback acknowledgedCallback)
	{
		this.m_deckRecord = deckRecord;
		if (this.m_deckRecord == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckOption.Init() called with a null AdventureDeckDbfRecord!", Array.Empty<object>());
			return;
		}
		string name;
		string text;
		this.m_deckContents = CollectionManager.Get().LoadDeckFromDBF(this.m_deckRecord.DeckId, out name, out text);
		this.m_databaseId = (long)deckRecord.DeckId;
		base.InitWidget(name, locked, lockedText, false, completed, newlyUnlocked, acknowledgedCallback);
		this.InitDataModel(deckRecord);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000B71C File Offset: 0x0000991C
	public override void Select()
	{
		base.Select();
		if (this.m_dataModel == null)
		{
			Log.Adventures.PrintError("Attempting to set deck pouch option selected but data model was null!", Array.Empty<object>());
			return;
		}
		AdventureDungeonCrawlDeckOption.DeckOptionSelectedCallback deckOptionSelectedCallback = this.m_selectedCallback as AdventureDungeonCrawlDeckOption.DeckOptionSelectedCallback;
		if (deckOptionSelectedCallback == null)
		{
			Log.Adventures.PrintError("Attempting to execute a callback for the AdventureDungeonCrawlDeckOption, but no callback was provided!", Array.Empty<object>());
			return;
		}
		deckOptionSelectedCallback(this.m_deckRecord.DeckId, this.m_deckContents, this.m_dataModel.Locked);
	}

	// Token: 0x04000172 RID: 370
	private AdventureDeckDbfRecord m_deckRecord;

	// Token: 0x04000173 RID: 371
	private List<long> m_deckContents;

	// Token: 0x020012AB RID: 4779
	// (Invoke) Token: 0x0600D4EF RID: 54511
	public delegate void DeckOptionSelectedCallback(int deckID, List<long> deckContents, bool deckIsLocked);
}
