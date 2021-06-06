using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class ArenaRewardPaper : MonoBehaviour
{
	// Token: 0x060023C4 RID: 9156 RVA: 0x000B2553 File Offset: 0x000B0753
	public static AssetReference GetDefaultRewardPaper()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return ArenaRewardPaper.DEFAULT_REWARD_PAPER;
		}
		return ArenaRewardPaper.DEFAULT_REWARD_PAPER_PHONE;
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x000B256C File Offset: 0x000B076C
	public IEnumerator PlayRewardBurnAway(PlayMakerFSM rewardFSM)
	{
		Animation component = base.GetComponent<Animation>();
		component.Play();
		yield return new WaitForSeconds(component.clip.length);
		rewardFSM.SendEvent("FINISHED");
		yield break;
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x000B2584 File Offset: 0x000B0784
	public void PlayEmberWipeFX()
	{
		ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
	}

	// Token: 0x040013E5 RID: 5093
	public GameObject m_XmarksRoot;

	// Token: 0x040013E6 RID: 5094
	public List<GameObject> m_XmarkBox;

	// Token: 0x040013E7 RID: 5095
	public GameObject m_Xmark1;

	// Token: 0x040013E8 RID: 5096
	public GameObject m_Xmark2;

	// Token: 0x040013E9 RID: 5097
	public GameObject m_Xmark3;

	// Token: 0x040013EA RID: 5098
	public UberText m_WinsUberText;

	// Token: 0x040013EB RID: 5099
	public UberText m_LossesUberText;

	// Token: 0x040013EC RID: 5100
	public UberText m_EventEndsText;

	// Token: 0x040013ED RID: 5101
	private static readonly AssetReference DEFAULT_REWARD_PAPER = new AssetReference("ArenaPaper.prefab:0c4143d801e717543a456f444d689a16");

	// Token: 0x040013EE RID: 5102
	private static readonly AssetReference DEFAULT_REWARD_PAPER_PHONE = new AssetReference("ArenaPaper_phone.prefab:644a36f346814cc41bf925997db07f5e");
}
