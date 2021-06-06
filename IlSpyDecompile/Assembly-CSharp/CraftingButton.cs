using UnityEngine;

public class CraftingButton : PegUIElement
{
	public Material undoMaterial;

	public Material disabledMaterial;

	public Material enabledMaterial;

	public UberText labelText;

	public MeshRenderer buttonRenderer;

	public GameObject m_costObject;

	public Transform m_disabledCostBone;

	public Transform m_enabledCostBone;

	private bool isEnabled;

	public virtual void DisableButton()
	{
		OnEnabled(enable: false);
		buttonRenderer.SetMaterial(disabledMaterial);
		labelText.Text = "";
	}

	public virtual void EnterUndoMode()
	{
		OnEnabled(enable: true);
		buttonRenderer.SetMaterial(undoMaterial);
		labelText.Text = GameStrings.Get("GLUE_CRAFTING_UNDO");
	}

	public virtual void EnableButton()
	{
		OnEnabled(enable: true);
		buttonRenderer.SetMaterial(enabledMaterial);
	}

	public bool IsButtonEnabled()
	{
		return isEnabled;
	}

	private void OnEnabled(bool enable)
	{
		isEnabled = enable;
		GetComponent<Collider>().enabled = enable;
		if (m_costObject != null)
		{
			if (m_enabledCostBone != null && m_disabledCostBone != null)
			{
				m_costObject.transform.position = (enable ? m_enabledCostBone.position : m_disabledCostBone.position);
			}
			else
			{
				m_costObject.SetActive(enable);
			}
		}
	}
}
