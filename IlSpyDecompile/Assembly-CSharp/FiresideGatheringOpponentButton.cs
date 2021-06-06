using System.Collections;
using UnityEngine;

public class FiresideGatheringOpponentButton : PegUIElement
{
	public UberText m_name;

	public GameObject m_highlight;

	public GameObject m_rootObject;

	public Transform m_upBone;

	public Transform m_downBone;

	public Color m_friendNameColor;

	public Color m_patronNameColor;

	public MeshRenderer m_mainButtonMesh;

	public Material m_friendlyDuelsMaterial;

	public Material m_firesideBrawlMaterial;

	private BnetPlayer m_associatedBnetPlayer;

	public BnetPlayer AssociatedBnetPlayer
	{
		get
		{
			return m_associatedBnetPlayer;
		}
		set
		{
			m_associatedBnetPlayer = value;
		}
	}

	public void SetName(string name)
	{
		m_name.Text = name;
	}

	public void Select()
	{
		SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		m_highlight.SetActive(value: true);
		SetEnabled(enabled: false);
		Depress();
	}

	public void Deselect()
	{
		m_highlight.SetActive(value: false);
		Raise();
		SetEnabled(enabled: true);
	}

	public void Raise()
	{
		Raise(0.1f);
	}

	public void SetIsFriend(bool isFriend)
	{
		m_name.TextColor = (isFriend ? m_friendNameColor : m_patronNameColor);
	}

	public void SetIsFiresideBrawl(bool isFiresideBrawl)
	{
		m_mainButtonMesh.SetMaterial(isFiresideBrawl ? m_firesideBrawlMaterial : m_friendlyDuelsMaterial);
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
	}
}
