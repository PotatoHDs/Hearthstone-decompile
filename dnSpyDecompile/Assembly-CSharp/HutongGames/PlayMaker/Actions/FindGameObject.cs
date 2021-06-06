using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C26 RID: 3110
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds a Game Object by Name and/or Tag.")]
	public class FindGameObject : FsmStateAction
	{
		// Token: 0x06009E2F RID: 40495 RVA: 0x0032AE67 File Offset: 0x00329067
		public override void Reset()
		{
			this.objectName = "";
			this.withTag = "Untagged";
			this.store = null;
		}

		// Token: 0x06009E30 RID: 40496 RVA: 0x0032AE90 File Offset: 0x00329090
		public override void OnEnter()
		{
			this.Find();
			base.Finish();
		}

		// Token: 0x06009E31 RID: 40497 RVA: 0x0032AEA0 File Offset: 0x003290A0
		private void Find()
		{
			if (!(this.withTag.Value != "Untagged"))
			{
				this.store.Value = GameObject.Find(this.objectName.Value);
				return;
			}
			if (!string.IsNullOrEmpty(this.objectName.Value))
			{
				foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(this.withTag.Value))
				{
					if (gameObject.name == this.objectName.Value)
					{
						this.store.Value = gameObject;
						return;
					}
				}
				this.store.Value = null;
				return;
			}
			this.store.Value = GameObject.FindGameObjectWithTag(this.withTag.Value);
		}

		// Token: 0x06009E32 RID: 40498 RVA: 0x0032AF65 File Offset: 0x00329165
		public override string ErrorCheck()
		{
			if (string.IsNullOrEmpty(this.objectName.Value) && string.IsNullOrEmpty(this.withTag.Value))
			{
				return "Specify Name, Tag, or both.";
			}
			return null;
		}

		// Token: 0x04008389 RID: 33673
		[Tooltip("The name of the GameObject to find. You can leave this empty if you specify a Tag.")]
		public FsmString objectName;

		// Token: 0x0400838A RID: 33674
		[UIHint(UIHint.Tag)]
		[Tooltip("Find a GameObject with this tag. If Object Name is specified then both name and Tag must match.")]
		public FsmString withTag;

		// Token: 0x0400838B RID: 33675
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject store;
	}
}
