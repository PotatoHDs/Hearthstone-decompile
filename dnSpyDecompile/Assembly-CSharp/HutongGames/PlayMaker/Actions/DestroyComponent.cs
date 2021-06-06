using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0E RID: 3086
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Component of an Object.")]
	public class DestroyComponent : FsmStateAction
	{
		// Token: 0x06009DCF RID: 40399 RVA: 0x00329E54 File Offset: 0x00328054
		public override void Reset()
		{
			this.aComponent = null;
			this.gameObject = null;
			this.component = null;
		}

		// Token: 0x06009DD0 RID: 40400 RVA: 0x00329E6B File Offset: 0x0032806B
		public override void OnEnter()
		{
			this.DoDestroyComponent((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			base.Finish();
		}

		// Token: 0x06009DD1 RID: 40401 RVA: 0x00329EA0 File Offset: 0x003280A0
		private void DoDestroyComponent(GameObject go)
		{
			this.aComponent = go.GetComponent(ReflectionUtils.GetGlobalType(this.component.Value));
			if (this.aComponent == null)
			{
				base.LogError("No such component: " + this.component.Value);
				return;
			}
			UnityEngine.Object.Destroy(this.aComponent);
		}

		// Token: 0x04008332 RID: 33586
		[RequiredField]
		[Tooltip("The GameObject that owns the Component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008333 RID: 33587
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
		[Tooltip("The name of the Component to destroy.")]
		public FsmString component;

		// Token: 0x04008334 RID: 33588
		private Component aComponent;
	}
}
