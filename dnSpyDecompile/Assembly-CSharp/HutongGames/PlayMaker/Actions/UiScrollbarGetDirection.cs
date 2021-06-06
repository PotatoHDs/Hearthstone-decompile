using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E83 RID: 3715
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the direction of a UI Scrollbar component.")]
	public class UiScrollbarGetDirection : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A92B RID: 43307 RVA: 0x00351F05 File Offset: 0x00350105
		public override void Reset()
		{
			this.gameObject = null;
			this.direction = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A92C RID: 43308 RVA: 0x00351F1C File Offset: 0x0035011C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A92D RID: 43309 RVA: 0x00351F64 File Offset: 0x00350164
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A92E RID: 43310 RVA: 0x00351F6C File Offset: 0x0035016C
		private void DoGetValue()
		{
			if (this.scrollbar != null)
			{
				this.direction.Value = this.scrollbar.direction;
			}
		}

		// Token: 0x04008FD5 RID: 36821
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FD6 RID: 36822
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the direction of the UI Scrollbar.")]
		[ObjectType(typeof(UnityEngine.UI.Scrollbar.Direction))]
		public FsmEnum direction;

		// Token: 0x04008FD7 RID: 36823
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FD8 RID: 36824
		private UnityEngine.UI.Scrollbar scrollbar;
	}
}
