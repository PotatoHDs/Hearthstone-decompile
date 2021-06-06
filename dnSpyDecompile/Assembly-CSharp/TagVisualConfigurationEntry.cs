using System;
using UnityEngine;

// Token: 0x02000998 RID: 2456
[Serializable]
public class TagVisualConfigurationEntry
{
	// Token: 0x06008649 RID: 34377 RVA: 0x002B5339 File Offset: 0x002B3539
	private bool IsReferenceTag()
	{
		return this.m_ReferenceTag > GAME_TAG.TAG_NOT_SET;
	}

	// Token: 0x040071E2 RID: 29154
	[CustomEditField(SortPopupByName = true)]
	public GAME_TAG m_Tag;

	// Token: 0x040071E3 RID: 29155
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IgnoreCanShowActorVisuals;

	// Token: 0x040071E4 RID: 29156
	[CustomEditField(SortPopupByName = true, Sections = "Settings")]
	[Tooltip("Use this to avoid repeating yourself when the Tags do the same thing (e.g. Shifting, Shifting_Weapon, Shifting_Spell).")]
	public GAME_TAG m_ReferenceTag;

	// Token: 0x040071E5 RID: 29157
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IsPlayStateSpell;

	// Token: 0x040071E6 RID: 29158
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IsHandStateSpell;

	// Token: 0x040071E7 RID: 29159
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time the Tag changes, do these before handling \"Tag Added\", \"Tag Removed\", or \"After Always\" actions.")]
	public TagVisualStateConfiguration m_BeforeAlways;

	// Token: 0x040071E8 RID: 29160
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time a Tag changes from \"0\".")]
	public TagVisualStateConfiguration m_TagAdded;

	// Token: 0x040071E9 RID: 29161
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time a Tag changes to \"0\".")]
	public TagVisualStateConfiguration m_TagRemoved;

	// Token: 0x040071EA RID: 29162
	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time the Tag changes, do these after handling \"Tag Added\", \"Tag Removed\", or \"Before Always\" actions.")]
	public TagVisualStateConfiguration m_AfterAlways;
}
