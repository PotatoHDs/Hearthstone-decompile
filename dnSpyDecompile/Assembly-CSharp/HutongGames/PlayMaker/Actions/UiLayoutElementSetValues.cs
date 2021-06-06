using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4F RID: 3663
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets various properties of a UI Layout Element component.")]
	public class UiLayoutElementSetValues : ComponentAction<LayoutElement>
	{
		// Token: 0x0600A82F RID: 43055 RVA: 0x0034ECB4 File Offset: 0x0034CEB4
		public override void Reset()
		{
			this.gameObject = null;
			this.minWidth = new FsmFloat
			{
				UseVariable = true
			};
			this.minHeight = new FsmFloat
			{
				UseVariable = true
			};
			this.preferredWidth = new FsmFloat
			{
				UseVariable = true
			};
			this.preferredHeight = new FsmFloat
			{
				UseVariable = true
			};
			this.flexibleWidth = new FsmFloat
			{
				UseVariable = true
			};
			this.flexibleHeight = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A830 RID: 43056 RVA: 0x0034ED34 File Offset: 0x0034CF34
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.layoutElement = this.cachedComponent;
			}
			this.DoSetValues();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A831 RID: 43057 RVA: 0x0034ED7C File Offset: 0x0034CF7C
		public override void OnUpdate()
		{
			this.DoSetValues();
		}

		// Token: 0x0600A832 RID: 43058 RVA: 0x0034ED84 File Offset: 0x0034CF84
		private void DoSetValues()
		{
			if (this.layoutElement == null)
			{
				return;
			}
			if (!this.minWidth.IsNone)
			{
				this.layoutElement.minWidth = this.minWidth.Value;
			}
			if (!this.minHeight.IsNone)
			{
				this.layoutElement.minHeight = this.minHeight.Value;
			}
			if (!this.preferredWidth.IsNone)
			{
				this.layoutElement.preferredWidth = this.preferredWidth.Value;
			}
			if (!this.preferredHeight.IsNone)
			{
				this.layoutElement.preferredHeight = this.preferredHeight.Value;
			}
			if (!this.flexibleWidth.IsNone)
			{
				this.layoutElement.flexibleWidth = this.flexibleWidth.Value;
			}
			if (!this.flexibleHeight.IsNone)
			{
				this.layoutElement.flexibleHeight = this.flexibleHeight.Value;
			}
		}

		// Token: 0x04008EA9 RID: 36521
		[RequiredField]
		[CheckForComponent(typeof(LayoutElement))]
		[Tooltip("The GameObject with the UI LayoutElement component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EAA RID: 36522
		[ActionSection("Values")]
		[Tooltip("The minimum width this layout element should have.")]
		public FsmFloat minWidth;

		// Token: 0x04008EAB RID: 36523
		[Tooltip("The minimum height this layout element should have.")]
		public FsmFloat minHeight;

		// Token: 0x04008EAC RID: 36524
		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		public FsmFloat preferredWidth;

		// Token: 0x04008EAD RID: 36525
		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		public FsmFloat preferredHeight;

		// Token: 0x04008EAE RID: 36526
		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleWidth;

		// Token: 0x04008EAF RID: 36527
		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleHeight;

		// Token: 0x04008EB0 RID: 36528
		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008EB1 RID: 36529
		private LayoutElement layoutElement;
	}
}
