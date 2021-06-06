using System.Collections;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class BaconScene : PegasusScene
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	private bool m_screenPrefabLoaded;

	private bool m_ratingInfoReceived;

	private bool m_premiumInfoReceived;

	private bool m_gameSaveDataReceived;

	private GameObject m_baconDisplayRoot;

	private BaconDisplay m_baconDisplay;

	private void Start()
	{
		Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, OnBaconRatingInfo);
		Network.Get().RequestBaconRatingInfo();
		Network.Get().RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, OnBaconPremiumStatus);
		Network.Get().RequestBattlegroundsPremiumStatus();
		GameSaveDataManager.Get().Request(GameSaveKeyId.BACON, OnGameSaveDataReceived);
		StartCoroutine(NotifySceneLoadedWhenReady());
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override bool IsUnloading()
	{
		return false;
	}

	public override void Unload()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null)
			{
				bnetBar.ToggleActive(active: true);
			}
		}
		if (m_baconDisplayRoot != null)
		{
			Object.Destroy(m_baconDisplayRoot.gameObject);
		}
	}

	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_screenPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"BaconScene.OnScreenLoaded() - failed to load screen {assetRef}");
		}
		else
		{
			m_baconDisplayRoot = go;
		}
	}

	private void OnBaconRatingInfo()
	{
		Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, OnBaconRatingInfo);
		m_ratingInfoReceived = true;
	}

	private void OnBaconPremiumStatus()
	{
		Network.Get().RemoveNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, OnBaconPremiumStatus);
		m_premiumInfoReceived = true;
	}

	private void OnGameSaveDataReceived(bool success)
	{
		m_gameSaveDataReceived = true;
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!m_ratingInfoReceived)
		{
			yield return null;
		}
		while (!m_premiumInfoReceived)
		{
			yield return null;
		}
		while (!m_gameSaveDataReceived)
		{
			yield return null;
		}
		AssetLoader.Get().InstantiatePrefab((string)m_screenPrefab, OnScreenPrefabLoaded);
		while (!m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (m_baconDisplayRoot == null)
		{
			yield return null;
		}
		while (m_baconDisplayRoot.GetComponentInChildren<BaconDisplay>() == null)
		{
			yield return null;
		}
		m_baconDisplay = m_baconDisplayRoot.GetComponentInChildren<BaconDisplay>();
		while (!m_baconDisplay.IsFinishedLoading)
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
	}
}
