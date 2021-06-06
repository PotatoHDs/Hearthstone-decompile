using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E50 RID: 3664
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the explicit navigation properties of a UI Selectable component. ")]
	public class UiNavigationExplicitGetProperties : ComponentAction<Selectable>
	{
		// Token: 0x0600A834 RID: 43060 RVA: 0x0034EE72 File Offset: 0x0034D072
		public override void Reset()
		{
			this.gameObject = null;
			this.selectOnDown = null;
			this.selectOnUp = null;
			this.selectOnLeft = null;
			this.selectOnRight = null;
		}

		// Token: 0x0600A835 RID: 43061 RVA: 0x0034EE98 File Offset: 0x0034D098
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A836 RID: 43062 RVA: 0x0034EED8 File Offset: 0x0034D0D8
		private void DoGetValue()
		{
			if (this._selectable != null)
			{
				if (!this.selectOnUp.IsNone)
				{
					this.selectOnUp.Value = ((this._selectable.navigation.selectOnUp == null) ? null : this._selectable.navigation.selectOnUp.gameObject);
				}
				if (!this.selectOnDown.IsNone)
				{
					this.selectOnDown.Value = ((this._selectable.navigation.selectOnDown == null) ? null : this._selectable.navigation.selectOnDown.gameObject);
				}
				if (!this.selectOnLeft.IsNone)
				{
					this.selectOnLeft.Value = ((this._selectable.navigation.selectOnLeft == null) ? null : this._selectable.navigation.selectOnLeft.gameObject);
				}
				if (!this.selectOnRight.IsNone)
				{
					this.selectOnRight.Value = ((this._selectable.navigation.selectOnRight == null) ? null : this._selectable.navigation.selectOnRight.gameObject);
				}
			}
		}

		// Token: 0x04008EB2 RID: 36530
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EB3 RID: 36531
		[Tooltip("The down Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnDown;

		// Token: 0x04008EB4 RID: 36532
		[Tooltip("The up Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnUp;

		// Token: 0x04008EB5 RID: 36533
		[Tooltip("The left Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnLeft;

		// Token: 0x04008EB6 RID: 36534
		[Tooltip("The right Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnRight;

		// Token: 0x04008EB7 RID: 36535
		private Selectable _selectable;
	}
}
