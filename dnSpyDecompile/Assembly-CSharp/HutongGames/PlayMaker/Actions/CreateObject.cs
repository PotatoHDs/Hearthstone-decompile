using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C01 RID: 3073
	[ActionCategory(ActionCategory.GameObject)]
	[ActionTarget(typeof(GameObject), "gameObject", true)]
	[Tooltip("Creates a Game Object, usually using a Prefab.")]
	public class CreateObject : FsmStateAction
	{
		// Token: 0x06009DA3 RID: 40355 RVA: 0x00329712 File Offset: 0x00327912
		public override void Reset()
		{
			this.gameObject = null;
			this.spawnPoint = null;
			this.position = new FsmVector3
			{
				UseVariable = true
			};
			this.rotation = new FsmVector3
			{
				UseVariable = true
			};
			this.storeObject = null;
		}

		// Token: 0x06009DA4 RID: 40356 RVA: 0x00329750 File Offset: 0x00327950
		public override void OnEnter()
		{
			GameObject value = this.gameObject.Value;
			if (value != null)
			{
				Vector3 a = Vector3.zero;
				Vector3 euler = Vector3.zero;
				if (this.spawnPoint.Value != null)
				{
					a = this.spawnPoint.Value.transform.position;
					if (!this.position.IsNone)
					{
						a += this.position.Value;
					}
					euler = ((!this.rotation.IsNone) ? this.rotation.Value : this.spawnPoint.Value.transform.eulerAngles);
				}
				else
				{
					if (!this.position.IsNone)
					{
						a = this.position.Value;
					}
					if (!this.rotation.IsNone)
					{
						euler = this.rotation.Value;
					}
				}
				GameObject value2 = UnityEngine.Object.Instantiate<GameObject>(value, a, Quaternion.Euler(euler));
				this.storeObject.Value = value2;
			}
			base.Finish();
		}

		// Token: 0x0400830C RID: 33548
		[RequiredField]
		[Tooltip("GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;

		// Token: 0x0400830D RID: 33549
		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;

		// Token: 0x0400830E RID: 33550
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;

		// Token: 0x0400830F RID: 33551
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		// Token: 0x04008310 RID: 33552
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;
	}
}
