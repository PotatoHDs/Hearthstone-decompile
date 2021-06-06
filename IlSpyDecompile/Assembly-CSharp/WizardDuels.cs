using Hearthstone.DataModels;
using PegasusUtil;

public class WizardDuels : StandardGameEntity
{
	private PVPDRLobbyDataModel m_pvpdrDataModel;

	public override void OnCreate()
	{
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
		GetPVPDRDataModel();
	}

	public override void StartGameplaySoundtracks()
	{
		if (m_pvpdrDataModel != null && m_pvpdrDataModel.Wins >= 9)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_SCH_FinalLevels);
		}
		else
		{
			base.StartGameplaySoundtracks();
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_SCH_Mulligan);
	}

	private PVPDRLobbyDataModel GetPVPDRDataModel()
	{
		if (m_pvpdrDataModel != null)
		{
			return m_pvpdrDataModel;
		}
		Network.Get().SendPVPDRSessionInfoRequest();
		return null;
	}

	private void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pVPDRSessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pVPDRSessionInfoResponse.HasSession)
		{
			m_pvpdrDataModel = new PVPDRLobbyDataModel();
			m_pvpdrDataModel.Wins = (int)pVPDRSessionInfoResponse.Session.Wins;
			m_pvpdrDataModel.Losses = (int)pVPDRSessionInfoResponse.Session.Losses;
			m_pvpdrDataModel.HasSession = pVPDRSessionInfoResponse.Session.HasSession;
			m_pvpdrDataModel.IsSessionActive = pVPDRSessionInfoResponse.Session.IsActive;
			m_pvpdrDataModel.IsPaidEntry = pVPDRSessionInfoResponse.Session.IsPaidEntry;
		}
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
	}
}
