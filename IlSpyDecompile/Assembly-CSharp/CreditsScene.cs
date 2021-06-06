using UnityEngine;

public class CreditsScene : PegasusScene
{
	private bool m_unloading;

	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab("Credits.prefab:4ffef537c5070494eb038d15271a6ebe", OnUIScreenLoaded);
		if (InactivePlayerKicker.Get() != null)
		{
			InactivePlayerKicker.Get().SetShouldCheckForInactivity(check: false);
		}
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		m_unloading = true;
		if (InactivePlayerKicker.Get() != null)
		{
			InactivePlayerKicker.Get().SetShouldCheckForInactivity(check: true);
		}
		m_unloading = false;
	}

	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"CreditsScene.OnUIScreenLoaded() - failed to load screen {assetRef}");
		}
	}
}
