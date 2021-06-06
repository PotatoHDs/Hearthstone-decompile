using System;
using System.Collections.Generic;
using System.Text;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

public class ChangedCardMgr : IService
{
	private class TrackedCard
	{
		public int Index { get; set; }

		public int DbId { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return $"{Index},{DbId},{Count}-";
		}
	}

	public const int MaxViewCount = 1;

	private List<TrackedCard> m_cards = new List<TrackedCard>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		string @string = Options.Get().GetString(Option.CHANGED_CARDS_DATA);
		if (string.IsNullOrEmpty(@string))
		{
			yield break;
		}
		string[] array = @string.Split('-');
		foreach (string text in array)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			string[] array2 = text.Split(',');
			if (array2.Length == 3)
			{
				for (int j = 0; j < 3; j++)
				{
					string.IsNullOrEmpty(array2[j]);
				}
				if (GeneralUtils.TryParseInt(array2[0], out var val) && GeneralUtils.TryParseInt(array2[1], out var val2) && GeneralUtils.TryParseInt(array2[2], out var val3))
				{
					TrackedCard trackedCard = new TrackedCard();
					trackedCard.Index = val;
					trackedCard.DbId = val2;
					trackedCard.Count = val3;
					AddCard(trackedCard);
				}
			}
		}
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public static ChangedCardMgr Get()
	{
		return HearthstoneServices.Get<ChangedCardMgr>();
	}

	private TrackedCard FindCard(int dbId)
	{
		return m_cards.Find((TrackedCard card) => card.DbId == dbId);
	}

	private void AddCard(TrackedCard card)
	{
		TrackedCard trackedCard = FindCard(card.DbId);
		if (trackedCard != null)
		{
			if (trackedCard.Index < card.Index)
			{
				trackedCard.Index = card.Index;
				trackedCard.Count = card.Count;
			}
		}
		else
		{
			m_cards.Add(card);
		}
	}

	private void Save()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (TrackedCard card in m_cards)
		{
			stringBuilder.Append(card.ToString());
		}
		Options.Get().SetString(Option.CHANGED_CARDS_DATA, stringBuilder.ToString());
		Log.ChangedCards.Print("Saved CHANGED_CARDS_DATA " + stringBuilder.ToString());
	}

	public bool HasSeenCardChange(int index, int dbId)
	{
		TrackedCard trackedCard = FindCard(dbId);
		if (trackedCard == null)
		{
			return false;
		}
		if (index == trackedCard.Index)
		{
			return trackedCard.Count >= 1;
		}
		return false;
	}

	public void MarkCardChangeSeen(int index, int dbId)
	{
		TrackedCard trackedCard = FindCard(dbId);
		if (trackedCard == null)
		{
			trackedCard = new TrackedCard();
			trackedCard.Index = index;
			trackedCard.DbId = dbId;
			trackedCard.Count = 0;
			AddCard(trackedCard);
		}
		if (trackedCard.Index < index)
		{
			Log.ChangedCards.PrintWarning("Updating to a newer change version for card " + trackedCard);
			trackedCard.Index = index;
			trackedCard.Count = 0;
		}
		if (index == trackedCard.Index && trackedCard.Count < 1)
		{
			trackedCard.Count++;
			Save();
		}
	}

	public void ResetCardChangesSeen()
	{
		m_cards.Clear();
		Save();
	}
}
