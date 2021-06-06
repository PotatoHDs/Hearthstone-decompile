using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class ArenaPhoneControl : MonoBehaviour
{
	// Token: 0x060023BA RID: 9146 RVA: 0x000B2206 File Offset: 0x000B0406
	private void Awake()
	{
		this.m_CurrentMode = ArenaPhoneControl.ControlMode.ChooseHero;
		this.m_ButtonCollider.enabled = false;
		this.m_ChooseText.Text = GameStrings.Get("GLUE_CHOOSE_YOUR_HERO");
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000B2230 File Offset: 0x000B0430
	[ContextMenu("ChooseHero")]
	public void SetModeChooseHero()
	{
		this.SetMode(ArenaPhoneControl.ControlMode.ChooseHero);
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x000B2239 File Offset: 0x000B0439
	[ContextMenu("ChooseCard")]
	public void SetModeChooseCard()
	{
		this.SetMode(ArenaPhoneControl.ControlMode.ChooseCard);
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x000B2242 File Offset: 0x000B0442
	[ContextMenu("CardCountViewDeck")]
	public void SetModeCardCountViewDeck()
	{
		this.SetMode(ArenaPhoneControl.ControlMode.CardCountViewDeck);
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x000B224B File Offset: 0x000B044B
	[ContextMenu("ViewDeck")]
	public void SetModeViewDeck()
	{
		this.SetMode(ArenaPhoneControl.ControlMode.ViewDeck);
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x000B2254 File Offset: 0x000B0454
	[ContextMenu("ChooseHeroPower")]
	public void SetModeChooseHeroPower()
	{
		this.SetMode(ArenaPhoneControl.ControlMode.ChooseHeroPower);
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x000B2260 File Offset: 0x000B0460
	public void SetMode(ArenaPhoneControl.ControlMode mode)
	{
		if (mode == this.m_CurrentMode)
		{
			return;
		}
		switch (mode)
		{
		case ArenaPhoneControl.ControlMode.ChooseHero:
			this.m_ViewDeckButton.SetActive(false);
			this.m_ButtonCollider.enabled = false;
			this.m_ChooseText.Text = GameStrings.Get("GLUE_CHOOSE_YOUR_HERO");
			this.m_ChooseDetailText.Text = string.Empty;
			if (this.m_CurrentMode == ArenaPhoneControl.ControlMode.CardCountViewDeck)
			{
				this.RotateTo(180f, 0f);
			}
			break;
		case ArenaPhoneControl.ControlMode.ChooseHeroPower:
			this.m_ViewDeckButton.SetActive(false);
			this.m_ButtonCollider.enabled = false;
			this.m_ChooseText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_TITLE");
			this.m_ChooseDetailText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_DETAIL");
			if (this.m_CurrentMode == ArenaPhoneControl.ControlMode.CardCountViewDeck)
			{
				this.RotateTo(180f, 0f);
			}
			break;
		case ArenaPhoneControl.ControlMode.ChooseCard:
			this.m_ViewDeckButton.SetActive(false);
			this.m_ButtonCollider.enabled = false;
			this.m_ChooseText.Text = GameStrings.Get("GLUE_DRAFT_INSTRUCTIONS");
			this.m_ChooseDetailText.Text = string.Empty;
			if (this.m_CurrentMode == ArenaPhoneControl.ControlMode.CardCountViewDeck)
			{
				this.RotateTo(180f, 0f);
			}
			break;
		case ArenaPhoneControl.ControlMode.CardCountViewDeck:
			this.m_ButtonCollider.center = this.m_CountAndViewDeckCollCenter;
			this.m_ButtonCollider.size = this.m_CountAndViewDeckCollSize;
			this.m_ButtonCollider.enabled = true;
			this.RotateTo(0f, 180f);
			break;
		case ArenaPhoneControl.ControlMode.ViewDeck:
			this.m_ButtonCollider.center = this.m_ViewDeckCollCenter;
			this.m_ButtonCollider.size = this.m_ViewDeckCollSize;
			this.m_ViewDeckButton.SetActive(true);
			this.m_ButtonCollider.enabled = true;
			if (this.m_CurrentMode == ArenaPhoneControl.ControlMode.CardCountViewDeck)
			{
				this.RotateTo(180f, 0f);
			}
			break;
		case ArenaPhoneControl.ControlMode.Rewards:
			this.m_ViewDeckButton.SetActive(false);
			this.m_ButtonCollider.enabled = false;
			this.m_ChooseText.Text = string.Empty;
			this.m_ChooseDetailText.Text = string.Empty;
			if (this.m_CurrentMode == ArenaPhoneControl.ControlMode.CardCountViewDeck)
			{
				this.RotateTo(180f, 0f);
			}
			break;
		}
		this.m_CurrentMode = mode;
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x000B24AC File Offset: 0x000B06AC
	private void RotateTo(float rotFrom, float rotTo)
	{
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			rotFrom,
			"to",
			rotTo,
			"time",
			1f,
			"easetype",
			iTween.EaseType.easeOutBounce,
			"onupdate",
			new Action<object>(delegate(object val)
			{
				base.transform.localRotation = Quaternion.Euler((float)val, 0f, 0f);
			})
		}));
	}

	// Token: 0x040013DC RID: 5084
	public UberText m_ChooseText;

	// Token: 0x040013DD RID: 5085
	public UberText m_ChooseDetailText;

	// Token: 0x040013DE RID: 5086
	public GameObject m_ViewDeckButton;

	// Token: 0x040013DF RID: 5087
	public BoxCollider m_ButtonCollider;

	// Token: 0x040013E0 RID: 5088
	public Vector3 m_CountAndViewDeckCollCenter;

	// Token: 0x040013E1 RID: 5089
	public Vector3 m_CountAndViewDeckCollSize;

	// Token: 0x040013E2 RID: 5090
	public Vector3 m_ViewDeckCollCenter;

	// Token: 0x040013E3 RID: 5091
	public Vector3 m_ViewDeckCollSize;

	// Token: 0x040013E4 RID: 5092
	private ArenaPhoneControl.ControlMode m_CurrentMode;

	// Token: 0x020015A8 RID: 5544
	public enum ControlMode
	{
		// Token: 0x0400AE92 RID: 44690
		ChooseHero,
		// Token: 0x0400AE93 RID: 44691
		ChooseHeroPower,
		// Token: 0x0400AE94 RID: 44692
		ChooseCard,
		// Token: 0x0400AE95 RID: 44693
		CardCountViewDeck,
		// Token: 0x0400AE96 RID: 44694
		ViewDeck,
		// Token: 0x0400AE97 RID: 44695
		Rewards
	}
}
