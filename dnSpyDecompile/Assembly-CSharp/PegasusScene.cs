using System;
using UnityEngine;

// Token: 0x02000689 RID: 1673
public class PegasusScene : MonoBehaviour
{
	// Token: 0x06005D97 RID: 23959 RVA: 0x001E70BC File Offset: 0x001E52BC
	protected virtual void Awake()
	{
		SceneMgr sceneMgr = SceneMgr.Get();
		if (sceneMgr != null)
		{
			sceneMgr.SetScene(this);
			return;
		}
		Log.All.PrintWarning("PegasusScene.Awake called when SceneMgr is null!", Array.Empty<object>());
	}

	// Token: 0x06005D98 RID: 23960 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PreUnload()
	{
	}

	// Token: 0x06005D99 RID: 23961 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool IsUnloading()
	{
		return false;
	}

	// Token: 0x06005D9A RID: 23962 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void Unload()
	{
	}

	// Token: 0x06005D9B RID: 23963 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool IsTransitioning()
	{
		return false;
	}

	// Token: 0x06005D9C RID: 23964 RVA: 0x001E70F0 File Offset: 0x001E52F0
	public virtual bool HandleKeyboardInput()
	{
		if (BackButton.backKey != KeyCode.None && InputCollection.GetKeyUp(BackButton.backKey))
		{
			if (DialogManager.Get().ShowingDialog())
			{
				DialogManager.Get().GoBack();
				return true;
			}
			if (ChatMgr.Get().IsFriendListShowing() || ChatMgr.Get().IsChatLogFrameShown())
			{
				ChatMgr.Get().GoBack();
				return true;
			}
			if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
			{
				OptionsMenu.Get().Hide(true);
				return true;
			}
			if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
			{
				MiscellaneousMenu.Get().Hide();
				return true;
			}
			if (BnetBar.Get() != null && BnetBar.Get().IsGameMenuShown())
			{
				BnetBar.Get().HideGameMenu();
				return true;
			}
			if (Navigation.GoBack())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005D9D RID: 23965 RVA: 0x001E71CC File Offset: 0x001E53CC
	public virtual void ExecuteSceneDrivenTransition(Action onTransitionCompleteCallback)
	{
		Log.All.PrintError("Scene.ExecuteSceneDrivenTransition - Function was not overridden!", Array.Empty<object>());
		onTransitionCompleteCallback();
	}

	// Token: 0x020021A4 RID: 8612
	// (Invoke) Token: 0x06012430 RID: 74800
	public delegate void BackButtonPressedDelegate();
}
