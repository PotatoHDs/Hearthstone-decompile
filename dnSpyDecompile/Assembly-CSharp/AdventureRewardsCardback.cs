using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000048 RID: 72
[CustomEditClass]
public class AdventureRewardsCardback : MonoBehaviour
{
	// Token: 0x060003FD RID: 1021 RVA: 0x0001845C File Offset: 0x0001665C
	public void ShowCardBackReward()
	{
		this.HideCardBackReward();
		if (this.m_cardBackAppearAnimation == null || string.IsNullOrEmpty(this.m_cardBackAppearAnimationName) || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StopCoroutine("AnimateCardBackIn");
		base.StartCoroutine("AnimateCardBackIn");
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000184AF File Offset: 0x000166AF
	public void HideCardBackReward()
	{
		if (this.m_cardBackObject != null)
		{
			this.m_cardBackObject.SetActive(false);
		}
		if (this.m_cardBackText != null)
		{
			this.m_cardBackText.gameObject.SetActive(false);
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000184EA File Offset: 0x000166EA
	private void Awake()
	{
		if (Application.isPlaying)
		{
			this.LoadCardBackWithId();
		}
		this.m_cardBackTextOrigScale = this.m_cardBackText.transform.localScale;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00018510 File Offset: 0x00016710
	private void LoadCardBackWithId()
	{
		if (this.m_cardBackObject != null)
		{
			UnityEngine.Object.Destroy(this.m_cardBackObject);
		}
		if (this.m_cardBackId < 0)
		{
			Debug.LogError("Card back ID must be a positive number");
			return;
		}
		this.m_cardBackObjectLoading = CardBackManager.Get().LoadCardBackByIndex(this.m_cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			GameObject gameObject = cardBackData.m_GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.name = "CARD_BACK_" + cardBackData.m_CardBackIndex;
			Actor component = gameObject.GetComponent<Actor>();
			if (component != null)
			{
				GameObject cardMesh = component.m_cardMesh;
				component.SetCardbackUpdateIgnore(true);
				component.SetUnlit();
				if (cardMesh != null)
				{
					Material material = cardMesh.GetComponent<Renderer>().GetMaterial();
					if (material.HasProperty("_SpecularIntensity"))
					{
						material.SetFloat("_SpecularIntensity", 0f);
					}
				}
			}
			this.m_cardBackObject = gameObject;
			SceneUtils.SetLayer(this.m_cardBackObject, this.m_cardBackContainer.gameObject.layer, null);
			GameUtils.SetParent(this.m_cardBackObject, this.m_cardBackContainer, false);
			this.m_cardBackObject.transform.localPosition = Vector3.zero;
			this.m_cardBackObject.transform.localScale = Vector3.one;
			this.m_cardBackObject.transform.localRotation = Quaternion.identity;
			AnimationUtil.FloatyPosition(this.m_cardBackContainer, this.m_driftRadius, this.m_driftTime);
			this.HideCardBackReward();
			this.m_cardBackObjectLoading = false;
		}, null);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0001856D File Offset: 0x0001676D
	private IEnumerator AnimateCardBackIn()
	{
		this.m_cardBackAppearAnimation.Stop(this.m_cardBackAppearAnimationName);
		this.m_cardBackAppearAnimation.Rewind(this.m_cardBackAppearAnimationName);
		yield return new WaitForSeconds(this.m_cardBackAppearDelay);
		this.m_cardBackAppearAnimation.Play(this.m_cardBackAppearAnimationName);
		if (!string.IsNullOrEmpty(this.m_cardBackAppearSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_cardBackAppearSound);
		}
		yield return new WaitForSeconds(this.m_cardBackAppearTime);
		while (this.m_cardBackObjectLoading)
		{
			yield return null;
		}
		if (this.m_cardBackObject != null)
		{
			this.m_cardBackObject.SetActive(true);
		}
		this.m_cardBackText.gameObject.SetActive(true);
		this.m_cardBackText.transform.localScale = Vector3.one * 0.01f;
		iTween.ScaleTo(this.m_cardBackText.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_cardBackTextOrigScale,
			"time",
			this.m_cardBackAppearTime
		}));
		yield break;
	}

	// Token: 0x040002B6 RID: 694
	public GeneralStoreAdventureContent m_parentContent;

	// Token: 0x040002B7 RID: 695
	public GameObject m_cardBackContainer;

	// Token: 0x040002B8 RID: 696
	public Animation m_cardBackAppearAnimation;

	// Token: 0x040002B9 RID: 697
	public UberText m_cardBackText;

	// Token: 0x040002BA RID: 698
	public string m_cardBackAppearAnimationName;

	// Token: 0x040002BB RID: 699
	public float m_cardBackAppearDelay = 0.5f;

	// Token: 0x040002BC RID: 700
	public float m_cardBackAppearTime = 0.5f;

	// Token: 0x040002BD RID: 701
	public int m_cardBackId = -1;

	// Token: 0x040002BE RID: 702
	public float m_driftRadius = 0.1f;

	// Token: 0x040002BF RID: 703
	public float m_driftTime = 10f;

	// Token: 0x040002C0 RID: 704
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_cardBackAppearSound;

	// Token: 0x040002C1 RID: 705
	private GameObject m_cardBackObject;

	// Token: 0x040002C2 RID: 706
	private bool m_cardBackObjectLoading;

	// Token: 0x040002C3 RID: 707
	private Vector3 m_cardBackTextOrigScale;
}
