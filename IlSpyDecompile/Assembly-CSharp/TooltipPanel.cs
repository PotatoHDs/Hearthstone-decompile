using UnityEngine;

public class TooltipPanel : MonoBehaviour
{
	public UberText m_name;

	public UberText m_body;

	public GameObject m_background;

	private float k_scaleOffset = 1.2f;

	private float k_scaleOffsetPhone = 4.2f;

	private float k_scaleOffsetTablet = 2f;

	private bool m_destroyed;

	protected float m_initialBackgroundHeight;

	protected Vector3 m_initialBackgroundScale = Vector3.zero;

	public const float GAMEPLAY_SCALE_FOR_SHOW_TOOLTIP = 0.75f;

	public static PlatformDependentValue<float> HAND_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.65f,
		Phone = 0.8f
	};

	public static PlatformDependentValue<float> GAMEPLAY_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.75f,
		Phone = 1.4f
	};

	public static PlatformDependentValue<float> GAMEPLAY_SCALE_LARGE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.9f,
		Phone = 0.625f
	};

	public static PlatformDependentValue<float> HISTORY_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.48f,
		Phone = 0.853f
	};

	public static PlatformDependentValue<float> MULLIGAN_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.65f,
		Phone = 0.4f
	};

	public const float GAMEPLAY_HERO_POWER_SCALE = 0.6f;

	public static PlatformDependentValue<float> BOX_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 8f,
		Phone = 4.5f
	};

	public const float OPEN_BOX_SCALE_FOR_SHOW_TOOLTIP = 4f;

	public static PlatformDependentValue<float> COLLECTION_MANAGER_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 4f,
		Phone = 8f
	};

	public static PlatformDependentValue<float> FORGE_SCALE = COLLECTION_MANAGER_SCALE;

	public static PlatformDependentValue<float> ADVENTURE_SCALE = COLLECTION_MANAGER_SCALE;

	public const float PACK_OPENING_SCALE = 2.75f;

	public const float UNOPENED_PACK_SCALE = 5f;

	public const float DECK_HELPER_SCALE = 3.75f;

	protected float scaleToUse;

	public bool Destroyed
	{
		get
		{
			if (!m_destroyed && (bool)m_name)
			{
				return !m_body;
			}
			return true;
		}
	}

	private void Awake()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.Tooltip);
		FORGE_SCALE = COLLECTION_MANAGER_SCALE;
		scaleToUse = GAMEPLAY_SCALE;
	}

	private void OnDestroy()
	{
		Object.Destroy(m_name);
		m_name = null;
		Object.Destroy(m_body);
		m_body = null;
		Object.Destroy(m_background);
		m_background = null;
		m_destroyed = true;
	}

	public void Reset()
	{
		base.transform.localScale = Vector3.one;
		base.transform.eulerAngles = Vector3.zero;
	}

	public void SetScale(float newScale)
	{
		scaleToUse = newScale;
		base.transform.localScale = new Vector3(scaleToUse, scaleToUse, scaleToUse);
	}

	public virtual void Initialize(string keywordName, string keywordText)
	{
		SetName(keywordName);
		SetBodyText(keywordText);
		base.gameObject.SetActive(value: true);
		m_name.UpdateNow();
		m_body.UpdateNow();
	}

	public void SetName(string s)
	{
		m_name.Text = s;
	}

	public void SetBodyText(string s)
	{
		m_body.Text = s;
	}

	public virtual float GetHeight()
	{
		return m_background.GetComponent<Renderer>().bounds.size.z;
	}

	public virtual float GetWidth()
	{
		return m_background.GetComponent<Renderer>().bounds.size.x;
	}

	public bool IsTextRendered()
	{
		if (m_name.IsDone())
		{
			return m_body.IsDone();
		}
		return false;
	}

	public void ShiftBodyText()
	{
		if (!Destroyed && m_name.Text.Length == 0)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_body.transform.position += new Vector3(0f, 0f, m_name.Height + m_name.LineSpacing * k_scaleOffsetPhone);
			}
			else if (PlatformSettings.IsMobileRuntimeOS && !UniversalInputManager.UsePhoneUI)
			{
				m_body.transform.position += new Vector3(0f, 0f, m_name.Height + m_name.LineSpacing * k_scaleOffsetTablet);
			}
			else
			{
				m_body.transform.position += new Vector3(0f, 0f, m_name.Height + m_name.LineSpacing * k_scaleOffset);
			}
		}
	}
}
