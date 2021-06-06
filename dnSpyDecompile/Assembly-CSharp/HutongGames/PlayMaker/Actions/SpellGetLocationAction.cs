using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8B RID: 3979
	[ActionCategory("Pegasus")]
	[Tooltip("Get position from the bones in the board")]
	public class SpellGetLocationAction : SpellAction
	{
		// Token: 0x0600ADC4 RID: 44484 RVA: 0x0036240D File Offset: 0x0036060D
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADC5 RID: 44485 RVA: 0x00362420 File Offset: 0x00360620
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				return;
			}
			GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(this.m_spell, this.m_Location, this.m_Bone.Value);
			if (spellLocationObject != null)
			{
				this.m_StorePosition.Value = spellLocationObject.transform.position;
			}
			base.Finish();
		}

		// Token: 0x0400949D RID: 38045
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x0400949E RID: 38046
		[Tooltip("Choose a location from the spell to get the position from")]
		public SpellLocation m_Location;

		// Token: 0x0400949F RID: 38047
		[Tooltip("Choose a bone, usually used with board location")]
		public FsmString m_Bone;

		// Token: 0x040094A0 RID: 38048
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the position")]
		public FsmVector3 m_StorePosition;
	}
}
