using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9D RID: 2973
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Adds a Component to a Game Object. Use this to change the behaviour of objects on the fly. Optionally remove the Component on exiting the state.")]
	public class AddComponent : FsmStateAction
	{
		// Token: 0x06009BA1 RID: 39841 RVA: 0x003200A7 File Offset: 0x0031E2A7
		public override void Reset()
		{
			this.gameObject = null;
			this.component = null;
			this.storeComponent = null;
		}

		// Token: 0x06009BA2 RID: 39842 RVA: 0x003200BE File Offset: 0x0031E2BE
		public override void OnEnter()
		{
			this.DoAddComponent();
			base.Finish();
		}

		// Token: 0x06009BA3 RID: 39843 RVA: 0x003200CC File Offset: 0x0031E2CC
		public override void OnExit()
		{
			if (this.removeOnExit.Value && this.addedComponent != null)
			{
				UnityEngine.Object.Destroy(this.addedComponent);
			}
		}

		// Token: 0x06009BA4 RID: 39844 RVA: 0x003200F4 File Offset: 0x0031E2F4
		private void DoAddComponent()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.addedComponent = ownerDefaultTarget.AddComponent(ReflectionUtils.GetGlobalType(this.component.Value));
			this.storeComponent.Value = this.addedComponent;
			if (this.addedComponent == null)
			{
				base.LogError("Can't add component: " + this.component.Value);
			}
		}

		// Token: 0x040080ED RID: 33005
		[RequiredField]
		[Tooltip("The GameObject to add the Component to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040080EE RID: 33006
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
		[Title("Component Type")]
		[Tooltip("The type of Component to add to the Game Object.")]
		public FsmString component;

		// Token: 0x040080EF RID: 33007
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Component))]
		[Tooltip("Store the component in an Object variable. E.g., to use with Set Property.")]
		public FsmObject storeComponent;

		// Token: 0x040080F0 RID: 33008
		[Tooltip("Remove the Component when this State is exited.")]
		public FsmBool removeOnExit;

		// Token: 0x040080F1 RID: 33009
		private Component addedComponent;
	}
}
