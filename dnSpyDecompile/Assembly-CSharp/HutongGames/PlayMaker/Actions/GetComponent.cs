using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4F RID: 3151
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Gets a Component attached to a GameObject and stores it in an Object variable. NOTE: Set the Object variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
	public class GetComponent : FsmStateAction
	{
		// Token: 0x06009EE5 RID: 40677 RVA: 0x0032CC1B File Offset: 0x0032AE1B
		public override void Reset()
		{
			this.gameObject = null;
			this.storeComponent = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EE6 RID: 40678 RVA: 0x0032CC32 File Offset: 0x0032AE32
		public override void OnEnter()
		{
			this.DoGetComponent();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EE7 RID: 40679 RVA: 0x0032CC48 File Offset: 0x0032AE48
		public override void OnUpdate()
		{
			this.DoGetComponent();
		}

		// Token: 0x06009EE8 RID: 40680 RVA: 0x0032CC50 File Offset: 0x0032AE50
		private void DoGetComponent()
		{
			if (this.storeComponent == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.storeComponent.IsNone)
			{
				return;
			}
			this.storeComponent.Value = ownerDefaultTarget.GetComponent(this.storeComponent.ObjectType);
		}

		// Token: 0x04008442 RID: 33858
		[Tooltip("The GameObject that owns the component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008443 RID: 33859
		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Store the component in an Object variable.\nNOTE: Set theObject variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
		public FsmObject storeComponent;

		// Token: 0x04008444 RID: 33860
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
