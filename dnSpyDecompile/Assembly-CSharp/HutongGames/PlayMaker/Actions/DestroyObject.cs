using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0F RID: 3087
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Game Object.")]
	public class DestroyObject : FsmStateAction
	{
		// Token: 0x06009DD3 RID: 40403 RVA: 0x00329EFE File Offset: 0x003280FE
		public override void Reset()
		{
			this.gameObject = null;
			this.delay = 0f;
		}

		// Token: 0x06009DD4 RID: 40404 RVA: 0x00329F18 File Offset: 0x00328118
		public override void OnEnter()
		{
			GameObject value = this.gameObject.Value;
			if (value != null)
			{
				if (this.delay.Value <= 0f)
				{
					UnityEngine.Object.Destroy(value);
				}
				else
				{
					UnityEngine.Object.Destroy(value, this.delay.Value);
				}
				if (this.detachChildren.Value)
				{
					value.transform.DetachChildren();
				}
			}
			base.Finish();
		}

		// Token: 0x06009DD5 RID: 40405 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnUpdate()
		{
		}

		// Token: 0x04008335 RID: 33589
		[RequiredField]
		[Tooltip("The GameObject to destroy.")]
		public FsmGameObject gameObject;

		// Token: 0x04008336 RID: 33590
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Optional delay before destroying the Game Object.")]
		public FsmFloat delay;

		// Token: 0x04008337 RID: 33591
		[Tooltip("Detach children before destroying the Game Object.")]
		public FsmBool detachChildren;
	}
}
