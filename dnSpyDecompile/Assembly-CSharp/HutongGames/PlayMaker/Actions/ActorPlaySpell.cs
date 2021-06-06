using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0B RID: 3851
	[ActionCategory("Pegasus")]
	[Tooltip("Play a spell on the given Actor")]
	public class ActorPlaySpell : FsmStateAction
	{
		// Token: 0x0600ABA2 RID: 43938 RVA: 0x00359630 File Offset: 0x00357830
		public override void Reset()
		{
			this.m_Spell = SpellType.NONE;
			this.m_actorObject = null;
		}

		// Token: 0x0600ABA3 RID: 43939 RVA: 0x00359640 File Offset: 0x00357840
		public override void OnEnter()
		{
			if (!this.m_actorObject.IsNone)
			{
				Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(this.m_actorObject.Value);
				if (actor != null && this.m_Spell != SpellType.NONE)
				{
					actor.ActivateSpellBirthState(this.m_Spell);
				}
			}
			base.Finish();
		}

		// Token: 0x0600ABA4 RID: 43940 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnUpdate()
		{
		}

		// Token: 0x0400925B RID: 37467
		public SpellType m_Spell;

		// Token: 0x0400925C RID: 37468
		public FsmGameObject m_actorObject;
	}
}
