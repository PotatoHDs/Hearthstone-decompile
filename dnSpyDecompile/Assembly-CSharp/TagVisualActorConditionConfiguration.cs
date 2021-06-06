using System;
using UnityEngine;

// Token: 0x02000995 RID: 2453
[Serializable]
public class TagVisualActorConditionConfiguration
{
	// Token: 0x0600863F RID: 34367 RVA: 0x002B52B5 File Offset: 0x002B34B5
	private bool ShouldHideTagValueParameters()
	{
		return this.m_Condition != TagVisualActorCondition.DOES_TAG_HAVE_VALUE;
	}

	// Token: 0x06008640 RID: 34368 RVA: 0x002B52C3 File Offset: 0x002B34C3
	private bool ShouldHideSpellStateParameters()
	{
		return this.m_Condition != TagVisualActorCondition.DOES_SPELL_HAVE_STATE;
	}

	// Token: 0x06008641 RID: 34369 RVA: 0x002B52D1 File Offset: 0x002B34D1
	private bool ShouldHideCompoundConditionParameters()
	{
		return this.m_Condition != TagVisualActorCondition.AND && this.m_Condition != TagVisualActorCondition.OR;
	}

	// Token: 0x06008642 RID: 34370 RVA: 0x002B52EA File Offset: 0x002B34EA
	private bool ShouldHideAllConditionParameters()
	{
		return this.ShouldHideTagValueParameters() && this.ShouldHideSpellStateParameters() && this.ShouldHideCompoundConditionParameters();
	}

	// Token: 0x040071D8 RID: 29144
	[CustomEditField(Sections = "Condition Configuration")]
	[Tooltip("Condition to evaluate")]
	public TagVisualActorCondition m_Condition;

	// Token: 0x040071D9 RID: 29145
	[CustomEditField(Sections = "Condition Configuration")]
	[Tooltip("Evaluate this condition opposite to the initial result")]
	public bool m_InvertCondition;

	// Token: 0x040071DA RID: 29146
	[CustomEditField(Sections = "Condition Configuration", HidePredicate = "ShouldHideAllConditionParameters")]
	public TagVisualActorConditionParameters m_Parameters;
}
