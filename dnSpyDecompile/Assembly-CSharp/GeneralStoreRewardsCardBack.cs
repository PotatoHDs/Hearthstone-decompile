using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000705 RID: 1797
[CustomEditClass]
public class GeneralStoreRewardsCardBack : MonoBehaviour
{
	// Token: 0x060064AD RID: 25773 RVA: 0x0020E589 File Offset: 0x0020C789
	public void SetCardBack(int id)
	{
		if (id == -1 || id == this.m_cardBackId)
		{
			return;
		}
		this.LoadCardBackWithId(id);
	}

	// Token: 0x060064AE RID: 25774 RVA: 0x0020E5A0 File Offset: 0x0020C7A0
	public void SetPreorderText(string text)
	{
		this.m_cardBackText.Text = text;
	}

	// Token: 0x060064AF RID: 25775 RVA: 0x0020E5B0 File Offset: 0x0020C7B0
	public void ShowCardBackReward()
	{
		this.HideCardBackReward();
		if (this.m_cardBackId == -1)
		{
			return;
		}
		if (this.m_cardBackAppearAnimation == null || string.IsNullOrEmpty(this.m_cardBackAppearAnimationName) || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StartCoroutine("AnimateCardBackIn");
	}

	// Token: 0x060064B0 RID: 25776 RVA: 0x0020E602 File Offset: 0x0020C802
	public void HideCardBackReward()
	{
		base.StopCoroutine("AnimateCardBackIn");
		if (this.m_cardBackContainer != null)
		{
			this.m_cardBackContainer.SetActive(false);
		}
	}

	// Token: 0x060064B1 RID: 25777 RVA: 0x0020E629 File Offset: 0x0020C829
	private void Awake()
	{
		this.m_cardBackTextOrigScale = this.m_cardBackText.transform.localScale;
	}

	// Token: 0x060064B2 RID: 25778 RVA: 0x0020E644 File Offset: 0x0020C844
	private void LoadCardBackWithId(int cardBackId)
	{
		if (this.m_cardBackObject != null)
		{
			UnityEngine.Object.Destroy(this.m_cardBackObject);
		}
		if (cardBackId < 0)
		{
			Debug.LogError("Card back ID must be a positive number");
			return;
		}
		this.m_cardBackId = cardBackId;
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
			if (this.m_cardBackContainer != null)
			{
				this.m_cardBackContainer.SetActive(false);
			}
			this.m_cardBackObjectLoading = false;
		}, null);
	}

	// Token: 0x060064B3 RID: 25779 RVA: 0x0020E6A3 File Offset: 0x0020C8A3
	private IEnumerator AnimateCardBackIn()
	{
		this.m_cardBackText.gameObject.SetActive(false);
		this.m_cardBackAppearAnimation.Stop(this.m_cardBackAppearAnimationName);
		this.m_cardBackAppearAnimation.Rewind(this.m_cardBackAppearAnimationName);
		yield return new WaitForSeconds(this.m_cardBackAppearDelay);
		while (this.m_cardBackObjectLoading)
		{
			yield return null;
		}
		this.m_cardBackContainer.SetActive(true);
		this.m_cardBackAppearAnimation.Play(this.m_cardBackAppearAnimationName);
		if (!string.IsNullOrEmpty(this.m_cardBackAppearSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_cardBackAppearSound);
		}
		yield return new WaitForSeconds(this.m_cardBackAppearTime);
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

	// Token: 0x040053B2 RID: 21426
	public GameObject m_cardBackContainer;

	// Token: 0x040053B3 RID: 21427
	public Animation m_cardBackAppearAnimation;

	// Token: 0x040053B4 RID: 21428
	public UberText m_cardBackText;

	// Token: 0x040053B5 RID: 21429
	public string m_cardBackAppearAnimationName;

	// Token: 0x040053B6 RID: 21430
	public float m_cardBackAppearDelay = 0.5f;

	// Token: 0x040053B7 RID: 21431
	public float m_cardBackAppearTime = 0.5f;

	// Token: 0x040053B8 RID: 21432
	public float m_driftRadius = 0.1f;

	// Token: 0x040053B9 RID: 21433
	public float m_driftTime = 10f;

	// Token: 0x040053BA RID: 21434
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_cardBackAppearSound;

	// Token: 0x040053BB RID: 21435
	private int m_cardBackId = -1;

	// Token: 0x040053BC RID: 21436
	private GameObject m_cardBackObject;

	// Token: 0x040053BD RID: 21437
	private bool m_cardBackObjectLoading;

	// Token: 0x040053BE RID: 21438
	private Vector3 m_cardBackTextOrigScale;
}
