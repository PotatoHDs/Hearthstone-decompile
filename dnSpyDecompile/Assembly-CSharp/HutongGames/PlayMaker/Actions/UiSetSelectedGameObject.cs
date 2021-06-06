using System;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4D RID: 3661
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the EventSystem's currently select GameObject.")]
	public class UiSetSelectedGameObject : FsmStateAction
	{
		// Token: 0x0600A826 RID: 43046 RVA: 0x0034E99E File Offset: 0x0034CB9E
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A827 RID: 43047 RVA: 0x0034E9A7 File Offset: 0x0034CBA7
		public override void OnEnter()
		{
			this.DoSetSelectedGameObject();
			base.Finish();
		}

		// Token: 0x0600A828 RID: 43048 RVA: 0x0034E9B5 File Offset: 0x0034CBB5
		private void DoSetSelectedGameObject()
		{
			EventSystem.current.SetSelectedGameObject(this.gameObject.Value);
		}

		// Token: 0x04008E98 RID: 36504
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject to select.")]
		public FsmGameObject gameObject;
	}
}
