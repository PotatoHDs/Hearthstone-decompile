using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006CC RID: 1740
public class RitualSpellController : SpellController
{
	// Token: 0x0600617F RID: 24959 RVA: 0x001FD8B0 File Offset: 0x001FBAB0
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		Entity sourceEntity = taskList.GetSourceEntity(true);
		Player controller = sourceEntity.GetController();
		if (taskList.IsOrigin())
		{
			return true;
		}
		if (!this.AllowedToSeeCthun(taskList))
		{
			return false;
		}
		if (!taskList.IsEndOfBlock())
		{
			return false;
		}
		if (this.m_skipIfCthunInPlay && this.IsCthunInPlay(controller))
		{
			return false;
		}
		this.m_ritualEntity = this.GetProxyEntityFromSourceEntity(sourceEntity);
		if (this.m_ritualEntity == null)
		{
			return false;
		}
		PowerTaskList origin = taskList.GetOrigin();
		this.m_ritualEntityClone = origin.GetRitualEntityClone();
		if (this.m_ritualEntityClone == null)
		{
			return false;
		}
		Card card = sourceEntity.GetCard();
		base.SetSource(card);
		Card card2 = this.m_ritualEntity.GetCard();
		base.AddTarget(card2);
		return true;
	}

	// Token: 0x06006180 RID: 24960 RVA: 0x001FD956 File Offset: 0x001FBB56
	protected override void OnProcessTaskList()
	{
		base.StartCoroutine(this.DoRitualEffect());
	}

	// Token: 0x06006181 RID: 24961 RVA: 0x001FD968 File Offset: 0x001FBB68
	protected bool AllowedToSeeCthun(PowerTaskList taskList)
	{
		Entity sourceEntity = taskList.GetSourceEntity(true);
		Player controller = sourceEntity.GetController();
		if (sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN))
		{
			int numCthunPiecesPlayed = this.GetNumCthunPiecesPlayed(controller);
			if (numCthunPiecesPlayed >= 4)
			{
				return false;
			}
			if (numCthunPiecesPlayed == this.GetLastProgress(controller))
			{
				return false;
			}
		}
		else if (!controller.HasTag(GAME_TAG.SEEN_CTHUN))
		{
			return false;
		}
		return true;
	}

	// Token: 0x06006182 RID: 24962 RVA: 0x001FD9BC File Offset: 0x001FBBBC
	protected int GetNumCthunPiecesPlayed(Player controller)
	{
		if (controller == null)
		{
			return 0;
		}
		int num = 0;
		if (controller.GetTag(GAME_TAG.PLAYED_CTHUN_EYE) != 0)
		{
			num++;
		}
		if (controller.GetTag(GAME_TAG.PLAYED_CTHUN_BODY) != 0)
		{
			num++;
		}
		if (controller.GetTag(GAME_TAG.PLAYED_CTHUN_MAW) != 0)
		{
			num++;
		}
		if (controller.GetTag(GAME_TAG.PLAYED_CTHUN_HEART) != 0)
		{
			num++;
		}
		return num;
	}

	// Token: 0x06006183 RID: 24963 RVA: 0x001FDA15 File Offset: 0x001FBC15
	private IEnumerator DoRitualEffect()
	{
		Entity sourceEntity = this.m_taskList.GetSourceEntity(true);
		Player sourceController = sourceEntity.GetController();
		bool isCthunShattered = sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN);
		if (this.m_taskList.IsOrigin())
		{
			int num = this.FindLatestProxyCreationTask(isCthunShattered ? GAME_TAG.PROXY_CTHUN_SHATTERED : GAME_TAG.PROXY_CTHUN);
			if (num >= 0)
			{
				if (isCthunShattered)
				{
					this.UpdateLastProgress(sourceController);
				}
				List<PowerTask> taskList = this.m_taskList.GetTaskList();
				PowerTask latestCreationTask = taskList[num];
				this.m_taskList.DoTasks(0, num + 1);
				while (!latestCreationTask.IsCompleted())
				{
					yield return null;
				}
				latestCreationTask = null;
			}
			this.m_ritualEntity = this.GetProxyEntityFromSourceEntity(sourceEntity);
			this.m_ritualEntityClone = this.m_ritualEntity.CloneForHistory(null);
			this.m_taskList.SetRitualEntityClone(this.m_ritualEntityClone);
			if (!this.m_taskList.IsEndOfBlock())
			{
				this.FinishRitual();
				yield break;
			}
			if (!this.AllowedToSeeCthun(this.m_taskList))
			{
				this.FinishRitual();
				yield break;
			}
		}
		this.m_taskList.DoAllTasks();
		while (!this.m_taskList.IsComplete())
		{
			yield return null;
		}
		HistoryManager historyManager = HistoryManager.Get();
		int ritualEntId = base.GetSource().GetEntity().GetEntityId();
		while (historyManager.HasBigCard() && historyManager.GetCurrentBigCard().GetEntity().GetEntityId() == ritualEntId)
		{
			yield return null;
		}
		Entity entity = this.m_showBonusAnims ? this.m_ritualEntityClone : this.m_ritualEntity;
		this.m_ritualActor = this.LoadRitualActor(entity);
		if (this.m_ritualActor == null)
		{
			this.FinishRitual();
			yield break;
		}
		this.UpdateAndPositionRitualActor();
		if (this.m_ritualSpell != null)
		{
			if (isCthunShattered)
			{
				yield return new WaitForSeconds(this.m_cthunShatteredDelay);
			}
			Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_ritualSpell);
			spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnRitualSpellStateFinished), this.m_ritualActor);
			spell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
			spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished));
			sourceEntity.SetTag(GAME_TAG.DATABASE_ID, GameUtils.TranslateCardIdToDbId(sourceEntity.GetCardId(), false));
			spell.AddTarget(sourceEntity.GetCard().gameObject);
			TransformUtil.AttachAndPreserveLocalTransform(spell.transform, this.m_ritualActor.transform);
			this.m_ritualActor.GetHealthText().RenderQueue = 1;
			this.m_ritualActor.GetAttackText().RenderQueue = 1;
			spell.Activate();
			this.m_progressText.Text = string.Format("{0}/4", this.GetNumCthunPiecesPlayed(sourceController));
		}
		if (this.m_showBonusAnims)
		{
			yield return new WaitForSeconds(this.m_prebuffDisplayTime);
			if (!this.m_finished)
			{
				this.m_ritualActor.SetEntity(this.m_ritualEntity);
				if (!this.m_ritualEntityClone.HasTag(GAME_TAG.TAUNT) && this.m_ritualEntity.HasTag(GAME_TAG.TAUNT))
				{
					this.m_ritualActor.ActivateTaunt();
				}
				this.m_ritualActor.UpdateAllComponents();
			}
		}
		if (this.m_ritualSpell == null)
		{
			float seconds = this.m_showBonusAnims ? Mathf.Max(0f, this.m_noSpellDisplayTime - this.m_prebuffDisplayTime) : this.m_noSpellDisplayTime;
			yield return new WaitForSeconds(seconds);
			this.m_ritualActor.Destroy();
			this.FinishRitual();
		}
		yield break;
	}

	// Token: 0x06006184 RID: 24964 RVA: 0x001FDA24 File Offset: 0x001FBC24
	private int FindLatestProxyCreationTask(GAME_TAG proxyTag)
	{
		int num = -1;
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			Network.PowerType type = power.Type;
			if (type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Tag == (int)proxyTag && histTagChange.Value > 0 && i > num)
				{
					num = i;
				}
			}
			else if (type == Network.PowerType.FULL_ENTITY && i > num)
			{
				num = i;
			}
		}
		return num;
	}

	// Token: 0x06006185 RID: 24965 RVA: 0x001FDA9A File Offset: 0x001FBC9A
	public void OnSpellFinished(Spell spell, object userData)
	{
		this.OnFinishedTaskList();
	}

	// Token: 0x06006186 RID: 24966 RVA: 0x001FDAA4 File Offset: 0x001FBCA4
	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		Entity sourceEntity = this.m_taskList.GetSourceEntity(true);
		Player controller = sourceEntity.GetController();
		bool flag = sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN);
		if (eventName != "showCthun")
		{
			Debug.LogError("RitualSpellController received unexpected Spell Event " + eventName);
		}
		if (this.m_hideRitualActor)
		{
			this.m_ritualActor.Show();
			if (flag)
			{
				this.UpdateLastProgress(controller);
				this.m_progressText.gameObject.SetActive(true);
			}
			if (this.m_tauntSpellInstance != null)
			{
				this.m_tauntSpellInstance.Activate();
			}
		}
	}

	// Token: 0x06006187 RID: 24967 RVA: 0x001FDB33 File Offset: 0x001FBD33
	private void OnRitualSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			((Actor)userData).Destroy();
			this.FinishRitual();
		}
	}

	// Token: 0x06006188 RID: 24968 RVA: 0x001FDB4E File Offset: 0x001FBD4E
	private void FinishRitual()
	{
		this.m_finished = true;
		if (this.m_processingTaskList)
		{
			this.OnFinishedTaskList();
		}
		this.OnFinished();
	}

	// Token: 0x06006189 RID: 24969 RVA: 0x001FDB6C File Offset: 0x001FBD6C
	private Actor LoadRitualActor(Entity entity)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetZoneActor(entity, TAG_ZONE.PLAY), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning("RitualSpellController unable to load Ritual Actor GameObject.");
			return null;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning("RitualSpellController Ritual Actor GameObject contains no Actor component.");
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		component.SetEntity(entity);
		component.SetCardDefFromEntity(entity);
		return component;
	}

	// Token: 0x0600618A RID: 24970 RVA: 0x001FDBD8 File Offset: 0x001FBDD8
	private void UpdateAndPositionRitualActor()
	{
		if (this.m_ritualActor.GetEntity().HasTag(GAME_TAG.TAUNT))
		{
			Spell spell = (this.m_ritualEntity.GetPremiumType() == TAG_PREMIUM.NORMAL) ? this.m_tauntInstantSpell : this.m_tauntInstantPremiumSpell;
			if (spell != null)
			{
				this.m_tauntSpellInstance = UnityEngine.Object.Instantiate<Spell>(spell);
				TransformUtil.AttachAndPreserveLocalTransform(this.m_tauntSpellInstance.transform, this.m_ritualActor.transform);
				if (!this.m_hideRitualActor)
				{
					this.m_tauntSpellInstance.Activate();
				}
			}
			else
			{
				Debug.LogWarning("RitualSpellController does not have a instant taunt spell hooked up.");
			}
		}
		this.m_ritualActor.UpdateMinionStatsImmediately();
		if (this.m_hideRitualActor)
		{
			this.m_ritualActor.Hide();
		}
		string name = (this.m_ritualActor.GetEntity().GetControllerSide() == Player.Side.FRIENDLY) ? this.m_friendlyRitualBoneName : this.m_opponentRitualBoneName;
		Transform parent = Board.Get().FindBone(name);
		this.m_ritualActor.transform.parent = parent;
		this.m_ritualActor.transform.localPosition = Vector3.zero;
		this.m_progressText.transform.parent = this.m_ritualActor.gameObject.transform;
		this.m_progressText.gameObject.transform.localPosition = this.m_progressTextOffset;
	}

	// Token: 0x0600618B RID: 24971 RVA: 0x001FDD14 File Offset: 0x001FBF14
	private bool IsCthunInPlay(Player player)
	{
		foreach (Card card in player.GetBattlefieldZone().GetCards())
		{
			if (card.GetController() == player && card.GetEntity().GetCardId() == "OG_280")
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600618C RID: 24972 RVA: 0x001FDD8C File Offset: 0x001FBF8C
	private Entity GetProxyEntityFromSourceEntity(Entity sourceEntity)
	{
		Player controller = sourceEntity.GetController();
		int tag;
		if (sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN))
		{
			tag = controller.GetTag(GAME_TAG.PROXY_CTHUN_SHATTERED);
		}
		else
		{
			tag = controller.GetTag(GAME_TAG.PROXY_CTHUN);
		}
		if (tag == 0)
		{
			return null;
		}
		return GameState.Get().GetEntity(tag);
	}

	// Token: 0x0600618D RID: 24973 RVA: 0x001FDDDC File Offset: 0x001FBFDC
	private int GetLastProgress(Player player)
	{
		int result = 0;
		RitualSpellController.s_lastProgressMap.TryGetValue(player.GetEntityId(), out result);
		return result;
	}

	// Token: 0x0600618E RID: 24974 RVA: 0x001FDE00 File Offset: 0x001FC000
	private void UpdateLastProgress(Player player)
	{
		int entityId = player.GetEntityId();
		if (RitualSpellController.s_lastProgressMap.ContainsKey(entityId))
		{
			RitualSpellController.s_lastProgressMap[entityId] = this.GetNumCthunPiecesPlayed(player);
			return;
		}
		RitualSpellController.s_lastProgressMap.Add(entityId, this.GetNumCthunPiecesPlayed(player));
	}

	// Token: 0x0400514C RID: 20812
	public Spell m_ritualSpell;

	// Token: 0x0400514D RID: 20813
	public float m_noSpellDisplayTime = 3f;

	// Token: 0x0400514E RID: 20814
	public string m_friendlyRitualBoneName = "FriendlyRitual";

	// Token: 0x0400514F RID: 20815
	public string m_opponentRitualBoneName = "OpponentRitual";

	// Token: 0x04005150 RID: 20816
	public bool m_hideRitualActor = true;

	// Token: 0x04005151 RID: 20817
	public Spell m_tauntInstantSpell;

	// Token: 0x04005152 RID: 20818
	public Spell m_tauntInstantPremiumSpell;

	// Token: 0x04005153 RID: 20819
	public bool m_skipIfCthunInPlay;

	// Token: 0x04005154 RID: 20820
	public bool m_showBonusAnims;

	// Token: 0x04005155 RID: 20821
	public float m_prebuffDisplayTime = 1f;

	// Token: 0x04005156 RID: 20822
	private Entity m_ritualEntity;

	// Token: 0x04005157 RID: 20823
	private Entity m_ritualEntityClone;

	// Token: 0x04005158 RID: 20824
	private bool m_finished;

	// Token: 0x04005159 RID: 20825
	private Actor m_ritualActor;

	// Token: 0x0400515A RID: 20826
	private Spell m_tauntSpellInstance;

	// Token: 0x0400515B RID: 20827
	public UberText m_progressText;

	// Token: 0x0400515C RID: 20828
	public Vector3 m_progressTextOffset;

	// Token: 0x0400515D RID: 20829
	public float m_cthunShatteredDelay;

	// Token: 0x0400515E RID: 20830
	private static Map<int, int> s_lastProgressMap = new Map<int, int>();
}
