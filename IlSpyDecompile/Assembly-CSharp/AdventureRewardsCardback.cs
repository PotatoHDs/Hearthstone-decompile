using System.Collections;
using UnityEngine;

[CustomEditClass]
public class AdventureRewardsCardback : MonoBehaviour
{
	public GeneralStoreAdventureContent m_parentContent;

	public GameObject m_cardBackContainer;

	public Animation m_cardBackAppearAnimation;

	public UberText m_cardBackText;

	public string m_cardBackAppearAnimationName;

	public float m_cardBackAppearDelay = 0.5f;

	public float m_cardBackAppearTime = 0.5f;

	public int m_cardBackId = -1;

	public float m_driftRadius = 0.1f;

	public float m_driftTime = 10f;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_cardBackAppearSound;

	private GameObject m_cardBackObject;

	private bool m_cardBackObjectLoading;

	private Vector3 m_cardBackTextOrigScale;

	public void ShowCardBackReward()
	{
		HideCardBackReward();
		if (!(m_cardBackAppearAnimation == null) && !string.IsNullOrEmpty(m_cardBackAppearAnimationName) && base.gameObject.activeInHierarchy)
		{
			StopCoroutine("AnimateCardBackIn");
			StartCoroutine("AnimateCardBackIn");
		}
	}

	public void HideCardBackReward()
	{
		if (m_cardBackObject != null)
		{
			m_cardBackObject.SetActive(value: false);
		}
		if (m_cardBackText != null)
		{
			m_cardBackText.gameObject.SetActive(value: false);
		}
	}

	private void Awake()
	{
		if (Application.isPlaying)
		{
			LoadCardBackWithId();
		}
		m_cardBackTextOrigScale = m_cardBackText.transform.localScale;
	}

	private void LoadCardBackWithId()
	{
		if (m_cardBackObject != null)
		{
			Object.Destroy(m_cardBackObject);
		}
		if (m_cardBackId < 0)
		{
			Debug.LogError("Card back ID must be a positive number");
			return;
		}
		m_cardBackObjectLoading = CardBackManager.Get().LoadCardBackByIndex(m_cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			GameObject gameObject = cardBackData.m_GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.name = "CARD_BACK_" + cardBackData.m_CardBackIndex;
			Actor component = gameObject.GetComponent<Actor>();
			if (component != null)
			{
				GameObject cardMesh = component.m_cardMesh;
				component.SetCardbackUpdateIgnore(ignoreUpdate: true);
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
			m_cardBackObject = gameObject;
			SceneUtils.SetLayer(m_cardBackObject, m_cardBackContainer.gameObject.layer);
			GameUtils.SetParent(m_cardBackObject, m_cardBackContainer);
			m_cardBackObject.transform.localPosition = Vector3.zero;
			m_cardBackObject.transform.localScale = Vector3.one;
			m_cardBackObject.transform.localRotation = Quaternion.identity;
			AnimationUtil.FloatyPosition(m_cardBackContainer, m_driftRadius, m_driftTime);
			HideCardBackReward();
			m_cardBackObjectLoading = false;
		});
	}

	private IEnumerator AnimateCardBackIn()
	{
		m_cardBackAppearAnimation.Stop(m_cardBackAppearAnimationName);
		m_cardBackAppearAnimation.Rewind(m_cardBackAppearAnimationName);
		yield return new WaitForSeconds(m_cardBackAppearDelay);
		m_cardBackAppearAnimation.Play(m_cardBackAppearAnimationName);
		if (!string.IsNullOrEmpty(m_cardBackAppearSound))
		{
			SoundManager.Get().LoadAndPlay(m_cardBackAppearSound);
		}
		yield return new WaitForSeconds(m_cardBackAppearTime);
		while (m_cardBackObjectLoading)
		{
			yield return null;
		}
		if (m_cardBackObject != null)
		{
			m_cardBackObject.SetActive(value: true);
		}
		m_cardBackText.gameObject.SetActive(value: true);
		m_cardBackText.transform.localScale = Vector3.one * 0.01f;
		iTween.ScaleTo(m_cardBackText.gameObject, iTween.Hash("scale", m_cardBackTextOrigScale, "time", m_cardBackAppearTime));
	}
}
