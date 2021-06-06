using UnityEngine;

public class CollectionHeroSkin : MonoBehaviour
{
	public MeshRenderer m_classIcon;

	public GameObject m_favoriteBanner;

	public UberText m_favoriteBannerText;

	public GameObject m_shadow;

	public Spell m_socketFX;

	public UberText m_name;

	public GameObject m_nameShadow;

	public UberText m_collectionManagerName;

	private bool m_showName = true;

	public bool ShowName
	{
		get
		{
			return m_showName;
		}
		set
		{
			m_showName = value;
			Actor component = base.gameObject.GetComponent<Actor>();
			UberText activeNameText = GetActiveNameText();
			component.OverrideNameText(activeNameText);
			if (m_nameShadow != null)
			{
				m_nameShadow.gameObject.SetActive(m_showName && !UniversalInputManager.UsePhoneUI);
			}
		}
	}

	public void Awake()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			component.SetUseShortName(useShortName: true);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				component.OverrideNameText(null);
			}
		}
		ShowName = m_showName;
	}

	public void SetClass(TAG_CLASS classTag)
	{
		if (m_classIcon != null)
		{
			Vector2 value = CollectionPageManager.s_classTextureOffsets[classTag];
			Renderer component = m_classIcon.GetComponent<Renderer>();
			(Application.isPlaying ? component.GetMaterial() : component.GetSharedMaterial()).SetTextureOffset("_MainTex", value);
		}
		if (m_favoriteBannerText != null)
		{
			m_favoriteBannerText.Text = GameStrings.Format("GLUE_COLLECTION_MANAGER_FAVORITE_DEFAULT_TEXT", GameStrings.GetClassName(classTag));
		}
	}

	public void ShowShadow(bool show)
	{
		if (!(m_shadow == null))
		{
			m_shadow.SetActive(show);
		}
	}

	public void ShowFavoriteBanner(bool show)
	{
		if (!(m_favoriteBanner == null))
		{
			m_favoriteBanner.SetActive(show);
		}
	}

	public void ShowSocketFX()
	{
		if (!(m_socketFX == null) && m_socketFX.gameObject.activeInHierarchy)
		{
			m_socketFX.gameObject.SetActive(value: true);
			m_socketFX.Activate();
		}
	}

	public void HideSocketFX()
	{
		if (m_socketFX != null)
		{
			m_socketFX.Deactivate();
		}
	}

	public void ShowCollectionManagerText()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			UberText activeNameText = GetActiveNameText();
			component.OverrideNameText(activeNameText);
			if (component.isMissingCard())
			{
				component.UpdateMissingCardArt();
			}
		}
	}

	private UberText GetActiveNameText()
	{
		if (!m_showName)
		{
			return null;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			return m_name;
		}
		return m_collectionManagerName;
	}

	[ContextMenu("Toggle Missing Card Effect")]
	private void ToggleMissingCardEffect()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			if (component.isMissingCard())
			{
				component.DisableMissingCardEffect();
			}
			else
			{
				component.MissingCardEffect();
			}
			component.UpdateAllComponents();
		}
	}
}
