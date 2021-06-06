using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterDialogSequence : IEnumerable<CharacterDialog>, IEnumerable
{
	private List<CharacterDialog> dialogItems;

	public CharacterDialogDbfRecord m_characterDialogRecord;

	public int m_onCompleteBannerId;

	public bool m_ignorePopups = true;

	public bool m_deferOnComplete = true;

	public bool m_blockInput = true;

	public Action<CharacterDialogSequence> m_onPreShow;

	public int Count => dialogItems.Count;

	public CharacterDialogSequence(int dialogSequenceId, CharacterDialogEventType eventType = CharacterDialogEventType.UNSPECIFIED)
	{
		CharacterDialogDbfRecord characterDialogDbfRecord = (m_characterDialogRecord = GameDbf.CharacterDialog.GetRecord(dialogSequenceId));
		m_onCompleteBannerId = characterDialogDbfRecord.OnCompleteBannerId;
		m_ignorePopups = characterDialogDbfRecord.IgnorePopups;
		m_deferOnComplete = characterDialogDbfRecord.DeferOnComplete;
		m_blockInput = characterDialogDbfRecord.BlockInput;
		List<CharacterDialogItemsDbfRecord> list = GameDbf.CharacterDialogItems.GetRecords().FindAll(delegate(CharacterDialogItemsDbfRecord obj)
		{
			CharacterDialogEventType characterDialogEventType = EnumUtils.SafeParse(obj.AchieveEventType, CharacterDialogEventType.UNSPECIFIED, ignoreCase: true);
			return (eventType == CharacterDialogEventType.UNSPECIFIED || characterDialogEventType == eventType) && obj.CharacterDialogId == dialogSequenceId;
		});
		list.Sort(delegate(CharacterDialogItemsDbfRecord a, CharacterDialogItemsDbfRecord b)
		{
			if (a.PlayOrder < b.PlayOrder)
			{
				return -1;
			}
			return (a.PlayOrder > b.PlayOrder) ? 1 : 0;
		});
		dialogItems = new List<CharacterDialog>();
		foreach (CharacterDialogItemsDbfRecord item2 in list)
		{
			CharacterDialog item = new CharacterDialog
			{
				dbfRecordId = item2.ID,
				playOrder = item2.PlayOrder,
				useInnkeeperQuote = item2.UseInnkeeperQuote,
				prefabName = item2.PrefabName,
				audioName = item2.AudioName,
				useAltSpeechBubble = item2.AltBubblePosition,
				waitBefore = (float)item2.WaitBefore,
				waitAfter = (float)item2.WaitAfter,
				persistPrefab = item2.PersistPrefab,
				useAltPosition = item2.AltPosition,
				minimumDurationSeconds = (float)item2.MinimumDurationSeconds,
				localeExtraSeconds = (float)item2.LocaleExtraSeconds,
				bubbleText = item2.BubbleText
			};
			dialogItems.Add(item);
		}
	}

	public static List<string> GetAudioOfCharacterDialogSequence(int dialogSequenceId)
	{
		List<string> list = new List<string>();
		CharacterDialogDbfRecord record = GameDbf.CharacterDialog.GetRecord(dialogSequenceId);
		foreach (CharacterDialogItemsDbfRecord item in GameDbf.CharacterDialogItems.GetRecords().FindAll((CharacterDialogItemsDbfRecord obj) => obj.CharacterDialogId == record.ID))
		{
			list.Add(item.AudioName);
		}
		return list;
	}

	public IEnumerator<CharacterDialog> GetEnumerator()
	{
		foreach (CharacterDialog dialogItem in dialogItems)
		{
			yield return dialogItem;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
