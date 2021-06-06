using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFC RID: 3580
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Set the Tag on all children of a GameObject. Optionally filter by component.")]
	public class SetTagsOnChildren : FsmStateAction
	{
		// Token: 0x0600A6B7 RID: 42679 RVA: 0x00349FC3 File Offset: 0x003481C3
		public override void Reset()
		{
			this.gameObject = null;
			this.tag = null;
			this.filterByComponent = null;
		}

		// Token: 0x0600A6B8 RID: 42680 RVA: 0x00349FDA File Offset: 0x003481DA
		public override void OnEnter()
		{
			this.SetTag(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x0600A6B9 RID: 42681 RVA: 0x00349FFC File Offset: 0x003481FC
		private void SetTag(GameObject parent)
		{
			if (parent == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.filterByComponent.Value))
			{
				using (IEnumerator enumerator = parent.transform.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						((Transform)obj).gameObject.tag = this.tag.Value;
					}
					goto IL_AC;
				}
			}
			this.UpdateComponentFilter();
			if (this.componentFilter != null)
			{
				Component[] componentsInChildren = parent.GetComponentsInChildren(this.componentFilter);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].gameObject.tag = this.tag.Value;
				}
			}
			IL_AC:
			base.Finish();
		}

		// Token: 0x0600A6BA RID: 42682 RVA: 0x0034A0CC File Offset: 0x003482CC
		private void UpdateComponentFilter()
		{
			this.componentFilter = ReflectionUtils.GetGlobalType(this.filterByComponent.Value);
			if (this.componentFilter == null)
			{
				this.componentFilter = ReflectionUtils.GetGlobalType("UnityEngine." + this.filterByComponent.Value);
			}
			if (this.componentFilter == null)
			{
				Debug.LogWarning("Couldn't get type: " + this.filterByComponent.Value);
			}
		}

		// Token: 0x04008D33 RID: 36147
		[RequiredField]
		[Tooltip("GameObject Parent")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D34 RID: 36148
		[RequiredField]
		[UIHint(UIHint.Tag)]
		[Tooltip("Set Tag To...")]
		public FsmString tag;

		// Token: 0x04008D35 RID: 36149
		[UIHint(UIHint.ScriptComponent)]
		[Tooltip("Only set the Tag on children with this component.")]
		public FsmString filterByComponent;

		// Token: 0x04008D36 RID: 36150
		private Type componentFilter;
	}
}
