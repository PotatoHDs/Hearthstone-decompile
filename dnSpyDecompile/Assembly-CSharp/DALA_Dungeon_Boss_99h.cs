using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000479 RID: 1145
public class DALA_Dungeon_Boss_99h : DALA_Dungeon
{
	// Token: 0x06003DF9 RID: 15865 RVA: 0x0014636C File Offset: 0x0014456C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01.prefab:a78b045849c256541bd980732271ea96"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DFA RID: 15866 RVA: 0x001463D0 File Offset: 0x001445D0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DALA_Dungeon_Boss_99h.REPLACE_ME;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_99h.REPLACE_ME;
	}

	// Token: 0x06003DFB RID: 15867 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x001463F8 File Offset: 0x001445F8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003DFD RID: 15869 RVA: 0x0014640E File Offset: 0x0014460E
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x06003DFE RID: 15870 RVA: 0x00146424 File Offset: 0x00144624
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x04002992 RID: 10642
	private static readonly AssetReference REPLACE_ME = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01.prefab:a78b045849c256541bd980732271ea96");

	// Token: 0x04002993 RID: 10643
	private HashSet<string> m_playedLines = new HashSet<string>();
}
