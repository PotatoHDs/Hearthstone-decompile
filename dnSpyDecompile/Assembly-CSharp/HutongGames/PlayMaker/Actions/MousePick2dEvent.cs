using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D15 RID: 3349
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends Events based on mouse interactions with a 2d Game Object: MouseOver, MouseDown, MouseUp, MouseOff.")]
	public class MousePick2dEvent : FsmStateAction
	{
		// Token: 0x0600A265 RID: 41573 RVA: 0x0033BDC8 File Offset: 0x00339FC8
		public override void Reset()
		{
			this.GameObject = null;
			this.mouseOver = null;
			this.mouseDown = null;
			this.mouseUp = null;
			this.mouseOff = null;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = true;
		}

		// Token: 0x0600A266 RID: 41574 RVA: 0x0033BE17 File Offset: 0x0033A017
		public override void OnEnter()
		{
			this.DoMousePickEvent();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A267 RID: 41575 RVA: 0x0033BE2D File Offset: 0x0033A02D
		public override void OnUpdate()
		{
			this.DoMousePickEvent();
		}

		// Token: 0x0600A268 RID: 41576 RVA: 0x0033BE38 File Offset: 0x0033A038
		private void DoMousePickEvent()
		{
			if (this.DoRaycast())
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

		// Token: 0x0600A269 RID: 41577 RVA: 0x0033BEC4 File Offset: 0x0033A0C4
		private bool DoRaycast()
		{
			GameObject y = (this.GameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.GameObject.GameObject.Value;
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.PositiveInfinity, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			Fsm.RecordLastRaycastHit2DInfo(base.Fsm, rayIntersection);
			return rayIntersection.transform != null && rayIntersection.transform.gameObject == y;
		}

		// Token: 0x040088B8 RID: 35000
		[CheckForComponent(typeof(Collider2D))]
		[Tooltip("The GameObject with a Collider2D attached.")]
		public FsmOwnerDefault GameObject;

		// Token: 0x040088B9 RID: 35001
		[Tooltip("Event to send when the mouse is over the GameObject.")]
		public FsmEvent mouseOver;

		// Token: 0x040088BA RID: 35002
		[Tooltip("Event to send when the mouse is pressed while over the GameObject.")]
		public FsmEvent mouseDown;

		// Token: 0x040088BB RID: 35003
		[Tooltip("Event to send when the mouse is released while over the GameObject.")]
		public FsmEvent mouseUp;

		// Token: 0x040088BC RID: 35004
		[Tooltip("Event to send when the mouse moves off the GameObject.")]
		public FsmEvent mouseOff;

		// Token: 0x040088BD RID: 35005
		[Tooltip("Pick only from these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;

		// Token: 0x040088BE RID: 35006
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040088BF RID: 35007
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
