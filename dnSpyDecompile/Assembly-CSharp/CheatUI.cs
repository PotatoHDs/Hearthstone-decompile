using System;
using UnityEngine;

// Token: 0x02000B42 RID: 2882
public class CheatUI : MonoBehaviour
{
	// Token: 0x060098D2 RID: 39122 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x060098D3 RID: 39123 RVA: 0x0031768D File Offset: 0x0031588D
	private void Start()
	{
		this.m_CheatManagerMenu.SetActive(false);
	}

	// Token: 0x060098D4 RID: 39124 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnEnable()
	{
	}

	// Token: 0x060098D5 RID: 39125 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnDisable()
	{
	}

	// Token: 0x060098D6 RID: 39126 RVA: 0x0031768D File Offset: 0x0031588D
	public void CloseCheatMenu()
	{
		this.m_CheatManagerMenu.SetActive(false);
	}

	// Token: 0x060098D7 RID: 39127 RVA: 0x0031769C File Offset: 0x0031589C
	private void Update()
	{
		if (InputCollection.GetKey(KeyCode.LeftControl) && InputCollection.GetKey(KeyCode.LeftAlt) && InputCollection.GetKey(KeyCode.LeftShift) && InputCollection.GetKeyDown(KeyCode.C))
		{
			this.m_CheatManagerMenu.SetActive(!this.m_CheatManagerMenu.activeSelf);
			this.SetActiveTabOnOpen();
		}
	}

	// Token: 0x060098D8 RID: 39128 RVA: 0x003176F8 File Offset: 0x003158F8
	private void SetActiveTabOnOpen()
	{
		string a = "Level";
		if (a == "Match")
		{
			this.m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(0);
			return;
		}
		if (!(a == "ClosedBox"))
		{
			this.m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(3);
			return;
		}
		this.m_CheatManagerMenu.GetComponent<CheatMenu>().SetAsActiveTab(3);
	}

	// Token: 0x04007FD1 RID: 32721
	public GameObject m_CheatManagerMenu;
}
