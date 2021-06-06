using System;
using UnityEngine;

[Serializable]
public class TagVisualConfigurationEntry
{
	[CustomEditField(SortPopupByName = true)]
	public GAME_TAG m_Tag;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IgnoreCanShowActorVisuals;

	[CustomEditField(SortPopupByName = true, Sections = "Settings")]
	[Tooltip("Use this to avoid repeating yourself when the Tags do the same thing (e.g. Shifting, Shifting_Weapon, Shifting_Spell).")]
	public GAME_TAG m_ReferenceTag;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IsPlayStateSpell;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	public bool m_IsHandStateSpell;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time the Tag changes, do these before handling \"Tag Added\", \"Tag Removed\", or \"After Always\" actions.")]
	public TagVisualStateConfiguration m_BeforeAlways;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time a Tag changes from \"0\".")]
	public TagVisualStateConfiguration m_TagAdded;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time a Tag changes to \"0\".")]
	public TagVisualStateConfiguration m_TagRemoved;

	[CustomEditField(Sections = "Settings", HidePredicate = "IsReferenceTag")]
	[Tooltip("A list of actions to perform every time the Tag changes, do these after handling \"Tag Added\", \"Tag Removed\", or \"Before Always\" actions.")]
	public TagVisualStateConfiguration m_AfterAlways;

	private bool IsReferenceTag()
	{
		return m_ReferenceTag != GAME_TAG.TAG_NOT_SET;
	}
}
