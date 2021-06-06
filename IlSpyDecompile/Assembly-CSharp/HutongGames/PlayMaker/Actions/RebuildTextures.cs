namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Rebuilds all dirty textures. By default the rebuild is spread over multiple frames so it won't halt the game. Check Immediately to rebuild all textures in a single frame.")]
	public class RebuildTextures : FsmStateAction
	{
		[RequiredField]
		public FsmMaterial substanceMaterial;

		[RequiredField]
		public FsmBool immediately;

		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			immediately = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoRebuildTextures();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoRebuildTextures();
		}

		private void DoRebuildTextures()
		{
		}
	}
}
