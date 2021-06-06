using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000936 RID: 2358
public class TooltipZone : MonoBehaviour
{
	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x0600822B RID: 33323 RVA: 0x002A50E2 File Offset: 0x002A32E2
	// (set) Token: 0x0600822C RID: 33324 RVA: 0x002A5100 File Offset: 0x002A3300
	[Overridable]
	public GameLayer LayerOverride
	{
		get
		{
			if (this.m_layerOverride == null)
			{
				return GameLayer.Default;
			}
			return this.m_layerOverride.Value;
		}
		set
		{
			this.m_layerOverride = new GameLayer?(value);
			GameObject tooltipObject = this.GetTooltipObject(0);
			if (tooltipObject != null)
			{
				SceneUtils.SetLayer(tooltipObject, value);
			}
		}
	}

	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x0600822D RID: 33325 RVA: 0x002A5131 File Offset: 0x002A3331
	// (set) Token: 0x0600822E RID: 33326 RVA: 0x002A5150 File Offset: 0x002A3350
	[Overridable]
	public GameLayer ScreenConstraintLayerOverride
	{
		get
		{
			if (this.m_screenConstraintLayerOverride == null)
			{
				return GameLayer.Default;
			}
			return this.m_screenConstraintLayerOverride.Value;
		}
		set
		{
			this.m_screenConstraintLayerOverride = new GameLayer?(value);
			GameObject tooltipObject = this.GetTooltipObject(0);
			if (tooltipObject != null)
			{
				SceneUtils.SetLayer(tooltipObject, value);
			}
		}
	}

	// Token: 0x17000768 RID: 1896
	// (get) Token: 0x0600822F RID: 33327 RVA: 0x002A5181 File Offset: 0x002A3381
	// (set) Token: 0x06008230 RID: 33328 RVA: 0x002A518C File Offset: 0x002A338C
	[Overridable]
	public string HeadlineText
	{
		get
		{
			return this.m_defaultHeadlineText;
		}
		set
		{
			this.m_defaultHeadlineText = GameStrings.Get(value);
			TooltipPanel tooltipPanel = this.GetTooltipPanel(0);
			if (tooltipPanel != null)
			{
				tooltipPanel.SetName(this.m_defaultHeadlineText);
				tooltipPanel.m_name.UpdateNow(false);
			}
		}
	}

	// Token: 0x17000769 RID: 1897
	// (get) Token: 0x06008231 RID: 33329 RVA: 0x002A51CE File Offset: 0x002A33CE
	// (set) Token: 0x06008232 RID: 33330 RVA: 0x002A51D8 File Offset: 0x002A33D8
	[Overridable]
	public string BodyText
	{
		get
		{
			return this.m_defaultBodyText;
		}
		set
		{
			this.m_defaultBodyText = GameStrings.Get(value);
			TooltipPanel tooltipPanel = this.GetTooltipPanel(0);
			if (tooltipPanel != null)
			{
				tooltipPanel.SetBodyText(this.m_defaultBodyText);
				tooltipPanel.m_body.UpdateNow(false);
			}
		}
	}

	// Token: 0x1700076A RID: 1898
	// (get) Token: 0x06008233 RID: 33331 RVA: 0x002A521A File Offset: 0x002A341A
	// (set) Token: 0x06008234 RID: 33332 RVA: 0x002A5224 File Offset: 0x002A3424
	[Overridable]
	public float Scale
	{
		get
		{
			return this.m_defaultScale;
		}
		set
		{
			this.m_defaultScale = value;
			TooltipPanel tooltipPanel = this.GetTooltipPanel(0);
			if (tooltipPanel != null)
			{
				tooltipPanel.SetScale(value);
			}
		}
	}

	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x06008235 RID: 33333 RVA: 0x002A5250 File Offset: 0x002A3450
	// (set) Token: 0x06008236 RID: 33334 RVA: 0x002A5259 File Offset: 0x002A3459
	[Overridable]
	public bool Shown
	{
		get
		{
			return this.IsShowingTooltip(0);
		}
		set
		{
			if (value)
			{
				this.ShowTooltip(0);
				return;
			}
			this.HideTooltip();
		}
	}

