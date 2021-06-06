using System;

// Token: 0x020002F5 RID: 757
public class FriendlyScene : PlayGameScene
{
	// Token: 0x06002838 RID: 10296 RVA: 0x000CA370 File Offset: 0x000C8570
	protected override void Awake()
	{
		base.Awake();
		FriendlyScene.s_instance = this;
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x000CA37E File Offset: 0x000C857E
	private void OnDestroy()
	{
		FriendlyScene.s_instance = null;
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x000CA386 File Offset: 0x000C8586
	public static FriendlyScene Get()
	{
		return FriendlyScene.s_instance;
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x000CA38D File Offset: 0x000C858D
	public override string GetScreenPath()
	{
		return "Friendly.prefab:6309a1b209d2a8747b3870a5d9e968b3";
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x000CA394 File Offset: 0x000C8594
	public override void Unload()
	{
		base.Unload();
		FriendlyDisplay.Get().Unload();
	}

	// Token: 0x040016D5 RID: 5845
	private static FriendlyScene s_instance;
}
