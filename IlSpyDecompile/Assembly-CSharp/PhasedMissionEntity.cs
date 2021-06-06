using UnityEngine;

public class PhasedMissionEntity : MissionEntity
{
	public string m_PopupPrefabNameAndGUID = "PhaseProgress_Next.prefab:7013b28700033444c9f20897a59edaa0";

	public string m_PopupText = "GAMEPLAY_RESTART_PUZZLES";

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.PHASED_RESTART) == 1)
		{
			PhaseComplete();
			GameState.Get().Restart();
		}
		else
		{
			base.NotifyOfGameOver(gameResult);
		}
	}

	public virtual void PhaseComplete()
	{
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_PopupPrefabNameAndGUID, AssetLoadingOptions.IgnorePrefabPosition);
		UberText[] componentsInChildren = gameObject.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetGameStringText(m_PopupText);
		}
		gameObject = AssetLoader.Get().InstantiatePrefab(m_PopupPrefabNameAndGUID, AssetLoadingOptions.IgnorePrefabPosition);
		componentsInChildren = gameObject.GetComponentsInChildren<UberText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetGameStringText(m_PopupText);
		}
		SceneUtils.SetLayer(gameObject, 0);
	}
}
