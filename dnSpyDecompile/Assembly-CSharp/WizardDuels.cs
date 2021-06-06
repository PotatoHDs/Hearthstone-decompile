using System;
using Hearthstone.DataModels;
using PegasusUtil;

// Token: 0x0200057F RID: 1407
public class WizardDuels : StandardGameEntity
{
	// Token: 0x06004E5A RID: 20058 RVA: 0x0019DB46 File Offset: 0x0019BD46
	public override void OnCreate()
	{
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse), null);
		this.GetPVPDRDataModel();
	}

	// Token: 0x06004E5B RID: 20059 RVA: 0x0019DB71 File Offset: 0x0019BD71
	public override void StartGameplaySoundtracks()
	{
		if (this.m_pvpdrDataModel != null && this.m_pvpdrDataModel.Wins >= 9)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_SCH_FinalLevels);
			return;
		}
		base.StartGameplaySoundtracks();
	}

	// Token: 0x06004E5C RID: 20060 RVA: 0x0019DBA1 File Offset: 0x0019BDA1
	public override void StartMulliganSoundtracks(bool soft)
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_SCH_Mulligan);
	}

	// Token: 0x06004E5D RID: 20061 RVA: 0x0019DBB3 File Offset: 0x0019BDB3
	private PVPDRLobbyDataModel GetPVPDRDataModel()
	{
		if (this.m_pvpdrDataModel != null)
		{
			return this.m_pvpdrDataModel;
		}
		Network.Get().SendPVPDRSessionInfoRequest();
		return null;
	}

	// Token: 0x06004E5E RID: 20062 RVA: 0x0019DBD0 File Offset: 0x0019BDD0
	private void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pvpdrsessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pvpdrsessionInfoResponse.HasSession)
		{
			this.m_pvpdrDataModel = new PVPDRLobbyDataModel();
			this.m_pvpdrDataModel.Wins = (int)pvpdrsessionInfoResponse.Session.Wins;
			this.m_pvpdrDataModel.Losses = (int)pvpdrsessionInfoResponse.Session.Losses;
			this.m_pvpdrDataModel.HasSession = pvpdrsessionInfoResponse.Session.HasSession;
			this.m_pvpdrDataModel.IsSessionActive = pvpdrsessionInfoResponse.Session.IsActive;
			this.m_pvpdrDataModel.IsPaidEntry = pvpdrsessionInfoResponse.Session.IsPaidEntry;
		}
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse));
	}

	// Token: 0x04004525 RID: 17701
	private PVPDRLobbyDataModel m_pvpdrDataModel;
}
