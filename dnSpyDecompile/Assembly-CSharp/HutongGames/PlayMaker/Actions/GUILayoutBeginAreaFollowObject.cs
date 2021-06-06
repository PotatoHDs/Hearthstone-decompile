using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA3 RID: 3235
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a GUILayout area that follows the specified game object. Useful for overlays (e.g., playerName). NOTE: Block must end with a corresponding GUILayoutEndArea.")]
	public class GUILayoutBeginAreaFollowObject : FsmStateAction
	{
		// Token: 0x0600A052 RID: 41042 RVA: 0x00330C3C File Offset: 0x0032EE3C
		public override void Reset()
		{
			this.gameObject = null;
			this.offsetLeft = 0f;
			this.offsetTop = 0f;
			this.width = 1f;
			this.height = 1f;
			this.normalized = true;
			this.style = "";
		}

		// Token: 0x0600A053 RID: 41043 RVA: 0x00330CAC File Offset: 0x0032EEAC
		public override void OnGUI()
		{
			GameObject value = this.gameObject.Value;
			if (value == null || Camera.main == null)
			{
				GUILayoutBeginAreaFollowObject.DummyBeginArea();
				return;
			}
			Vector3 position = value.transform.position;
			if (Camera.main.transform.InverseTransformPoint(position).z < 0f)
			{
				GUILayoutBeginAreaFollowObject.DummyBeginArea();
				return;
			}
			Vector2 vector = Camera.main.WorldToScreenPoint(position);
			float x = vector.x + (this.normalized.Value ? (this.offsetLeft.Value * (float)Screen.width) : this.offsetLeft.Value);
			float y = vector.y + (this.normalized.Value ? (this.offsetTop.Value * (float)Screen.width) : this.offsetTop.Value);
			Rect screenRect = new Rect(x, y, this.width.Value, this.height.Value);
			if (this.normalized.Value)
			{
				screenRect.width *= (float)Screen.width;
				screenRect.height *= (float)Screen.height;
			}
			screenRect.y = (float)Screen.height - screenRect.y;
			GUILayout.BeginArea(screenRect, this.style.Value);
		}

		// Token: 0x0600A054 RID: 41044 RVA: 0x00330E04 File Offset: 0x0032F004
		private static void DummyBeginArea()
		{
			GUILayout.BeginArea(default(Rect));
		}

		// Token: 0x040085D8 RID: 34264
		[RequiredField]
		[Tooltip("The GameObject to follow.")]
		public FsmGameObject gameObject;

		// Token: 0x040085D9 RID: 34265
		[RequiredField]
		public FsmFloat offsetLeft;

		// Token: 0x040085DA RID: 34266
		[RequiredField]
		public FsmFloat offsetTop;

		// Token: 0x040085DB RID: 34267
		[RequiredField]
		public FsmFloat width;

		// Token: 0x040085DC RID: 34268
		[RequiredField]
		public FsmFloat height;

		// Token: 0x040085DD RID: 34269
		[Tooltip("Use normalized screen coordinates (0-1).")]
		public FsmBool normalized;

		// Token: 0x040085DE RID: 34270
		[Tooltip("Optional named style in the current GUISkin")]
		public FsmString style;
	}
}
