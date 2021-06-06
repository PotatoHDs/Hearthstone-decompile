using System.Collections;
using PegasusGame;
using UnityEngine;

public class MagneticPlaySpell : Spell
{
	public float m_AttachSpeed = 3f;

	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		base.AttachPowerTaskList(taskList);
		foreach (PowerTask task in taskList.GetTaskList())
		{
			Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET)
			{
				return true;
			}
		}
		if (taskList.IsEndOfBlock())
		{
			MagneticPlayData magneticPlayData = GetSourceCard().GetMagneticPlayData();
			if (magneticPlayData != null)
			{
				if (magneticPlayData.m_beamSpell != null)
				{
					Object.Destroy(magneticPlayData.m_beamSpell.gameObject);
				}
				magneticPlayData.m_playedCard.GetActor().ToggleForceIdle(bOn: false);
				magneticPlayData.m_playedCard.UpdateActorState();
				magneticPlayData.m_targetMech.GetActor().ToggleForceIdle(bOn: false);
				magneticPlayData.m_targetMech.UpdateActorState();
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT));
				SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			}
		}
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(DoMagneticEffect());
	}

	private IEnumerator DoMagneticEffect()
	{
		Card sourceCard = GetSourceCard();
		Card targetCard = GetTargetCard();
		MagneticPlayData magneticPlayData = sourceCard.GetMagneticPlayData();
		if (magneticPlayData == null)
		{
			ZonePlay battlefieldZone = sourceCard.GetController().GetBattlefieldZone();
			magneticPlayData = new MagneticPlayData
			{
				m_playedCard = sourceCard,
				m_targetMech = targetCard,
				m_beamSpell = Object.Instantiate(battlefieldZone.GetMagneticBeamSpell())
			};
			sourceCard.SetMagneticPlayData(magneticPlayData);
			magneticPlayData.m_beamSpell.SetSource(sourceCard.gameObject);
			magneticPlayData.m_beamSpell.AddTarget(targetCard.gameObject);
			magneticPlayData.m_beamSpell.Activate();
			SpellUtils.ActivateBirthIfNecessary(sourceCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT));
			SpellUtils.ActivateBirthIfNecessary(targetCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			yield return new WaitForSeconds(0.5f);
		}
		Card source = magneticPlayData.m_playedCard;
		Card target = magneticPlayData.m_targetMech;
		source.SetDoNotSort(on: true);
		Vector3 delta = source.transform.position - target.transform.position;
		while (delta.sqrMagnitude != 0f)
		{
			delta = Vector3.MoveTowards(delta, Vector3.zero, m_AttachSpeed * Time.deltaTime);
			source.transform.position = target.transform.position + delta;
			SpellUtils.ActivateBirthIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			yield return null;
		}
		source.HideCard();
		source.SetDoNotSort(on: false);
		sourceCard.GetActor().ToggleForceIdle(bOn: false);
		sourceCard.UpdateActorState();
		targetCard.GetActor().ToggleForceIdle(bOn: false);
		targetCard.UpdateActorState();
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_playedCard.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_RIGHT));
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_targetMech.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
		SpellUtils.ActivateDeathIfNecessary(magneticPlayData.m_beamSpell);
		if (magneticPlayData.m_beamSpell != null)
		{
			Object.Destroy(magneticPlayData.m_beamSpell.gameObject);
		}
		else
		{
			Log.Gameplay.PrintError("{0}.DoMagneticEffect(): magneticPlayData.m_beamSpell is null! Source={1}. Target={2}.", this, sourceCard, targetCard);
		}
		sourceCard.SetMagneticPlayData(null);
		OnStateFinished();
		OnSpellFinished();
	}
}
