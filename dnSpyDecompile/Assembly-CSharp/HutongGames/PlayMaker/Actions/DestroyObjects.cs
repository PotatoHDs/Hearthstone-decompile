using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C10 RID: 3088
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys GameObjects in an array.")]
	public class DestroyObjects : FsmStateAction
	{
		// Token: 0x06009DD7 RID: 40407 RVA: 0x00329F83 File Offset: 0x00328183
		public override void Reset()
		{
			this.gameObjects = null;
			this.delay = 0f;
		}

		// Token: 0x06009DD8 RID: 40408 RVA: 0x00329F9C File Offset: 0x0032819C
		public override void OnEnter()
		{
			if (this.gameObjects.Values != null)
			{
				foreach (GameObject gameObject in this.gameObjects.Values)
				{
					if (gameObject != null)
					{
						if (this.delay.Value <= 0f)
						{
							UnityEngine.Object.Destroy(gameObject);
						}
						else
						{
							UnityEngine.Object.Destroy(gameObject, this.delay.Value);
						}
						if (this.detachChildren.Value)
						{
							gameObject.transform.DetachChildren();
						}
					}
				}
			}
			base.Finish();
		}

		// Token: 0x04008338 RID: 33592
		[RequiredField]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		[Tooltip("The GameObjects to destroy.")]
		public FsmArray gameObjects;

		// Token: 0x04008339 RID: 33593
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Optional delay before destroying the Game Objects.")]
		public FsmFloat delay;

		// Token: 0x0400833A RID: 33594
		[Tooltip("Detach children before destroying the Game Objects.")]
		public FsmBool detachChildren;
	}
}
