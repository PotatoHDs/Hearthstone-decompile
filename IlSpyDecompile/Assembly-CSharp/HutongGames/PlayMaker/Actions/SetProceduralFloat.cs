namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Set a named float property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		[RequiredField]
		[Tooltip("The named float property in the material.")]
		public FsmString floatProperty;

		[RequiredField]
		[Tooltip("The value to set the property to.")]
		public FsmFloat floatValue;

		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			floatProperty = "";
			floatValue = 0f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetProceduralFloat();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetProceduralFloat();
		}

		private void DoSetProceduralFloat()
		{
		}
	}
}
