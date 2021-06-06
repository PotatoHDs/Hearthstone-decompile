public class TournamentScene : PlayGameScene
{
	private static TournamentScene s_instance;

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static TournamentScene Get()
	{
		return s_instance;
	}

	public override string GetScreenPath()
	{
		return "Tournament.prefab:e6cb7fa773178834ebff4e16c3847ede";
	}

	public override void Unload()
	{
		base.Unload();
		if (TournamentDisplay.Get() != null)
		{
			TournamentDisplay.Get().SceneUnload();
		}
	}
}
