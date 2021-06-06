using System;
using UnityEngine;

// Token: 0x02000996 RID: 2454
[Serializable]
public class TagVisualActionConfiguration
{
	// Token: 0x06008644 RID: 34372 RVA: 0x002B5304 File Offset: 0x002B3504
	private bool ShouldHideSpellStateActionParameters()
	{
		return this.m_Action != TagVisualActorFunction.ACTIVATE_SPELL_STATE;
	}

	// Token: 0x06008645 RID: 34373 RVA: 0x002B5312 File Offset: 0x002B3512
	private bool ShouldHideSoundActionParameters()
	{
		return this.m_Action != TagVisualActorFunction.PLAY_SOUND_PREFAB;
	}

	// Token: 0x06008646 RID: 34374 RVA: 0x002B5320 File Offset: 0x002B3520
	private bool ShouldHideStateFunctionActionParameters()
	{
		return this.m_Action != TagVisualActorFunction.ACTIVATE_STATE_FUNCTION && this.m_Action != TagVisualActorFunction.DEACTIVATE_STATE_FUNCTION;
	}

	// Token: 0x040071DB RID: 29147
	[Tooltip("Action to perform")]
	public TagVisualActorFunction m_Action;

	// Token: 0x040071DC RID: 29148
	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateActionParameters")]
	[Tooltip("Required for ACTIVATE_SPELL_STATE")]
	public SpellType m_SpellType;

	// Token: 0x040071DD RID: 29149
	[CustomEditField(HidePredicate = "ShouldHideSpellStateActionParameters")]
	[Tooltip("Required for ACTIVATE_SPELL_STATE")]
	public SpellStateType m_SpellState;

	// Token: 0x040071DE RID: 29150
	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideStateFunctionActionParameters")]
	[Tooltip("Required for ACTIVATE_STATE_FUNCTION/DEACTIVATE_STATE_FUNCTION")]
	public TagVisualActorStateFunction m_StateFunctionParameters;

	// Token: 0x040071DF RID: 29151
	[CustomEditField(T = EditType.SOUND_PREFAB, HidePredicate = "ShouldHideSoundActionParameters")]
	[Tooltip("Required for PLAY_SOUND_PREFAB")]
	public string m_PlaySoundPrefabParameters;

	// Token: 0x040071E0 RID: 29152
	[Tooltip("Some actions may only need to be executed under certain conditions (defaults to ALWAYS)")]
	public TagVisualActorConditionConfiguration m_Condition;
}
