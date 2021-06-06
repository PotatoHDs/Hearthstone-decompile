using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBDial : PegUIElement
{
	public delegate void ItemChosenCallback(object selection, object prevSelection);

	public delegate string ItemTextCallback(object val);

	[CustomEditField(Sections = "Arrows")]
	public UIBButton m_TopArrow;

	[CustomEditField(Sections = "Arrows")]
	public UIBButton m_BottomArrow;

	[CustomEditField(Sections = "Arrows")]
	public MeshRenderer m_TopArrowMesh;

	[CustomEditField(Sections = "Arrows")]
	public MeshRenderer m_BottomArrowMesh;

	[CustomEditField(Sections = "Arrows")]
	public Material m_EnabledArrowButtonMaterial;

	[CustomEditField(Sections = "Arrows")]
	public Material m_DisabledArrowButtonMaterial;

	[CustomEditField(Sections = "Dial")]
	public GameObject m_Dial;

	[CustomEditField(Sections = "Dial")]
	public List<DialItem> m_DialItems = new List<DialItem>();

	[CustomEditField(Sections = "Dial")]
	public float m_AnimateSpinTime = 0.25f;

	[CustomEditField(Sections = "Dial")]
	public int m_StartingSelectedIndex;

	[CustomEditField(Sections = "Dial")]
	public int m_NumItemsShowingAbove = 5;

	[CustomEditField(Sections = "Dial")]
	public int m_NumItemsShowingBelow = 5;

	[CustomEditField(Sections = "Dial")]
	public float m_StartingHoldRollFrequency = 0.15f;

	[CustomEditField(Sections = "Dial")]
	public float m_MaxHoldRollFrequency = 0.075f;

	[CustomEditField(Sections = "Dial")]
	public float m_MaxFrequencyThreshold = 2f;

	[CustomEditField(Sections = "Dial")]
	public bool m_EnableMaxFrequency;

	[CustomEditField(Sections = "Tooltip")]
	public bool m_EnableTooltip = true;

	[CustomEditField(Sections = "Tooltip")]
	public GameObject m_TooltipPrefab;

	[CustomEditField(Sections = "Tooltip")]
	public Transform m_TooltipBone;

	private ItemChosenCallback m_itemChosenCallback = delegate
	{
	};

	private ItemTextCallback m_itemTextCallback = DefaultItemTextCallback;

	private DialItem m_selectedItem;

	private List<object> m_items = new List<object>();

	private int m_itemsSelectedIndex;

	private int m_itemsFirstVisibleIndex;

	private int m_itemsLastVisibleIndex;

	private int m_dialItemsSelectedIndex;

	private int m_dialItemsFirstIndex;

	private int m_dialItemsLastIndex;

	private TooltipPanel m_tooltip;

	private string m_tooltipHeaderText;

	private string m_tooltipDescText;

	private bool m_topArrowHeld;

	private bool m_bottomArrowHeld;

	private bool m_mouseOver;

	private float m_rollRotationAmount;

	private Vector3 m_upRotationEuler;

	private Vector3 m_downRotationEuler;

	private Quaternion m_upRotationQuat;

	private Quaternion m_downRotationQuat;

	private Quaternion m_desiredOrientation = Quaternion.identity;

	private float m_currentHoldRollFrequency;

	private float m_holdPeriod;

	private float m_totalHoldDuration;

	private const string ITWEEN_SPIN_NAME = "spin";

	protected override void Awake()
	{
		m_dialItemsSelectedIndex = m_StartingSelectedIndex;
		m_dialItemsFirstIndex = m_dialItemsSelectedIndex - m_NumItemsShowingAbove;
		m_dialItemsLastIndex = m_dialItemsSelectedIndex + m_NumItemsShowingBelow;
		base.Awake();
	}

	public void Start()
	{
		if (m_TopArrow != null)
		{
			m_TopArrow.AddEventListener(UIEventType.RELEASE, OnTopArrowReleased);
			m_TopArrow.AddEventListener(UIEventType.HOLD, OnTopArrowHeld);
			m_TopArrow.AddEventListener(UIEventType.ROLLOVER, OnArrowOver);
			m_TopArrow.AddEventListener(UIEventType.ROLLOUT, OnArrowOut);
			m_TopArrow.gameObject.SetActive(value: false);
		}
		if (m_BottomArrow != null)
		{
			m_BottomArrow.AddEventListener(UIEventType.RELEASE, OnBottomArrowReleased);
			m_BottomArrow.AddEventListener(UIEventType.HOLD, OnBottomArrowHeld);
			m_BottomArrow.AddEventListener(UIEventType.ROLLOVER, OnArrowOver);
			m_BottomArrow.AddEventListener(UIEventType.ROLLOUT, OnArrowOut);
			m_BottomArrow.gameObject.SetActive(value: false);
		}
		m_rollRotationAmount = 360f / (float)m_DialItems.Count;
		m_upRotationEuler = m_rollRotationAmount * Vector3.right;
		m_downRotationEuler = m_rollRotationAmount * Vector3.left;
		m_desiredOrientation = m_Dial.transform.localRotation;
		m_upRotationQuat = Quaternion.AngleAxis(m_rollRotationAmount, Vector3.right);
		m_downRotationQuat = Quaternion.AngleAxis(m_rollRotationAmount, Vector3.left);
		m_currentHoldRollFrequency = m_StartingHoldRollFrequency;
	}

	private void Update()
	{
		if (!UniversalInputManager.Get().IsTouchMode() && !m_topArrowHeld && !m_bottomArrowHeld)
		{
			UpdateMouseInput();
		}
		if (m_totalHoldDuration >= m_MaxFrequencyThreshold && m_EnableMaxFrequency)
		{
			m_currentHoldRollFrequency = m_MaxHoldRollFrequency;
		}
		if (m_holdPeriod > m_currentHoldRollFrequency)
		{
			if (m_topArrowHeld)
			{
				RollUpOne();
			}
			else if (m_bottomArrowHeld)
			{
				RollDownOne();
			}
			m_holdPeriod = 0f;
		}
		if (m_topArrowHeld || m_bottomArrowHeld)
		{
			m_holdPeriod += Time.deltaTime;
			m_totalHoldDuration += Time.deltaTime;
		}
	}

	private void UpdateMouseInput()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		if (GetComponent<Collider>().Raycast(ray, out var _, camera.farClipPlane))
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis < 0f)
			{
				RollDownOne();
			}
			else if (axis > 0f)
			{
				RollUpOne();
			}
		}
	}

	protected override void OnDestroy()
	{
		m_EnabledArrowButtonMaterial = null;
		m_DisabledArrowButtonMaterial = null;
		base.OnDestroy();
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		m_mouseOver = true;
		m_TopArrow.gameObject.SetActive(value: true);
		m_BottomArrow.gameObject.SetActive(value: true);
		ShowTooltip();
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		m_mouseOver = false;
		m_TopArrow.gameObject.SetActive(value: false);
		m_BottomArrow.gameObject.SetActive(value: false);
		HideTooltip();
	}

	private void ShowTooltip()
	{
		if (m_EnableTooltip && m_TooltipPrefab != null && m_TooltipBone != null)
		{
			if (m_tooltip != null)
			{
				UnityEngine.Object.DestroyImmediate(m_tooltip.gameObject);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(m_TooltipPrefab);
			SceneUtils.SetLayer(gameObject, GameLayer.UI);
			m_tooltip = gameObject.GetComponent<TooltipPanel>();
			m_tooltip.Reset();
			m_tooltip.Initialize(m_tooltipHeaderText, m_tooltipDescText);
			GameUtils.SetParent(m_tooltip, m_TooltipBone);
		}
	}

	private void HideTooltip()
	{
		if (m_EnableTooltip && m_tooltip != null)
		{
			UnityEngine.Object.DestroyImmediate(m_tooltip.gameObject);
		}
	}

	public void UpdateTooltip(string headerText, string descText)
	{
		if (m_EnableTooltip)
		{
			m_tooltipHeaderText = headerText;
			m_tooltipDescText = descText;
			if (m_mouseOver)
			{
				ShowTooltip();
			}
		}
	}

	public void AddItem(object value)
	{
		m_items.Add(value);
		LayoutDial();
	}

	public bool RemoveItem(object value)
	{
		int num = FindItemIndex(value);
		if (num < 0)
		{
			return false;
		}
		m_items.RemoveAt(num);
		LayoutDial();
		return true;
	}

	public void ClearItems()
	{
		m_items.Clear();
		LayoutDial();
	}

	public object GetSelection()
	{
		if (m_selectedItem == null)
		{
			return null;
		}
		return m_selectedItem.GetValue();
	}

	private void OnTopArrowReleased(UIEvent e)
	{
		if (m_topArrowHeld)
		{
			StopHoldingArrow();
		}
		else
		{
			RollUpOne();
		}
	}

	private void OnTopArrowHeld(UIEvent e)
	{
		m_topArrowHeld = true;
		m_currentHoldRollFrequency = m_StartingHoldRollFrequency;
	}

	private void OnBottomArrowReleased(UIEvent e)
	{
		if (m_bottomArrowHeld)
		{
			StopHoldingArrow();
		}
		else
		{
			RollDownOne();
		}
	}

	private void OnBottomArrowHeld(UIEvent e)
	{
		m_bottomArrowHeld = true;
		m_currentHoldRollFrequency = m_StartingHoldRollFrequency;
	}

	private void OnArrowOver(UIEvent e)
	{
		TriggerOver();
	}

	private void OnArrowOut(UIEvent e)
	{
		StopHoldingArrow();
	}

	private void LayoutDial()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		foreach (DialItem dialItem in m_DialItems)
		{
			dialItem.SetValue(null, string.Empty);
		}
		m_itemsSelectedIndex = 0;
		m_itemsFirstVisibleIndex = m_itemsSelectedIndex - m_NumItemsShowingAbove;
		m_itemsLastVisibleIndex = m_itemsSelectedIndex + m_NumItemsShowingBelow;
		int num = m_NumItemsShowingBelow + 1;
		int num2 = 0;
		int num3 = m_dialItemsSelectedIndex;
		foreach (object item in m_items)
		{
			if (num3 >= m_DialItems.Count)
			{
				num3 = 0;
			}
			m_DialItems[num3].SetValue(item, m_itemTextCallback(item));
			num3++;
			if (++num2 >= num)
			{
				break;
			}
		}
		m_selectedItem = m_DialItems[m_dialItemsSelectedIndex];
		UpdateArrowButtonsState();
	}

	private void RollUpOne()
	{
		if (m_items.Count == 0 || m_itemsSelectedIndex == 0)
		{
			StopHoldingArrow();
			return;
		}
		if (iTween.CountByName(m_Dial, "spin") > 0)
		{
			iTween.StopByName(m_Dial, "spin");
			m_Dial.transform.localRotation = m_desiredOrientation;
		}
		Quaternion targetOrientation = m_Dial.transform.localRotation * m_upRotationQuat;
		m_desiredOrientation = targetOrientation;
		Hashtable args = iTween.Hash("amount", m_upRotationEuler, "time", m_AnimateSpinTime, "easeType", iTween.EaseType.easeOutBounce, "isLocal", true, "name", "spin", "oncomplete", (Action<object>)delegate
		{
			m_Dial.transform.localRotation = targetOrientation;
		});
		iTween.RotateAdd(m_Dial, args);
		m_itemsSelectedIndex--;
		if (--m_dialItemsSelectedIndex < 0)
		{
			m_dialItemsSelectedIndex = m_DialItems.Count - 1;
		}
		m_itemsFirstVisibleIndex--;
		if (--m_dialItemsFirstIndex < 0)
		{
			m_dialItemsFirstIndex = m_DialItems.Count - 1;
		}
		if (m_itemsFirstVisibleIndex >= 0)
		{
			object val = m_items[m_itemsFirstVisibleIndex];
			m_DialItems[m_dialItemsFirstIndex].SetValue(val, m_itemTextCallback(val));
		}
		else
		{
			m_DialItems[m_dialItemsFirstIndex].SetValue(null, string.Empty);
		}
		m_DialItems[m_dialItemsLastIndex].SetValue(null, string.Empty);
		m_itemsLastVisibleIndex--;
		if (--m_dialItemsLastIndex < 0)
		{
			m_dialItemsLastIndex = m_DialItems.Count - 1;
		}
		m_selectedItem = m_DialItems[m_dialItemsSelectedIndex];
		m_itemChosenCallback(m_items[m_itemsSelectedIndex], m_items[m_itemsSelectedIndex + 1]);
		UpdateArrowButtonsState();
	}

	private void RollDownOne()
	{
		if (m_items.Count == 0 || m_itemsSelectedIndex == m_items.Count - 1)
		{
			StopHoldingArrow();
			return;
		}
		if (iTween.CountByName(m_Dial, "spin") > 0)
		{
			iTween.StopByName(m_Dial, "spin");
			m_Dial.transform.localRotation = m_desiredOrientation;
		}
		Quaternion targetOrientation = m_Dial.transform.localRotation * m_downRotationQuat;
		m_desiredOrientation = targetOrientation;
		Hashtable args = iTween.Hash("amount", m_downRotationEuler, "time", m_AnimateSpinTime, "easeType", iTween.EaseType.easeOutBounce, "isLocal", true, "name", "spin", "oncomplete", (Action<object>)delegate
		{
			m_Dial.transform.localRotation = targetOrientation;
		});
		iTween.RotateAdd(m_Dial, args);
		m_itemsSelectedIndex++;
		if (++m_dialItemsSelectedIndex >= m_DialItems.Count)
		{
			m_dialItemsSelectedIndex = 0;
		}
		m_itemsLastVisibleIndex++;
		if (++m_dialItemsLastIndex >= m_DialItems.Count)
		{
			m_dialItemsLastIndex = 0;
		}
		if (m_itemsLastVisibleIndex < m_items.Count)
		{
			object val = m_items[m_itemsLastVisibleIndex];
			m_DialItems[m_dialItemsLastIndex].SetValue(val, m_itemTextCallback(val));
		}
		else
		{
			m_DialItems[m_dialItemsLastIndex].SetValue(null, string.Empty);
		}
		m_DialItems[m_dialItemsFirstIndex].SetValue(null, string.Empty);
		m_itemsFirstVisibleIndex++;
		if (++m_dialItemsFirstIndex >= m_DialItems.Count)
		{
			m_dialItemsFirstIndex = 0;
		}
		m_selectedItem = m_DialItems[m_dialItemsSelectedIndex];
		m_itemChosenCallback(m_items[m_itemsSelectedIndex], m_items[m_itemsSelectedIndex - 1]);
		UpdateArrowButtonsState();
	}

	public ItemChosenCallback GetItemChosenCallback()
	{
		return m_itemChosenCallback;
	}

	public void SetItemChosenCallback(ItemChosenCallback callback)
	{
		m_itemChosenCallback = callback ?? ((ItemChosenCallback)delegate
		{
		});
	}

	public ItemTextCallback GetItemTextCallback()
	{
		return m_itemTextCallback;
	}

	public void SetItemTextCallback(ItemTextCallback callback)
	{
		m_itemTextCallback = callback ?? new ItemTextCallback(DefaultItemTextCallback);
	}

	public static string DefaultItemTextCallback(object val)
	{
		if (val != null)
		{
			return val.ToString();
		}
		return string.Empty;
	}

	private int FindItemIndex(object value)
	{
		for (int i = 0; i < m_items.Count; i++)
		{
			if (m_items[i] == value)
			{
				return i;
			}
		}
		return -1;
	}

	private int FindVisibleItemIndex(object value)
	{
		for (int i = 0; i < m_DialItems.Count; i++)
		{
			if (m_DialItems[i].GetValue() == value)
			{
				return i;
			}
		}
		return -1;
	}

	private void UpdateArrowButtonsState()
	{
		if (m_TopArrowMesh != null)
		{
			if (m_itemsSelectedIndex == 0)
			{
				m_TopArrowMesh.SetMaterial(m_DisabledArrowButtonMaterial);
				m_TopArrow.GetComponent<Collider>().enabled = false;
			}
			else
			{
				m_TopArrowMesh.SetMaterial(m_EnabledArrowButtonMaterial);
				m_TopArrow.GetComponent<Collider>().enabled = true;
			}
		}
		if (m_BottomArrowMesh != null)
		{
			if (m_itemsSelectedIndex == m_items.Count - 1)
			{
				m_BottomArrowMesh.SetMaterial(m_DisabledArrowButtonMaterial);
				m_BottomArrow.GetComponent<Collider>().enabled = false;
			}
			else
			{
				m_BottomArrowMesh.SetMaterial(m_EnabledArrowButtonMaterial);
				m_BottomArrow.GetComponent<Collider>().enabled = true;
			}
		}
	}

	private void StopHoldingArrow()
	{
		m_bottomArrowHeld = false;
		m_topArrowHeld = false;
		m_totalHoldDuration = 0f;
	}
}
