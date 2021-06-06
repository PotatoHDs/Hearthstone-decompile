using System;
using UnityEngine;

[Serializable]
public class TagVisualActorConditionConfiguration
{
	[CustomEditField(Sections = "Condition Configuration")]
	[Tooltip("Condition to evaluate")]
	public TagVisualActorCondition m_Condition;

	[CustomEditField(Sections = "Condition Configuration")]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertCondition;

	[CustomEditField(Sections = "Condition Configuration", HidePredicate = "ShouldHideAllConditionParameters")]
	public TagVisualActorConditionParameters m_Parameters;

	private bool ShouldHideTagValueParameters()
	{
		return m_Condition != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	private bool ShouldHideSpellStateParameters()
	{
		return m_Condition != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}

	private bool ShouldHideCompoundConditionParameters()
	{
		if (m_Condition != TagVisualActorCondition.AND)
		{
			return m_Condition != TagVisualActorCondition.OR;
		}
		return false;
	}

	private bool ShouldHideAllConditionParameters()
	{
		if (ShouldHideTagValueParameters() && ShouldHideSpellStateParameters())
		{
			return ShouldHideCompoundConditionParameters();
		}
		return false;
	}
}
