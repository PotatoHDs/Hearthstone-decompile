using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8D RID: 3725
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the direction of a UI Slider component.")]
	public class UiSliderGetDirection : ComponentAction<Slider>
	{
		// Token: 0x0600A963 RID: 43363 RVA: 0x00352816 File Offset: 0x00350A16
		public override void Reset()
		{
			this.gameObject = null;
			this.direction = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A964 RID: 43364 RVA: 0x00352830 File Offset: 0x00350A30
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A965 RID: 43365 RVA: 0x00352878 File Offset: 0x00350A78
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A966 RID: 43366 RVA: 0x00352880 File Offset: 0x00350A80
		private void DoGetValue()
		{
			if (this.slider != null)
			{
				this.direction.Value = this.slider.direction;
			}
		}

		// Token: 0x0400900E RID: 36878
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400900F RID: 36879
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction of the UI Slider.")]
		[ObjectType(typeof(Slider.Direction))]
		public FsmEnum direction;

		// Token: 0x04009010 RID: 36880
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009011 RID: 36881
		private Slider slider;
	}
}
