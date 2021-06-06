using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class TooltipZone : MonoBehaviour
{
	public delegate void TooltipChangeCallback(bool shown);

	public enum TooltipLayoutDirection
	{
		DOWN,
		UP
	}

	public GameObject tooltipPrefab;

	public Transform tooltipDisplayLocation;

	public Transform touchTooltipLocation;

	public GameObject targetObject;

	public TooltipLayoutDirection m_tooltipDirection;

	private List<GameObject> m_tooltips = new List<GameObject>();

	private TooltipChangeCallback m_changeCallback;

	private string m_defaultHeadlineText = string.Empty;

	private string m_defaultBodyText = string.Empty;

	private float m_defaultScale = 1f;

	private GameLayer? m_layerOverride;

	private GameLayer? m_screenConstraintLayerOverride;

	[Overridable]
	public GameLayer LayerOverride
	{
		get
		{
			if (!m_layerOverride.HasValue)
			{
				return GameLayer.Default;
			}
			return m_layerOverride.Value;
		}
		set
		{
			m_layerOverride = value;
			GameObject tooltipObject = GetTooltipObject();
			if (tooltipObject != null)
			{
				SceneUtils.SetLayer(tooltipObject, value);
			}
		}
	}

	[Overridable]
	public GameLayer ScreenConstraintLayerOverride
	{
		get
		{
			if (!m_screenConstraintLayerOverride.HasValue)
			{
				return GameLayer.Default;
			}
			return m_screenConstraintLayerOverride.Value;
		}
		set
		{
			m_screenConstraintLayerOverride = value;
			GameObject tooltipObject = GetTooltipObject();
			if (tooltipObject != null)
			{
				SceneUtils.SetLayer(tooltipObject, value);
			}
		}
	}

	[Overridable]
	public string HeadlineText
	{
		get
		{
			return m_defaultHeadlineText;
		}
		set
		{
			m_defaultHeadlineText = GameStrings.Get(value);
			TooltipPanel tooltipPanel = GetTooltipPanel();
			if (tooltipPanel != null)
			{
				tooltipPanel.SetName(m_defaultHeadlineText);
				tooltipPanel.m_name.UpdateNow();
			}
		}
	}

	[Overridable]
	public string BodyText
	{
		get
		{
			return m_defaultBodyText;
		}
		set
		{
			m_defaultBodyText = GameStrings.Get(value);
			TooltipPanel tooltipPanel = GetTooltipPanel();
			if (tooltipPanel != null)
			{
				tooltipPanel.SetBodyText(m_defaultBodyText);
				tooltipPanel.m_body.UpdateNow();
			}
		}
	}

	[Overridable]
	public float Scale
	{
		get
		{
			return m_defaultScale;
		}
		set
		{
			m_defaultScale = value;
			TooltipPanel tooltipPanel = GetTooltipPanel();
			if (tooltipPanel != null)
			{
				tooltipPanel.SetScale(value);
			}
		}
	}

	[Overridable]
	public bool Shown
	{
		get
		{
			return IsShowingTooltip();
		}
		set
		{
			if (value)
			{
				ShowTooltip();
			}
			else
			{
				HideTooltip();
			}
		}
	}

	private void Awake()
	{
		WidgetTransform component = GetComponent<WidgetTransform>();
		if (component != null && GetComponent<Clickable>() == null)
		{
			component.CreateBoxCollider(base.gameObject);
		}
	}

	public GameObject GetTooltipObject(int index = 0)
	{
		if (index < 0 || index >= m_tooltips.Count)
		{
			return null;
		}
		return m_tooltips[index];
	}

	public TooltipPanel GetTooltipPanel(int index = 0)
	{
		GameObject tooltipObject = GetTooltipObject(index);
		if (tooltipObject == null)
		{
			return null;
		}
		return tooltipObject.GetComponent<TooltipPanel>();
	}

	public bool IsShowingTooltip(int index = 0)
	{
		return GetTooltipObject(index) != null;
	}

	public TooltipPanel ShowTooltip(int index = 0)
	{
		TooltipPanel tooltipPanel = ShowTooltip(m_defaultHeadlineText, m_defaultBodyText, m_defaultScale, Vector3.zero, index);
		if (tooltipPanel != null)
		{
			tooltipPanel.SetScale(m_defaultScale);
			if (m_layerOverride.HasValue)
			{
				SceneUtils.SetLayer(tooltipPanel, m_layerOverride.Value);
			}
		}
		return tooltipPanel;
	}

	public TooltipPanel ShowTooltip(string headline, string bodytext, float scale, int index = 0)
	{
		return ShowTooltip(headline, bodytext, scale, Vector3.zero, index);
	}

	public TooltipPanel ShowTooltip(string headline, string bodytext, float scale, Vector3 localOffset, int index = 0)
	{
		if (IsShowingTooltip(index))
		{
			return m_tooltips[index].GetComponent<TooltipPanel>();
		}
		if (index < 0)
		{
			return null;
		}
		while (m_tooltips.Count <= index)
		{
			m_tooltips.Add(null);
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			scale *= 2f;
		}
		m_tooltips[index] = Object.Instantiate(tooltipPrefab);
		TooltipPanel component = m_tooltips[index].GetComponent<TooltipPanel>();
		component.Reset();
		component.Initialize(headline, bodytext);
		component.SetScale(scale);
		if (UniversalInputManager.Get().IsTouchMode() && touchTooltipLocation != null)
		{
			component.transform.position = touchTooltipLocation.position;
			component.transform.rotation = touchTooltipLocation.rotation;
		}
		else if (tooltipDisplayLocation != null)
		{
			component.transform.position = tooltipDisplayLocation.position;
			component.transform.rotation = tooltipDisplayLocation.rotation;
		}
		component.transform.parent = base.transform;
		component.transform.localPosition += localOffset;
		Vector3 heightOfPreviousTooltips = GetHeightOfPreviousTooltips(index);
		component.transform.localPosition += heightOfPreviousTooltips;
		int layer = base.gameObject.layer;
		if (m_screenConstraintLayerOverride.HasValue)
		{
			layer = (int)m_screenConstraintLayerOverride.Value;
		}
		TransformUtil.ConstrainToScreen(m_tooltips[index], layer);
		if (m_changeCallback != null)
		{
			m_changeCallback(shown: true);
		}
		component.ShiftBodyText();
		return component;
	}

	public void ShowGameplayTooltip(string headline, string bodytext, int index = 0)
	{
		ShowGameplayTooltip(headline, bodytext, Vector3.zero, index);
	}

	public void ShowGameplayTooltip(string headline, string bodytext, Vector3 localOffset, int index = 0)
	{
		ShowTooltip(headline, bodytext, 0.75f, index);
	}

	public void ShowGameplayTooltipLarge(string headline, string bodytext, int index = 0)
	{
		ShowGameplayTooltipLarge(headline, bodytext, Vector3.zero, index);
	}

	public void ShowGameplayTooltipLarge(string headline, string bodytext, Vector3 localOffset, int index = 0)
	{
		ShowTooltip(headline, bodytext, TooltipPanel.GAMEPLAY_SCALE_LARGE, localOffset, index);
	}

	public void ShowBoxTooltip(string headline, string bodytext, int index = 0)
	{
		ShowTooltip(headline, bodytext, TooltipPanel.BOX_SCALE, index);
	}

	public TooltipPanel ShowLayerTooltip(string headline, string bodytext, int index = 0)
	{
		return ShowLayerTooltip(headline, bodytext, 1f, index);
	}

	public TooltipPanel ShowLayerTooltip(string headline, string bodytext, float scale, int index = 0)
	{
		TooltipPanel tooltipPanel = ShowTooltip(headline, bodytext, scale, index);
		if (tooltipDisplayLocation == null || tooltipPanel == null)
		{
			return tooltipPanel;
		}
		tooltipPanel.transform.parent = tooltipDisplayLocation.transform;
		Vector3 localScale = new Vector3(scale, scale, scale);
		tooltipPanel.transform.localScale = localScale;
		SceneUtils.SetLayer(m_tooltips[index], tooltipDisplayLocation.gameObject.layer);
		return tooltipPanel;
	}

	public void ShowSocialTooltip(Component target, string headline, string bodytext, float scale, GameLayer layer, int index = 0)
	{
		ShowSocialTooltip(target.gameObject, headline, bodytext, scale, layer, index);
	}

	public void ShowSocialTooltip(GameObject targetObject, string headline, string bodytext, float scale, GameLayer layer, int index = 0)
	{
		ShowTooltip(headline, bodytext, scale, index);
		SceneUtils.SetLayer(m_tooltips[index], layer);
		Camera camera = CameraUtils.FindFirstByLayer(targetObject.layer);
		Camera camera2 = CameraUtils.FindFirstByLayer(m_tooltips[index].layer);
		if (camera != camera2)
		{
			Vector3 position = camera.WorldToScreenPoint(m_tooltips[index].transform.position);
			Vector3 position2 = camera2.ScreenToWorldPoint(position);
			m_tooltips[index].transform.position = position2;
		}
	}

	public void ShowMultiColumnTooltip(string headline, string bodytext, string[] columnsText, float scale, int index = 0)
	{
		TooltipPanel tooltipPanel = ShowTooltip(headline, bodytext, scale, index);
		if (tooltipPanel is MultiColumnTooltipPanel)
		{
			MultiColumnTooltipPanel multiColumnTooltipPanel = (MultiColumnTooltipPanel)tooltipPanel;
			if (columnsText.Length > multiColumnTooltipPanel.m_textColumns.Count)
			{
				Log.All.PrintWarning("ShowMultiColumnTooltip - Attempting to display {0} columns of text, when the prefab only supports {1} columns!", columnsText.Length, multiColumnTooltipPanel.m_textColumns.Count);
			}
			for (int i = 0; i < columnsText.Length && i < multiColumnTooltipPanel.m_textColumns.Count; i++)
			{
				multiColumnTooltipPanel.m_textColumns[i].Text = columnsText[i];
			}
		}
	}

	private Vector3 GetHeightOfPreviousTooltips(int currentIndex)
	{
		float num = 0f;
		if (m_tooltipDirection == TooltipLayoutDirection.DOWN)
		{
			for (int i = 0; i < currentIndex; i++)
			{
				if (IsShowingTooltip(i))
				{
					TooltipPanel component = GetTooltipObject(i).GetComponent<TooltipPanel>();
					num -= component.GetHeight() / 2f;
				}
			}
		}
		else if (m_tooltipDirection == TooltipLayoutDirection.UP)
		{
			for (int j = 1; j <= currentIndex; j++)
			{
				if (IsShowingTooltip(j))
				{
					TooltipPanel component2 = GetTooltipObject(j).GetComponent<TooltipPanel>();
					num += component2.GetHeight();
				}
			}
		}
		return new Vector3(0f, 0f, num);
	}

	public void AnchorTooltipTo(GameObject target, Anchor targetAnchorPoint, Anchor tooltipAnchorPoint, int index = 0)
	{
		if (IsShowingTooltip(index))
		{
			TransformUtil.SetPoint(m_tooltips[index], tooltipAnchorPoint, target, targetAnchorPoint);
		}
	}

	public void HideTooltip()
	{
		if (m_changeCallback != null)
		{
			m_changeCallback(shown: false);
		}
		for (int i = 0; i < m_tooltips.Count; i++)
		{
			if (m_tooltips[i] != null)
			{
				Object.Destroy(m_tooltips[i]);
			}
		}
	}

	public void SetTooltipChangeCallback(TooltipChangeCallback callback)
	{
		m_changeCallback = callback;
	}
}
