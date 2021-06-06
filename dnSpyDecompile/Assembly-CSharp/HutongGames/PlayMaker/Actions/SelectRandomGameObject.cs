using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9A RID: 3482
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Selects a Random Game Object from an array of Game Objects.")]
	public class SelectRandomGameObject : FsmStateAction
	{
		// Token: 0x0600A508 RID: 42248 RVA: 0x003455D8 File Offset: 0x003437D8
		public override void Reset()
		{
			this.gameObjects = new FsmGameObject[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.storeGameObject = null;
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x0034562B File Offset: 0x0034382B
		public override void OnEnter()
		{
			this.DoSelectRandomGameObject();
			base.Finish();
		}

		// Token: 0x0600A50A RID: 42250 RVA: 0x0034563C File Offset: 0x0034383C
		private void DoSelectRandomGameObject()
		{
			if (this.gameObjects == null)
			{
				return;
			}
			if (this.gameObjects.Length == 0)
			{
				return;
			}
			if (this.storeGameObject == null)
			{
				return;
			}
			int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			if (randomWeightedIndex != -1)
			{
				this.storeGameObject.Value = this.gameObjects[randomWeightedIndex].Value;
			}
		}

		// Token: 0x04008BA1 RID: 35745
		[CompoundArray("Game Objects", "Game Object", "Weight")]
		public FsmGameObject[] gameObjects;

		// Token: 0x04008BA2 RID: 35746
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008BA3 RID: 35747
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;
	}
}
