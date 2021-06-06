using UnityEngine;

public class CardCrafting_WepSetParent : MonoBehaviour
{
	public GameObject m_Parent;

	public Transform m_OrgParent;

	public GameObject m_ManaGem;

	public GameObject m_Portrait;

	public GameObject m_NameBanner;

	public GameObject m_RarityGem;

	public GameObject m_Discription;

	public GameObject m_Swords;

	public GameObject m_Shield;

	private void Start()
	{
		if (!m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	public void SetParentManaGem()
	{
		if ((bool)m_Parent)
		{
			m_ManaGem.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentPortrait()
	{
		if ((bool)m_Parent)
		{
			m_Portrait.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentNameBanner()
	{
		if ((bool)m_Parent)
		{
			m_NameBanner.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentRarityGem()
	{
		if ((bool)m_Parent)
		{
			m_RarityGem.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentDiscription()
	{
		if ((bool)m_Parent)
		{
			m_Discription.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentSwords()
	{
		if ((bool)m_Parent)
		{
			m_Swords.transform.parent = m_Parent.transform;
		}
	}

	public void SetParentShield()
	{
		if ((bool)m_Parent)
		{
			m_Shield.transform.parent = m_Parent.transform;
		}
	}

	public void SetBackToOrgParent()
	{
		if ((bool)m_OrgParent)
		{
			m_ManaGem.transform.parent = m_OrgParent;
		}
		m_Portrait.transform.parent = m_OrgParent;
		m_NameBanner.transform.parent = m_OrgParent;
		m_RarityGem.transform.parent = m_OrgParent;
		m_Discription.transform.parent = m_OrgParent;
		m_Swords.transform.parent = m_OrgParent;
		m_Shield.transform.parent = m_OrgParent;
	}
}