	// Token: 0x06008237 RID: 33335 RVA: 0x002A5270 File Offset: 0x002A3470
	private void Awake()
	{
		WidgetTransform component = base.GetComponent<WidgetTransform>();
		if (component != null && base.GetComponent<Clickable>() == null)
		{
			component.CreateBoxCollider(base.gameObject);
		}
	}

	// Token: 0x06008238 RID: 33336 RVA: 0x002A52A8 File Offset: 0x002A34A8
	public GameObject GetTooltipObject(int index = 0)
	{
		if (index < 0 || index >= this.m_tooltips.Count)
		{
			return null;
		}
		return this.m_tooltips[index];
	}

	// Token: 0x06008239 RID: 33337 RVA: 0x002A52CC File Offset: 0x002A34CC
	public TooltipPanel GetTooltipPanel(int index = 0)
	{
		GameObject tooltipObject = this.GetTooltipObject(index);
		if (tooltipObject == null)
		{
			return null;
		}
		return tooltipObject.GetComponent<TooltipPanel>();
	}

	// Token: 0x0600823A RID: 33338 RVA: 0x002A52F2 File Offset: 0x002A34F2
	public bool IsShowingTooltip(int index = 0)
	{
		return this.GetTooltipObject(index) != null;
	}

	// Token: 0x0600823B RID: 33339 RVA: 0x002A5304 File Offset: 0x002A3504
	public TooltipPanel ShowTooltip(int index = 0)
	{
		TooltipPanel tooltipPanel = this.ShowTooltip(this.m_defaultHeadlineText, this.m_defaultBodyText, this.m_defaultScale, Vector3.zero, index);
		if (tooltipPanel != null)
		{
			tooltipPanel.SetScale(this.m_defaultScale);
			if (this.m_layerOverride != null)
			{
				SceneUtils.SetLayer(tooltipPanel, this.m_layerOverride.Value);
			}
		}
		return tooltipPanel;
	}

	// Token: 0x0600823C RID: 33340 RVA: 0x002A5364 File Offset: 0x002A3564
	public TooltipPanel ShowTooltip(string headline, string bodytext, float scale, int index = 0)
	{
		return this.ShowTooltip(headline, bodytext, scale, Vector3.zero, index);
	}

	// Token: 0x0600823D RID: 33341 RVA: 0x002A5378 File Offset: 0x002A3578
	public TooltipPanel ShowTooltip(string headline, string bodytext, float scale, Vector3 localOffset, int index = 0)
	{
		if (this.IsShowingTooltip(index))
		{
			return this.m_tooltips[index].GetComponent<TooltipPanel>();
		}
		if (index < 0)
		{
			return null;
		}
		while (this.m_tooltips.Count <= index)
		{
			this.m_tooltips.Add(null);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			scale *= 2f;
		}
		this.m_tooltips[index] = UnityEngine.Object.Instantiate<GameObject>(this.tooltipPrefab);
		TooltipPanel component = this.m_tooltips[index].GetComponent<TooltipPanel>();
		component.Reset();
		component.Initialize(headline, bodytext);
		component.SetScale(scale);
		if (UniversalInputManager.Get().IsTouchMode() && this.touchTooltipLocation != null)
		{
			component.transform.position = this.touchTooltipLocation.position;
			component.transform.rotation = this.touchTooltipLocation.rotation;
		}
		else if (this.tooltipDisplayLocation != null)
		{
			component.transform.position = this.tooltipDisplayLocation.position;
			component.transform.rotation = this.tooltipDisplayLocation.rotation;
		}
		component.transform.parent = base.transform;
		component.transform.localPosition += localOffset;
		Vector3 heightOfPreviousTooltips = this.GetHeightOfPreviousTooltips(index);
		component.transform.localPosition += heightOfPreviousTooltips;
		int layer = base.gameObject.layer;
		if (this.m_screenConstraintLayerOverride != null)
		{
			layer = (int)this.m_screenConstraintLayerOverride.Value;
		}
		TransformUtil.ConstrainToScreen(this.m_tooltips[index], layer);
		if (this.m_changeCallback != null)
		{
			this.m_changeCallback(true);
		}
		component.ShiftBodyText();
		return component;
	}

	// Token: 0x0600823E RID: 33342 RVA: 0x002A5537 File Offset: 0x002A3737
	public void ShowGameplayTooltip(string headline, string bodytext, int index = 0)
	{
		this.ShowGameplayTooltip(headline, bodytext, Vector3.zero, index);
	}

