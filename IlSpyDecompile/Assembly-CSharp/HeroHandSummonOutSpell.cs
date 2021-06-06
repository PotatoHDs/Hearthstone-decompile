using UnityEngine;

public class HeroHandSummonOutSpell : Spell
{
	private const string FRIENDLY_BONE_NAME = "FriendlyHeroSummonOut";

	private const string OPPONENT_BONE_NAME = "OpponentHeroSummonOut";

	public float m_MoveTime;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		MoveToTarget();
	}

	private void MoveToTarget()
	{
		Card sourceCard = GetSourceCard();
		string text = ((sourceCard.GetControllerSide() == Player.Side.FRIENDLY) ? "FriendlyHeroSummonOut" : "OpponentHeroSummonOut");
		Transform transform = Board.Get().FindBone(text);
		if (transform == null)
		{
			Debug.LogErrorFormat("Failed to find a target bone: {0}, card: {1}", text, sourceCard);
		}
		else
		{
			sourceCard.SetDoNotSort(on: true);
			iTween.MoveTo(sourceCard.gameObject, transform.position, m_MoveTime);
			iTween.RotateTo(sourceCard.gameObject, transform.localEulerAngles, m_MoveTime);
			iTween.ScaleTo(sourceCard.gameObject, transform.localScale, m_MoveTime);
		}
	}

	public override void OnSpellFinished()
	{
		GetSourceCard().SetDoNotSort(on: false);
		base.OnSpellFinished();
	}
}
