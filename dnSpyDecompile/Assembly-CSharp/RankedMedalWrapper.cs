using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200064A RID: 1610
[RequireComponent(typeof(WidgetTemplate))]
public class RankedMedalWrapper : MonoBehaviour
{
	// Token: 0x06005AC7 RID: 23239 RVA: 0x001D9E5D File Offset: 0x001D805D
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
	}

	// Token: 0x06005AC8 RID: 23240 RVA: 0x001D9E6B File Offset: 0x001D806B
	public void BindRankedPlayDataModel(RankedPlayDataModel dataModel)
	{
		if (dataModel != this.GetRankedPlayDataModel())
		{
			this.m_widget.BindDataModel(dataModel, false);
		}
	}

	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x06005AC9 RID: 23241 RVA: 0x001D9E84 File Offset: 0x001D8084
	public bool IsReady
	{
		get
		{
			if (this.m_widget == null || !this.m_widget.IsReady || this.m_widget.IsChangingStates)
			{
				return false;
			}
			RankedMedal componentInChildren = this.m_widget.GetComponentInChildren<RankedMedal>(false);
			return !(componentInChildren == null) && componentInChildren.IsReady;
		}
	}

	// Token: 0x06005ACA RID: 23242 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005ACB RID: 23243 RVA: 0x001D9EDC File Offset: 0x001D80DC
	public void Show(bool useLegacyRankedPlay)
	{
		base.gameObject.SetActive(true);
		if (useLegacyRankedPlay)
		{
			this.m_widget.TriggerEvent("SHOW_LEGACY", default(Widget.TriggerEventParameters));
			return;
		}
		this.m_widget.TriggerEvent("SHOW_NEW", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06005ACC RID: 23244 RVA: 0x001D9F30 File Offset: 0x001D8130
	private RankedPlayDataModel GetRankedPlayDataModel()
	{
		IDataModel dataModel;
		this.m_widget.GetDataModel(123, out dataModel);
		return dataModel as RankedPlayDataModel;
	}

	// Token: 0x04004D9C RID: 19868
	private Widget m_widget;
}
