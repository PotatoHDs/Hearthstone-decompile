using UnityEngine;

public class ManaCounter : MonoBehaviour
{
	public Player.Side m_Side;

	public GameObject m_phoneGemContainer;

	public UberText m_availableManaPhone;

	public UberText m_permanentManaPhone;

	private Player m_player;

	private UberText m_textMesh;

	private GameObject m_phoneGem;

	private void Awake()
	{
		m_textMesh = GetComponent<UberText>();
		if (m_Side != Player.Side.FRIENDLY)
		{
			if (m_availableManaPhone != null)
			{
				string message = "The property m_availableManaPhone is set on ManaCounter for non-friendly mana crystals. This should be null.";
				SceneDebugger.Get().AddErrorMessage(message);
			}
			if (m_permanentManaPhone != null)
			{
				string message2 = "The property m_permanentManaPhone is set on ManaCounter for non-friendly mana crystals. This should be null.";
				SceneDebugger.Get().AddErrorMessage(message2);
			}
		}
	}

	private void Start()
	{
		m_textMesh.Text = GameStrings.Format("GAMEPLAY_MANA_COUNTER", "0", "0");
	}

	public void InitializeLargeResourceGameObject(string resourcePath)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_phoneGem != null)
			{
				Object.Destroy(m_phoneGem);
			}
			m_phoneGem = AssetLoader.Get().InstantiatePrefab(resourcePath, AssetLoadingOptions.IgnorePrefabPosition);
			GameUtils.SetParent(m_phoneGem, m_phoneGemContainer, withRotation: true);
		}
	}

	public void SetPlayer(Player player)
	{
		m_player = player;
	}

	public Player GetPlayer()
	{
		return m_player;
	}

	public GameObject GetPhoneGem()
	{
		return m_phoneGem;
	}

	public void UpdateText()
	{
		if (base.gameObject.activeInHierarchy)
		{
			int num = m_player.GetTag(GAME_TAG.RESOURCES);
			if (!base.gameObject.activeInHierarchy)
			{
				base.gameObject.SetActive(value: true);
			}
			int numAvailableResources = m_player.GetNumAvailableResources();
			string text = ((!UniversalInputManager.UsePhoneUI || num < 10) ? GameStrings.Format("GAMEPLAY_MANA_COUNTER", numAvailableResources, num) : numAvailableResources.ToString());
			m_textMesh.Text = text;
			if ((bool)UniversalInputManager.UsePhoneUI && m_availableManaPhone != null && m_Side == Player.Side.FRIENDLY)
			{
				m_availableManaPhone.Text = numAvailableResources.ToString();
				m_permanentManaPhone.Text = num.ToString();
			}
		}
	}
}
