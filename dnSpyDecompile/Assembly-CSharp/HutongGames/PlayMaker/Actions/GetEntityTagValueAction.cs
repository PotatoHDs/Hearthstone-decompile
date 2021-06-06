using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3D RID: 3901
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the value of an Entity's tag in passed int.")]
	public class GetEntityTagValueAction : SpellAction
	{
		// Token: 0x0600AC7C RID: 44156 RVA: 0x0035D26A File Offset: 0x0035B46A
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_spellObject);
		}

		// Token: 0x0600AC7D RID: 44157 RVA: 0x0035D27D File Offset: 0x0035B47D
		public override void Reset()
		{
			this.m_TagValue = null;
		}

		// Token: 0x0600AC7E RID: 44158 RVA: 0x0035D288 File Offset: 0x0035B488
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_TagValue == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store tag value!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			Entity entity = base.GetEntity(this.m_whichEntity);
			if (entity == null)
			{
				global::Log.All.PrintError("{0}.OnEnter() - FAILED to find relevant entity: \"{1}\"", new object[]
				{
					this,
					this.m_whichEntity
				});
				base.Finish();
				return;
			}
			this.m_TagValue.Value = entity.GetTag(this.m_tagToCheck);
			base.Finish();
		}

		// Token: 0x04009351 RID: 37713
		public FsmOwnerDefault m_spellObject;

		// Token: 0x04009352 RID: 37714
		public SpellAction.Which m_whichEntity;

		// Token: 0x04009353 RID: 37715
		public GAME_TAG m_tagToCheck;

		// Token: 0x04009354 RID: 37716
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_TagValue;
	}
}