	// Token: 0x0600823F RID: 33343 RVA: 0x002A5547 File Offset: 0x002A3747
	public void ShowGameplayTooltip(string headline, string bodytext, Vector3 localOffset, int index = 0)
	{
		this.ShowTooltip(headline, bodytext, 0.75f, index);
	}

	// Token: 0x06008240 RID: 33344 RVA: 0x002A5559 File Offset: 0x002A3759
	public void ShowGameplayTooltipLarge(string headline, string bodytext, int index = 0)
	{
		this.ShowGameplayTooltipLarge(headline, bodytext, Vector3.zero, index);
	}

	// Token: 0x06008241 RID: 33345 RVA: 0x002A5569 File Offset: 0x002A3769
	public void ShowGameplayTooltipLarge(string headline, string bodytext, Vector3 localOffset, int index = 0)
	{
		this.ShowTooltip(headline, bodytext, TooltipPanel.GAMEPLAY_SCALE_LARGE, localOffset, index);
	}

	// Token: 0x06008242 RID: 33346 RVA: 0x002A5581 File Offset: 0x002A3781
	public void ShowBoxTooltip(string headline, string bodytext, int index = 0)
	{
		this.ShowTooltip(headline, bodytext, TooltipPanel.BOX_SCALE, index);
	}

	// Token: 0x06008243 RID: 33347 RVA: 0x002A5597 File Offset: 0x002A3797
	public TooltipPanel ShowLayerTooltip(string headline, string bodytext, int index = 0)
	{
		return this.ShowLayerTooltip(headline, bodytext, 1f, index);
	}

	// Token: 0x06008244 RID: 33348 RVA: 0x002A55A8 File Offset: 0x002A37A8
	public TooltipPanel ShowLayerTooltip(string headline, string bodytext, float scale, int index = 0)
	{
		TooltipPanel tooltipPanel = this.ShowTooltip(headline, bodytext, scale, index);
		if (this.tooltipDisplayLocation == null || tooltipPanel == null)
		{
			return tooltipPanel;
		}
		tooltipPanel.transform.parent = this.tooltipDisplayLocation.transform;
		Vector3 localScale = new Vector3(scale, scale, scale);
		tooltipPanel.transform.localScale = localScale;
		SceneUtils.SetLayer(this.m_tooltips[index], this.tooltipDisplayLocation.gameObject.layer, null);
		return tooltipPanel;
	}

	// Token: 0x06008245 RID: 33349 RVA: 0x002A5632 File Offset: 0x002A3832
	public void ShowSocialTooltip(Component target, string headline, string bodytext, float scale, GameLayer layer, int index = 0)
	{
		this.ShowSocialTooltip(target.gameObject, headline, bodytext, scale, layer, index);
	}

	// Token: 0x06008246 RID: 33350 RVA: 0x002A5648 File Offset: 0x002A3848
	public void ShowSocialTooltip(GameObject targetObject, string headline, string bodytext, float scale, GameLayer layer, int index = 0)
	{
		this.ShowTooltip(headline, bodytext, scale, index);
		SceneUtils.SetLayer(this.m_tooltips[index], layer);
		Camera camera = CameraUtils.FindFirstByLayer(targetObject.layer);
		Camera camera2 = CameraUtils.FindFirstByLayer(this.m_tooltips[index].layer);
		if (camera != camera2)
		{
			Vector3 position = camera.WorldToScreenPoint(this.m_tooltips[index].transform.position);
			Vector3 position2 = camera2.ScreenToWorldPoint(position);
			this.m_tooltips[index].transform.position = position2;
		}
	}

	// Token: 0x06008247 RID: 33351 RVA: 0x002A56E4 File Offset: 0x002A38E4
	public void ShowMultiColumnTooltip(string headline, string bodytext, string[] columnsText, float scale, int index = 0)
	{
		TooltipPanel tooltipPanel = this.ShowTooltip(headline, bodytext, scale, index);
		if (tooltipPanel is MultiColumnTooltipPanel)
		{
			MultiColumnTooltipPanel multiColumnTooltipPanel = (MultiColumnTooltipPanel)tooltipPanel;
			if (columnsText.Length > multiColumnTooltipPanel.m_textColumns.Count)
			{
				Log.All.PrintWarning("ShowMultiColumnTooltip - Attempting to display {0} columns of text, when the prefab only supports {1} columns!", new object[]
				{
					columnsText.Length,
					multiColumnTooltipPanel.m_textColumns.Count
				});
			}
			int num = 0;
			while (num < columnsText.Length && num < multiColumnTooltipPanel.m_textColumns.Count)
			{
				multiColumnTooltipPanel.m_textColumns[num].Text = columnsText[num];
				num++;
			}
		}
	}

