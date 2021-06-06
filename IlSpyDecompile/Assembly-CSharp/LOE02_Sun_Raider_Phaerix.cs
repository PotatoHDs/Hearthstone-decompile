using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE02_Sun_Raider_Phaerix : LOE_MissionEntity
{
	private int m_staffLinesPlayed;

	private bool m_damageLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_01_RESPONSE.prefab:003ddb96a133c634b8f74c8a9ef1e55c");
		PreloadSound("VO_LOE_01_WOUNDED.prefab:0fb9c01bbbbacd0408fb478d13b9574b");
		PreloadSound("VO_LOE_01_STAFF.prefab:b412b19ab6e0def45a74aeb7ebb60ec1");
		PreloadSound("VO_LOE_01_STAFF_2.prefab:587ffb164487ac0429b1dac0ca33b9aa");
		PreloadSound("VO_LOE_02_PHAERIX_STAFF_RECOVER.prefab:bdbf17959a28fa247976168e5d545f5d");
		PreloadSound("VO_LOE_01_STAFF_2_RENO.prefab:81e1aae8f257ed448bcdbd89ea881fc5");
		PreloadSound("VO_LOE_01_WIN_2.prefab:e8acaf5e90b7f7a419739aba63b6f8bc");
		PreloadSound("VO_LOE_01_WIN_2_ALT_2.prefab:4d3c656d8071a8e438cd2fc2f4d5b862");
		PreloadSound("VO_LOE_01_START.prefab:e4840e7e825b2aa438f783b50bb19584");
		PreloadSound("VO_LOE_01_WIN.prefab:ece5a717571dc164db373500aed7a707");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_LOE_01_RESPONSE.prefab:003ddb96a133c634b8f74c8a9ef1e55c",
						m_stringTag = "VO_LOE_01_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (m_staffLinesPlayed >= missionEvent)
		{
			yield break;
		}
		if (missionEvent > 9)
		{
			if (!m_damageLinePlayed)
			{
				m_damageLinePlayed = true;
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_WOUNDED.prefab:0fb9c01bbbbacd0408fb478d13b9574b"));
			}
			yield break;
		}
		m_staffLinesPlayed = missionEvent;
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_STAFF.prefab:b412b19ab6e0def45a74aeb7ebb60ec1"));
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_01_STAFF_2.prefab:587ffb164487ac0429b1dac0ca33b9aa", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_02_PHAERIX_STAFF_RECOVER.prefab:bdbf17959a28fa247976168e5d545f5d", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_STAFF_2_RENO.prefab:81e1aae8f257ed448bcdbd89ea881fc5"));
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOE_01_START.prefab:e4840e7e825b2aa438f783b50bb19584", Notification.SpeechBubbleDirection.TopRight, actor));
			yield return new WaitForSeconds(4f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_WIN_2.prefab:e8acaf5e90b7f7a419739aba63b6f8bc"));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_01_WIN.prefab:ece5a717571dc164db373500aed7a707", 0f, allowRepeatDuringSession: false));
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_01_WIN_2_ALT_2.prefab:4d3c656d8071a8e438cd2fc2f4d5b862", 0f, allowRepeatDuringSession: false));
		}
	}
}
