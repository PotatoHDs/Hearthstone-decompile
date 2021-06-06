using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA1 RID: 2977
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Adds a Script to a Game Object. Use this to change the behaviour of objects on the fly. Optionally remove the Script on exiting the state.")]
	public class AddScript : FsmStateAction
	{
		// Token: 0x06009BB6 RID: 39862 RVA: 0x003204BF File Offset: 0x0031E6BF
		public override void Reset()
		{
			this.gameObject = null;
			this.script = null;
		}

		// Token: 0x06009BB7 RID: 39863 RVA: 0x003204CF File Offset: 0x0031E6CF
		public override void OnEnter()
		{
			this.DoAddComponent((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			base.Finish();
		}

		// Token: 0x06009BB8 RID: 39864 RVA: 0x00320502 File Offset: 0x0031E702
		public override void OnExit()
		{
			if (this.removeOnExit.Value && this.addedComponent != null)
			{
				UnityEngine.Object.Destroy(this.addedComponent);
			}
		}

		// Token: 0x06009BB9 RID: 39865 RVA: 0x0032052C File Offset: 0x0031E72C
		private void DoAddComponent(GameObject go)
		{
			this.addedComponent = go.AddComponent(ReflectionUtils.GetGlobalType(this.script.Value));
			if (this.addedComponent == null)
			{
				base.LogError("Can't add script: " + this.script.Value);
			}
		}

		// Token: 0x04008106 RID: 33030
		[RequiredField]
		[Tooltip("The GameObject to add the script to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008107 RID: 33031
		[RequiredField]
		[Tooltip("The Script to add to the GameObject.")]
		[UIHint(UIHint.ScriptComponent)]
		public FsmString script;

		// Token: 0x04008108 RID: 33032
		[Tooltip("Remove the script from the GameObject when this State is exited.")]
		public FsmBool removeOnExit;

		// Token: 0x04008109 RID: 33033
		private Component addedComponent;
	}
}
