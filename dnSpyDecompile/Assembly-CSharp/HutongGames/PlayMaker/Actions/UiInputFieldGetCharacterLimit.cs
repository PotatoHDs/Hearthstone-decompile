using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6C RID: 3692
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the Character Limit value of a UI InputField component. This is the maximum number of characters that the user can type into the field.")]
	public class UiInputFieldGetCharacterLimit : ComponentAction<InputField>
	{
		// Token: 0x0600A8B9 RID: 43193 RVA: 0x00350D46 File Offset: 0x0034EF46
		public override void Reset()
		{
			this.characterLimit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8BA RID: 43194 RVA: 0x00350D58 File Offset: 0x0034EF58
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A8BB RID: 43195 RVA: 0x00350DA0 File Offset: 0x0034EFA0
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A8BC RID: 43196 RVA: 0x00350DA8 File Offset: 0x0034EFA8
		private void DoGetValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this.characterLimit.Value = this.inputField.characterLimit;
			base.Fsm.Event((this.inputField.characterLimit > 0) ? this.isLimitedEvent : this.hasNoLimitEvent);
		}

		// Token: 0x04008F59 RID: 36697
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F5A RID: 36698
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The maximum number of characters that the user can type into the UI InputField component.")]
		public FsmInt characterLimit;

		// Token: 0x04008F5B RID: 36699
		[Tooltip("Event sent if limit is infinite (equal to 0)")]
		public FsmEvent hasNoLimitEvent;

		// Token: 0x04008F5C RID: 36700
		[Tooltip("Event sent if limit is more than 0")]
		public FsmEvent isLimitedEvent;

		// Token: 0x04008F5D RID: 36701
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008F5E RID: 36702
		private InputField inputField;
	}
}
