using UnityEngine;

public class SetRenderSettings : MonoBehaviour
{
	public Color m_ambient;

	private bool m_ambient_shouldUpdate;

	private Color m_lastSavedAmbient;

	private void enableAmbientUpdates()
	{
		m_ambient_shouldUpdate = true;
		if (LoadingScreen.Get() != null && LoadingScreen.Get().IsPreviousSceneActive())
		{
			m_lastSavedAmbient = m_ambient;
			LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(OnPreviousSceneDestroyed);
		}
		else
		{
			RenderSettings.ambientLight = m_ambient;
		}
	}

	private void disableAmbientUpdates()
	{
		m_ambient_shouldUpdate = false;
	}

	private void Update()
	{
		if (m_ambient_shouldUpdate)
		{
			m_lastSavedAmbient = m_ambient;
			if (LoadingScreen.Get() == null || !LoadingScreen.Get().IsPreviousSceneActive())
			{
				RenderSettings.ambientLight = m_ambient;
			}
		}
	}

	public void SetColor(Color newColor)
	{
		m_ambient = newColor;
		m_lastSavedAmbient = newColor;
	}

	private void OnPreviousSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(OnPreviousSceneDestroyed);
		RenderSettings.ambientLight = m_lastSavedAmbient;
	}
}
