using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F66 RID: 3942
	[ActionCategory("Pegasus")]
	[Tooltip("Randomly picks an object from a list and creates it. THE CREATED OBJECT MUST BE DESTROYED!")]
	public class RandomCreateObjectAction : FsmStateAction
	{
		// Token: 0x0600AD21 RID: 44321 RVA: 0x00360054 File Offset: 0x0035E254
		public override void Reset()
		{
			this.m_CreatedObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_Objects = new FsmGameObject[3];
			this.m_Weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
		}

		// Token: 0x0600AD22 RID: 44322 RVA: 0x003600B4 File Offset: 0x0035E2B4
		public override void OnEnter()
		{
			if (this.m_Objects == null || this.m_Objects.Length == 0)
			{
				base.Finish();
				return;
			}
			GameObject gameObject = null;
			FsmGameObject fsmGameObject = this.m_Objects[ActionHelpers.GetRandomWeightedIndex(this.m_Weights)];
			if (fsmGameObject != null && !fsmGameObject.IsNone && fsmGameObject.Value)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(fsmGameObject.Value);
				TransformUtil.CopyWorld(gameObject, fsmGameObject.Value);
			}
			if (this.m_CreatedObject != null)
			{
				this.m_CreatedObject.Value = gameObject;
			}
			base.Finish();
		}

		// Token: 0x04009405 RID: 37893
		[UIHint(UIHint.Variable)]
		[Tooltip("The created object gets stored in this variable.")]
		public FsmGameObject m_CreatedObject;

		// Token: 0x04009406 RID: 37894
		[CompoundArray("Random Objects", "Object", "Weight")]
		public FsmGameObject[] m_Objects;

		// Token: 0x04009407 RID: 37895
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] m_Weights;
	}
}
