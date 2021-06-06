using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class FiresideGatheringOpponentButton : PegUIElement
{
	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x060027D1 RID: 10193 RVA: 0x000C81DB File Offset: 0x000C63DB
	// (set) Token: 0x060027D2 RID: 10194 RVA: 0x000C81E3 File Offset: 0x000C63E3
	public BnetPlayer AssociatedBnetPlayer
	{
		get
		{
			return this.m_associatedBnetPlayer;
		}
		set
		{
			this.m_associatedBnetPlayer = value;
		}
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x000C81EC File Offset: 0x000C63EC
	public void SetName(string name)
	{
		this.m_name.Text = name;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x000C81FA File Offset: 0x000C63FA
	public void Select()
	{
		SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		this.m_highlight.SetActive(true);
		this.SetEnabled(false, false);
		this.Depress();
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x000C8230 File Offset: 0x000C6430
	public void Deselect()
	{
		this.m_highlight.SetActive(false);
		this.Raise();
		this.SetEnabled(true, false);
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x000C824C File Offset: 0x000C644C
	public void Raise()
	{
		this.Raise(0.1f);
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x000C8259 File Offset: 0x000C6459
	public void SetIsFriend(bool isFriend)
	{
		this.m_name.TextColor = (isFriend ? this.m_friendNameColor : this.m_patronNameColor);
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x000C8277 File Offset: 0x000C6477
	public void SetIsFiresideBrawl(bool isFiresideBrawl)
	{
		this.m_mainButtonMesh.SetMaterial(isFiresideBrawl ? this.m_firesideBrawlMaterial : this.m_friendlyDuelsMaterial);
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x000C8298 File Offset: 0x000C6498
	private void Raise(float time)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_upBone.localPosition,
			"time",
			time,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_rootObject, args);
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x000C830C File Offset: 0x000C650C
	private void Depress()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_downBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_rootObject, args);
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x000C8384 File Offset: 0x000C6584
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
	}

	// Token: 0x04001696 RID: 5782
	public UberText m_name;

	// Token: 0x04001697 RID: 5783
	public GameObject m_highlight;

	// Token: 0x04001698 RID: 5784
	public GameObject m_rootObject;

	// Token: 0x04001699 RID: 5785
	public Transform m_upBone;

	// Token: 0x0400169A RID: 5786
	public Transform m_downBone;

	// Token: 0x0400169B RID: 5787
	public Color m_friendNameColor;

	// Token: 0x0400169C RID: 5788
	public Color m_patronNameColor;

	// Token: 0x0400169D RID: 5789
	public MeshRenderer m_mainButtonMesh;

	// Token: 0x0400169E RID: 5790
	public Material m_friendlyDuelsMaterial;

	// Token: 0x0400169F RID: 5791
	public Material m_firesideBrawlMaterial;

	// Token: 0x040016A0 RID: 5792
	private BnetPlayer m_associatedBnetPlayer;
}
