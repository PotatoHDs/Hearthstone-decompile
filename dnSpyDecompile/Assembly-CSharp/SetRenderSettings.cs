using System;
using UnityEngine;

// Token: 0x02000A8B RID: 2699
public class SetRenderSettings : MonoBehaviour
{
	// Token: 0x06009078 RID: 36984 RVA: 0x002EE054 File Offset: 0x002EC254
	private void enableAmbientUpdates()
	{
		this.m_ambient_shouldUpdate = true;
		if (LoadingScreen.Get() != null && LoadingScreen.Get().IsPreviousSceneActive())
		{
			this.m_lastSavedAmbient = this.m_ambient;
			LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnPreviousSceneDestroyed));
			return;
		}
		RenderSettings.ambientLight = this.m_ambient;
	}

	// Token: 0x06009079 RID: 36985 RVA: 0x002EE0B0 File Offset: 0x002EC2B0
	private void disableAmbientUpdates()
	{
		this.m_ambient_shouldUpdate = false;
	}

	// Token: 0x0600907A RID: 36986 RVA: 0x002EE0B9 File Offset: 0x002EC2B9
	private void Update()
	{
		if (this.m_ambient_shouldUpdate)
		{
			this.m_lastSavedAmbient = this.m_ambient;
			if (LoadingScreen.Get() == null || !LoadingScreen.Get().IsPreviousSceneActive())
			{
				RenderSettings.ambientLight = this.m_ambient;
			}
		}
	}

	// Token: 0x0600907B RID: 36987 RVA: 0x002EE0F3 File Offset: 0x002EC2F3
	public void SetColor(Color newColor)
	{
		this.m_ambient = newColor;
		this.m_lastSavedAmbient = newColor;
	}

	// Token: 0x0600907C RID: 36988 RVA: 0x002EE103 File Offset: 0x002EC303
	private void OnPreviousSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnPreviousSceneDestroyed));
		RenderSettings.ambientLight = this.m_lastSavedAmbient;
	}

	// Token: 0x0400794A RID: 31050
	public Color m_ambient;

	// Token: 0x0400794B RID: 31051
	private bool m_ambient_shouldUpdate;

	// Token: 0x0400794C RID: 31052
	private Color m_lastSavedAmbient;
}
