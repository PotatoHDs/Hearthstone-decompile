using System;
using UnityEngine;

// Token: 0x02000868 RID: 2152
public class BoardTutorial : MonoBehaviour
{
	// Token: 0x0600742B RID: 29739 RVA: 0x0025435F File Offset: 0x0025255F
	private void Awake()
	{
		BoardTutorial.s_instance = this;
		SceneUtils.EnableRenderers(this.m_Highlight, false);
		SceneUtils.EnableRenderers(this.m_EnemyHighlight, false);
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	// Token: 0x0600742C RID: 29740 RVA: 0x0025439C File Offset: 0x0025259C
	private void OnDestroy()
	{
		BoardTutorial.s_instance = null;
	}

	// Token: 0x0600742D RID: 29741 RVA: 0x002543A4 File Offset: 0x002525A4
	public static BoardTutorial Get()
	{
		return BoardTutorial.s_instance;
	}

	// Token: 0x0600742E RID: 29742 RVA: 0x002543AB File Offset: 0x002525AB
	public void EnableHighlight(bool enable)
	{
		if (this.m_highlightEnabled == enable)
		{
			return;
		}
		this.m_highlightEnabled = enable;
		this.UpdateHighlight();
	}

	// Token: 0x0600742F RID: 29743 RVA: 0x002543C4 File Offset: 0x002525C4
	public void EnableEnemyHighlight(bool enable)
	{
		if (this.m_enemyHighlightEnabled == enable)
		{
			return;
		}
		this.m_enemyHighlightEnabled = enable;
		this.UpdateEnemyHighlight();
	}

	// Token: 0x06007430 RID: 29744 RVA: 0x002543DD File Offset: 0x002525DD
	public void EnableFullHighlight(bool enable)
	{
		this.EnableHighlight(enable);
		this.EnableEnemyHighlight(enable);
	}

	// Token: 0x06007431 RID: 29745 RVA: 0x002543ED File Offset: 0x002525ED
	public bool IsHighlightEnabled()
	{
		return this.m_highlightEnabled;
	}

	// Token: 0x06007432 RID: 29746 RVA: 0x002543F8 File Offset: 0x002525F8
	private void UpdateHighlight()
	{
		if (this.m_highlightEnabled)
		{
			SceneUtils.EnableRenderers(this.m_Highlight, this.m_highlightEnabled);
			this.m_Highlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_On");
			return;
		}
		this.m_Highlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_Off");
	}

	// Token: 0x06007433 RID: 29747 RVA: 0x0025444C File Offset: 0x0025264C
	private void UpdateEnemyHighlight()
	{
		if (this.m_enemyHighlightEnabled)
		{
			SceneUtils.EnableRenderers(this.m_EnemyHighlight, this.m_enemyHighlightEnabled);
			this.m_EnemyHighlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_On");
			return;
		}
		this.m_EnemyHighlight.GetComponent<Animation>().Play("Glow_PlayArea_Player_Off");
	}

	// Token: 0x04005C48 RID: 23624
	public GameObject m_Highlight;

	// Token: 0x04005C49 RID: 23625
	public GameObject m_EnemyHighlight;

	// Token: 0x04005C4A RID: 23626
	public Light m_ManaSpotlight;

	// Token: 0x04005C4B RID: 23627
	private static BoardTutorial s_instance;

	// Token: 0x04005C4C RID: 23628
	private bool m_highlightEnabled;

	// Token: 0x04005C4D RID: 23629
	private bool m_enemyHighlightEnabled;
}
