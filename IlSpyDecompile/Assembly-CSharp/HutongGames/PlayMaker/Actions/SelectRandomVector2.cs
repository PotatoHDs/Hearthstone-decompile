namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Select a Random Vector2 from a Vector2 array.")]
	public class SelectRandomVector2 : FsmStateAction
	{
		[Tooltip("The array of Vectors and respective weights")]
		[CompoundArray("Vectors", "Vector", "Weight")]
		public FsmVector2[] vector2Array;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The picked vector2")]
		public FsmVector2 storeVector2;

		public override void Reset()
		{
			vector2Array = new FsmVector2[3];
			weights = new FsmFloat[3] { 1f, 1f, 1f };
			storeVector2 = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomColor();
			Finish();
		}

		private void DoSelectRandomColor()
		{
			if (vector2Array != null && vector2Array.Length != 0 && storeVector2 != null)
			{
				int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				if (randomWeightedIndex != -1)
				{
					storeVector2.Value = vector2Array[randomWeightedIndex].Value;
				}
			}
		}
	}
}
