using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF2 RID: 3314
	[ActionCategory(ActionCategory.Input)]
	[ActionTarget(typeof(GameObject), "GameObject", false)]
	[Tooltip("Sends Events based on mouse interactions with a Game Object: MouseOver, MouseDown, MouseUp, MouseOff. Use Ray Distance to set how close the camera must be to pick the object.\n\nNOTE: Picking uses the Main Camera.")]
	public class MousePickEvent : FsmStateAction
	{
		// Token: 0x0600A1AA RID: 41386 RVA: 0x003388B8 File Offset: 0x00336AB8
		public override void Reset()
		{
			this.GameObject = null;
			this.rayDistance = 100f;
			this.mouseOver = null;
			this.mouseDown = null;
			this.mouseUp = null;
			this.mouseOff = null;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = true;
		}

		// Token: 0x0600A1AB RID: 41387 RVA: 0x00338917 File Offset: 0x00336B17
		public override void OnEnter()
		{
			this.DoMousePickEvent();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x0033892D File Offset: 0x00336B2D
		public override void OnUpdate()
		{
			this.DoMousePickEvent();
		}

		// Token: 0x0600A1AD RID: 41389 RVA: 0x00338938 File Offset: 0x00336B38
		private void DoMousePickEvent()
		{
			bool flag = this.DoRaycast();
			base.Fsm.RaycastHitInfo = ActionHelpers.mousePickInfo;
			if (flag)
			{
				if (this.mouseDown != null && Input.GetMouseButtonDown(0))
				{
					base.Fsm.Event(this.mouseDown);
				}
				if (this.mouseOver != null)
				{
					base.Fsm.Event(this.mouseOver);
				}
				if (this.mouseUp != null && Input.GetMouseButtonUp(0))
				{
					base.Fsm.Event(this.mouseUp);
					return;
				}
			}
			else if (this.mouseOff != null)
			{
				base.Fsm.Event(this.mouseOff);
			}
		}

		// Token: 0x0600A1AE RID: 41390 RVA: 0x003389D4 File Offset: 0x00336BD4
		private bool DoRaycast()
		{
			return ActionHelpers.IsMouseOver((this.GameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.GameObject.GameObject.Value, this.rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
		}

		// Token: 0x0600A1AF RID: 41391 RVA: 0x00338A2C File Offset: 0x00336C2C
		public override string ErrorCheck()
		{
			return "" + ActionHelpers.CheckRayDistance(this.rayDistance.Value) + ActionHelpers.CheckPhysicsSetup(base.Fsm.GetOwnerDefaultTarget(this.GameObject));
		}

		// Token: 0x040087B7 RID: 34743
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault GameObject;

		// Token: 0x040087B8 RID: 34744
		[Tooltip("Length of the ray to cast from the camera.")]
		public FsmFloat rayDistance = 100f;

		// Token: 0x040087B9 RID: 34745
		[Tooltip("Event to send when the mouse is over the GameObject.")]
		public FsmEvent mouseOver;

		// Token: 0x040087BA RID: 34746
		[Tooltip("Event to send when the mouse is pressed while over the GameObject.")]
		public FsmEvent mouseDown;

		// Token: 0x040087BB RID: 34747
		[Tooltip("Event to send when the mouse is released while over the GameObject.")]
		public FsmEvent mouseUp;

		// Token: 0x040087BC RID: 34748
		[Tooltip("Event to send when the mouse moves off the GameObject.")]
		public FsmEvent mouseOff;

		// Token: 0x040087BD RID: 34749
		[Tooltip("Pick only from these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;

		// Token: 0x040087BE RID: 34750
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040087BF RID: 34751
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
