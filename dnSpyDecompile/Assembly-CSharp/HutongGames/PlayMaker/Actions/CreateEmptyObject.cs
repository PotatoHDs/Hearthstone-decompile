using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C00 RID: 3072
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Creates a Game Object at a spawn point.\nUse a Game Object and/or Position/Rotation for the Spawn Point. If you specify a Game Object, Position is used as a local offset, and Rotation will override the object's rotation.")]
	public class CreateEmptyObject : FsmStateAction
	{
		// Token: 0x06009DA0 RID: 40352 RVA: 0x0032959D File Offset: 0x0032779D
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

		// Token: 0x06009DA1 RID: 40353 RVA: 0x003295D8 File Offset: 0x003277D8
		public override void OnEnter()
		{
			GameObject value = this.gameObject.Value;
			Vector3 a = Vector3.zero;
			Vector3 eulerAngles = Vector3.zero;
			if (this.spawnPoint.Value != null)
			{
				a = this.spawnPoint.Value.transform.position;
				if (!this.position.IsNone)
				{
					a += this.position.Value;
				}
				if (!this.rotation.IsNone)
				{
					eulerAngles = this.rotation.Value;
				}
				else
				{
					eulerAngles = this.spawnPoint.Value.transform.eulerAngles;
				}
			}
			else
			{
				if (!this.position.IsNone)
				{
					a = this.position.Value;
				}
				if (!this.rotation.IsNone)
				{
					eulerAngles = this.rotation.Value;
				}
			}
			GameObject gameObject = this.storeObject.Value;
			if (value != null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(value);
				this.storeObject.Value = gameObject;
			}
			else
			{
				gameObject = new GameObject("EmptyObjectFromNull");
				this.storeObject.Value = gameObject;
			}
			if (gameObject != null)
			{
				gameObject.transform.position = a;
				gameObject.transform.eulerAngles = eulerAngles;
			}
			base.Finish();
		}

		// Token: 0x04008307 RID: 33543
		[Tooltip("Optional GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;

		// Token: 0x04008308 RID: 33544
		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;

		// Token: 0x04008309 RID: 33545
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;

		// Token: 0x0400830A RID: 33546
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		// Token: 0x0400830B RID: 33547
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;
	}
}
