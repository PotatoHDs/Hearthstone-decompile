using System.Collections;
using UnityEngine;

public class PracticeAIButton : PegUIElement
{
	public UberText m_name;

	public UberText m_backsideName;

	public GameObject m_frontCover;

	public GameObject m_backsideCover;

	public HighlightState m_highlight;

	public GameObject m_unlockEffect;

	public GameObject m_questBang;

	public int m_PortraitMaterialIdx = -1;

	public GameObject m_rootObject;

	public Transform m_upBone;

	public Transform m_downBone;

	public Transform m_coveredBone;

	private int m_missionID;

	private long m_deckID;

	private bool m_covered;

	private bool m_locked;

	private bool m_infoSet;

	private bool m_usingBackside;

	private TAG_CLASS m_class;

	private DefLoader.DisposableCardDef m_cardDef;

	private const float FLIPPED_X_ROTATION = 180f;

	private const float NORMAL_X_ROTATION = 0f;

	private readonly string FLIP_COROUTINE = "WaitThenFlip";

	private readonly Vector3 GLOW_QUAD_NORMAL_LOCAL_POS = new Vector3(-0.1953466f, 1.336676f, 0.00721521f);

	private readonly Vector3 GLOW_QUAD_FLIPPED_LOCAL_POS = new Vector3(-0.1953466f, -1.336676f, 0.00721521f);

	public int GetMissionID()
	{
		return m_missionID;
	}

	public long GetDeckID()
	{
		return m_deckID;
	}

	public TAG_CLASS GetClass()
	{
		return m_class;
	}

	public void PlayUnlockGlow()
	{
		m_unlockEffect.GetComponent<Animation>().Play("AITileGlow");
	}

	public void Lock(bool locked)
	{
		m_locked = locked;
		float value = (m_locked ? 1 : 0);
		bool flag = ((!m_locked) ? true : false);
		SetEnabled(flag);
		GetShowingMaterial().SetFloat("_Desaturate", value);
		m_rootObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
	}

	public void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, int missionID, bool flip)
	{
		SetInfo(name, buttonClass, cardDef, missionID, 0L, flip);
	}

	public void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, long deckID, bool flip)
	{
		SetInfo(name, buttonClass, cardDef, 0, deckID, flip);
	}

	public void CoverUp(bool flip)
	{
		m_covered = true;
		if (flip)
		{
			GetHiddenNameMesh().Text = "";
			GetHiddenCover().GetComponent<Renderer>().enabled = true;
			Flip();
		}
		else
		{
			GetShowingNameMesh().Text = "";
			GetShowingCover().GetComponent<Renderer>().enabled = true;
		}
		Hashtable args = iTween.Hash("position", m_coveredBone.localPosition, "time", 0.25f, "isLocal", true, "easeType", iTween.EaseType.linear);
		iTween.MoveTo(m_rootObject, args);
		SetEnabled(enabled: false);
	}

	public void Select()
	{
		SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		SetEnabled(enabled: false);
		Depress();
	}

	public void Deselect()
	{
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		if (!m_covered)
		{
			Raise();
			if (!m_locked)
			{
				SetEnabled(enabled: true);
			}
		}
	}

	public void Raise()
	{
		Raise(0.1f);
	}

	public void ShowQuestBang(bool shown)
	{
		m_questBang.SetActive(shown);
	}

	private void Flip()
	{
		StopCoroutine(FLIP_COROUTINE);
		m_usingBackside = !m_usingBackside;
		StartCoroutine(FLIP_COROUTINE, m_usingBackside);
	}

	private IEnumerator WaitThenFlip(bool flipToBackside)
	{
		iTween.StopByName(base.gameObject, "flip");
		yield return new WaitForEndOfFrame();
		float x = (flipToBackside ? 0f : 180f);
		m_rootObject.transform.localEulerAngles = new Vector3(x, 0f, 0f);
		Hashtable args = iTween.Hash("amount", new Vector3(180f, 0f, 0f), "time", 0.25f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self, "name", "flip");
		iTween.RotateAdd(m_rootObject, args);
		float x2 = (flipToBackside ? 180f : 0f);
		m_highlight.transform.localEulerAngles = new Vector3(x2, 0f, 0f);
		m_unlockEffect.transform.localPosition = (flipToBackside ? GLOW_QUAD_FLIPPED_LOCAL_POS : GLOW_QUAD_NORMAL_LOCAL_POS);
	}

	private UberText GetShowingNameMesh()
	{
		if (!m_usingBackside)
		{
			return m_name;
		}
		return m_backsideName;
	}

	private UberText GetHiddenNameMesh()
	{
		if (!m_usingBackside)
		{
			return m_backsideName;
		}
		return m_name;
	}

	private Material GetShowingMaterial()
	{
		int materialIndex = ((!m_usingBackside) ? 1 : 2);
		return m_rootObject.GetComponent<Renderer>().GetMaterial(materialIndex);
	}

	private void SetShowingMaterial(Material mat)
	{
		int materialIndex = ((!m_usingBackside) ? 1 : 2);
		m_rootObject.GetComponent<Renderer>().SetMaterial(materialIndex, mat);
	}

	private Material GetHiddenMaterial()
	{
		int materialIndex = (m_usingBackside ? 1 : 2);
		return m_rootObject.GetComponent<Renderer>().GetMaterial(materialIndex);
	}

	private void SetHiddenMaterial(Material mat)
	{
		int materialIndex = (m_usingBackside ? 1 : 2);
		m_rootObject.GetComponent<Renderer>().SetMaterial(materialIndex, mat);
	}

	private GameObject GetShowingCover()
	{
		if (!m_usingBackside)
		{
			return m_frontCover;
		}
		return m_backsideCover;
	}

	private GameObject GetHiddenCover()
	{
		if (!m_usingBackside)
		{
			return m_backsideCover;
		}
		return m_frontCover;
	}

	private void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, int missionID, long deckID, bool flip)
	{
		SetMissionID(missionID);
		SetDeckID(deckID);
		SetButtonClass(buttonClass);
		m_cardDef?.Dispose();
		m_cardDef = cardDef;
		Material practiceAIPortrait = m_cardDef.CardDef.GetPracticeAIPortrait();
		if (flip)
		{
			GetHiddenNameMesh().Text = name;
			if (practiceAIPortrait != null)
			{
				SetHiddenMaterial(practiceAIPortrait);
			}
			Flip();
		}
		else
		{
			if (m_infoSet)
			{
				Debug.LogWarning("PracticeAIButton.SetInfo() - button is being re-initialized!");
			}
			m_infoSet = true;
			if (practiceAIPortrait != null)
			{
				SetShowingMaterial(practiceAIPortrait);
			}
			GetShowingNameMesh().Text = name;
			SetOriginalLocalPosition();
		}
		m_covered = false;
		GetShowingCover().GetComponent<Renderer>().enabled = false;
	}

	private void SetMissionID(int missionID)
	{
		m_missionID = missionID;
	}

	private void SetDeckID(long deckID)
	{
		m_deckID = deckID;
	}

	private void SetButtonClass(TAG_CLASS buttonClass)
	{
		m_class = buttonClass;
	}

	private void Raise(float time)
	{
		Hashtable args = iTween.Hash("position", m_upBone.localPosition, "time", time, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_rootObject, args);
	}

	private void Depress()
	{
		Hashtable args = iTween.Hash("position", m_downBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_rootObject, args);
	}

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	protected override void OnDestroy()
	{
		m_cardDef?.Dispose();
		m_cardDef = null;
		base.OnDestroy();
	}
}
