using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E36 RID: 3638
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class UiEventSystemCurrentRayCastAll : FsmStateAction
	{
		// Token: 0x0600A7C8 RID: 42952 RVA: 0x0034D983 File Offset: 0x0034BB83
		public override void Reset()
		{
			this.screenPosition = null;
			this.orScreenPosition2d = new FsmVector2
			{
				UseVariable = true
			};
			this.gameObjectList = null;
			this.hitCount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7C9 RID: 42953 RVA: 0x0034D9B3 File Offset: 0x0034BBB3
		public override void OnEnter()
		{
			this.ExecuteRayCastAll();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7CA RID: 42954 RVA: 0x0034D9C9 File Offset: 0x0034BBC9
		public override void OnUpdate()
		{
			this.ExecuteRayCastAll();
		}

		// Token: 0x0600A7CB RID: 42955 RVA: 0x0034D9D4 File Offset: 0x0034BBD4
		private void ExecuteRayCastAll()
		{
			this.pointer = new PointerEventData(EventSystem.current);
			if (!this.orScreenPosition2d.IsNone)
			{
				this.pointer.position = this.orScreenPosition2d.Value;
			}
			else
			{
				this.pointer.position = new Vector2(this.screenPosition.Value.x, this.screenPosition.Value.y);
			}
			EventSystem.current.RaycastAll(this.pointer, this.raycastResults);
			if (!this.hitCount.IsNone)
			{
				this.hitCount.Value = this.raycastResults.Count;
			}
			this.gameObjectList.Resize(this.raycastResults.Count);
			int index = 0;
			foreach (RaycastResult raycastResult in this.raycastResults)
			{
				if (!this.gameObjectList.IsNone)
				{
					this.gameObjectList.Set(index, raycastResult.gameObject);
				}
			}
		}

		// Token: 0x04008E56 RID: 36438
		[RequiredField]
		[Tooltip("The ScreenPosition in pixels")]
		public FsmVector3 screenPosition;

		// Token: 0x04008E57 RID: 36439
		[Tooltip("The ScreenPosition in a Vector2")]
		public FsmVector2 orScreenPosition2d;

		// Token: 0x04008E58 RID: 36440
		[Tooltip("GameObjects hit by the raycast")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray gameObjectList;

		// Token: 0x04008E59 RID: 36441
		[Tooltip("Number of hits")]
		[UIHint(UIHint.Variable)]
		public FsmInt hitCount;

		// Token: 0x04008E5A RID: 36442
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008E5B RID: 36443
		private PointerEventData pointer;

		// Token: 0x04008E5C RID: 36444
		private List<RaycastResult> raycastResults = new List<RaycastResult>();
	}
}
