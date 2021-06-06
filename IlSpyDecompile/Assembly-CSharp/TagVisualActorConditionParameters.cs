using System;
using UnityEngine;

[Serializable]
public class TagVisualActorConditionParameters
{
	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_Tag;

	[CustomEditField(Label = "Tag Comparison Operator", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperator;

	[CustomEditField(Label = "Tag Value to Compare", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public int m_Value;

	[CustomEditField(Label = "Tag Owner Entity", HidePredicate = "ShouldHideTagValueParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntity;

	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellType;

	[CustomEditField(HidePredicate = "ShouldHideSpellStateParameters", HidePredicateInParent = true)]
	[Tooltip("Required for DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellState;

	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Required for AND/OR")]
	public TagVisualActorCondition m_ConditionLHS;

	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertConditionLHS;

	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_TagLHS;

	[CustomEditField(Label = "Tag Comparison Operator LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperatorLHS;

	[CustomEditField(Label = "Tag Value to Compare LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public int m_ValueLHS;

	[CustomEditField(Label = "Tag Owner Entity LHS", HidePredicate = "ShouldHideTagValueParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntityLHS;

	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellTypeLHS;

	[CustomEditField(HidePredicate = "ShouldHideSpellStateParametersLHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellStateLHS;

	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Required for AND/OR")]
	public TagVisualActorCondition m_ConditionRHS;

	[CustomEditField(HidePredicate = "ShouldHideCompoundConditionParameters", HidePredicateInParent = true)]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertConditionRHS;

	[CustomEditField(SortPopupByName = true, Label = "Tag to Compare RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public GAME_TAG m_TagRHS;

	[CustomEditField(Label = "Tag Comparison Operator RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionComparisonOperator m_ComparisonOperatorRHS;

	[CustomEditField(Label = "Tag Value to Compare RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public int m_ValueRHS;

	[CustomEditField(Label = "Tag Owner Entity RHS", HidePredicate = "ShouldHideTagValueParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_TAG_HAVE_VALUE")]
	public TagVisualActorConditionEntity m_TagComparisonEntityRHS;

	[CustomEditField(SortPopupByName = true, HidePredicate = "ShouldHideSpellStateParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellType m_SpellTypeRHS;

	[CustomEditField(HidePredicate = "ShouldHideSpellStateParametersRHS")]
	[Tooltip("Required for AND/OR + DOES_SPELL_HAVE_STATE")]
	public SpellStateType m_SpellStateRHS;

	private bool ShouldHideTagValueParametersLHS()
	{
		return m_ConditionLHS != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	private bool ShouldHideSpellStateParametersLHS()
	{
		return m_ConditionLHS != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}

	private bool ShouldHideTagValueParametersRHS()
	{
		return m_ConditionRHS != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	private bool ShouldHideSpellStateParametersRHS()
	{
		return m_ConditionRHS != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}
}
