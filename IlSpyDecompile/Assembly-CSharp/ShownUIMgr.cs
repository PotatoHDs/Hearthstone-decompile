using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

public class ShownUIMgr : IService
{
	public enum UI_WINDOW
	{
		NONE,
		GENERAL_STORE,
		ARENA_STORE,
		TAVERN_BRAWL_STORE,
		QUEST_LOG
	}

	private UI_WINDOW m_shownUI;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public static ShownUIMgr Get()
	{
		return HearthstoneServices.Get<ShownUIMgr>();
	}

	public void SetShownUI(UI_WINDOW uiWindow)
	{
		m_shownUI = uiWindow;
	}

	public UI_WINDOW GetShownUI()
	{
		return m_shownUI;
	}

	public bool HasShownUI()
	{
		return m_shownUI != UI_WINDOW.NONE;
	}

	public void ClearShownUI()
	{
		m_shownUI = UI_WINDOW.NONE;
	}
}
