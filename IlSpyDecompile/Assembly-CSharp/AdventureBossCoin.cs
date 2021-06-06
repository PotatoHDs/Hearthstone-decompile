using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureBossCoin : PegUIElement
{
	private const string s_EventCoinFlip = "Flip";

	public GameObject m_Coin;

	public MeshRenderer m_PortraitRenderer;

	public int m_PortraitMaterialIndex = 1;

	public GameObject m_Connector;

	public StateEventTable m_CoinStateTable;

	public PegUIElement m_DisabledCollider;

	private bool m_Enabled;

	private static bool neverRun;

	public void SetPortraitMaterial(AdventureBossDef bossDef)
	{
		List<Material> list = m_PortraitRenderer?.GetMaterials();
		if (list != null && m_PortraitMaterialIndex < list.Count)
		{
			Material material = bossDef.m_CoinPortraitMaterial.GetMaterial();
			list[m_PortraitMaterialIndex] = material;
			m_PortraitRenderer.SetMaterials(list);
		}
	}

	public void ShowConnector(bool show)
	{
		if (m_Connector != null)
		{
			m_Connector.SetActive(show);
		}
	}

	public void Enable(bool flag, bool animate = true)
	{
		GetComponent<Collider>().enabled = flag;
		if (m_DisabledCollider != null)
		{
			m_DisabledCollider.gameObject.SetActive(!flag);
		}
		if (m_Enabled != flag)
		{
			m_Enabled = flag;
			if (animate && flag)
			{
				ShowCoin(show: false);
				m_CoinStateTable.TriggerState("Flip");
			}
			else
			{
				ShowCoin(flag);
			}
		}
	}

	public void Select(bool selected)
	{
		UIBHighlight component = GetComponent<UIBHighlight>();
		if (!(component == null))
		{
			component.AlwaysOver = selected;
			if (selected)
			{
				EnableFancyHighlight(enable: false);
			}
		}
	}

	public void HighlightOnce()
	{
		UIBHighlight component = GetComponent<UIBHighlight>();
		if (!(component == null))
		{
			component.HighlightOnce();
		}
	}

	public void ShowNewLookGlow()
	{
		EnableFancyHighlight(enable: true);
	}

	private void EnableFancyHighlight(bool enable)
	{
		UIBHighlightStateControl component = GetComponent<UIBHighlightStateControl>();
		if (!(component == null))
		{
			component.Select(enable);
		}
	}

	private void ShowCoin(bool show)
	{
		if (!(m_Coin == null))
		{
			TransformUtil.SetEulerAngleZ(m_Coin, show ? 0f : (-180f));
		}
	}

	private void Update()
	{
		if (neverRun)
		{
			Debug.Log("TEST");
		}
	}
}
