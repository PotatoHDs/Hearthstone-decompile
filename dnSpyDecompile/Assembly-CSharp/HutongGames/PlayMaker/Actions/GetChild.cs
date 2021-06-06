using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4A RID: 3146
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds the Child of a GameObject by Name and/or Tag. Use this to find attach points etc. NOTE: This action will search recursively through all children and return the first match; To find a specific child use Find Child.")]
	public class GetChild : FsmStateAction
	{
		// Token: 0x06009ECF RID: 40655 RVA: 0x0032C77E File Offset: 0x0032A97E
		public override void Reset()
		{
			this.gameObject = null;
			this.childName = "";
			this.withTag = "Untagged";
			this.storeResult = null;
		}

		// Token: 0x06009ED0 RID: 40656 RVA: 0x0032C7AE File Offset: 0x0032A9AE
		public override void OnEnter()
		{
			this.storeResult.Value = GetChild.DoGetChildByName(base.Fsm.GetOwnerDefaultTarget(this.gameObject), this.childName.Value, this.withTag.Value);
			base.Finish();
		}

		// Token: 0x06009ED1 RID: 40657 RVA: 0x0032C7F0 File Offset: 0x0032A9F0
		private static GameObject DoGetChildByName(GameObject root, string name, string tag)
		{
			if (root == null)
			{
				return null;
			}
			foreach (object obj in root.transform)
			{
				Transform transform = (Transform)obj;
				if (!string.IsNullOrEmpty(name))
				{
					if (transform.name == name)
					{
						if (string.IsNullOrEmpty(tag))
						{
							return transform.gameObject;
						}
						if (transform.tag.Equals(tag))
						{
							return transform.gameObject;
						}
					}
				}
				else if (!string.IsNullOrEmpty(tag) && transform.tag == tag)
				{
					return transform.gameObject;
				}
				GameObject gameObject = GetChild.DoGetChildByName(transform.gameObject, name, tag);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x06009ED2 RID: 40658 RVA: 0x0032C8D4 File Offset: 0x0032AAD4
		public override string ErrorCheck()
		{
			if (string.IsNullOrEmpty(this.childName.Value) && string.IsNullOrEmpty(this.withTag.Value))
			{
				return "Specify Child Name, Tag, or both.";
			}
			return null;
		}

		// Token: 0x0400842D RID: 33837
		[RequiredField]
		[Tooltip("The GameObject to search.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400842E RID: 33838
		[Tooltip("The name of the child to search for.")]
		public FsmString childName;

		// Token: 0x0400842F RID: 33839
		[UIHint(UIHint.Tag)]
		[Tooltip("The Tag to search for. If Child Name is set, both name and Tag need to match.")]
		public FsmString withTag;

		// Token: 0x04008430 RID: 33840
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject storeResult;
	}
}
