using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AEF RID: 2799
public class UIBDial : PegUIElement
{
	// Token: 0x060094D9 RID: 38105 RVA: 0x00303727 File Offset: 0x00301927
	protected override void Awake()
	{
		this.m_dialItemsSelectedIndex = this.m_StartingSelectedIndex;
		this.m_dialItemsFirstIndex = this.m_dialItemsSelectedIndex - this.m_NumItemsShowingAbove;
		this.m_dialItemsLastIndex = this.m_dialItemsSelectedIndex + this.m_NumItemsShowingBelow;
		base.Awake();
	}

	// Token: 0x060094DA RID: 38106 RVA: 0x00303764 File Offset: 0x00301964
	public void Start()
	{
		if (this.m_TopArrow != null)
		{
			this.m_TopArrow.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTopArrowReleased));
			this.m_TopArrow.AddEventListener(UIEventType.HOLD, new UIEvent.Handler(this.OnTopArrowHeld));
			this.m_TopArrow.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnArrowOver));
			this.m_TopArrow.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnArrowOut));
			this.m_TopArrow.gameObject.SetActive(false);
		}
		if (this.m_BottomArrow != null)
		{
			this.m_BottomArrow.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBottomArrowReleased));
			this.m_BottomArrow.AddEventListener(UIEventType.HOLD, new UIEvent.Handler(this.OnBottomArrowHeld));
			this.m_BottomArrow.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnArrowOver));
			this.m_BottomArrow.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnArrowOut));
			this.m_BottomArrow.gameObject.SetActive(false);
		}
		this.m_rollRotationAmount = 360f / (float)this.m_DialItems.Count;
		this.m_upRotationEuler = this.m_rollRotationAmount * Vector3.right;
		this.m_downRotationEuler = this.m_rollRotationAmount * Vector3.left;
		this.m_desiredOrientation = this.m_Dial.transform.localRotation;
		this.m_upRotationQuat = Quaternion.AngleAxis(this.m_rollRotationAmount, Vector3.right);
		this.m_downRotationQuat = Quaternion.AngleAxis(this.m_rollRotationAmount, Vector3.left);
		this.m_currentHoldRollFrequency = this.m_StartingHoldRollFrequency;
	}

	// Token: 0x060094DB RID: 38107 RVA: 0x0030390C File Offset: 0x00301B0C
	private void Update()
	{
		if (!UniversalInputManager.Get().IsTouchMode() && !this.m_topArrowHeld && !this.m_bottomArrowHeld)
		{
			this.UpdateMouseInput();
		}
		if (this.m_totalHoldDuration >= this.m_MaxFrequencyThreshold && this.m_EnableMaxFrequency)
		{
			this.m_currentHoldRollFrequency = this.m_MaxHoldRollFrequency;
		}
		if (this.m_holdPeriod > this.m_currentHoldRollFrequency)
		{
			if (this.m_topArrowHeld)
			{
				this.RollUpOne();
			}
			else if (this.m_bottomArrowHeld)
			{
				this.RollDownOne();
			}
			this.m_holdPeriod = 0f;
		}
		if (this.m_topArrowHeld || this.m_bottomArrowHeld)
		{
			this.m_holdPeriod += Time.deltaTime;
			this.m_totalHoldDuration += Time.deltaTime;
		}
	}

	// Token: 0x060094DC RID: 38108 RVA: 0x003039C8 File Offset: 0x00301BC8
	private void UpdateMouseInput()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		if (camera == null)
		{
			return;
		}
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		RaycastHit raycastHit;
		if (!base.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis < 0f)
		{
			this.RollDownOne();
			return;
		}
		if (axis > 0f)
		{
			this.RollUpOne();
		}
	}

	// Token: 0x060094DD RID: 38109 RVA: 0x00303A40 File Offset: 0x00301C40
	protected override void OnDestroy()
	{
		this.m_EnabledArrowButtonMaterial = null;
		this.m_DisabledArrowButtonMaterial = null;
		base.OnDestroy();
	}

	// Token: 0x060094DE RID: 38110 RVA: 0x00303A56 File Offset: 0x00301C56
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		this.m_mouseOver = true;
		this.m_TopArrow.gameObject.SetActive(true);
		this.m_BottomArrow.gameObject.SetActive(true);
		this.ShowTooltip();
	}

	// Token: 0x060094DF RID: 38111 RVA: 0x00303A8E File Offset: 0x00301C8E
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		this.m_mouseOver = false;
		this.m_TopArrow.gameObject.SetActive(false);
		this.m_BottomArrow.gameObject.SetActive(false);
		this.HideTooltip();
	}

	// Token: 0x060094E0 RID: 38112 RVA: 0x00303AC8 File Offset: 0x00301CC8
	private void ShowTooltip()
	{
		if (!this.m_EnableTooltip)
		{
			return;
		}
		if (this.m_TooltipPrefab != null && this.m_TooltipBone != null)
		{
			if (this.m_tooltip != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_tooltip.gameObject);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_TooltipPrefab);
			SceneUtils.SetLayer(gameObject, GameLayer.UI);
			this.m_tooltip = gameObject.GetComponent<TooltipPanel>();
			this.m_tooltip.Reset();
			this.m_tooltip.Initialize(this.m_tooltipHeaderText, this.m_tooltipDescText);
			GameUtils.SetParent(this.m_tooltip, this.m_TooltipBone, false);
		}
	}

	// Token: 0x060094E1 RID: 38113 RVA: 0x00303B6B File Offset: 0x00301D6B
	private void HideTooltip()
	{
		if (!this.m_EnableTooltip)
		{
			return;
		}
		if (this.m_tooltip != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_tooltip.gameObject);
		}
	}

	// Token: 0x060094E2 RID: 38114 RVA: 0x00303B94 File Offset: 0x00301D94
	public void UpdateTooltip(string headerText, string descText)
	{
		if (!this.m_EnableTooltip)
		{
			return;
		}
		this.m_tooltipHeaderText = headerText;
		this.m_tooltipDescText = descText;
		if (this.m_mouseOver)
		{
			this.ShowTooltip();
		}
	}

	// Token: 0x060094E3 RID: 38115 RVA: 0x00303BBB File Offset: 0x00301DBB
	public void AddItem(object value)
	{
		this.m_items.Add(value);
		this.LayoutDial();
	}

	// Token: 0x060094E4 RID: 38116 RVA: 0x00303BD0 File Offset: 0x00301DD0
	public bool RemoveItem(object value)
	{
		int num = this.FindItemIndex(value);
		if (num < 0)
		{
			return false;
		}
		this.m_items.RemoveAt(num);
		this.LayoutDial();
		return true;
	}

	// Token: 0x060094E5 RID: 38117 RVA: 0x00303BFE File Offset: 0x00301DFE
	public void ClearItems()
	{
		this.m_items.Clear();
		this.LayoutDial();
	}

	// Token: 0x060094E6 RID: 38118 RVA: 0x00303C11 File Offset: 0x00301E11
	public object GetSelection()
	{
		if (this.m_selectedItem == null)
		{
			return null;
		}
		return this.m_selectedItem.GetValue();
	}

	// Token: 0x060094E7 RID: 38119 RVA: 0x00303C2E File Offset: 0x00301E2E
	private void OnTopArrowReleased(UIEvent e)
	{
		if (this.m_topArrowHeld)
		{
			this.StopHoldingArrow();
			return;
		}
		this.RollUpOne();
	}

	// Token: 0x060094E8 RID: 38120 RVA: 0x00303C45 File Offset: 0x00301E45
	private void OnTopArrowHeld(UIEvent e)
	{
		this.m_topArrowHeld = true;
		this.m_currentHoldRollFrequency = this.m_StartingHoldRollFrequency;
	}

	// Token: 0x060094E9 RID: 38121 RVA: 0x00303C5A File Offset: 0x00301E5A
	private void OnBottomArrowReleased(UIEvent e)
	{
		if (this.m_bottomArrowHeld)
		{
			this.StopHoldingArrow();
			return;
		}
		this.RollDownOne();
	}

	// Token: 0x060094EA RID: 38122 RVA: 0x00303C71 File Offset: 0x00301E71
	private void OnBottomArrowHeld(UIEvent e)
	{
		this.m_bottomArrowHeld = true;
		this.m_currentHoldRollFrequency = this.m_StartingHoldRollFrequency;
	}

	// Token: 0x060094EB RID: 38123 RVA: 0x00303C86 File Offset: 0x00301E86
	private void OnArrowOver(UIEvent e)
	{
		this.TriggerOver();
	}

	// Token: 0x060094EC RID: 38124 RVA: 0x00303C8E File Offset: 0x00301E8E
	private void OnArrowOut(UIEvent e)
	{
		this.StopHoldingArrow();
	}

	// Token: 0x060094ED RID: 38125 RVA: 0x00303C98 File Offset: 0x00301E98
	private void LayoutDial()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		foreach (DialItem dialItem in this.m_DialItems)
		{
			dialItem.SetValue(null, string.Empty);
		}
		this.m_itemsSelectedIndex = 0;
		this.m_itemsFirstVisibleIndex = this.m_itemsSelectedIndex - this.m_NumItemsShowingAbove;
		this.m_itemsLastVisibleIndex = this.m_itemsSelectedIndex + this.m_NumItemsShowingBelow;
		int num = this.m_NumItemsShowingBelow + 1;
		int num2 = 0;
		int num3 = this.m_dialItemsSelectedIndex;
		foreach (object val in this.m_items)
		{
			if (num3 >= this.m_DialItems.Count)
			{
				num3 = 0;
			}
			this.m_DialItems[num3].SetValue(val, this.m_itemTextCallback(val));
			num3++;
			if (++num2 >= num)
			{
				break;
			}
		}
		this.m_selectedItem = this.m_DialItems[this.m_dialItemsSelectedIndex];
		this.UpdateArrowButtonsState();
	}

	// Token: 0x060094EE RID: 38126 RVA: 0x00303DD4 File Offset: 0x00301FD4
	private void RollUpOne()
	{
		if (this.m_items.Count == 0 || this.m_itemsSelectedIndex == 0)
		{
			this.StopHoldingArrow();
			return;
		}
		if (iTween.CountByName(this.m_Dial, "spin") > 0)
		{
			iTween.StopByName(this.m_Dial, "spin");
			this.m_Dial.transform.localRotation = this.m_desiredOrientation;
		}
		Quaternion targetOrientation = this.m_Dial.transform.localRotation * this.m_upRotationQuat;
		this.m_desiredOrientation = targetOrientation;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			this.m_upRotationEuler,
			"time",
			this.m_AnimateSpinTime,
			"easeType",
			iTween.EaseType.easeOutBounce,
			"isLocal",
			true,
			"name",
			"spin",
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_Dial.transform.localRotation = targetOrientation;
			})
		});
		iTween.RotateAdd(this.m_Dial, args);
		this.m_itemsSelectedIndex--;
		int num = this.m_dialItemsSelectedIndex - 1;
		this.m_dialItemsSelectedIndex = num;
		if (num < 0)
		{
			this.m_dialItemsSelectedIndex = this.m_DialItems.Count - 1;
		}
		this.m_itemsFirstVisibleIndex--;
		num = this.m_dialItemsFirstIndex - 1;
		this.m_dialItemsFirstIndex = num;
		if (num < 0)
		{
			this.m_dialItemsFirstIndex = this.m_DialItems.Count - 1;
		}
		if (this.m_itemsFirstVisibleIndex >= 0)
		{
			object val = this.m_items[this.m_itemsFirstVisibleIndex];
			this.m_DialItems[this.m_dialItemsFirstIndex].SetValue(val, this.m_itemTextCallback(val));
		}
		else
		{
			this.m_DialItems[this.m_dialItemsFirstIndex].SetValue(null, string.Empty);
		}
		this.m_DialItems[this.m_dialItemsLastIndex].SetValue(null, string.Empty);
		this.m_itemsLastVisibleIndex--;
		num = this.m_dialItemsLastIndex - 1;
		this.m_dialItemsLastIndex = num;
		if (num < 0)
		{
			this.m_dialItemsLastIndex = this.m_DialItems.Count - 1;
		}
		this.m_selectedItem = this.m_DialItems[this.m_dialItemsSelectedIndex];
		this.m_itemChosenCallback(this.m_items[this.m_itemsSelectedIndex], this.m_items[this.m_itemsSelectedIndex + 1]);
		this.UpdateArrowButtonsState();
	}

	// Token: 0x060094EF RID: 38127 RVA: 0x00304068 File Offset: 0x00302268
	private void RollDownOne()
	{
		if (this.m_items.Count == 0 || this.m_itemsSelectedIndex == this.m_items.Count - 1)
		{
			this.StopHoldingArrow();
			return;
		}
		if (iTween.CountByName(this.m_Dial, "spin") > 0)
		{
			iTween.StopByName(this.m_Dial, "spin");
			this.m_Dial.transform.localRotation = this.m_desiredOrientation;
		}
		Quaternion targetOrientation = this.m_Dial.transform.localRotation * this.m_downRotationQuat;
		this.m_desiredOrientation = targetOrientation;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			this.m_downRotationEuler,
			"time",
			this.m_AnimateSpinTime,
			"easeType",
			iTween.EaseType.easeOutBounce,
			"isLocal",
			true,
			"name",
			"spin",
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_Dial.transform.localRotation = targetOrientation;
			})
		});
		iTween.RotateAdd(this.m_Dial, args);
		this.m_itemsSelectedIndex++;
		int num = this.m_dialItemsSelectedIndex + 1;
		this.m_dialItemsSelectedIndex = num;
		if (num >= this.m_DialItems.Count)
		{
			this.m_dialItemsSelectedIndex = 0;
		}
		this.m_itemsLastVisibleIndex++;
		num = this.m_dialItemsLastIndex + 1;
		this.m_dialItemsLastIndex = num;
		if (num >= this.m_DialItems.Count)
		{
			this.m_dialItemsLastIndex = 0;
		}
		if (this.m_itemsLastVisibleIndex < this.m_items.Count)
		{
			object val = this.m_items[this.m_itemsLastVisibleIndex];
			this.m_DialItems[this.m_dialItemsLastIndex].SetValue(val, this.m_itemTextCallback(val));
		}
		else
		{
			this.m_DialItems[this.m_dialItemsLastIndex].SetValue(null, string.Empty);
		}
		this.m_DialItems[this.m_dialItemsFirstIndex].SetValue(null, string.Empty);
		this.m_itemsFirstVisibleIndex++;
		num = this.m_dialItemsFirstIndex + 1;
		this.m_dialItemsFirstIndex = num;
		if (num >= this.m_DialItems.Count)
		{
			this.m_dialItemsFirstIndex = 0;
		}
		this.m_selectedItem = this.m_DialItems[this.m_dialItemsSelectedIndex];
		this.m_itemChosenCallback(this.m_items[this.m_itemsSelectedIndex], this.m_items[this.m_itemsSelectedIndex - 1]);
		this.UpdateArrowButtonsState();
	}

	// Token: 0x060094F0 RID: 38128 RVA: 0x0030430A File Offset: 0x0030250A
	public UIBDial.ItemChosenCallback GetItemChosenCallback()
	{
		return this.m_itemChosenCallback;
	}

	// Token: 0x060094F1 RID: 38129 RVA: 0x00304312 File Offset: 0x00302512
	public void SetItemChosenCallback(UIBDial.ItemChosenCallback callback)
	{
		UIBDial.ItemChosenCallback itemChosenCallback = callback;
		if (callback == null && (itemChosenCallback = UIBDial.<>c.<>9__71_0) == null)
		{
			itemChosenCallback = (UIBDial.<>c.<>9__71_0 = delegate(object <p0>, object <p1>)
			{
			});
		}
		this.m_itemChosenCallback = itemChosenCallback;
	}

	// Token: 0x060094F2 RID: 38130 RVA: 0x0030433E File Offset: 0x0030253E
	public UIBDial.ItemTextCallback GetItemTextCallback()
	{
		return this.m_itemTextCallback;
	}

	// Token: 0x060094F3 RID: 38131 RVA: 0x00304346 File Offset: 0x00302546
	public void SetItemTextCallback(UIBDial.ItemTextCallback callback)
	{
		this.m_itemTextCallback = (callback ?? new UIBDial.ItemTextCallback(UIBDial.DefaultItemTextCallback));
	}

	// Token: 0x060094F4 RID: 38132 RVA: 0x0030435F File Offset: 0x0030255F
	public static string DefaultItemTextCallback(object val)
	{
		if (val != null)
		{
			return val.ToString();
		}
		return string.Empty;
	}

	// Token: 0x060094F5 RID: 38133 RVA: 0x00304370 File Offset: 0x00302570
	private int FindItemIndex(object value)
	{
		for (int i = 0; i < this.m_items.Count; i++)
		{
			if (this.m_items[i] == value)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060094F6 RID: 38134 RVA: 0x003043A8 File Offset: 0x003025A8
	private int FindVisibleItemIndex(object value)
	{
		for (int i = 0; i < this.m_DialItems.Count; i++)
		{
			if (this.m_DialItems[i].GetValue() == value)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060094F7 RID: 38135 RVA: 0x003043E4 File Offset: 0x003025E4
	private void UpdateArrowButtonsState()
	{
		if (this.m_TopArrowMesh != null)
		{
			if (this.m_itemsSelectedIndex == 0)
			{
				this.m_TopArrowMesh.SetMaterial(this.m_DisabledArrowButtonMaterial);
				this.m_TopArrow.GetComponent<Collider>().enabled = false;
			}
			else
			{
				this.m_TopArrowMesh.SetMaterial(this.m_EnabledArrowButtonMaterial);
				this.m_TopArrow.GetComponent<Collider>().enabled = true;
			}
		}
		if (this.m_BottomArrowMesh != null)
		{
			if (this.m_itemsSelectedIndex == this.m_items.Count - 1)
			{
				this.m_BottomArrowMesh.SetMaterial(this.m_DisabledArrowButtonMaterial);
				this.m_BottomArrow.GetComponent<Collider>().enabled = false;
				return;
			}
			this.m_BottomArrowMesh.SetMaterial(this.m_EnabledArrowButtonMaterial);
			this.m_BottomArrow.GetComponent<Collider>().enabled = true;
		}
	}

	// Token: 0x060094F8 RID: 38136 RVA: 0x003044B5 File Offset: 0x003026B5
	private void StopHoldingArrow()
	{
		this.m_bottomArrowHeld = false;
		this.m_topArrowHeld = false;
		this.m_totalHoldDuration = 0f;
	}

	// Token: 0x04007CC5 RID: 31941
	[CustomEditField(Sections = "Arrows")]
	public UIBButton m_TopArrow;

	// Token: 0x04007CC6 RID: 31942
	[CustomEditField(Sections = "Arrows")]
	public UIBButton m_BottomArrow;

	// Token: 0x04007CC7 RID: 31943
	[CustomEditField(Sections = "Arrows")]
	public MeshRenderer m_TopArrowMesh;

	// Token: 0x04007CC8 RID: 31944
	[CustomEditField(Sections = "Arrows")]
	public MeshRenderer m_BottomArrowMesh;

	// Token: 0x04007CC9 RID: 31945
	[CustomEditField(Sections = "Arrows")]
	public Material m_EnabledArrowButtonMaterial;

	// Token: 0x04007CCA RID: 31946
	[CustomEditField(Sections = "Arrows")]
	public Material m_DisabledArrowButtonMaterial;

	// Token: 0x04007CCB RID: 31947
	[CustomEditField(Sections = "Dial")]
	public GameObject m_Dial;

	// Token: 0x04007CCC RID: 31948
	[CustomEditField(Sections = "Dial")]
	public List<DialItem> m_DialItems = new List<DialItem>();

	// Token: 0x04007CCD RID: 31949
	[CustomEditField(Sections = "Dial")]
	public float m_AnimateSpinTime = 0.25f;

	// Token: 0x04007CCE RID: 31950
	[CustomEditField(Sections = "Dial")]
	public int m_StartingSelectedIndex;

	// Token: 0x04007CCF RID: 31951
	[CustomEditField(Sections = "Dial")]
	public int m_NumItemsShowingAbove = 5;

	// Token: 0x04007CD0 RID: 31952
	[CustomEditField(Sections = "Dial")]
	public int m_NumItemsShowingBelow = 5;

	// Token: 0x04007CD1 RID: 31953
	[CustomEditField(Sections = "Dial")]
	public float m_StartingHoldRollFrequency = 0.15f;

	// Token: 0x04007CD2 RID: 31954
	[CustomEditField(Sections = "Dial")]
	public float m_MaxHoldRollFrequency = 0.075f;

	// Token: 0x04007CD3 RID: 31955
	[CustomEditField(Sections = "Dial")]
	public float m_MaxFrequencyThreshold = 2f;

	// Token: 0x04007CD4 RID: 31956
	[CustomEditField(Sections = "Dial")]
	public bool m_EnableMaxFrequency;

	// Token: 0x04007CD5 RID: 31957
	[CustomEditField(Sections = "Tooltip")]
	public bool m_EnableTooltip = true;

	// Token: 0x04007CD6 RID: 31958
	[CustomEditField(Sections = "Tooltip")]
	public GameObject m_TooltipPrefab;

	// Token: 0x04007CD7 RID: 31959
	[CustomEditField(Sections = "Tooltip")]
	public Transform m_TooltipBone;

	// Token: 0x04007CD8 RID: 31960
	private UIBDial.ItemChosenCallback m_itemChosenCallback = delegate(object <p0>, object <p1>)
	{
	};

	// Token: 0x04007CD9 RID: 31961
	private UIBDial.ItemTextCallback m_itemTextCallback = new UIBDial.ItemTextCallback(UIBDial.DefaultItemTextCallback);

	// Token: 0x04007CDA RID: 31962
	private DialItem m_selectedItem;

	// Token: 0x04007CDB RID: 31963
	private List<object> m_items = new List<object>();

	// Token: 0x04007CDC RID: 31964
	private int m_itemsSelectedIndex;

	// Token: 0x04007CDD RID: 31965
	private int m_itemsFirstVisibleIndex;

	// Token: 0x04007CDE RID: 31966
	private int m_itemsLastVisibleIndex;

	// Token: 0x04007CDF RID: 31967
	private int m_dialItemsSelectedIndex;

	// Token: 0x04007CE0 RID: 31968
	private int m_dialItemsFirstIndex;

	// Token: 0x04007CE1 RID: 31969
	private int m_dialItemsLastIndex;

	// Token: 0x04007CE2 RID: 31970
	private TooltipPanel m_tooltip;

	// Token: 0x04007CE3 RID: 31971
	private string m_tooltipHeaderText;

	// Token: 0x04007CE4 RID: 31972
	private string m_tooltipDescText;

	// Token: 0x04007CE5 RID: 31973
	private bool m_topArrowHeld;

	// Token: 0x04007CE6 RID: 31974
	private bool m_bottomArrowHeld;

	// Token: 0x04007CE7 RID: 31975
	private bool m_mouseOver;

	// Token: 0x04007CE8 RID: 31976
	private float m_rollRotationAmount;

	// Token: 0x04007CE9 RID: 31977
	private Vector3 m_upRotationEuler;

	// Token: 0x04007CEA RID: 31978
	private Vector3 m_downRotationEuler;

	// Token: 0x04007CEB RID: 31979
	private Quaternion m_upRotationQuat;

	// Token: 0x04007CEC RID: 31980
	private Quaternion m_downRotationQuat;

	// Token: 0x04007CED RID: 31981
	private Quaternion m_desiredOrientation = Quaternion.identity;

	// Token: 0x04007CEE RID: 31982
	private float m_currentHoldRollFrequency;

	// Token: 0x04007CEF RID: 31983
	private float m_holdPeriod;

	// Token: 0x04007CF0 RID: 31984
	private float m_totalHoldDuration;

	// Token: 0x04007CF1 RID: 31985
	private const string ITWEEN_SPIN_NAME = "spin";

	// Token: 0x02002725 RID: 10021
	// (Invoke) Token: 0x06013903 RID: 80131
	public delegate void ItemChosenCallback(object selection, object prevSelection);

	// Token: 0x02002726 RID: 10022
	// (Invoke) Token: 0x06013907 RID: 80135
	public delegate string ItemTextCallback(object val);
}
