namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Selects a Random Game Object from an array of Game Objects.")]
	public class SelectRandomGameObject : FsmStateAction
	{
		[CompoundArray("Game Objects", "Game Object", "Weight")]
		public FsmGameObject[] gameObjects;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[3];
			weights = new FsmFloat[3] { 1f, 1f, 1f };
			storeGameObject = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomGameObject();
			Finish();
		}

		private void DoSelectRandomGameObject()
		{
			if (gameObjects != null && gameObjects.Length != 0 && storeGameObject != null)
			{
				int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				if (randomWeightedIndex != -1)
				{
					storeGameObject.Value = gameObjects[randomWeightedIndex].Value;
				}
			}
		}
	}
}
