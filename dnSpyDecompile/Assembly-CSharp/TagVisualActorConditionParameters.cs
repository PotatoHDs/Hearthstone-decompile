using System;
using UnityEngine;

// Token: 0x02000994 RID: 2452
[Serializable]
public class TagVisualActorConditionParameters
{
	// Token: 0x0600863A RID: 34362 RVA: 0x002B527D File Offset: 0x002B347D
	private bool ShouldHideTagValueParametersLHS()
	{
		return this.m_ConditionLHS != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	// Token: 0x0600863B RID: 34363 RVA: 0x002B528B File Offset: 0x002B348B
	private bool ShouldHideSpellStateParametersLHS()
	{
		return this.m_ConditionLHS != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}

	// Token: 0x0600863C RID: 34364 RVA: 0x002B5299 File Offset: 0x002B3499
	private bool ShouldHideTagValueParametersRHS()
	{
		return this.m_ConditionRHS != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	// Token: 0x0600863D RID: 34365 RVA: 0x002B52A7 File Offset: 0x002B34A7
	private bool ShouldHideSpellStateParametersRHS()
	{
		return this.m_ConditionRHS != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}

	// Token: 0x040071C2 RID: 29122
	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_Tag;

	// Token: 0x040071C3 RID: 29123
	[CustomEditField(Label = "Tag Comparison Operator", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperator;

	// Token: 0x040071C4 RID: 29124
	[CustomEditField(Label = "Tag Value to Compare", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public int m_Value;

	// Token: 0x040071C5 RID: 29125
	[CustomEditField(Label = "Tag Owner Entity", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntity;

	// Token: 0x040071C6 RID: 29126
	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellType;

	// Token: 0x040071C7 RID: 29127
	[CustomEditField(HidePredicate = "ShouldHideSpellStateParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellState;

	// Token: 0x040071C8 RID: 29128
	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Required for AND/OR")]
	public TagVisualActorCondition m_ConditionLHS;

	// Token: 0x040071C9 RID: 29129
	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertConditionLHS;

	// Token: 0x040071CA RID: 29130
	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_TagLHS;

	// Token: 0x040071CB RID: 29131
	[CustomEditField(Label = "Tag Comparison Operator LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperatorLHS;

	// Token: 0x040071CC RID: 29132
	[CustomEditField(Label = "Tag Value to Compare LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public int m_ValueLHS;

	// Token: 0x040071CD RID: 29133
	[CustomEditField(Label = "Tag Owner Entity LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntityLHS;

	// Token: 0x040071CE RID: 29134
	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellTypeLHS;

	// Token: 0x040071CF RID: 29135
	[CustomEditField(HidePredicate = "ShouldHideSpellStateParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellStateLHS;

	// Token: 0x040071D0 RID: 29136
	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Required for AND/OR")]
	public TagVisualActorCondition m_ConditionRHS;

	// Token: 0x040071D1 RID: 29137
	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertConditionRHS;

	// Token: 0x040071D2 RID: 29138
	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_TagRHS;

	// Token: 0x040071D3 RID: 29139
	[CustomEditField(Label = "Tag Comparison Operator RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperatorRHS;

	// Token: 0x040071D4 RID: 29140
	[CustomEditField(Label = "Tag Value to Compare RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public int m_ValueRHS;

	// Token: 0x040071D5 RID: 29141
	[CustomEditField(Label = "Tag Owner Entity RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntityRHS;

	// Token: 0x040071D6 RID: 29142
	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellTypeRHS;

	// Token: 0x040071D7 RID: 29143
	[CustomEditField(HidePredicate = "ShouldHideSpellStateParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellStateRHS;
}
