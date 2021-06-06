using System;

// Token: 0x02000742 RID: 1858
public class TournamentScene : PlayGameScene
{
	// Token: 0x06006949 RID: 26953 RVA: 0x00225028 File Offset: 0x00223228
	protected override void Awake()
	{
		base.Awake();
		TournamentScene.s_instance = this;
	}

	// Token: 0x0600694A RID: 26954 RVA: 0x00225036 File Offset: 0x00223236
	private void OnDestroy()
	{
		TournamentScene.s_instance = null;
	}

	// Token: 0x0600694B RID: 26955 RVA: 0x0022503E File Offset: 0x0022323E
	public static TournamentScene Get()
	{
		return TournamentScene.s_instance;
	}

	// Token: 0x0600694C RID: 26956 RVA: 0x00225045 File Offset: 0x00223245
	public override string GetScreenPath()
	{
		return "Tournament.prefab:e6cb7fa773178834ebff4e16c3847ede";
	}

	// Token: 0x0600694D RID: 26957 RVA: 0x0022504C File Offset: 0x0022324C
	public override void Unload()
	{
		base.Unload();
		if (TournamentDisplay.Get() != null)
		{
			TournamentDisplay.Get().SceneUnload();
		}
	}

	// Token: 0x04005621 RID: 22049
	private static TournamentScene s_instance;
}
