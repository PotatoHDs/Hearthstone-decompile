using System;
using UnityEngine;

// Token: 0x020007F6 RID: 2038
public class HeroHandSummonOutSpell : Spell
{
	// Token: 0x06006EE2 RID: 28386 RVA: 0x0023BC2D File Offset: 0x00239E2D
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.MoveToTarget();
	}

	// Token: 0x06006EE3 RID: 28387 RVA: 0x0023BC3C File Offset: 0x00239E3C
	private void MoveToTarget()
	{
		Card sourceCard = base.GetSourceCard();
		string text = (sourceCard.GetControllerSide() == Player.Side.FRIENDLY) ? "FriendlyHeroSummonOut" : "OpponentHeroSummonOut";
		Transform transform = Board.Get().FindBone(text);
		if (transform == null)
		{
			Debug.LogErrorFormat("Failed to find a target bone: {0}, card: {1}", new object[]
			{
				text,
				sourceCard
			});
			return;
		}
		sourceCard.SetDoNotSort(true);
		iTween.MoveTo(sourceCard.gameObject, transform.position, this.m_MoveTime);
		iTween.RotateTo(sourceCard.gameObject, transform.localEulerAngles, this.m_MoveTime);
		iTween.ScaleTo(sourceCard.gameObject, transform.localScale, this.m_MoveTime);
	}

	// Token: 0x06006EE4 RID: 28388 RVA: 0x0023BCE0 File Offset: 0x00239EE0
	public override void OnSpellFinished()
	{
		base.GetSourceCard().SetDoNotSort(false);
		base.OnSpellFinished();
	}

	// Token: 0x040058F2 RID: 22770
	private const string FRIENDLY_BONE_NAME = "FriendlyHeroSummonOut";

	// Token: 0x040058F3 RID: 22771
	private const string OPPONENT_BONE_NAME = "OpponentHeroSummonOut";

	// Token: 0x040058F4 RID: 22772
	public float m_MoveTime;
}
