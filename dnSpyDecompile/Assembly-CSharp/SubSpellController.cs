using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D3 RID: 1747
public class SubSpellController : SpellController
{
	// Token: 0x060061C1 RID: 25025 RVA: 0x001FE7E8 File Offset: 0x001FC9E8
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		SubSpellController.SubSpellInstance subSpellInstanceForTasklist = this.GetSubSpellInstanceForTasklist(taskList);
		if (subSpellInstanceForTasklist == null)
		{
			return false;
		}
		if (!SpellUtils.CanAddPowerTargets(taskList))
		{
			this.CheckForSubSpellEnd(taskList);
			return false;
		}
		Network.HistSubSpellStart subSpellStartTask = subSpellInstanceForTasklist.SubSpellStartTask;
		Entity entity = taskList.GetSourceEntity(true);
		if (subSpellStartTask.SourceEntityID != 0)
		{
			entity = GameState.Get().GetEntity(subSpellStartTask.SourceEntityID);
		}
		Card card = (entity != null) ? entity.GetCard() : null;
		base.SetSource(card);
		if (subSpellStartTask.TargetEntityIDS.Count > 0)
		{
			using (List<int>.Enumerator enumerator = subSpellStartTask.TargetEntityIDS.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id = enumerator.Current;
					Entity entity2 = GameState.Get().GetEntity(id);
					if (entity2 != null)
					{
						Card card2 = entity2.GetCard();
						if (!(card2 == null) && !(card == card2) && !base.IsTarget(card2))
						{
							base.AddTarget(card2);
						}
					}
				}
				goto IL_138;
			}
		}
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			PowerTask task = taskList2[i];
			Card targetCardFromPowerTask = base.GetTargetCardFromPowerTask(task);
			if (!(targetCardFromPowerTask == null) && !(card == targetCardFromPowerTask) && !base.IsTarget(targetCardFromPowerTask))
			{
				base.AddTarget(targetCardFromPowerTask);
			}
		}
		IL_138:
		bool flag = card != null || this.m_targets.Count > 0;
		if (!flag)
		{
			this.CheckForSubSpellEnd(taskList);
		}
		return flag;
	}

	// Token: 0x060061C2 RID: 25026 RVA: 0x001FE964 File Offset: 0x001FCB64
	private SubSpellController.SubSpellInstance GetSubSpellInstanceForTasklist(PowerTaskList taskList)
	{
		SubSpellController.SubSpellInstance subSpellInstance = null;
		Network.HistSubSpellStart subSpellStart = taskList.GetSubSpellStart();
		if (subSpellStart != null)
		{
			subSpellInstance = new SubSpellController.SubSpellInstance();
			subSpellInstance.SubSpellStartTask = subSpellStart;
			this.m_subSpellInstanceStack.Push(subSpellInstance);
			string spellPrefabGUID = subSpellStart.SpellPrefabGUID;
			AssetLoader.Get().InstantiatePrefab(new AssetReference(spellPrefabGUID), new PrefabCallback<GameObject>(this.OnSubSpellLoaded), subSpellInstance, AssetLoadingOptions.None);
		}
		else if (this.m_subSpellInstanceStack.Count > 0)
		{
			subSpellInstance = this.m_subSpellInstanceStack.Peek();
		}
		return subSpellInstance;
	}

	// Token: 0x060061C3 RID: 25027 RVA: 0x001FE9DC File Offset: 0x001FCBDC
	public void OnSubSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		SubSpellController.SubSpellInstance subSpellInstance = (SubSpellController.SubSpellInstance)callbackData;
		subSpellInstance.SpellLoaded = true;
		if (this.m_subSpellInstanceStack.Count <= 0)
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Loaded GameObject without an active sub-spell! GameObject: {1}", new object[]
			{
				this,
				go
			});
			return;
		}
		if (!this.m_subSpellInstanceStack.Contains(subSpellInstance))
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): SubSpellInstance is not on the active sub-spell stack! GameObject: {1}", new object[]
			{
				this,
				go
			});
			return;
		}
		if (go == null)
		{
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Failed to load spell prefab! Prefab GUID: {1}", new object[]
			{
				this,
				subSpellInstance.SubSpellStartTask.SpellPrefabGUID
			});
			return;
		}
		Spell component = go.GetComponent<Spell>();
		if (component == null)
		{
			UnityEngine.Object.Destroy(go);
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Loaded spell prefab doesn't have a Spell component! Spell Prefab: {1}", new object[]
			{
				this,
				go
			});
			return;
		}
		if (subSpellInstance.SpellInstance != null)
		{
			UnityEngine.Object.Destroy(go);
			Log.Power.PrintError("{0}.OnSubSpellLoaded(): Active SubSpellInstance already has an existing spell. Existing Spell: {1}, New Spell: {2}", new object[]
			{
				this,
				subSpellInstance.SpellInstance,
				component
			});
			return;
		}
		component.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSubSpellStateFinished));
		subSpellInstance.SpellInstance = component;
	}

	// Token: 0x060061C4 RID: 25028 RVA: 0x001FEB08 File Offset: 0x001FCD08
	protected override void OnProcessTaskList()
	{
		if (this.m_subSpellInstanceStack.Count <= 0)
		{
			Log.Spells.PrintError("{0}.OnProcessTaskList(): No active sub-spell!", new object[]
			{
				this
			});
			this.OnFinishedTaskList();
			return;
		}
		base.StartCoroutine(this.WaitForSubSpellThenDoTaskList());
	}

	// Token: 0x060061C5 RID: 25029 RVA: 0x001FEB45 File Offset: 0x001FCD45
	private IEnumerator WaitForSubSpellThenDoTaskList()
	{
		SubSpellController.SubSpellInstance subSpellInstance = this.m_subSpellInstanceStack.Peek();
		while (!subSpellInstance.SpellLoaded)
		{
			yield return null;
		}
		if (!this.AttachTasklistToSubSpell(this.m_taskList, subSpellInstance))
		{
			this.CheckForSubSpellEnd(this.m_taskList);
			this.OnFinishedTaskList();
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
		subSpellInstance.SpellInstance.AddFinishedCallback(new Spell.FinishedCallback(this.OnSubSpellFinished));
		subSpellInstance.SpellInstance.ActivateState(SpellStateType.ACTION);
		yield break;
	}

	// Token: 0x060061C6 RID: 25030 RVA: 0x001FEB54 File Offset: 0x001FCD54
	private bool AttachTasklistToSubSpell(PowerTaskList taskList, SubSpellController.SubSpellInstance subSpellInstance)
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
				Card source = base.GetSource();
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
				foreach (int id in subSpellStartTask.TargetEntityIDS)
				{
					Entity entity2 = GameState.Get().GetEntity(id);
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

	// Token: 0x060061C7 RID: 25031 RVA: 0x001FEC98 File Offset: 0x001FCE98
	private void OnSubSpellFinished(Spell spell, object userData)
	{
		this.CheckForSubSpellEnd(spell.GetPowerTaskList());
		this.OnFinishedTaskList();
	}

	// Token: 0x060061C8 RID: 25032 RVA: 0x001FECAC File Offset: 0x001FCEAC
	private void CheckForSubSpellEnd(PowerTaskList taskList)
	{
		if (taskList.GetSubSpellEnd() != null)
		{
			if (this.m_subSpellInstanceStack.Count <= 0)
			{
				Log.Spells.PrintError("{0}.CheckForSubSpellEnd(): SubSpellEnd task hit without an active sub-spell!", new object[]
				{
					this
				});
				return;
			}
			this.m_subSpellInstanceStack.Pop();
		}
	}

	// Token: 0x060061C9 RID: 25033 RVA: 0x001FECEC File Offset: 0x001FCEEC
	private void OnSubSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		SubSpellController.SubSpellInstance[] array = this.m_subSpellInstanceStack.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].SpellInstance == spell)
			{
				return;
			}
		}
		base.StartCoroutine(this.DestroySpellAfterDelay(spell));
	}

	// Token: 0x060061CA RID: 25034 RVA: 0x001FED3B File Offset: 0x001FCF3B
	private IEnumerator DestroySpellAfterDelay(Spell spell)
	{
		yield return new WaitForSeconds(10f);
		if (spell != null && spell.gameObject != null)
		{
			UnityEngine.Object.Destroy(spell.gameObject);
		}
		yield break;
	}

	// Token: 0x04005171 RID: 20849
	private Stack<SubSpellController.SubSpellInstance> m_subSpellInstanceStack = new Stack<SubSpellController.SubSpellInstance>();

	// Token: 0x02002235 RID: 8757
	private class SubSpellInstance
	{
		// Token: 0x0400E2C3 RID: 58051
		public Network.HistSubSpellStart SubSpellStartTask;

		// Token: 0x0400E2C4 RID: 58052
		public Spell SpellInstance;

		// Token: 0x0400E2C5 RID: 58053
		public bool SpellLoaded;
	}
}
