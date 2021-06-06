public class FriendlyScene : PlayGameScene
{
	private static FriendlyScene s_instance;

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static FriendlyScene Get()
	{
		return s_instance;
	}

	public override string GetScreenPath()
	{
		return "Friendly.prefab:6309a1b209d2a8747b3870a5d9e968b3";
	}

	public override void Unload()
	{
		base.Unload();
		FriendlyDisplay.Get().Unload();
	}
}
