using System;
using System.Collections;
using PegasusGame;
using UnityEngine;

// Token: 0x02000803 RID: 2051
public class MagneticPlaySpell : Spell
{
	// Token: 0x06006F30 RID: 28464 RVA: 0x0023CCFC File Offset: 0x0023AEFC
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		base.AttachPowerTaskList(taskList);
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET)
			{
				return true;
			}
		}
		if (taskList.IsEndOfBlock())
		{
			MagneticPlayData magneticPlayData = base.GetSourceCard().GetMagneticPlayData();
			if (magneticPlayData != null)
			{
				if (magneticPlayData.m_beamSpell != null)
				{
					UnityEngine.Object.Destroy(magneticPlayData.m_beamSpell.gameObject);
				}
				magneticPlayData.m_playedCard.GetActor().ToggleForceIdle(false);
				magneticPlayData.m_playedCard.UpdateActorState(false);
				magneticPlayData.m_targetMech.GetActor().ToggleForceIdle(false);
				magneticPlayData.m_targetMech.UpdateActorState(false);
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT, true));
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			}
		}
		return false;
	}

	// Token: 0x06006F31 RID: 28465 RVA: 0x0023CE2C File Offset: 0x0023B02C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoMagneticEffect());
	}

	// Token: 0x06006F32 RID: 28466 RVA: 0x0023CE42 File Offset: 0x0023B042
	private IEnumerator DoMagneticEffect()
	{
		Card sourceCard = base.GetSourceCard();
		Card targetCard = base.GetTargetCard();
		MagneticPlayData magneticPlayData = sourceCard.GetMagneticPlayData();
		if (magneticPlayData == null)
		{
			ZonePlay battlefieldZone = sourceCard.GetController().GetBattlefieldZone();
			magneticPlayData = new MagneticPlayData();
			magneticPlayData.m_playedCard = sourceCard;
			magneticPlayData.m_targetMech = targetCard;
			magneticPlayData.m_beamSpell = UnityEngine.Object.Instantiate<MagneticBeamSpell>(battlefieldZone.GetMagneticBeamSpell());
			sourceCard.SetMagneticPlayData(magneticPlayData);
			magneticPlayData.m_beamSpell.SetSource(sourceCard.gameObject);
			magneticPlayData.m_beamSpell.AddTarget(targetCard.gameObject);
			magneticPlayData.m_beamSpell.Activate();
			SpellUtils.ActivateBirthIfNecessary(sourceCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT, true));
			SpellUtils.ActivateBirthIfNecessary(targetCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			yield return new WaitForSeconds(0.5f);
		}
		Card source = magneticPlayData.m_playedCard;
		Card target = magneticPlayData.m_targetMech;
		source.SetDoNotSort(true);
		Vector3 delta = source.transform.position - target.transform.position;
		while (delta.sqrMagnitude != 0f)
		{
			delta = Vector3.MoveTowards(delta, Vector3.zero, this.m_AttachSpeed * Time.deltaTime);
			source.transform.position = target.transform.position + delta;
			SpellUtils.ActivateBirthIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			yield return null;
		}
		source.HideCard();
		source.SetDoNotSort(false);
		sourceCard.GetActor().ToggleForceIdle(false);
		sourceCard.UpdateActorState(false);
		targetCard.GetActor().ToggleForceIdle(false);
		targetCard.UpdateActorState(false);
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT, true));
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_beamSpell);
		if (magneticPlayData.m_beamSpell != null)
		{
			UnityEngine.Object.Destroy(magneticPlayData.m_beamSpell.gameObject);
		}
		else
		{
			Log.Gameplay.PrintError("{0}.DoMagneticEffect(): magneticPlayData.m_beamSpell is null! Source={1}. Target={2}.", new object[]
			{
				this,
				sourceCard,
				targetCard
			});
		}
		sourceCard.SetMagneticPlayData(null);
		this.OnStateFinished();
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x04005926 RID: 22822
	public float m_AttachSpeed = 3f;
}
