using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualSpellController : SpellController
{
	public Spell m_ritualSpell;

	public float m_noSpellDisplayTime = 3f;

	public string m_friendlyRitualBoneName = "FriendlyRitual";

	public string m_opponentRitualBoneName = "OpponentRitual";

	public bool m_hideRitualActor = true;

	public Spell m_tauntInstantSpell;

	public Spell m_tauntInstantPremiumSpell;

	public bool m_skipIfCthunInPlay;

	public bool m_showBonusAnims;

	public float m_prebuffDisplayTime = 1f;

	private Entity m_ritualEntity;

	private Entity m_ritualEntityClone;

	private bool m_finished;

	private Actor m_ritualActor;

	private Spell m_tauntSpellInstance;

	public UberText m_progressText;

	public Vector3 m_progressTextOffset;

	public float m_cthunShatteredDelay;

	private static Map<int, int> s_lastProgressMap = new Map<int, int>();

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		Entity sourceEntity = taskList.GetSourceEntity();
		Player controller = sourceEntity.GetController();
		if (taskList.IsOrigin())
		{
			return true;
		}
		if (!AllowedToSeeCthun(taskList))
		{
			return false;
		}
		if (!taskList.IsEndOfBlock())
		{
			return false;
		}
		if (m_skipIfCthunInPlay && IsCthunInPlay(controller))
		{
			return false;
		}
		m_ritualEntity = GetProxyEntityFromSourceEntity(sourceEntity);
		if (m_ritualEntity == null)
		{
			return false;
		}
		PowerTaskList origin = taskList.GetOrigin();
		m_ritualEntityClone = origin.GetRitualEntityClone();
		if (m_ritualEntityClone == null)
		{
			return false;
		}
		Card card = sourceEntity.GetCard();
		SetSource(card);
		Card card2 = m_ritualEntity.GetCard();
		AddTarget(card2);
		return true;
	}

	protected override void OnProcessTaskList()
	{
		StartCoroutine(DoRitualEffect());
	}

	protected bool AllowedToSeeCthun(PowerTaskList taskList)
	{
		Entity sourceEntity = taskList.GetSourceEntity();
		Player controller = sourceEntity.GetController();
		if (sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN))
		{
			int numCthunPiecesPlayed = GetNumCthunPiecesPlayed(controller);
			if (numCthunPiecesPlayed >= 4)
			{
				return false;
			}
			if (numCthunPiecesPlayed == GetLastProgress(controller))
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

	private IEnumerator DoRitualEffect()
	{
		Entity sourceEntity = m_taskList.GetSourceEntity();
		Player sourceController = sourceEntity.GetController();
		bool isCthunShattered = sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN);
		if (m_taskList.IsOrigin())
		{
			int num = FindLatestProxyCreationTask(isCthunShattered ? GAME_TAG.PROXY_CTHUN_SHATTERED : GAME_TAG.PROXY_CTHUN);
			if (num >= 0)
			{
				if (isCthunShattered)
				{
					UpdateLastProgress(sourceController);
				}
				List<PowerTask> taskList = m_taskList.GetTaskList();
				PowerTask latestCreationTask = taskList[num];
				m_taskList.DoTasks(0, num + 1);
				while (!latestCreationTask.IsCompleted())
				{
					yield return null;
				}
			}
			m_ritualEntity = GetProxyEntityFromSourceEntity(sourceEntity);
			m_ritualEntityClone = m_ritualEntity.CloneForHistory(null);
			m_taskList.SetRitualEntityClone(m_ritualEntityClone);
			if (!m_taskList.IsEndOfBlock())
			{
				FinishRitual();
				yield break;
			}
			if (!AllowedToSeeCthun(m_taskList))
			{
				FinishRitual();
				yield break;
			}
		}
		m_taskList.DoAllTasks();
		while (!m_taskList.IsComplete())
		{
			yield return null;
		}
		HistoryManager historyManager = HistoryManager.Get();
		int ritualEntId = GetSource().GetEntity().GetEntityId();
		while (historyManager.HasBigCard() && historyManager.GetCurrentBigCard().GetEntity().GetEntityId() == ritualEntId)
		{
			yield return null;
		}
		Entity entity = (m_showBonusAnims ? m_ritualEntityClone : m_ritualEntity);
		m_ritualActor = LoadRitualActor(entity);
		if (m_ritualActor == null)
		{
			FinishRitual();
			yield break;
		}
		UpdateAndPositionRitualActor();
		if (m_ritualSpell != null)
		{
			if (isCthunShattered)
			{
				yield return new WaitForSeconds(m_cthunShatteredDelay);
			}
			Spell spell = Object.Instantiate(m_ritualSpell);
			spell.AddStateFinishedCallback(OnRitualSpellStateFinished, m_ritualActor);
			spell.AddSpellEventCallback(OnSpellEvent);
			spell.AddFinishedCallback(OnSpellFinished);
			sourceEntity.SetTag(GAME_TAG.DATABASE_ID, GameUtils.TranslateCardIdToDbId(sourceEntity.GetCardId()));
			spell.AddTarget(sourceEntity.GetCard().gameObject);
			TransformUtil.AttachAndPreserveLocalTransform(spell.transform, m_ritualActor.transform);
			m_ritualActor.GetHealthText().RenderQueue = 1;
			m_ritualActor.GetAttackText().RenderQueue = 1;
			spell.Activate();
			m_progressText.Text = $"{GetNumCthunPiecesPlayed(sourceController)}/4";
		}
		if (m_showBonusAnims)
		{
			yield return new WaitForSeconds(m_prebuffDisplayTime);
			if (!m_finished)
			{
				m_ritualActor.SetEntity(m_ritualEntity);
				if (!m_ritualEntityClone.HasTag(GAME_TAG.TAUNT) && m_ritualEntity.HasTag(GAME_TAG.TAUNT))
				{
					m_ritualActor.ActivateTaunt();
				}
				m_ritualActor.UpdateAllComponents();
			}
		}
		if (m_ritualSpell == null)
		{
			float seconds = (m_showBonusAnims ? Mathf.Max(0f, m_noSpellDisplayTime - m_prebuffDisplayTime) : m_noSpellDisplayTime);
			yield return new WaitForSeconds(seconds);
			m_ritualActor.Destroy();
			FinishRitual();
		}
	}

	private int FindLatestProxyCreationTask(GAME_TAG proxyTag)
	{
		int num = -1;
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			switch (power.Type)
			{
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Tag == (int)proxyTag && histTagChange.Value > 0 && i > num)
				{
					num = i;
				}
				break;
			}
			case Network.PowerType.FULL_ENTITY:
				if (i > num)
				{
					num = i;
				}
				break;
			}
		}
		return num;
	}

	public void OnSpellFinished(Spell spell, object userData)
	{
		OnFinishedTaskList();
	}

	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		Entity sourceEntity = m_taskList.GetSourceEntity();
		Player controller = sourceEntity.GetController();
		bool flag = sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN);
		if (eventName != "showCthun")
		{
			Debug.LogError("RitualSpellController received unexpected Spell Event " + eventName);
		}
		if (m_hideRitualActor)
		{
			m_ritualActor.Show();
			if (flag)
			{
				UpdateLastProgress(controller);
				m_progressText.gameObject.SetActive(value: true);
			}
			if (m_tauntSpellInstance != null)
			{
				m_tauntSpellInstance.Activate();
			}
		}
	}

	private void OnRitualSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			((Actor)userData).Destroy();
			FinishRitual();
		}
	}

	private void FinishRitual()
	{
		m_finished = true;
		if (m_processingTaskList)
		{
			OnFinishedTaskList();
		}
		OnFinished();
	}

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
			Object.Destroy(gameObject);
			return null;
		}
		component.SetEntity(entity);
		component.SetCardDefFromEntity(entity);
		return component;
	}

	private void UpdateAndPositionRitualActor()
	{
		if (m_ritualActor.GetEntity().HasTag(GAME_TAG.TAUNT))
		{
			Spell spell = ((m_ritualEntity.GetPremiumType() == TAG_PREMIUM.NORMAL) ? m_tauntInstantSpell : m_tauntInstantPremiumSpell);
			if (spell != null)
			{
				m_tauntSpellInstance = Object.Instantiate(spell);
				TransformUtil.AttachAndPreserveLocalTransform(m_tauntSpellInstance.transform, m_ritualActor.transform);
				if (!m_hideRitualActor)
				{
					m_tauntSpellInstance.Activate();
				}
			}
			else
			{
				Debug.LogWarning("RitualSpellController does not have a instant taunt spell hooked up.");
			}
		}
		m_ritualActor.UpdateMinionStatsImmediately();
		if (m_hideRitualActor)
		{
			m_ritualActor.Hide();
		}
		string text = ((m_ritualActor.GetEntity().GetControllerSide() == Player.Side.FRIENDLY) ? m_friendlyRitualBoneName : m_opponentRitualBoneName);
		Transform parent = Board.Get().FindBone(text);
		m_ritualActor.transform.parent = parent;
		m_ritualActor.transform.localPosition = Vector3.zero;
		m_progressText.transform.parent = m_ritualActor.gameObject.transform;
		m_progressText.gameObject.transform.localPosition = m_progressTextOffset;
	}

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

	private Entity GetProxyEntityFromSourceEntity(Entity sourceEntity)
	{
		Player controller = sourceEntity.GetController();
		int num = 0;
		num = ((!sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN)) ? controller.GetTag(GAME_TAG.PROXY_CTHUN) : controller.GetTag(GAME_TAG.PROXY_CTHUN_SHATTERED));
		if (num == 0)
		{
			return null;
		}
		return GameState.Get().GetEntity(num);
	}

	private int GetLastProgress(Player player)
	{
		int value = 0;
		s_lastProgressMap.TryGetValue(player.GetEntityId(), out value);
		return value;
	}

	private void UpdateLastProgress(Player player)
	{
		int entityId = player.GetEntityId();
		if (s_lastProgressMap.ContainsKey(entityId))
		{
			s_lastProgressMap[entityId] = GetNumCthunPiecesPlayed(player);
		}
		else
		{
			s_lastProgressMap.Add(entityId, GetNumCthunPiecesPlayed(player));
		}
	}
}
