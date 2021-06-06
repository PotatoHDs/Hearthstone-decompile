using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C68 RID: 3176
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the pressed state of a Key.")]
	public class GetKey : FsmStateAction
	{
		// Token: 0x06009F5F RID: 40799 RVA: 0x0032E70B File Offset: 0x0032C90B
		public override void Reset()
		{
			this.key = KeyCode.None;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x0032E722 File Offset: 0x0032C922
		public override void OnEnter()
		{
			this.DoGetKey();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x0032E738 File Offset: 0x0032C938
		public override void OnUpdate()
		{
			this.DoGetKey();
		}

		// Token: 0x06009F62 RID: 40802 RVA: 0x0032E740 File Offset: 0x0032C940
		private void DoGetKey()
		{
			this.storeResult.Value = Input.GetKey(this.key);
		}

		// Token: 0x04008500 RID: 34048
		[RequiredField]
		[Tooltip("The key to test.")]
		public KeyCode key;

		// Token: 0x04008501 RID: 34049
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store if the key is down (True) or up (False).")]
		public FsmBool storeResult;

		// Token: 0x04008502 RID: 34050
		[Tooltip("Repeat every frame. Useful if you're waiting for a key press/release.")]
		public bool everyFrame;
	}
}
