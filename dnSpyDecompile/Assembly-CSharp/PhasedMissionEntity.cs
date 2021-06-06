using System;
using UnityEngine;

// Token: 0x02000592 RID: 1426
public class PhasedMissionEntity : MissionEntity
{
	// Token: 0x06004F31 RID: 20273 RVA: 0x001A044A File Offset: 0x0019E64A
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.PHASED_RESTART) == 1)
		{
			this.PhaseComplete();
			GameState.Get().Restart();
			return;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x06004F32 RID: 20274 RVA: 0x001A047C File Offset: 0x0019E67C
	public virtual void PhaseComplete()
	{
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad, null, null);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_PopupPrefabNameAndGUID, AssetLoadingOptions.IgnorePrefabPosition);
		UberText[] componentsInChildren = gameObject.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetGameStringText(this.m_PopupText);
		}
		gameObject = AssetLoader.Get().InstantiatePrefab(this.m_PopupPrefabNameAndGUID, AssetLoadingOptions.IgnorePrefabPosition);
		componentsInChildren = gameObject.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetGameStringText(this.m_PopupText);
		}
		SceneUtils.SetLayer(gameObject, 0, null);
	}

	// Token: 0x04004552 RID: 17746
	public string m_PopupPrefabNameAndGUID = "PhaseProgress_Next.prefab:7013b28700033444c9f20897a59edaa0";

	// Token: 0x04004553 RID: 17747
	public string m_PopupText = "GAMEPLAY_RESTART_PUZZLES";
}
