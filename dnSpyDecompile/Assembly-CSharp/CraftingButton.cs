using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class CraftingButton : PegUIElement
{
	// Token: 0x06001296 RID: 4758 RVA: 0x00069D77 File Offset: 0x00067F77
	public virtual void DisableButton()
	{
		this.OnEnabled(false);
		this.buttonRenderer.SetMaterial(this.disabledMaterial);
		this.labelText.Text = "";
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x00069DA1 File Offset: 0x00067FA1
	public virtual void EnterUndoMode()
	{
		this.OnEnabled(true);
		this.buttonRenderer.SetMaterial(this.undoMaterial);
		this.labelText.Text = GameStrings.Get("GLUE_CRAFTING_UNDO");
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x00069DD0 File Offset: 0x00067FD0
	public virtual void EnableButton()
	{
		this.OnEnabled(true);
		this.buttonRenderer.SetMaterial(this.enabledMaterial);
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x00069DEA File Offset: 0x00067FEA
	public bool IsButtonEnabled()
	{
		return this.isEnabled;
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x00069DF4 File Offset: 0x00067FF4
	private void OnEnabled(bool enable)
	{
		this.isEnabled = enable;
		base.GetComponent<Collider>().enabled = enable;
		if (this.m_costObject != null)
		{
			if (this.m_enabledCostBone != null && this.m_disabledCostBone != null)
			{
				this.m_costObject.transform.position = (enable ? this.m_enabledCostBone.position : this.m_disabledCostBone.position);
				return;
			}
			this.m_costObject.SetActive(enable);
		}
	}

	// Token: 0x04000BE9 RID: 3049
	public Material undoMaterial;

	// Token: 0x04000BEA RID: 3050
	public Material disabledMaterial;

	// Token: 0x04000BEB RID: 3051
	public Material enabledMaterial;

	// Token: 0x04000BEC RID: 3052
	public UberText labelText;

	// Token: 0x04000BED RID: 3053
	public MeshRenderer buttonRenderer;

	// Token: 0x04000BEE RID: 3054
	public GameObject m_costObject;

	// Token: 0x04000BEF RID: 3055
	public Transform m_disabledCostBone;

	// Token: 0x04000BF0 RID: 3056
	public Transform m_enabledCostBone;

	// Token: 0x04000BF1 RID: 3057
	private bool isEnabled;
}
