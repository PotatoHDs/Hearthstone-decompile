using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRewardPaper : MonoBehaviour
{
	public GameObject m_XmarksRoot;

	public List<GameObject> m_XmarkBox;

	public GameObject m_Xmark1;

	public GameObject m_Xmark2;

	public GameObject m_Xmark3;

	public UberText m_WinsUberText;

	public UberText m_LossesUberText;

	public UberText m_EventEndsText;

	private static readonly AssetReference DEFAULT_REWARD_PAPER = new AssetReference("ArenaPaper.prefab:0c4143d801e717543a456f444d689a16");

	private static readonly AssetReference DEFAULT_REWARD_PAPER_PHONE = new AssetReference("ArenaPaper_phone.prefab:644a36f346814cc41bf925997db07f5e");

	public static AssetReference GetDefaultRewardPaper()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return DEFAULT_REWARD_PAPER;
		}
		return DEFAULT_REWARD_PAPER_PHONE;
	}

	public IEnumerator PlayRewardBurnAway(PlayMakerFSM rewardFSM)
	{
		Animation component = GetComponent<Animation>();
		component.Play();
		yield return new WaitForSeconds(component.clip.length);
		rewardFSM.SendEvent("FINISHED");
	}

	public void PlayEmberWipeFX()
	{
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Play();
		}
	}
}
