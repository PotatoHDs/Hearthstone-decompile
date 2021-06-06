using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000834 RID: 2100
public class TwistingNetherSpell : SuperSpell
{
	// Token: 0x0600705C RID: 28764 RVA: 0x00243D98 File Offset: 0x00241F98
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		if (Gameplay.Get() != null)
		{
			if (histTagChange.Tag != 360)
			{
				return null;
			}
			if (histTagChange.Value != 1)
			{
				return null;
			}
		}
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity));
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x0600705D RID: 28765 RVA: 0x00243E1C File Offset: 0x0024201C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		if (base.IsFinished())
		{
			return;
		}
		this.Begin();
		if (this.CanFinish())
		{
			this.m_effectsPendingFinish--;
			base.FinishIfPossible();
		}
	}

	// Token: 0x0600705E RID: 28766 RVA: 0x00243E50 File Offset: 0x00242050
	protected override void CleanUp()
	{
		base.CleanUp();
		this.m_victims.Clear();
	}

	// Token: 0x0600705F RID: 28767 RVA: 0x00243E64 File Offset: 0x00242064
	private void Begin()
	{
		this.m_effectsPendingFinish++;
		Action<object> action = delegate(object amount)
		{
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			this.m_FinishTime,
			"onupdate",
			action,
			"oncomplete",
			"OnFinishTimeFinished",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
		this.Setup();
		this.Lift();
	}

	// Token: 0x06007060 RID: 28768 RVA: 0x00243F34 File Offset: 0x00242134
	private void Setup()
	{
		foreach (GameObject gameObject in base.GetTargets())
		{
			Card component = gameObject.GetComponent<Card>();
			component.SetDoNotSort(true);
			TwistingNetherSpell.Victim victim = new TwistingNetherSpell.Victim();
			victim.m_state = TwistingNetherSpell.VictimState.NONE;
			victim.m_card = component;
			this.m_victims.Add(victim);
		}
	}

	// Token: 0x06007061 RID: 28769 RVA: 0x00243FAC File Offset: 0x002421AC
	private void Lift()
	{
		foreach (TwistingNetherSpell.Victim victim in this.m_victims)
		{
			victim.m_state = TwistingNetherSpell.VictimState.LIFTING;
			Vector3 b = TransformUtil.RandomVector3(this.m_LiftInfo.m_OffsetMin, this.m_LiftInfo.m_OffsetMax);
			Vector3 vector = victim.m_card.transform.position + b;
			float num = UnityEngine.Random.Range(this.m_LiftInfo.m_DelayMin, this.m_LiftInfo.m_DelayMax);
			float num2 = UnityEngine.Random.Range(this.m_LiftInfo.m_DurationMin, this.m_LiftInfo.m_DurationMax);
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				vector,
				"delay",
				num,
				"time",
				num2,
				"easeType",
				this.m_LiftInfo.m_EaseType,
				"oncomplete",
				"OnLiftFinished",
				"oncompletetarget",
				base.gameObject,
				"oncompleteparams",
				victim
			});
			Vector3 vector2 = new Vector3(UnityEngine.Random.Range(this.m_LiftInfo.m_RotationMin, this.m_LiftInfo.m_RotationMax), UnityEngine.Random.Range(this.m_LiftInfo.m_RotationMin, this.m_LiftInfo.m_RotationMax), UnityEngine.Random.Range(this.m_LiftInfo.m_RotationMin, this.m_LiftInfo.m_RotationMax));
			float num3 = UnityEngine.Random.Range(this.m_LiftInfo.m_RotDelayMin, this.m_LiftInfo.m_RotDelayMax);
			float num4 = UnityEngine.Random.Range(this.m_LiftInfo.m_RotDurationMin, this.m_LiftInfo.m_RotDurationMax);
			Hashtable args2 = iTween.Hash(new object[]
			{
				"rotation",
				vector2,
				"delay",
				num3,
				"time",
				num4,
				"easeType",
				this.m_LiftInfo.m_EaseType
			});
			iTween.MoveTo(victim.m_card.gameObject, args);
			iTween.RotateTo(victim.m_card.gameObject, args2);
		}
	}

	// Token: 0x06007062 RID: 28770 RVA: 0x00244224 File Offset: 0x00242424
	private void OnLiftFinished(TwistingNetherSpell.Victim victim)
	{
		this.Float(victim);
	}

	// Token: 0x06007063 RID: 28771 RVA: 0x00244230 File Offset: 0x00242430
	private void Float(TwistingNetherSpell.Victim victim)
	{
		victim.m_state = TwistingNetherSpell.VictimState.FLOATING;
		float num = UnityEngine.Random.Range(this.m_FloatInfo.m_DurationMin, this.m_FloatInfo.m_DurationMax);
		Action<object> action = delegate(object amount)
		{
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			num,
			"onupdate",
			action,
			"oncomplete",
			"OnFloatFinished",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			victim
		});
		iTween.ValueTo(victim.m_card.gameObject, args);
	}

	// Token: 0x06007064 RID: 28772 RVA: 0x00244317 File Offset: 0x00242517
	private void OnFloatFinished(TwistingNetherSpell.Victim victim)
	{
		this.Drain(victim);
	}

	// Token: 0x06007065 RID: 28773 RVA: 0x00244320 File Offset: 0x00242520
	private void Drain(TwistingNetherSpell.Victim victim)
	{
		victim.m_state = TwistingNetherSpell.VictimState.LIFTING;
		float num = UnityEngine.Random.Range(this.m_DrainInfo.m_DelayMin, this.m_DrainInfo.m_DelayMax);
		float num2 = UnityEngine.Random.Range(this.m_DrainInfo.m_DurationMin, this.m_DrainInfo.m_DurationMax);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			base.transform.position,
			"delay",
			num,
			"time",
			num2,
			"easeType",
			this.m_DrainInfo.m_EaseType,
			"oncomplete",
			"OnDrainFinished",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			victim
		});
		iTween.MoveTo(victim.m_card.gameObject, args);
		float num3 = UnityEngine.Random.Range(this.m_SqueezeInfo.m_DelayMin, this.m_SqueezeInfo.m_DelayMax);
		float num4 = UnityEngine.Random.Range(this.m_SqueezeInfo.m_DurationMin, this.m_SqueezeInfo.m_DurationMax);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"scale",
			TwistingNetherSpell.DEAD_SCALE,
			"delay",
			num3,
			"time",
			num4,
			"easeType",
			this.m_SqueezeInfo.m_EaseType
		});
		iTween.ScaleTo(victim.m_card.gameObject, args2);
	}

	// Token: 0x06007066 RID: 28774 RVA: 0x002444BF File Offset: 0x002426BF
	private void OnDrainFinished(TwistingNetherSpell.Victim victim)
	{
		this.CleanUpVictim(victim);
	}

	// Token: 0x06007067 RID: 28775 RVA: 0x002444C8 File Offset: 0x002426C8
	private void OnFinishTimeFinished()
	{
		foreach (TwistingNetherSpell.Victim victim in this.m_victims)
		{
			this.CleanUpVictim(victim);
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x06007068 RID: 28776 RVA: 0x00244530 File Offset: 0x00242730
	private void CleanUpVictim(TwistingNetherSpell.Victim victim)
	{
		if (victim.m_state != TwistingNetherSpell.VictimState.DEAD)
		{
			victim.m_state = TwistingNetherSpell.VictimState.DEAD;
			victim.m_card.SetDoNotSort(false);
		}
	}

	// Token: 0x06007069 RID: 28777 RVA: 0x00244550 File Offset: 0x00242750
	private bool CanFinish()
	{
		using (List<TwistingNetherSpell.Victim>.Enumerator enumerator = this.m_victims.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_state != TwistingNetherSpell.VictimState.DEAD)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x04005A50 RID: 23120
	public float m_FinishTime;

	// Token: 0x04005A51 RID: 23121
	public TwistingNetherLiftInfo m_LiftInfo;

	// Token: 0x04005A52 RID: 23122
	public TwistingNetherFloatInfo m_FloatInfo;

	// Token: 0x04005A53 RID: 23123
	public TwistingNetherDrainInfo m_DrainInfo;

	// Token: 0x04005A54 RID: 23124
	public TwistingNetherSqueezeInfo m_SqueezeInfo;

	// Token: 0x04005A55 RID: 23125
	private static readonly Vector3 DEAD_SCALE = new Vector3(0.01f, 0.01f, 0.01f);

	// Token: 0x04005A56 RID: 23126
	private List<TwistingNetherSpell.Victim> m_victims = new List<TwistingNetherSpell.Victim>();

	// Token: 0x02002405 RID: 9221
	private enum VictimState
	{
		// Token: 0x0400E904 RID: 59652
		NONE,
		// Token: 0x0400E905 RID: 59653
		LIFTING,
		// Token: 0x0400E906 RID: 59654
		FLOATING,
		// Token: 0x0400E907 RID: 59655
		DRAINING,
		// Token: 0x0400E908 RID: 59656
		DEAD
	}

	// Token: 0x02002406 RID: 9222
	private class Victim
	{
		// Token: 0x0400E909 RID: 59657
		public TwistingNetherSpell.VictimState m_state;

		// Token: 0x0400E90A RID: 59658
		public Card m_card;
	}
}
