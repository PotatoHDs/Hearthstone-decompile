using UnityEngine;

public class PuzzleIntroSpell : Spell
{
	[SerializeField]
	private Transform m_ConfirmButton;

	public Transform GetConfirmButton()
	{
		return m_ConfirmButton;
	}
}
