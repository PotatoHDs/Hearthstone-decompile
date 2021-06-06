using Hearthstone.UI;
using UnityEngine;

public class TemporaryWidgetInstance : MonoBehaviour
{
	[SerializeField]
	private GameObject m_widgetPrefab;

	[SerializeField]
	private bool m_shouldLoad;

	private WidgetInstance m_instance;

	[SerializeField]
	[HideInInspector]
	private string m_prefabPath;

	[Overridable]
	public bool ShouldLoad
	{
		get
		{
			return m_shouldLoad;
		}
		set
		{
			m_shouldLoad = value;
			EnforceHaveInstance(m_shouldLoad);
		}
	}

	public WidgetInstance Instance => m_instance;

	public bool IsReady
	{
		get
		{
			if (!m_shouldLoad)
			{
				return true;
			}
			if (m_instance == null)
			{
				return false;
			}
			return m_instance.IsReady;
		}
	}

	public bool IsChangingStates
	{
		get
		{
			if (m_instance != null)
			{
				return m_instance.IsChangingStates;
			}
			return false;
		}
	}

	private void Start()
	{
		EnforceHaveInstance(m_shouldLoad);
	}

	private void OnDestroy()
	{
		if (m_instance != null)
		{
			DestroyInstance();
		}
	}

	private void EnforceHaveInstance(bool haveInstance)
	{
		if (Application.isPlaying)
		{
			if (haveInstance && m_instance == null)
			{
				CreateInstance();
			}
			else if (!haveInstance && m_instance != null)
			{
				DestroyInstance();
			}
		}
	}

	private void CreateInstance()
	{
		if (!(base.transform == null) && !(m_instance != null))
		{
			m_instance = WidgetInstance.Create(m_prefabPath);
			GameObject obj = m_instance.gameObject;
			obj.name = m_widgetPrefab.name;
			obj.transform.SetParent(base.transform, worldPositionStays: false);
		}
	}

	private void DestroyInstance()
	{
		if (!(m_instance == null))
		{
			Object.Destroy(m_instance.gameObject);
			m_instance = null;
		}
	}
}
