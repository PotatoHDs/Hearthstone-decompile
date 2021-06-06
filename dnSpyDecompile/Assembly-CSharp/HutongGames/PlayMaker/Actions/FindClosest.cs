using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C25 RID: 3109
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds the closest object to the specified Game Object.\nOptionally filter by Tag and Visibility.")]
	public class FindClosest : FsmStateAction
	{
		// Token: 0x06009E2A RID: 40490 RVA: 0x0032ACC0 File Offset: 0x00328EC0
		public override void Reset()
		{
			this.gameObject = null;
			this.withTag = "Untagged";
			this.ignoreOwner = true;
			this.mustBeVisible = false;
			this.storeObject = null;
			this.storeDistance = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E2B RID: 40491 RVA: 0x0032AD11 File Offset: 0x00328F11
		public override void OnEnter()
		{
			this.DoFindClosest();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E2C RID: 40492 RVA: 0x0032AD27 File Offset: 0x00328F27
		public override void OnUpdate()
		{
			this.DoFindClosest();
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x0032AD30 File Offset: 0x00328F30
		private void DoFindClosest()
		{
			GameObject gameObject = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			GameObject[] array;
			if (string.IsNullOrEmpty(this.withTag.Value) || this.withTag.Value == "Untagged")
			{
				array = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
			}
			else
			{
				array = GameObject.FindGameObjectsWithTag(this.withTag.Value);
			}
			GameObject value = null;
			float num = float.PositiveInfinity;
			foreach (GameObject gameObject2 in array)
			{
				if ((!this.ignoreOwner.Value || !(gameObject2 == base.Owner)) && (!this.mustBeVisible.Value || ActionHelpers.IsVisible(gameObject2)))
				{
					float sqrMagnitude = (gameObject.transform.position - gameObject2.transform.position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						num = sqrMagnitude;
						value = gameObject2;
					}
				}
			}
			this.storeObject.Value = value;
			if (!this.storeDistance.IsNone)
			{
				this.storeDistance.Value = Mathf.Sqrt(num);
			}
		}

		// Token: 0x04008382 RID: 33666
		[RequiredField]
		[Tooltip("The GameObject to measure from.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008383 RID: 33667
		[RequiredField]
		[UIHint(UIHint.Tag)]
		[Tooltip("Only consider objects with this Tag. NOTE: It's generally a lot quicker to find objects with a Tag!")]
		public FsmString withTag;

		// Token: 0x04008384 RID: 33668
		[Tooltip("If checked, ignores the object that owns this FSM.")]
		public FsmBool ignoreOwner;

		// Token: 0x04008385 RID: 33669
		[Tooltip("Only consider objects visible to the camera.")]
		public FsmBool mustBeVisible;

		// Token: 0x04008386 RID: 33670
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the closest object.")]
		public FsmGameObject storeObject;

		// Token: 0x04008387 RID: 33671
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance to the closest object.")]
		public FsmFloat storeDistance;

		// Token: 0x04008388 RID: 33672
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
