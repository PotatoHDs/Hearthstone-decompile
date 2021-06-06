using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_205h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Death_Long_02 = new AssetReference("VO_TRLA_205h_Female_Troll_Death_Long_02.prefab:1e3d6128dc8d05e4f8804b051fdf4159");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01.prefab:7e5e71e8c00c6cc4f9ed0fb975d94019");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01.prefab:43f03d5b2700bae49bfd062a5f77133c");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01.prefab:6ed062c3df246f6448d4f297d4bc1052");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01.prefab:3b70d01781b015c4ea71b880cf3e0188");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01.prefab:553d27a7d2db38e48958f023ed167b8f");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01.prefab:c8e87b6f20b54124e939a03e5bba39c1");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01.prefab:48ebae750b56c4641a6bbf7702b85ab8");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01.prefab:671effc28625f404084248879dc5ac40");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01.prefab:9e3d3f2dbed2c3841b938e87bce6acbc");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01.prefab:11eecb1ee68145543b0ad2b3db23f233");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01.prefab:89911ca0d25ca5d44a9f541a1b76a5e8");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01.prefab:a715d11664536d041b6d0babdc29a3bf");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01.prefab:349e60e93eb13ea4785fcd9c8b468193");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_01.prefab:42217f5ff6cce874d999458c3a5271c2");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_02.prefab:ef91a55cf28f6894aab7d5b42e3f9d45");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_03.prefab:fa2d345416b80e84ea3b26d4248e0292");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01.prefab:973d98684ef295a4fb82a0f3220d9c8c");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_Gonk_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_Gonk_01.prefab:a8259579db0e8f644925030253b40a9a");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_Hakkar_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_Hakkar_01.prefab:9e96e91a3b25b4741bd3d7f48b8e3f0f");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01.prefab:3848c58c94822f54e8d09f06ceedb8b3");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01.prefab:f28666e4c74c27c4f84709e233ed52db");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineComesBack_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineComesBack_01.prefab:34f65f3e6faa29544b3b4a345328af1f");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineComesBack_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineComesBack_02.prefab:bd173f0ef30b29a4d8135a3c4d76a20b");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01.prefab:66cb0553e4afc754392c4a4a3ee74c88");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02.prefab:053146bd504cf8349bb0f5aea18c10a3");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03.prefab:7682b5f4647bf6d46809e9696344ce42");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04.prefab:8ee9bf8fc1711cd49b6e9f1cbcbb4d93");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01.prefab:8b3b2a9face2d7e44a098be4be56296b");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02.prefab:38a9c3168b7eee9408ec24a1e2280c92");

	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03.prefab:80310a7386e0642418616804ad44038c");

	private List<string> m_WildLines = new List<string> { VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04 };

	private List<string> m_BondLines = new List<string> { VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02, VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03 };

	private List<string> m_ArmorLines = new List<string> { VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01, VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02, VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01, VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01, VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01, VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01, VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01,
			VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01, VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01, VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01, VO_TRLA_205h_Female_Troll_Shrine_Killed_01, VO_TRLA_205h_Female_Troll_Shrine_Killed_02, VO_TRLA_205h_Female_Troll_Shrine_Killed_03, VO_TRLA_205h_Female_Troll_Death_Long_02, VO_TRLA_205h_Female_Troll_ShrineComesBack_01, VO_TRLA_205h_Female_Troll_ShrineComesBack_02, VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04,
			VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01, VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02, VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03, VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01, VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02, VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03, VO_TRLA_205h_Female_Troll_Play_Gonk_01, VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01, VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01, VO_TRLA_205h_Female_Troll_Play_Hakkar_01,
			VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_205h_Female_Troll_Death_Long_02;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Shrine_Killed_01, VO_TRLA_205h_Female_Troll_Shrine_Killed_02, VO_TRLA_205h_Female_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01 };
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01 };
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01 };
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01 };
		TRL_Dungeon.s_mageShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01 };
		TRL_Dungeon.s_priestShrineDeathLines = new List<string> { VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01 };
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1001:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_ShrineComesBack_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_ShrineComesBack_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_WildLines);
			break;
		case 1005:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BondLines);
			break;
		case 1006:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_ArmorLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "UNG_926")
			{
				yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01);
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "TRL_241":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_Play_Gonk_01);
				break;
			case "TRL_223":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01);
				break;
			case "TRL_254":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01);
				break;
			case "TRL_541":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_205h_Female_Troll_Play_Hakkar_01);
				break;
			}
		}
	}
}
