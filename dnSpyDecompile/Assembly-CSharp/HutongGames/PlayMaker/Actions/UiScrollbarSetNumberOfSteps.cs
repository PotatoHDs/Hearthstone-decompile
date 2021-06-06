using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E87 RID: 3719
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the number of distinct scroll positions allowed for a UI Scrollbar component.")]
	public class UiScrollbarSetNumberOfSteps : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A93F RID: 43327 RVA: 0x00352268 File Offset: 0x00350468
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A940 RID: 43328 RVA: 0x00352288 File Offset: 0x00350488
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
			}
			this.originalValue = this.scrollbar.numberOfSteps;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A941 RID: 43329 RVA: 0x003522E1 File Offset: 0x003504E1
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A942 RID: 43330 RVA: 0x003522E9 File Offset: 0x003504E9
		private void DoSetValue()
		{
			if (this.scrollbar != null)
			{
				this.scrollbar.numberOfSteps = this.value.Value;
			}
		}

		// Token: 0x0600A943 RID: 43331 RVA: 0x0035230F File Offset: 0x0035050F
		public override void OnExit()
		{
			if (this.scrollbar == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollbar.numberOfSteps = this.originalValue;
			}
		}

		// Token: 0x04008FE8 RID: 36840
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FE9 RID: 36841
		[RequiredField]
		[Tooltip("The number of distinct scroll positions allowed for the UI Scrollbar.")]
		public FsmInt value;

		// Token: 0x04008FEA RID: 36842
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FEB RID: 36843
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FEC RID: 36844
		private UnityEngine.UI.Scrollbar scrollbar;

		// Token: 0x04008FED RID: 36845
		private int originalValue;
	}
}
