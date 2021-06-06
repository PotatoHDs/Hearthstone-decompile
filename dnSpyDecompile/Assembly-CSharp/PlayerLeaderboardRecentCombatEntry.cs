using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class PlayerLeaderboardRecentCombatEntry : MonoBehaviour
{
	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000F1783 File Offset: 0x000EF983
	private PlayerLeaderboardTile m_opponentLeaderboardTile
	{
		get
		{
			return this.m_opponentTileActor.GetComponent<PlayerLeaderboardTile>();
		}
	}

	// Token: 0x06002F50 RID: 12112 RVA: 0x000F1790 File Offset: 0x000EF990
	public void Awake()
	{
		this.m_opponentMeshRoot.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this.m_opponentMeshRoot.transform.localPosition = new Vector3(this.m_opponentMeshRoot.transform.localPosition.x, 0.01f, this.m_opponentMeshRoot.transform.localPosition.z);
		this.m_opponentTileActor.transform.localPosition = new Vector3(this.m_opponentTileActor.transform.localPosition.x, -0.5f, this.m_opponentTileActor.transform.localPosition.z);
	}

	// Token: 0x06002F51 RID: 12113 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnDestroy()
	{
	}

	// Token: 0x06002F52 RID: 12114 RVA: 0x000F184E File Offset: 0x000EFA4E
	private void SetActionTarget(PlayerLeaderboardRecentCombatEntry.RecentActionTarget target)
	{
		this.m_recentActionTarget = target;
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x000F1857 File Offset: 0x000EFA57
	private void SetActionType(PlayerLeaderboardRecentCombatEntry.RecentActionType type)
	{
		this.m_recentActionType = type;
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x000F1860 File Offset: 0x000EFA60
	private void SetSplatAmount(int splatAmount)
	{
		this.m_splatAmount = -splatAmount;
	}

	// Token: 0x06002F55 RID: 12117 RVA: 0x000F186C File Offset: 0x000EFA6C
	public void Load(PlayerLeaderboardCard source, PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo)
	{
		this.m_source = source;
		this.m_ownerId = recentCombatInfo.ownerId;
		this.m_opponentId = recentCombatInfo.opponentId;
		this.SetActionTarget((recentCombatInfo.damageTarget == this.m_ownerId) ? PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OWNER : ((recentCombatInfo.damageTarget == this.m_opponentId) ? PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT : PlayerLeaderboardRecentCombatEntry.RecentActionTarget.TIE));
		this.SetActionType(recentCombatInfo.isDefeated ? PlayerLeaderboardRecentCombatEntry.RecentActionType.DEATH : PlayerLeaderboardRecentCombatEntry.RecentActionType.DAMAGE);
		this.SetSplatAmount(recentCombatInfo.damage);
		this.LoadTileForPlayer(this.m_ownerId);
		this.LoadTileForPlayer(this.m_opponentId);
		this.UpdateDisplay();
		SceneUtils.SetLayer(base.gameObject, GameLayer.Tooltip);
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x000F190C File Offset: 0x000EFB0C
	private void UpdateDisplay()
	{
		this.m_iconOwnerSwords.SetActive(this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT || this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.TIE);
		this.m_iconOpponentSwords.SetActive(this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OWNER);
		this.m_iconOwnerSplat.SetActive(this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT || this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.TIE);
		this.m_iconOpponentSplat.SetActive(this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OWNER);
		this.m_opponentLeaderboardTile.SetSkullIconActive(this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT && this.m_recentActionType == PlayerLeaderboardRecentCombatEntry.RecentActionType.DEATH);
		this.m_opponentLeaderboardTile.SetHealthBarActive(false);
		GameObject splatIcon = (this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT || this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.TIE) ? this.m_iconOwnerSplat : this.m_iconOpponentSplat;
		this.UpdateSplatSpell(splatIcon);
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x000F19D5 File Offset: 0x000EFBD5
	private void UpdateSplatSpell(GameObject splatIcon)
	{
		DamageSplatSpell component = splatIcon.GetComponent<DamageSplatSpell>();
		component.SetDamage(-this.m_splatAmount);
		component.ChangeState(SpellStateType.IDLE);
		component.Show();
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x000F19F8 File Offset: 0x000EFBF8
	private void LoadTileForPlayer(int playerId)
	{
		Actor opponentTileActor = this.m_opponentTileActor;
		Entity entity;
		if (playerId == 0)
		{
			entity = PlayerLeaderboardManager.Get().GetOddManOutOpponentHero();
		}
		else
		{
			entity = GameState.Get().GetPlayerInfoMap()[playerId].GetPlayerHero();
		}
		if (entity == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatEntry.LoadTileForPlayer() - FAILED to load playerHeroEntity for playerId \"{0}\"", new object[]
			{
				playerId
			});
			return;
		}
		DefLoader.DisposableCardDef disposableCardDef = entity.ShareDisposableCardDef();
		if (disposableCardDef == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatEntry.LoadTileForPlayer() - FAILED to load cardDef for playerId \"{0}\"", new object[]
			{
				playerId
			});
			return;
		}
		Material[] array = new Material[2];
		DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(opponentTileActor.gameObject, disposableCardDef);
		}
		array[0] = opponentTileActor.GetMeshRenderer(false).GetMaterial();
		if (disposableCardDef.CardDef.GetLeaderboardTileFullPortrait() != null)
		{
			array[1] = disposableCardDef.CardDef.GetLeaderboardTileFullPortrait();
			opponentTileActor.GetMeshRenderer(false).SetMaterials(array);
		}
		else if (disposableCardDef.CardDef.GetHistoryTileFullPortrait() != null)
		{
			array[1] = disposableCardDef.CardDef.GetHistoryTileFullPortrait();
			opponentTileActor.GetMeshRenderer(false).SetMaterials(array);
		}
		else
		{
			opponentTileActor.GetMeshRenderer(false).GetMaterial(1).mainTexture = disposableCardDef.CardDef.GetPortraitTexture();
		}
		foreach (Renderer renderer in opponentTileActor.GetMeshRenderer(false).GetComponentsInChildren<Renderer>())
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		opponentTileActor.GetMeshRenderer(false).GetMaterial(1).color = Board.Get().m_HistoryTileColor;
		Color color = (GameState.Get().GetFriendlyPlayerId() == playerId) ? this.m_source.m_selfBorderColor : this.m_source.m_enemyBorderColor;
		this.SetBorderColor(this.PlayerIsDead(playerId) ? this.m_source.m_deadColor : color, opponentTileActor);
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x000F1BD2 File Offset: 0x000EFDD2
	private void SetBorderColor(Color color, Actor targetTile)
	{
		targetTile.GetMeshRenderer(false).GetMaterial().color = color;
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x000F1BE6 File Offset: 0x000EFDE6
	private bool PlayerIsDead(int playerId)
	{
		return this.m_recentActionType == PlayerLeaderboardRecentCombatEntry.RecentActionType.DEATH && ((this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OPPONENT && playerId == this.m_opponentId) || (this.m_recentActionTarget == PlayerLeaderboardRecentCombatEntry.RecentActionTarget.OWNER && playerId == this.m_ownerId));
	}

	// Token: 0x04001A5D RID: 6749
	public Actor m_opponentTileActor;

	// Token: 0x04001A5E RID: 6750
	public GameObject m_iconOwnerSwords;

	// Token: 0x04001A5F RID: 6751
	public GameObject m_iconOpponentSwords;

	// Token: 0x04001A60 RID: 6752
	public GameObject m_iconOwnerSplat;

	// Token: 0x04001A61 RID: 6753
	public GameObject m_iconOpponentSplat;

	// Token: 0x04001A62 RID: 6754
	public GameObject m_background;

	// Token: 0x04001A63 RID: 6755
	public GameObject m_opponentMeshRoot;

	// Token: 0x04001A64 RID: 6756
	private PlayerLeaderboardRecentCombatEntry.RecentActionType m_recentActionType;

	// Token: 0x04001A65 RID: 6757
	private PlayerLeaderboardRecentCombatEntry.RecentActionTarget m_recentActionTarget;

	// Token: 0x04001A66 RID: 6758
	private int m_ownerId;

	// Token: 0x04001A67 RID: 6759
	private int m_opponentId;

	// Token: 0x04001A68 RID: 6760
	private int m_splatAmount;

	// Token: 0x04001A69 RID: 6761
	private PlayerLeaderboardCard m_source;

	// Token: 0x04001A6A RID: 6762
	private const float TILE_PORTRAIT_MESH_Y_OFFSET = 0.01f;

	// Token: 0x04001A6B RID: 6763
	private const float TILE_Y_OFFSET = -0.5f;

	// Token: 0x020016D6 RID: 5846
	public enum RecentActionType
	{
		// Token: 0x0400B226 RID: 45606
		DAMAGE,
		// Token: 0x0400B227 RID: 45607
		DEATH
	}

	// Token: 0x020016D7 RID: 5847
	public enum RecentActionTarget
	{
		// Token: 0x0400B229 RID: 45609
		OWNER,
		// Token: 0x0400B22A RID: 45610
		OPPONENT,
		// Token: 0x0400B22B RID: 45611
		TIE
	}
}
