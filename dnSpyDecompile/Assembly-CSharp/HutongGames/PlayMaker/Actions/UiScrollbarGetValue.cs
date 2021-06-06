using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E84 RID: 3716
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the value of a UI Scrollbar component.")]
	public class UiScrollbarGetValue : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A930 RID: 43312 RVA: 0x00351F9F File Offset: 0x0035019F
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A931 RID: 43313 RVA: 0x00351FB8 File Offset: 0x003501B8
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

		// Token: 0x0600A932 RID: 43314 RVA: 0x00352000 File Offset: 0x00350200
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A933 RID: 43315 RVA: 0x00352008 File Offset: 0x00350208
		private void DoGetValue()
		{
			if (this.scrollbar != null)
			{
				this.value.Value = this.scrollbar.value;
			}
		}

		// Token: 0x04008FD9 RID: 36825
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FDA RID: 36826
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The position value of the UI Scrollbar.")]
		public FsmFloat value;

		// Token: 0x04008FDB RID: 36827
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FDC RID: 36828
		private UnityEngine.UI.Scrollbar scrollbar;
	}
}
