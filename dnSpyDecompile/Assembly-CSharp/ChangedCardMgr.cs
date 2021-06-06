using System;
using System.Collections.Generic;
using System.Text;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

// Token: 0x020009A3 RID: 2467
public class ChangedCardMgr : IService
{
	// Token: 0x060086AF RID: 34479 RVA: 0x002B7AD4 File Offset: 0x002B5CD4
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		string @string = Options.Get().GetString(Option.CHANGED_CARDS_DATA);
		if (string.IsNullOrEmpty(@string))
		{
			yield break;
		}
		foreach (string text in @string.Split(new char[]
		{
			'-'
		}))
		{
			if (!string.IsNullOrEmpty(text))
			{
				string[] array2 = text.Split(new char[]
				{
					','
				});
				if (array2.Length == 3)
				{
					for (int j = 0; j < 3; j++)
					{
						string.IsNullOrEmpty(array2[j]);
					}
					int index;
					int dbId;
					int count;
					if (GeneralUtils.TryParseInt(array2[0], out index) && GeneralUtils.TryParseInt(array2[1], out dbId) && GeneralUtils.TryParseInt(array2[2], out count))
					{
						this.AddCard(new ChangedCardMgr.TrackedCard
						{
							Index = index,
							DbId = dbId,
							Count = count
						});
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x060086B0 RID: 34480 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060086B1 RID: 34481 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060086B2 RID: 34482 RVA: 0x002B7AE3 File Offset: 0x002B5CE3
	public static ChangedCardMgr Get()
	{
		return HearthstoneServices.Get<ChangedCardMgr>();
	}

	// Token: 0x060086B3 RID: 34483 RVA: 0x002B7AEC File Offset: 0x002B5CEC
	private ChangedCardMgr.TrackedCard FindCard(int dbId)
	{
		return this.m_cards.Find((ChangedCardMgr.TrackedCard card) => card.DbId == dbId);
	}

	// Token: 0x060086B4 RID: 34484 RVA: 0x002B7B20 File Offset: 0x002B5D20
	private void AddCard(ChangedCardMgr.TrackedCard card)
	{
		ChangedCardMgr.TrackedCard trackedCard = this.FindCard(card.DbId);
		if (trackedCard != null)
		{
			if (trackedCard.Index < card.Index)
			{
				trackedCard.Index = card.Index;
				trackedCard.Count = card.Count;
			}
			return;
		}
		this.m_cards.Add(card);
	}

	// Token: 0x060086B5 RID: 34485 RVA: 0x002B7B70 File Offset: 0x002B5D70
	private void Save()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (ChangedCardMgr.TrackedCard trackedCard in this.m_cards)
		{
			stringBuilder.Append(trackedCard.ToString());
		}
		Options.Get().SetString(Option.CHANGED_CARDS_DATA, stringBuilder.ToString());
		Log.ChangedCards.Print("Saved CHANGED_CARDS_DATA " + stringBuilder.ToString(), Array.Empty<object>());
	}

	// Token: 0x060086B6 RID: 34486 RVA: 0x002B7C00 File Offset: 0x002B5E00
	public bool HasSeenCardChange(int index, int dbId)
	{
		ChangedCardMgr.TrackedCard trackedCard = this.FindCard(dbId);
		return trackedCard != null && index == trackedCard.Index && trackedCard.Count >= 1;
	}

	// Token: 0x060086B7 RID: 34487 RVA: 0x002B7C34 File Offset: 0x002B5E34
	public void MarkCardChangeSeen(int index, int dbId)
	{
		ChangedCardMgr.TrackedCard trackedCard = this.FindCard(dbId);
		if (trackedCard == null)
		{
			trackedCard = new ChangedCardMgr.TrackedCard();
			trackedCard.Index = index;
			trackedCard.DbId = dbId;
			trackedCard.Count = 0;
			this.AddCard(trackedCard);
		}
		if (trackedCard.Index < index)
		{
			Log.ChangedCards.PrintWarning("Updating to a newer change version for card " + trackedCard, Array.Empty<object>());
			trackedCard.Index = index;
			trackedCard.Count = 0;
		}
		if (index == trackedCard.Index && trackedCard.Count < 1)
		{
			ChangedCardMgr.TrackedCard trackedCard2 = trackedCard;
			int count = trackedCard2.Count + 1;
			trackedCard2.Count = count;
			this.Save();
		}
	}

	// Token: 0x060086B8 RID: 34488 RVA: 0x002B7CC7 File Offset: 0x002B5EC7
	public void ResetCardChangesSeen()
	{
		this.m_cards.Clear();
		this.Save();
	}

	// Token: 0x040071FC RID: 29180
	public const int MaxViewCount = 1;

	// Token: 0x040071FD RID: 29181
	private List<ChangedCardMgr.TrackedCard> m_cards = new List<ChangedCardMgr.TrackedCard>();

	// Token: 0x0200265D RID: 9821
	private class TrackedCard
	{
		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x060136E5 RID: 79589 RVA: 0x00533D8D File Offset: 0x00531F8D
		// (set) Token: 0x060136E6 RID: 79590 RVA: 0x00533D95 File Offset: 0x00531F95
		public int Index { get; set; }

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x060136E7 RID: 79591 RVA: 0x00533D9E File Offset: 0x00531F9E
		// (set) Token: 0x060136E8 RID: 79592 RVA: 0x00533DA6 File Offset: 0x00531FA6
		public int DbId { get; set; }

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x060136E9 RID: 79593 RVA: 0x00533DAF File Offset: 0x00531FAF
		// (set) Token: 0x060136EA RID: 79594 RVA: 0x00533DB7 File Offset: 0x00531FB7
		public int Count { get; set; }

		// Token: 0x060136EB RID: 79595 RVA: 0x00533DC0 File Offset: 0x00531FC0
		public override string ToString()
		{
			return string.Format("{0},{1},{2}-", this.Index, this.DbId, this.Count);
		}
	}
}
