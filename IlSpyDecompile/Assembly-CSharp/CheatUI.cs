using UnityEngine;

public class CheatUI : MonoBehaviour
{
	public GameObject m_CheatManagerMenu;

	private void Awake()
	{
	}

	private void Start()
	{
		m_CheatManagerMenu.SetActive(value: false);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void CloseCheatMenu()
	{
		m_CheatManagerMenu.SetActive(value: false);
	}

	private void Update()
	{
		if (InputCollection.GetKey(KeyCode.LeftControl) && InputCollection.GetKey(KeyCode.LeftAlt) && InputCollection.GetKey(KeyCode.LeftShift) && InputCollection.GetKeyDown(KeyCode.C))
		{
			m_CheatManagerMenu.SetActive(!m_CheatManagerMenu.activeSelf);
			SetActiveTabOnOpen();
		}
	}

	private void SetActiveTabOnOpen()
	{
		string text = "Level";
		if (!(text == "Match"))
		{
			if (text == "ClosedBox")
			{
				m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(3);
			}
			else
			{
				m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(3);
			}
		}
		else
		{
			m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(0);
		}
	}
}
