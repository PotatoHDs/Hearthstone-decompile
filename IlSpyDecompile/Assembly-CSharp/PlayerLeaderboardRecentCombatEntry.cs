using Blizzard.T5.AssetManager;
using UnityEngine;

public class PlayerLeaderboardRecentCombatEntry : MonoBehaviour
{
	public enum RecentActionType
	{
		DAMAGE,
		DEATH
	}

	public enum RecentActionTarget
	{
		OWNER,
		OPPONENT,
		TIE
	}

	public Actor m_opponentTileActor;

	public GameObject m_iconOwnerSwords;

	public GameObject m_iconOpponentSwords;

	public GameObject m_iconOwnerSplat;

	public GameObject m_iconOpponentSplat;

	public GameObject m_background;

	public GameObject m_opponentMeshRoot;

	private RecentActionType m_recentActionType;

	private RecentActionTarget m_recentActionTarget;

	private int m_ownerId;

	private int m_opponentId;

	private int m_splatAmount;

	private PlayerLeaderboardCard m_source;

	private const float TILE_PORTRAIT_MESH_Y_OFFSET = 0.01f;

	private const float TILE_Y_OFFSET = -0.5f;

	private PlayerLeaderboardTile m_opponentLeaderboardTile => m_opponentTileActor.GetComponent<PlayerLeaderboardTile>();

	public void Awake()
	{
		m_opponentMeshRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		m_opponentMeshRoot.transform.localPosition = new Vector3(m_opponentMeshRoot.transform.localPosition.x, 0.01f, m_opponentMeshRoot.transform.localPosition.z);
		m_opponentTileActor.transform.localPosition = new Vector3(m_opponentTileActor.transform.localPosition.x, -0.5f, m_opponentTileActor.transform.localPosition.z);
	}

	public void OnDestroy()
	{
	}

	private void SetActionTarget(RecentActionTarget target)
	{
		m_recentActionTarget = target;
	}

	private void SetActionType(RecentActionType type)
	{
		m_recentActionType = type;
	}

	private void SetSplatAmount(int splatAmount)
	{
		m_splatAmount = -splatAmount;
	}

	public void Load(PlayerLeaderboardCard source, PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo)
	{
		m_source = source;
		m_ownerId = recentCombatInfo.ownerId;
		m_opponentId = recentCombatInfo.opponentId;
		SetActionTarget((recentCombatInfo.damageTarget != m_ownerId) ? ((recentCombatInfo.damageTarget == m_opponentId) ? RecentActionTarget.OPPONENT : RecentActionTarget.TIE) : RecentActionTarget.OWNER);
		SetActionType(recentCombatInfo.isDefeated ? RecentActionType.DEATH : RecentActionType.DAMAGE);
		SetSplatAmount(recentCombatInfo.damage);
		LoadTileForPlayer(m_ownerId);
		LoadTileForPlayer(m_opponentId);
		UpdateDisplay();
		SceneUtils.SetLayer(base.gameObject, GameLayer.Tooltip);
	}

	private void UpdateDisplay()
	{
		m_iconOwnerSwords.SetActive(m_recentActionTarget == RecentActionTarget.OPPONENT || m_recentActionTarget == RecentActionTarget.TIE);
		m_iconOpponentSwords.SetActive(m_recentActionTarget == RecentActionTarget.OWNER);
		m_iconOwnerSplat.SetActive(m_recentActionTarget == RecentActionTarget.OPPONENT || m_recentActionTarget == RecentActionTarget.TIE);
		m_iconOpponentSplat.SetActive(m_recentActionTarget == RecentActionTarget.OWNER);
		m_opponentLeaderboardTile.SetSkullIconActive(m_recentActionTarget == RecentActionTarget.OPPONENT && m_recentActionType == RecentActionType.DEATH);
		m_opponentLeaderboardTile.SetHealthBarActive(active: false);
		GameObject splatIcon = ((m_recentActionTarget == RecentActionTarget.OPPONENT || m_recentActionTarget == RecentActionTarget.TIE) ? m_iconOwnerSplat : m_iconOpponentSplat);
		UpdateSplatSpell(splatIcon);
	}

	private void UpdateSplatSpell(GameObject splatIcon)
	{
		DamageSplatSpell component = splatIcon.GetComponent<DamageSplatSpell>();
		component.SetDamage(-m_splatAmount);
		component.ChangeState(SpellStateType.IDLE);
		component.Show();
	}

	private void LoadTileForPlayer(int playerId)
	{
		Actor opponentTileActor = m_opponentTileActor;
		Entity entity = null;
		entity = ((playerId != 0) ? GameState.Get().GetPlayerInfoMap()[playerId].GetPlayerHero() : PlayerLeaderboardManager.Get().GetOddManOutOpponentHero());
		if (entity == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatEntry.LoadTileForPlayer() - FAILED to load playerHeroEntity for playerId \"{0}\"", playerId);
			return;
		}
		DefLoader.DisposableCardDef disposableCardDef = entity.ShareDisposableCardDef();
		if (disposableCardDef == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatEntry.LoadTileForPlayer() - FAILED to load cardDef for playerId \"{0}\"", playerId);
			return;
		}
		Material[] array = new Material[2];
		HearthstoneServices.Get<DisposablesCleaner>()?.Attach(opponentTileActor.gameObject, disposableCardDef);
		array[0] = opponentTileActor.GetMeshRenderer().GetMaterial();
		if (disposableCardDef.CardDef.GetLeaderboardTileFullPortrait() != null)
		{
			array[1] = disposableCardDef.CardDef.GetLeaderboardTileFullPortrait();
			opponentTileActor.GetMeshRenderer().SetMaterials(array);
		}
		else if (disposableCardDef.CardDef.GetHistoryTileFullPortrait() != null)
		{
			array[1] = disposableCardDef.CardDef.GetHistoryTileFullPortrait();
			opponentTileActor.GetMeshRenderer().SetMaterials(array);
		}
		else
		{
			opponentTileActor.GetMeshRenderer().GetMaterial(1).mainTexture = disposableCardDef.CardDef.GetPortraitTexture();
		}
		Renderer[] componentsInChildren = opponentTileActor.GetMeshRenderer().GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		opponentTileActor.GetMeshRenderer().GetMaterial(1).color = Board.Get().m_HistoryTileColor;
		Color color = ((GameState.Get().GetFriendlyPlayerId() == playerId) ? m_source.m_selfBorderColor : m_source.m_enemyBorderColor);
		SetBorderColor(PlayerIsDead(playerId) ? m_source.m_deadColor : color, opponentTileActor);
	}

	private void SetBorderColor(Color color, Actor targetTile)
	{
		targetTile.GetMeshRenderer().GetMaterial().color = color;
	}

	private bool PlayerIsDead(int playerId)
	{
		if (m_recentActionType == RecentActionType.DEATH)
		{
			if (m_recentActionTarget != RecentActionTarget.OPPONENT || playerId != m_opponentId)
			{
				if (m_recentActionTarget == RecentActionTarget.OWNER)
				{
					return playerId == m_ownerId;
				}
				return false;
			}
			return true;
		}
		return false;
	}
}
