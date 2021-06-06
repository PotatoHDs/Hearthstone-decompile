namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Select a random Color from an array of Colors.")]
	public class SelectRandomColor : FsmStateAction
	{
		[CompoundArray("Colors", "Color", "Weight")]
		public FsmColor[] colors;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor storeColor;

		public override void Reset()
		{
			colors = new FsmColor[3];
			weights = new FsmFloat[3] { 1f, 1f, 1f };
			storeColor = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomColor();
			Finish();
		}

		private void DoSelectRandomColor()
		{
			if (colors != null && colors.Length != 0 && storeColor != null)
			{
				int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				if (randomWeightedIndex != -1)
				{
					storeColor.Value = colors[randomWeightedIndex].Value;
				}
			}
		}
	}
}