	// Token: 0x06008248 RID: 33352 RVA: 0x002A5780 File Offset: 0x002A3980
	private Vector3 GetHeightOfPreviousTooltips(int currentIndex)
	{
		float num = 0f;
		if (this.m_tooltipDirection == TooltipZone.TooltipLayoutDirection.DOWN)
		{
			for (int i = 0; i < currentIndex; i++)
			{
				if (this.IsShowingTooltip(i))
				{
					TooltipPanel component = this.GetTooltipObject(i).GetComponent<TooltipPanel>();
					num -= component.GetHeight() / 2f;
				}
			}
		}
		else if (this.m_tooltipDirection == TooltipZone.TooltipLayoutDirection.UP)
		{
			for (int j = 1; j <= currentIndex; j++)
			{
				if (this.IsShowingTooltip(j))
				{
					TooltipPanel component2 = this.GetTooltipObject(j).GetComponent<TooltipPanel>();
					num += component2.GetHeight();
				}
			}
		}
		return new Vector3(0f, 0f, num);
	}

	// Token: 0x06008249 RID: 33353 RVA: 0x002A5814 File Offset: 0x002A3A14
	public void AnchorTooltipTo(GameObject target, Anchor targetAnchorPoint, Anchor tooltipAnchorPoint, int index = 0)
	{
		if (!this.IsShowingTooltip(index))
		{
			return;
		}
		TransformUtil.SetPoint(this.m_tooltips[index], tooltipAnchorPoint, target, targetAnchorPoint);
	}

	// Token: 0x0600824A RID: 33354 RVA: 0x002A5838 File Offset: 0x002A3A38
	public void HideTooltip()
	{
		if (this.m_changeCallback != null)
		{
			this.m_changeCallback(false);
		}
		for (int i = 0; i < this.m_tooltips.Count; i++)
		{
			if (this.m_tooltips[i] != null)
			{
				UnityEngine.Object.Destroy(this.m_tooltips[i]);
			}
		}
	}

	// Token: 0x0600824B RID: 33355 RVA: 0x002A5894 File Offset: 0x002A3A94
	public void SetTooltipChangeCallback(TooltipZone.TooltipChangeCallback callback)
	{
		this.m_changeCallback = callback;
	}

	// Token: 0x04006D19 RID: 27929
	public GameObject tooltipPrefab;

	// Token: 0x04006D1A RID: 27930
	public Transform tooltipDisplayLocation;

	// Token: 0x04006D1B RID: 27931
	public Transform touchTooltipLocation;

	// Token: 0x04006D1C RID: 27932
	public GameObject targetObject;

	// Token: 0x04006D1D RID: 27933
	public TooltipZone.TooltipLayoutDirection m_tooltipDirection;

	// Token: 0x04006D1E RID: 27934
	private List<GameObject> m_tooltips = new List<GameObject>();

	// Token: 0x04006D1F RID: 27935
	private TooltipZone.TooltipChangeCallback m_changeCallback;

	// Token: 0x04006D20 RID: 27936
	private string m_defaultHeadlineText = string.Empty;

	// Token: 0x04006D21 RID: 27937
	private string m_defaultBodyText = string.Empty;

	// Token: 0x04006D22 RID: 27938
	private float m_defaultScale = 1f;

	// Token: 0x04006D23 RID: 27939
	private GameLayer? m_layerOverride;

	// Token: 0x04006D24 RID: 27940
	private GameLayer? m_screenConstraintLayerOverride;

	// Token: 0x020025F9 RID: 9721
	// (Invoke) Token: 0x0601353A RID: 79162
	public delegate void TooltipChangeCallback(bool shown);

	// Token: 0x020025FA RID: 9722
	public enum TooltipLayoutDirection
	{
		// Token: 0x0400EF3D RID: 61245
		DOWN,
		// Token: 0x0400EF3E RID: 61246
		UP
	}
}
