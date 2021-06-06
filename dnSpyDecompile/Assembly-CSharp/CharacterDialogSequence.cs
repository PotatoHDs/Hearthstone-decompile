using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000878 RID: 2168
public class CharacterDialogSequence : IEnumerable<CharacterDialog>, IEnumerable
{
	// Token: 0x170006F3 RID: 1779
	// (get) Token: 0x06007603 RID: 30211 RVA: 0x0025E137 File Offset: 0x0025C337
	public int Count
	{
		get
		{
			return this.dialogItems.Count;
		}
	}

	// Token: 0x06007604 RID: 30212 RVA: 0x0025E144 File Offset: 0x0025C344
	public CharacterDialogSequence(int dialogSequenceId, CharacterDialogEventType eventType = CharacterDialogEventType.UNSPECIFIED)
	{
		CharacterDialogDbfRecord record = GameDbf.CharacterDialog.GetRecord(dialogSequenceId);
		this.m_characterDialogRecord = record;
		this.m_onCompleteBannerId = record.OnCompleteBannerId;
		this.m_ignorePopups = record.IgnorePopups;
		this.m_deferOnComplete = record.DeferOnComplete;
		this.m_blockInput = record.BlockInput;
		List<CharacterDialogItemsDbfRecord> list = GameDbf.CharacterDialogItems.GetRecords().FindAll(delegate(CharacterDialogItemsDbfRecord obj)
		{
			CharacterDialogEventType characterDialogEventType = EnumUtils.SafeParse<CharacterDialogEventType>(obj.AchieveEventType, CharacterDialogEventType.UNSPECIFIED, true);
			return (eventType == CharacterDialogEventType.UNSPECIFIED || characterDialogEventType == eventType) && obj.CharacterDialogId == dialogSequenceId;
		});
		list.Sort(delegate(CharacterDialogItemsDbfRecord a, CharacterDialogItemsDbfRecord b)
		{
			if (a.PlayOrder < b.PlayOrder)
			{
				return -1;
			}
			if (a.PlayOrder > b.PlayOrder)
			{
				return 1;
			}
			return 0;
		});
		this.dialogItems = new List<CharacterDialog>();
		foreach (CharacterDialogItemsDbfRecord characterDialogItemsDbfRecord in list)
		{
			CharacterDialog item = default(CharacterDialog);
			item.dbfRecordId = characterDialogItemsDbfRecord.ID;
			item.playOrder = characterDialogItemsDbfRecord.PlayOrder;
			item.useInnkeeperQuote = characterDialogItemsDbfRecord.UseInnkeeperQuote;
			item.prefabName = characterDialogItemsDbfRecord.PrefabName;
			item.audioName = characterDialogItemsDbfRecord.AudioName;
			item.useAltSpeechBubble = characterDialogItemsDbfRecord.AltBubblePosition;
			item.waitBefore = (float)characterDialogItemsDbfRecord.WaitBefore;
			item.waitAfter = (float)characterDialogItemsDbfRecord.WaitAfter;
			item.persistPrefab = characterDialogItemsDbfRecord.PersistPrefab;
			item.useAltPosition = characterDialogItemsDbfRecord.AltPosition;
			item.minimumDurationSeconds = (float)characterDialogItemsDbfRecord.MinimumDurationSeconds;
			item.localeExtraSeconds = (float)characterDialogItemsDbfRecord.LocaleExtraSeconds;
			item.bubbleText = characterDialogItemsDbfRecord.BubbleText;
			this.dialogItems.Add(item);
		}
	}

	// Token: 0x06007605 RID: 30213 RVA: 0x0025E314 File Offset: 0x0025C514
	public static List<string> GetAudioOfCharacterDialogSequence(int dialogSequenceId)
	{
		List<string> list = new List<string>();
		CharacterDialogDbfRecord record = GameDbf.CharacterDialog.GetRecord(dialogSequenceId);
		foreach (CharacterDialogItemsDbfRecord characterDialogItemsDbfRecord in GameDbf.CharacterDialogItems.GetRecords().FindAll((CharacterDialogItemsDbfRecord obj) => obj.CharacterDialogId == record.ID))
		{
			list.Add(characterDialogItemsDbfRecord.AudioName);
		}
		return list;
	}

	// Token: 0x06007606 RID: 30214 RVA: 0x0025E3A0 File Offset: 0x0025C5A0
	public IEnumerator<CharacterDialog> GetEnumerator()
	{
		foreach (CharacterDialog characterDialog in this.dialogItems)
		{
			yield return characterDialog;
		}
		List<CharacterDialog>.Enumerator enumerator = default(List<CharacterDialog>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06007607 RID: 30215 RVA: 0x0025E3AF File Offset: 0x0025C5AF
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x04005D42 RID: 23874
	private List<CharacterDialog> dialogItems;

	// Token: 0x04005D43 RID: 23875
	public CharacterDialogDbfRecord m_characterDialogRecord;

	// Token: 0x04005D44 RID: 23876
	public int m_onCompleteBannerId;

	// Token: 0x04005D45 RID: 23877
	public bool m_ignorePopups = true;

	// Token: 0x04005D46 RID: 23878
	public bool m_deferOnComplete = true;

	// Token: 0x04005D47 RID: 23879
	public bool m_blockInput = true;

	// Token: 0x04005D48 RID: 23880
	public Action<CharacterDialogSequence> m_onPreShow;
}
