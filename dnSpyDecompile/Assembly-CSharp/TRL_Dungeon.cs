using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class TRL_Dungeon : TRL_MissionEntity
{
	// Token: 0x060039DB RID: 14811 RVA: 0x001267A8 File Offset: 0x001249A8
	public static TRL_Dungeon InstantiateTRLDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 456702420U)
		{
			if (num <= 389886134U)
			{
				if (num != 20631421U)
				{
					if (num == 389886134U)
					{
						if (opposingHeroCardID == "TRLA_205h")
						{
							return new TRL_Dungeon_Boss_205h();
						}
					}
				}
				else if (opposingHeroCardID == "TRLA_206h")
				{
					return new TRL_Dungeon_Boss_206h();
				}
			}
			else if (num != 422602729U)
			{
				if (num == 456702420U)
				{
					if (opposingHeroCardID == "TRLA_207h")
					{
						return new TRL_Dungeon_Boss_207h();
					}
				}
			}
			else if (opposingHeroCardID == "TRLA_202h")
			{
				return new TRL_Dungeon_Boss_202h();
			}
		}
		else if (num <= 4112693171U)
		{
			if (num != 4079873408U)
			{
				if (num == 4112693171U)
				{
					if (opposingHeroCardID == "TRLA_208h")
					{
						return new TRL_Dungeon_Boss_208h();
					}
				}
			}
			else if (opposingHeroCardID == "TRLA_203h")
			{
				return new TRL_Dungeon_Boss_203h();
			}
		}
		else if (num != 4113973099U)
		{
			if (num != 4248782431U)
			{
				if (num == 4281602194U)
				{
					if (opposingHeroCardID == "TRLA_201h")
					{
						return new TRL_Dungeon_Boss_201h();
					}
				}
			}
			else if (opposingHeroCardID == "TRLA_204h")
			{
				return new TRL_Dungeon_Boss_204h();
			}
		}
		else if (opposingHeroCardID == "TRLA_200h")
		{
			return new TRL_Dungeon_Boss_200h();
		}
		Log.All.PrintError("TRL_Dungeon.InstantiateTRLDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new TRL_Dungeon();
	}

	// Token: 0x060039DC RID: 14812 RVA: 0x00126930 File Offset: 0x00124B30
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_04
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x001269C8 File Offset: 0x00124BC8
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(TRL_Dungeon.s_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_deathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_deathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x00126A64 File Offset: 0x00124C64
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.GREETINGS)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineGreeting, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.WOW)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineWow, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.THREATEN)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineThreaten, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.WELL_PLAYED)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineWellPlayed, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.THANKS)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineThanks, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.OOPS)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineOops, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (emoteType == EmoteType.SORRY)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TRL_Dungeon.s_responseLineSorry, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x00126BCC File Offset: 0x00124DCC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = null;
		TRL_Dungeon.s_responseLineGreeting = null;
		TRL_Dungeon.s_responseLineWow = null;
		TRL_Dungeon.s_responseLineThreaten = null;
		TRL_Dungeon.s_responseLineWellPlayed = null;
		TRL_Dungeon.s_responseLineThanks = null;
		TRL_Dungeon.s_responseLineOops = null;
		TRL_Dungeon.s_responseLineSorry = null;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>();
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>();
		TRL_Dungeon.s_druidShrineDeathLines = new List<string>();
		TRL_Dungeon.s_hunterShrineDeathLines = new List<string>();
		TRL_Dungeon.s_mageShrineDeathLines = new List<string>();
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string>();
		TRL_Dungeon.s_priestShrineDeathLines = new List<string>();
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>();
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>();
		TRL_Dungeon.s_warlockShrineDeathLines = new List<string>();
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string>();
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060039E1 RID: 14817 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x00126C7D File Offset: 0x00124E7D
	private IEnumerator PlayRandomRikkarShrineDeathLine()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			yield break;
		}
		if (!this.CanPlayVOLines(actor.GetEntity(), GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO))
		{
			yield break;
		}
		string text = base.PopRandomLineWithChance(TRL_Dungeon.m_RikkarRandomLines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060039E3 RID: 14819 RVA: 0x00126C8C File Offset: 0x00124E8C
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x00126CAC File Offset: 0x00124EAC
	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x00126CE2 File Offset: 0x00124EE2
	private IEnumerator PlayClassOrGenericShrineDeathLine(List<string> classDeathLines)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string text = base.PopRandomLineWithChance(classDeathLines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		else
		{
			text = base.PopRandomLineWithChance(TRL_Dungeon.s_genericShrineDeathLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x00126CF8 File Offset: 0x00124EF8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 150:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_genericShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 151:
			break;
		case 152:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_druidShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 153:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_hunterShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 154:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_mageShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 155:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_paladinShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 156:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_priestShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 157:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_rogueShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 158:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_shamanShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 159:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_warlockShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		case 160:
			yield return this.PlayClassOrGenericShrineDeathLine(TRL_Dungeon.s_warriorShrineDeathLines);
			yield return this.PlayRandomRikkarShrineDeathLine();
			goto IL_3FB;
		default:
			if (missionEvent == 202)
			{
				string text = base.PopRandomLineWithChance(TRL_Dungeon.s_bossShrineDeathLines);
				if (text != null)
				{
					yield return base.PlayBossLine(actor, text, 2.5f);
					goto IL_3FB;
				}
				goto IL_3FB;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3FB:
		yield break;
	}

	// Token: 0x04002011 RID: 8209
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_01.prefab:6310971ec24c8b04c855b61ab0de2ee7");

	// Token: 0x04002012 RID: 8210
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_02.prefab:69df2bf9c601e064ebe4b0fcc9b7bff0");

	// Token: 0x04002013 RID: 8211
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_03.prefab:80a489f9a60de644d94dd836da122249");

	// Token: 0x04002014 RID: 8212
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_04.prefab:8f1be59482206f046a5bc90d77e5a215");

	// Token: 0x04002015 RID: 8213
	public static float f_shrineKillPlayRate = 1f;

	// Token: 0x04002016 RID: 8214
	public static string s_deathLine = null;

	// Token: 0x04002017 RID: 8215
	public static string s_responseLineGreeting = null;

	// Token: 0x04002018 RID: 8216
	public static string s_responseLineWow = null;

	// Token: 0x04002019 RID: 8217
	public static string s_responseLineThreaten = null;

	// Token: 0x0400201A RID: 8218
	public static string s_responseLineWellPlayed = null;

	// Token: 0x0400201B RID: 8219
	public static string s_responseLineThanks = null;

	// Token: 0x0400201C RID: 8220
	public static string s_responseLineOops = null;

	// Token: 0x0400201D RID: 8221
	public static string s_responseLineSorry = null;

	// Token: 0x0400201E RID: 8222
	public static List<string> s_bossShrineDeathLines = new List<string>();

	// Token: 0x0400201F RID: 8223
	public static List<string> s_genericShrineDeathLines = new List<string>();

	// Token: 0x04002020 RID: 8224
	public static List<string> s_druidShrineDeathLines = new List<string>();

	// Token: 0x04002021 RID: 8225
	public static List<string> s_hunterShrineDeathLines = new List<string>();

	// Token: 0x04002022 RID: 8226
	public static List<string> s_mageShrineDeathLines = new List<string>();

	// Token: 0x04002023 RID: 8227
	public static List<string> s_paladinShrineDeathLines = new List<string>();

	// Token: 0x04002024 RID: 8228
	public static List<string> s_priestShrineDeathLines = new List<string>();

	// Token: 0x04002025 RID: 8229
	public static List<string> s_rogueShrineDeathLines = new List<string>();

	// Token: 0x04002026 RID: 8230
	public static List<string> s_shamanShrineDeathLines = new List<string>();

	// Token: 0x04002027 RID: 8231
	public static List<string> s_warlockShrineDeathLines = new List<string>();

	// Token: 0x04002028 RID: 8232
	public static List<string> s_warriorShrineDeathLines = new List<string>();

	// Token: 0x04002029 RID: 8233
	protected static List<string> m_RikkarRandomLines = new List<string>
	{
		TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_01,
		TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_02,
		TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_03,
		TRL_Dungeon.VO_TRLA_209h_Male_Troll_Shrine_Killed_04
	};
}
