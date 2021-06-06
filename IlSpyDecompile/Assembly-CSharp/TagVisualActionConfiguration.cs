using System;
using UnityEngine;

[Serializable]
public class TagVisualActionConfiguration
{
	[Tooltip("Action to perform")]
	public TagVisualActorFunction m_Action;

	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateActionParameters")]
	[Tooltip("Required for ACTIVATE_SPELL_STATE")]
	public SpellType m_SpellType;

	[CustomEditField(HidePredicate = "ShouldHideSpellStateActionParameters")]
	[Tooltip("Required for ACTIVATE_SPELL_STATE")]
	public SpellStateType m_SpellState;

	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideStateFunctionActionParameters")]
	[Tooltip("Required for ACTIVATE_STATE_FUNCTION/DEACTIVATE_STATE_FUNCTION")]
	public TagVisualActorStateFunction m_StateFunctionParameters;

	[CustomEditField(T = EditType.SOUND_PREFAB, HidePredicate = "ShouldHideSoundActionParameters")]
	[Tooltip("Required for PLAY_SOUND_PREFAB")]
	public string m_PlaySoundPrefabParameters;

	[Tooltip("Some actions may only need to be executed under certain conditions (defaults to ALWAYS)")]
	public TagVisualActorConditionConfiguration m_Condition;

	private bool ShouldHideSpellStateActionParameters()
	{
		return m_Action != TagVisualActorFunction.ACTIVATE_SPELL_STATE;
	}

	private bool ShouldHideSoundActionParameters()
	{
		return m_Action != TagVisualActorFunction.PLAY_SOUND_PREFAB;
	}

	private bool ShouldHideStateFunctionActionParameters()
	{
		if (m_Action != TagVisualActorFunction.ACTIVATE_STATE_FUNCTION)
		{
			return m_Action != TagVisualActorFunction.DEACTIVATE_STATE_FUNCTION;
		}
		return false;
	}
}
