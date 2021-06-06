using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class ICC_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x060035C7 RID: 13767 RVA: 0x00112039 File Offset: 0x00110239
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCMulligan);
		}
	}

	// Token: 0x060035C8 RID: 13768 RVA: 0x0011204E File Offset: 0x0011024E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x00112060 File Offset: 0x00110260
	public IEnumerator UnBusyInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060035CA RID: 13770 RVA: 0x00112070 File Offset: 0x00110270
	protected Actor GetActorByCardId(string cardId)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x060035CB RID: 13771 RVA: 0x00112100 File Offset: 0x00110300
	protected Actor GetLichKingFriendlyMinion()
	{
		return this.GetActorByCardId("ICC_314");
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x0011210D File Offset: 0x0011030D
	protected IEnumerator IfPlayerPlaysDKHeroVO(Entity entity, Actor actor, string voString)
	{
		if (entity.GetCardType() == TAG_CARDTYPE.HERO && entity.GetCardSet() == TAG_CARD_SET.ICECROWN)
		{
			yield return new WaitForSeconds(0.3f);
			yield return base.PlayEasterEggLine(actor, voString, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D0B RID: 7435
	public Vector3 ragLinePosition = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
}
