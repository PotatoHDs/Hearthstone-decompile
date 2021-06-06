using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CardNerfGlows : MonoBehaviour
{
	public Material m_buffMaterial;

	public Material m_nerfMaterial;

	public GameObject m_attack;

	public GameObject m_health;

	public GameObject m_manaCost;

	public GameObject m_rarityGem;

	public GameObject m_art;

	public GameObject m_cardText;

	public GameObject m_cardName;

	public GameObject m_race;

	private void Awake()
	{
		HideAll();
	}

	public void SetGlowsForCard(CollectibleCard card, List<CardChangeDbfRecord> cardChanges)
	{
		HideAll();
		if (cardChanges == null)
		{
			return;
		}
		foreach (CardChangeDbfRecord cardChange in cardChanges)
		{
			if (cardChange.ChangeType == CardChange.ChangeType.BUFF || cardChange.ChangeType == CardChange.ChangeType.NERF)
			{
				Material material = ((cardChange.ChangeType == CardChange.ChangeType.BUFF) ? m_buffMaterial : m_nerfMaterial);
				switch (cardChange.TagId)
				{
				case 47:
					m_attack.GetComponent<Renderer>().SetMaterial(material);
					m_attack.SetActive(value: true);
					break;
				case 184:
					m_cardText.GetComponent<Renderer>().SetMaterial(material);
					m_cardText.SetActive(value: true);
					break;
				case 45:
					m_health.GetComponent<Renderer>().SetMaterial(material);
					m_health.SetActive(value: true);
					break;
				case 48:
					m_manaCost.GetComponent<Renderer>().SetMaterial(material);
					m_manaCost.SetActive(value: true);
					break;
				}
			}
		}
	}

	private void HideAll()
	{
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: false);
		}
	}
}
