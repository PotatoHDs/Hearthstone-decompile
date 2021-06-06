using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

// Token: 0x0200090E RID: 2318
public class ShownUIMgr : IService
{
	// Token: 0x060080E4 RID: 32996 RVA: 0x0029DD6F File Offset: 0x0029BF6F
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x060080E5 RID: 32997 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060080E6 RID: 32998 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060080E7 RID: 32999 RVA: 0x0029DD77 File Offset: 0x0029BF77
	public static ShownUIMgr Get()
	{
		return HearthstoneServices.Get<ShownUIMgr>();
	}

	// Token: 0x060080E8 RID: 33000 RVA: 0x0029DD7E File Offset: 0x0029BF7E
	public void SetShownUI(ShownUIMgr.UI_WINDOW uiWindow)
	{
		this.m_shownUI = uiWindow;
	}

	// Token: 0x060080E9 RID: 33001 RVA: 0x0029DD87 File Offset: 0x0029BF87
	public ShownUIMgr.UI_WINDOW GetShownUI()
	{
		return this.m_shownUI;
	}

	// Token: 0x060080EA RID: 33002 RVA: 0x0029DD8F File Offset: 0x0029BF8F
	public bool HasShownUI()
	{
		return this.m_shownUI > ShownUIMgr.UI_WINDOW.NONE;
	}

	// Token: 0x060080EB RID: 33003 RVA: 0x0029DD9A File Offset: 0x0029BF9A
	public void ClearShownUI()
	{
		this.m_shownUI = ShownUIMgr.UI_WINDOW.NONE;
	}

	// Token: 0x0400699B RID: 27035
	private ShownUIMgr.UI_WINDOW m_shownUI;

	// Token: 0x020025D8 RID: 9688
	public enum UI_WINDOW
	{
		// Token: 0x0400EEE1 RID: 61153
		NONE,
		// Token: 0x0400EEE2 RID: 61154
		GENERAL_STORE,
		// Token: 0x0400EEE3 RID: 61155
		ARENA_STORE,
		// Token: 0x0400EEE4 RID: 61156
		TAVERN_BRAWL_STORE,
		// Token: 0x0400EEE5 RID: 61157
		QUEST_LOG
	}
}
