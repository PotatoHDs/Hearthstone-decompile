using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AEE RID: 2798
[CustomEditClass]
[RequireComponent(typeof(Collider))]
public class UIBButton : PegUIElement
{
	// Token: 0x060094C2 RID: 38082 RVA: 0x0030300A File Offset: 0x0030120A
	protected override void OnPress()
	{
		if (!this.m_DepressOnOver)
		{
			this.Depress();
		}
	}

	// Token: 0x060094C3 RID: 38083 RVA: 0x0030301A File Offset: 0x0030121A
	protected override void OnRelease()
	{
		if ((!this.m_DepressOnOver && !this.m_HoldDepressionOnRelease) || (this.m_HoldingDepression && this.m_HoldDepressionOnRelease))
		{
			this.Raise();
			return;
		}
		if (this.m_HoldDepressionOnRelease)
		{
			this.m_HoldingDepression = true;
		}
	}

	// Token: 0x060094C4 RID: 38084 RVA: 0x00303052 File Offset: 0x00301252
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (this.m_Depressed && !this.m_HoldingDepression)
		{
			this.Raise();
		}
	}

	// Token: 0x060094C5 RID: 38085 RVA: 0x0030306A File Offset: 0x0030126A
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_DepressOnOver)
		{
			this.Depress();
		}
		this.Wiggle();
	}

	// Token: 0x060094C6 RID: 38086 RVA: 0x00303080 File Offset: 0x00301280
	public void Select()
	{
		this.Depress();
	}

	// Token: 0x060094C7 RID: 38087 RVA: 0x00303088 File Offset: 0x00301288
	public void Deselect()
	{
		this.Raise();
	}

	// Token: 0x060094C8 RID: 38088 RVA: 0x00303090 File Offset: 0x00301290
	public void Flip(bool faceUp, bool forceImmediate = false)
	{
		if (this.m_RootObject == null)
		{
			return;
		}
		this.InitOriginalRotation();
		this.m_targetRotation = (faceUp ? this.m_RootObjectOriginalRotation.Value : (this.m_RootObjectOriginalRotation.Value + this.m_DisabledRotation));
		iTween.StopByName(this.m_RootObject, "flip");
		if (this.m_AnimateFlip && !forceImmediate)
		{
			Vector3 vector = faceUp ? (-this.m_DisabledRotation) : this.m_DisabledRotation;
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				vector,
				"time",
				this.m_AnimateFlipTime,
				"easeType",
				iTween.EaseType.linear,
				"isLocal",
				true,
				"name",
				"flip",
				"oncomplete",
				new Action<object>(delegate(object o)
				{
					this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
				})
			});
			iTween.RotateAdd(this.m_RootObject, args);
			if (this.m_WigglePostFlip)
			{
				this.Wiggle(this.m_PostFlipWiggleAmount, this.m_targetRotation, this.m_PostFlipWiggleTime, this.m_AnimateFlipTime);
				return;
			}
		}
		else
		{
			this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
		}
	}

	// Token: 0x060094C9 RID: 38089 RVA: 0x003031E0 File Offset: 0x003013E0
	public void SetText(string text)
	{
		if (this.m_ButtonText != null)
		{
			this.m_ButtonText.Text = text;
		}
	}

	// Token: 0x060094CA RID: 38090 RVA: 0x003031FC File Offset: 0x003013FC
	public string GetText()
	{
		if (!this.m_ButtonText.GameStringLookup)
		{
			return this.m_ButtonText.Text;
		}
		return GameStrings.Get(this.m_ButtonText.Text);
	}

	// Token: 0x060094CB RID: 38091 RVA: 0x00303227 File Offset: 0x00301427
	public bool IsHoldingDepression()
	{
		return this.m_HoldingDepression;
	}

	// Token: 0x060094CC RID: 38092 RVA: 0x00303230 File Offset: 0x00301430
	private void Raise()
	{
		if (this.m_RootObject == null || !this.m_Depressed)
		{
			return;
		}
		this.m_Depressed = false;
		this.m_HoldingDepression = false;
		iTween.StopByName(this.m_RootObject, "depress");
		if (this.m_RaiseTime > 0f)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_RootObjectOriginalPosition,
				"time",
				this.m_RaiseTime,
				"easeType",
				this.m_DepressEaseType,
				"isLocal",
				true,
				"name",
				"depress"
			});
			iTween.MoveTo(this.m_RootObject, args);
			return;
		}
		this.m_RootObject.transform.localPosition = this.m_RootObjectOriginalPosition.Value;
	}

	// Token: 0x060094CD RID: 38093 RVA: 0x00303318 File Offset: 0x00301518
	private void Depress()
	{
		if (this.m_RootObject == null || this.m_Depressed || (UniversalInputManager.UsePhoneUI && !this.m_DepressOnPhone))
		{
			return;
		}
		this.InitOriginalPosition();
		this.m_Depressed = true;
		iTween.StopByName(this.m_RootObject, "depress");
		Vector3 vector = this.m_RootObjectOriginalPosition.Value + this.m_ClickDownOffset;
		if (this.m_DepressTime > 0f)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				vector,
				"time",
				this.m_DepressTime,
				"easeType",
				this.m_DepressEaseType,
				"isLocal",
				true,
				"name",
				"depress"
			});
			iTween.MoveTo(this.m_RootObject, args);
			return;
		}
		this.m_RootObject.transform.localPosition = vector;
	}

	// Token: 0x060094CE RID: 38094 RVA: 0x0030341C File Offset: 0x0030161C
	private void Wiggle()
	{
		if (this.m_RootObject == null || UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.InitOriginalRotation();
		this.Wiggle(this.m_WiggleAmount, this.m_RootObjectOriginalRotation.Value, this.m_WiggleTime, 0f);
	}

	// Token: 0x060094CF RID: 38095 RVA: 0x0030346C File Offset: 0x0030166C
	private void Wiggle(Vector3 amount, Vector3 originalRotation, float time, float delay)
	{
		if (this.m_RootObject == null || amount.sqrMagnitude == 0f || time <= 0f)
		{
			return;
		}
		this.InitOriginalRotation();
		if (iTween.CountByName(this.m_RootObject, "wiggle") > 0)
		{
			iTween.StopByName(this.m_RootObject, "wiggle");
			this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time,
			"delay",
			delay,
			"name",
			"wiggle",
			"onstart",
			new Action<object>(delegate(object o)
			{
				this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
			}),
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
			})
		});
		iTween.PunchRotation(this.m_RootObject, args);
	}

	// Token: 0x060094D0 RID: 38096 RVA: 0x0030356B File Offset: 0x0030176B
	private void InitOriginalRotation()
	{
		if (this.m_RootObject == null)
		{
			return;
		}
		if (this.m_RootObjectOriginalRotation == null)
		{
			this.m_RootObjectOriginalRotation = new Vector3?(this.m_RootObject.transform.localEulerAngles);
		}
	}

	// Token: 0x060094D1 RID: 38097 RVA: 0x003035A4 File Offset: 0x003017A4
	private void InitOriginalPosition()
	{
		if (this.m_RootObject == null)
		{
			return;
		}
		if (this.m_RootObjectOriginalPosition == null)
		{
			this.m_RootObjectOriginalPosition = new Vector3?(this.m_RootObject.transform.localPosition);
		}
	}

	// Token: 0x060094D2 RID: 38098 RVA: 0x003035DD File Offset: 0x003017DD
	private void OnDisable()
	{
		iTween.StopByName("wiggle");
		if (this.m_RootObject == null)
		{
			return;
		}
		this.m_RootObject.transform.localEulerAngles = this.m_targetRotation;
	}

	// Token: 0x060094D3 RID: 38099 RVA: 0x0030360E File Offset: 0x0030180E
	protected override void Awake()
	{
		base.Awake();
		if (this.m_RootObject == null)
		{
			return;
		}
		this.m_targetRotation = this.m_RootObject.transform.localEulerAngles;
	}

	// Token: 0x060094D4 RID: 38100 RVA: 0x0030363B File Offset: 0x0030183B
	protected override void OnTap()
	{
		base.OnTap();
		if (!string.IsNullOrEmpty(this.m_bubbleUpEvent))
		{
			SendEventUpwardStateAction.SendEventUpward(base.gameObject, this.m_bubbleUpEvent, null);
		}
	}

	// Token: 0x04007CAE RID: 31918
	[CustomEditField(Sections = "Button Objects")]
	public GameObject m_RootObject;

	// Token: 0x04007CAF RID: 31919
	[CustomEditField(Sections = "Text Object")]
	public UberText m_ButtonText;

	// Token: 0x04007CB0 RID: 31920
	[CustomEditField(Sections = "Click Depress Behavior")]
	public Vector3 m_ClickDownOffset = new Vector3(0f, -0.05f, 0f);

	// Token: 0x04007CB1 RID: 31921
	[CustomEditField(Sections = "Click Depress Behavior")]
	public float m_RaiseTime = 0.1f;

	// Token: 0x04007CB2 RID: 31922
	[CustomEditField(Sections = "Click Depress Behavior")]
	public float m_DepressTime = 0.1f;

	// Token: 0x04007CB3 RID: 31923
	[CustomEditField(Sections = "Click Depress Behavior")]
	public iTween.EaseType m_DepressEaseType = iTween.EaseType.linear;

	// Token: 0x04007CB4 RID: 31924
	[CustomEditField(Sections = "Click Depress Behavior")]
	public bool m_HoldDepressionOnRelease;

	// Token: 0x04007CB5 RID: 31925
	[CustomEditField(Sections = "Click Depress Behavior")]
	public bool m_DepressOnPhone;

	// Token: 0x04007CB6 RID: 31926
	[CustomEditField(Sections = "Roll Over Depress Behavior")]
	public bool m_DepressOnOver;

	// Token: 0x04007CB7 RID: 31927
	[CustomEditField(Sections = "Wiggle Behavior")]
	public Vector3 m_WiggleAmount = new Vector3(90f, 0f, 0f);

	// Token: 0x04007CB8 RID: 31928
	[CustomEditField(Sections = "Wiggle Behavior")]
	public float m_WiggleTime = 0.5f;

	// Token: 0x04007CB9 RID: 31929
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public Vector3 m_DisabledRotation = Vector3.zero;

	// Token: 0x04007CBA RID: 31930
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public bool m_AnimateFlip;

	// Token: 0x04007CBB RID: 31931
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public float m_AnimateFlipTime = 0.25f;

	// Token: 0x04007CBC RID: 31932
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public bool m_WigglePostFlip;

	// Token: 0x04007CBD RID: 31933
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public Vector3 m_PostFlipWiggleAmount = new Vector3(90f, 0f, 0f);

	// Token: 0x04007CBE RID: 31934
	[CustomEditField(Sections = "Flip Enable Behavior")]
	public float m_PostFlipWiggleTime = 0.5f;

	// Token: 0x04007CBF RID: 31935
	[CustomEditField(Sections = "Events")]
	public string m_bubbleUpEvent;

	// Token: 0x04007CC0 RID: 31936
	private Vector3? m_RootObjectOriginalPosition;

	// Token: 0x04007CC1 RID: 31937
	private Vector3? m_RootObjectOriginalRotation;

	// Token: 0x04007CC2 RID: 31938
	private bool m_Depressed;

	// Token: 0x04007CC3 RID: 31939
	private bool m_HoldingDepression;

	// Token: 0x04007CC4 RID: 31940
	private Vector3 m_targetRotation;
}
