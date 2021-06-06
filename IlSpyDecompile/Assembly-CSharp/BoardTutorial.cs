using UnityEngine;

public class BoardTutorial : MonoBehaviour
{
	public GameObject m_Highlight;

	public GameObject m_EnemyHighlight;

	public Light m_ManaSpotlight;

	private static BoardTutorial s_instance;

	private bool m_highlightEnabled;

	private bool m_enemyHighlightEnabled;

	private void Awake()
	{
		s_instance = this;
		SceneUtils.EnableRenderers(m_Highlight, enable: false);
		SceneUtils.EnableRenderers(m_EnemyHighlight, enable: false);
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static BoardTutorial Get()
	{
		return s_instance;
	}

	public void EnableHighlight(bool enable)
	{
		if (m_highlightEnabled != enable)
		{
			m_highlightEnabled = enable;
			UpdateHighlight();
		}
	}

	public void EnableEnemyHighlight(bool enable)
	{
		if (m_enemyHighlightEnabled != enable)
		{
			m_enemyHighlightEnabled = enable;
			UpdateEnemyHighlight();
		}
	}

	public void EnableFullHighlight(bool enable)
	{
		EnableHighlight(enable);
		EnableEnemyHighlight(enable);
	}

	public bool IsHighlightEnabled()
	{
		return m_highlightEnabled;
	}

	private void UpdateHighlight()
	{
		if (m_highlightEnabled)
		{
			SceneUtils.EnableRenderers(m_Highlight, m_highlightEnabled);
			m_Highlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_On");
		}
		else
		{
			m_Highlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_Off");
		}
	}

	private void UpdateEnemyHighlight()
	{
		if (m_enemyHighlightEnabled)
		{
			SceneUtils.EnableRenderers(m_EnemyHighlight, m_enemyHighlightEnabled);
			m_EnemyHighlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_On");
		}
		else
		{
			m_EnemyHighlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_Off");
		}
	}
}
