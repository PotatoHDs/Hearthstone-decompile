using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3C RID: 3900
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the specified tag value for Entity with passed Entity ID in passed int.")]
	public class GetEntityIDTagValueAction : FsmStateAction
	{
		// Token: 0x0600AC79 RID: 44153 RVA: 0x0035D18A File Offset: 0x0035B38A
		public override void Reset()
		{
			this.m_TagValue = null;
		}

		// Token: 0x0600AC7A RID: 44154 RVA: 0x0035D194 File Offset: 0x0035B394
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_entityID.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No entity ID was passed!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (this.m_TagValue.IsNone)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - No variable hooked up to store tag value!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			Entity entity = GameState.Get().GetEntity(this.m_entityID.Value);
			if (entity == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter() - FAILED to find entity with id {1}", new object[]
				{
					this,
					this.m_entityID.Value
				});
				base.Finish();
				return;
			}
			this.m_TagValue = entity.GetTag(this.m_tagToCheck);
			base.Finish();
		}

		// Token: 0x0400934E RID: 37710
		public FsmInt m_entityID;

		// Token: 0x0400934F RID: 37711
		public GAME_TAG m_tagToCheck;

		// Token: 0x04009350 RID: 37712
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_TagValue;
	}
}
