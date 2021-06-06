using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C95 RID: 3221
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get Vector3 Length.")]
	public class GetVectorLength : FsmStateAction
	{
		// Token: 0x0600A021 RID: 40993 RVA: 0x003300EF File Offset: 0x0032E2EF
		public override void Reset()
		{
			this.vector3 = null;
			this.storeLength = null;
		}

		// Token: 0x0600A022 RID: 40994 RVA: 0x003300FF File Offset: 0x0032E2FF
		public override void OnEnter()
		{
			this.DoVectorLength();
			base.Finish();
		}

		// Token: 0x0600A023 RID: 40995 RVA: 0x00330110 File Offset: 0x0032E310
		private void DoVectorLength()
		{
			if (this.vector3 == null)
			{
				return;
			}
			if (this.storeLength == null)
			{
				return;
			}
			this.storeLength.Value = this.vector3.Value.magnitude;
		}

		// Token: 0x0400859F RID: 34207
		public FsmVector3 vector3;

		// Token: 0x040085A0 RID: 34208
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeLength;
	}
}
