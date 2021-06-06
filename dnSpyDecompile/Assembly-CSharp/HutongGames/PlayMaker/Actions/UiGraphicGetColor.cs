using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E63 RID: 3683
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the color of a UI Graphic component. (E.g. UI Sprite)")]
	public class UiGraphicGetColor : ComponentAction<Graphic>
	{
		// Token: 0x0600A88C RID: 43148 RVA: 0x003506D0 File Offset: 0x0034E8D0
		public override void Reset()
		{
			this.gameObject = null;
			this.color = null;
		}

		// Token: 0x0600A88D RID: 43149 RVA: 0x003506E0 File Offset: 0x0034E8E0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.uiComponent = this.cachedComponent;
			}
			this.DoGetColorValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A88E RID: 43150 RVA: 0x00350728 File Offset: 0x0034E928
		public override void OnUpdate()
		{
			this.DoGetColorValue();
		}

		// Token: 0x0600A88F RID: 43151 RVA: 0x00350730 File Offset: 0x0034E930
		private void DoGetColorValue()
		{
			if (this.uiComponent != null)
			{
				this.color.Value = this.uiComponent.color;
			}
		}

		// Token: 0x04008F31 RID: 36657
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with the UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F32 RID: 36658
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color of the UI component")]
		public FsmColor color;

		// Token: 0x04008F33 RID: 36659
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F34 RID: 36660
		private Graphic uiComponent;
	}
}
