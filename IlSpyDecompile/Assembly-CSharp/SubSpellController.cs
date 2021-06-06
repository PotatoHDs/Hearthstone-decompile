using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpellController : SpellController
{
	private class SubSpellInstance
	{
		public Network.HistSubSpellStart SubSpellStartTask;

		public Spell SpellInstance;

		public bool SpellLoaded;
	}

	private Stack<SubSpellInstance> m_subSpellInstanceStack = new Stack<SubSpellInstance>();

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		SubSpellInstance subSpellInstanceForTasklist = GetSubSpellInstanceForTasklist(taskList);
		if (subSpellInstanceForTasklist == null)
		{
			return false;
		}
		if (!SpellUtils.CanAddPowerTargets(taskList))
		{
			CheckForSubSpellEnd(taskList);
			return false;
		}
		Network.HistSubSpellStart subSpellStartTask = subSpellInstanceForTasklist.SubSpellStartTask;
		Entity entity = taskList.GetSourceEntity();
		if (subSpellStartTask.SourceEntityID != 0)
		{
			entity = GameState.Get().GetEntity(subSpellStartTask.SourceEntityID);
		}
		Card card = entity?.GetCard();
		SetSource(card);
		if (subSpellStartTask.TargetEntityIDS.Count > 0)
		{
			foreach (int targetEntityID in subSpellStartTask.TargetEntityIDS)
			{
				Entity entity2 = GameState.Get().GetEntity(targetEntityID);
				if (entity2 != null)
				{
					Card card2 = entity2.GetCard();
					if (!(card2 == null) && !(card == card2) && !IsTarget(card2))
					{
						AddTarget(card2);
					}
				}
			}
		}
		else
		{
			List<PowerTask> taskList2 = m_taskList.GetTaskList();
			for (int i = 0; i < taskList2.Count; i++)
			{
				PowerTask task = taskList2[i];
				Card targetCardFromPowerTask = GetTargetCardFromPowerTask(task);
				if (!(targetCardFromPowerTask == null) && !(card == targetCardFromPowerTask) && !IsTarget(targetCardFromPowerTask))
				{
					AddTarget(targetCardFromPowerTask);
				}
			}
		}
		bool num = card != null || m_targets.Count > 0;
		if (!num)
		{
			CheckForSubSpellEnd(taskList);
		}
		return num;
	}

	private SubSpellInstance GetSubSpellInstanceForTasklist(PowerTaskList taskList)
	{
		SubSpellInstance subSpellInstance = null;
		Network.HistSubSpellStart subSpellStart = taskList.GetSubSpellStart();
		if (subSpellStart != null)
		{
			subSpellInstance = new SubSpellInstance();
			subSpellInstance.SubSpellStartTask = subSpellStart;
			m_subSpellInstanceStack.Push(subSpellInstance);
			string spellPrefabGUID = subSpellStart.SpellPrefabGUID;
			AssetLoader.Get().InstantiatePrefab(new AssetReference(spellPrefabGUID), OnSubSpellLoaded, subSpellInstance);
		}
		else if (m_subSpellInstanceStack.Count > 0)
		{
			subSpellInstance = m_subSpellInstanceStack.Peek();
		}
		return subSpellInstance;
	}

	public void OnSubSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		SubSpellInstance subSpellInstance = (SubSpellInstance)callbackData;
		subSpellInstance.SpellLoaded = true;
		if (m_subSpellInstanceStack.Count <= 0)
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Loaded GameObject without an active sub-spell! GameObject: {1}", this, go);
			return;
		}
		if (!m_subSpellInstanceStack.Contains(subSpellInstance))
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): SubSpellInstance is not on the active sub-spell stack! GameObject: {1}", this, go);
			return;
		}
		if (go == null)
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Failed to load spell prefab! Prefab GUID: {1}", this, subSpellInstance.SubSpellStartTask.SpellPrefabGUID);
			return;
		}
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			Object.Destroy(go);
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Loaded spell prefab doesn't have a Spell component! Spell Prefab: {1}", this, go);
		}
		else if (subSpellInstance.SpellInstance != null)
		{
			Object.Destroy(go);
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Active SubSpellInstance already has an existing spell. Existing Spell: {1}, New Spell: {2}", this, subSpellInstance.SpellInstance, component);
		}
		else
		{
			component.AddStateFinishedCallback(OnSubSpellStateFinished);
			subSpellInstance.SpellInstance = component;
		}
	}

	protected override void OnProcessTaskList()
	{
		if (m_subSpellInstanceStack.Count <= 0)
		{
			Log.Spells.PrintError("{0}.OnProcessTaskList(): No active sub-spell!", this);
			OnFinishedTaskList();
		}
		else
		{
			StartCoroutine(WaitForSubSpellThenDoTaskList());
		}
	}

	private IEnumerator WaitForSubSpellThenDoTaskList()
	{
		SubSpellInstance subSpellInstance = m_subSpellInstanceStack.Peek();
		while (!subSpellInstance.SpellLoaded)
		{
			yield return null;
		}
		if (!AttachTasklistToSubSpell(m_taskList, subSpellInstance))
		{
			CheckForSubSpellEnd(m_taskList);
			OnFinishedTaskList();
			yield break;
		}
		if (GameState.Get().IsTurnStartManagerActive())
		{
			TurnStartManager.Get().NotifyOfTriggerVisual();
			while (TurnStartManager.Get().IsTurnStartIndicatorShowing())
			{
				yield return null;
			}
		}
		subSpellInstance.SpellInstance.AddFinishedCallback(OnSubSpellFinished);
		subSpellInstance.SpellInstance.ActivateState(SpellStateType.ACTION);
	}

	private bool AttachTasklistToSubSpell(PowerTaskList taskList, SubSpellInstance subSpellInstance)
	{
		if (subSpellInstance.SpellInstance == null)
		{
			return false;
		}
		Spell spellInstance = subSpellInstance.SpellInstance;
		Network.HistSubSpellStart subSpellStartTask = subSpellInstance.SubSpellStartTask;
		if (spellInstance.AttachPowerTaskList(taskList))
		{
			if (subSpellStartTask.SourceEntityID != 0)
			{
				Entity entity = GameState.Get().GetEntity(subSpellStartTask.SourceEntityID);
				if (entity.GetCard() != null)
				{
					spellInstance.SetSource(entity.GetCard().gameObject);
					spellInstance.m_Location = SpellLocation.SOURCE;
				}
			}
			else
			{
				Card source = GetSource();
				if (source != null)
				{
					spellInstance.SetSource(source.gameObject);
				}
			}
			if (subSpellStartTask.TargetEntityIDS.Count > 0)
			{
				spellInstance.RemoveAllTargets();
				spellInstance.RemoveAllVisualTargets();
				if (spellInstance is SuperSpell)
				{
					(spellInstance as SuperSpell).m_TargetInfo.m_Behavior = SpellTargetBehavior.DEFAULT;
				}
				foreach (int targetEntityID in subSpellStartTask.TargetEntityIDS)
				{
					Entity entity2 = GameState.Get().GetEntity(targetEntityID);
					if (entity2 != null && entity2.GetCard() != null)
					{
						spellInstance.AddTarget(entity2.GetCard().gameObject);
					}
				}
			}
			return true;
		}
		return false;
	}

	private void OnSubSpellFinished(Spell spell, object userData)
	{
		CheckForSubSpellEnd(spell.GetPowerTaskList());
		OnFinishedTaskList();
	}

	private void CheckForSubSpellEnd(PowerTaskList taskList)
	{
		if (taskList.GetSubSpellEnd() != null)
		{
			if (m_subSpellInstanceStack.Count <= 0)
			{
				Log.Spells.PrintError("{0}.CheckForSubSpellEnd(): SubSpellEnd task hit without an active sub-spell!", this);
			}
			else
			{
				m_subSpellInstanceStack.Pop();
			}
		}
	}

	private void OnSubSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != 0)
		{
			return;
		}
		SubSpellInstance[] array = m_subSpellInstanceStack.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].SpellInstance == spell)
			{
				return;
			}
		}
		StartCoroutine(DestroySpellAfterDelay(spell));
	}

	private IEnumerator DestroySpellAfterDelay(Spell spell)
	{
		yield return new WaitForSeconds(10f);
		if (spell != null && spell.gameObject != null)
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
