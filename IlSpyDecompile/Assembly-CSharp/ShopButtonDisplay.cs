using Hearthstone.UI;
using UnityEngine;

public class ShopButtonDisplay : MonoBehaviour
{
	public enum DisplayType
	{
		BOOSTER,
		HERO,
		CARDBACK
	}

	public enum ProductIndex
	{
		AUTO,
		FIRST,
		SECOND,
		THIRD
	}

	[SerializeField]
	protected DisplayType displayType;

	[SerializeField]
	protected ProductIndex m_index;

	[Overridable]
	public ProductIndex Index
	{
		get
		{
			return m_index;
		}
		set
		{
			m_index = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Reload()
	{
	}
}
