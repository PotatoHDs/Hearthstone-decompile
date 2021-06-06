using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E24 RID: 3620
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Translates a Game Object. Use a Vector3 variable and/or XYZ components. To leave any axis unchanged, set variable to 'None'.")]
	public class Translate : FsmStateAction
	{
		// Token: 0x0600A75F RID: 42847 RVA: 0x0034C830 File Offset: 0x0034AA30
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.z = new FsmFloat
			{
				UseVariable = true
			};
			this.space = Space.Self;
			this.perSecond = true;
			this.everyFrame = true;
			this.lateUpdate = false;
			this.fixedUpdate = false;
		}

		// Token: 0x0600A760 RID: 42848 RVA: 0x0034C8A4 File Offset: 0x0034AAA4
		public override void OnPreprocess()
		{
			if (this.fixedUpdate)
			{
				base.Fsm.HandleFixedUpdate = true;
			}
			if (this.lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0600A761 RID: 42849 RVA: 0x0034C8CE File Offset: 0x0034AACE
		public override void OnEnter()
		{
			if (!this.everyFrame && !this.lateUpdate && !this.fixedUpdate)
			{
				this.DoTranslate();
				base.Finish();
			}
		}

		// Token: 0x0600A762 RID: 42850 RVA: 0x0034C8F4 File Offset: 0x0034AAF4
		public override void OnUpdate()
		{
			if (!this.lateUpdate && !this.fixedUpdate)
			{
				this.DoTranslate();
			}
		}

		// Token: 0x0600A763 RID: 42851 RVA: 0x0034C90C File Offset: 0x0034AB0C
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoTranslate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A764 RID: 42852 RVA: 0x0034C92A File Offset: 0x0034AB2A
		public override void OnFixedUpdate()
		{
			if (this.fixedUpdate)
			{
				this.DoTranslate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A765 RID: 42853 RVA: 0x0034C948 File Offset: 0x0034AB48
		private void DoTranslate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vector.IsNone ? new Vector3(this.x.Value, this.y.Value, this.z.Value) : this.vector.Value;
			if (!this.x.IsNone)
			{
				vector.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				vector.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				vector.z = this.z.Value;
			}
			if (!this.perSecond)
			{
				ownerDefaultTarget.transform.Translate(vector, this.space);
				return;
			}
			ownerDefaultTarget.transform.Translate(vector * Time.deltaTime, this.space);
		}

		// Token: 0x04008DFB RID: 36347
		[RequiredField]
		[Tooltip("The game object to translate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008DFC RID: 36348
		[UIHint(UIHint.Variable)]
		[Readonly]
		[Tooltip("A translation vector. NOTE: You can override individual axis below.")]
		public FsmVector3 vector;

		// Token: 0x04008DFD RID: 36349
		[Tooltip("Translation along x axis.")]
		public FsmFloat x;

		// Token: 0x04008DFE RID: 36350
		[Tooltip("Translation along y axis.")]
		public FsmFloat y;

		// Token: 0x04008DFF RID: 36351
		[Tooltip("Translation along z axis.")]
		public FsmFloat z;

		// Token: 0x04008E00 RID: 36352
		[Tooltip("Translate in local or world space.")]
		public Space space;

		// Token: 0x04008E01 RID: 36353
		[Tooltip("Translate over one second")]
		public bool perSecond;

		// Token: 0x04008E02 RID: 36354
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008E03 RID: 36355
		[Tooltip("Perform the translate in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;

		// Token: 0x04008E04 RID: 36356
		[Tooltip("Perform the translate in FixedUpdate. This is useful when working with rigid bodies and physics.")]
		public bool fixedUpdate;
	}
}
