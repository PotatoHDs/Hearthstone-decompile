using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006A3 RID: 1699
public class BoosterStack : MonoBehaviour
{
	// Token: 0x06005ECD RID: 24269 RVA: 0x001ED230 File Offset: 0x001EB430
	private void Start()
	{
		foreach (object obj in this.m_boosterContainer.transform)
		{
			Transform transform = (Transform)obj;
			int idx = this.m_boosters.Count;
			this.m_boosters.Add(transform.gameObject);
			Widget component = transform.GetComponent<Widget>();
			if (component != null && !component.IsReady)
			{
				component.RegisterReadyListener(delegate(object w)
				{
					this.OnBoosterReady(idx);
				}, null, true);
			}
			else
			{
				this.OnBoosterReady(idx);
			}
		}
	}

	// Token: 0x06005ECE RID: 24270 RVA: 0x001ED2F4 File Offset: 0x001EB4F4
	private void Update()
	{
		if (this.IsSettled())
		{
			return;
		}
		this.m_playTime += Time.deltaTime;
		int num = this.m_currentStackSize;
		if (this.m_playTime < this.m_startTime)
		{
			return;
		}
		if (this.m_playTime < this.m_endTime && !Mathf.Approximately(this.m_endTime, this.m_startTime))
		{
			int num2 = this.m_startingStackSize + Math.Sign(this.m_targetStackSize - this.m_startingStackSize);
			float num3 = (this.m_playTime - this.m_startTime) / (this.m_endTime - this.m_startTime);
			num = num2 + (int)(num3 * (float)(this.m_targetStackSize - num2));
		}
		else
		{
			num = this.m_targetStackSize;
			this.m_playTime = (this.m_endTime = (this.m_startTime = 0f));
		}
		if (num > this.m_currentStackSize)
		{
			this.PlayEventAcrossRange(BoosterStack.BoosterEvent.INTRO, this.m_currentStackSize, num - 1);
		}
		else if (num < this.m_currentStackSize)
		{
			this.PlayEventAcrossRange(BoosterStack.BoosterEvent.OUTRO, this.m_currentStackSize - 1, num);
		}
		this.m_currentStackSize = num;
	}

	// Token: 0x06005ECF RID: 24271 RVA: 0x001ED3F9 File Offset: 0x001EB5F9
	private void Awake()
	{
		this.SetStacks(this.m_targetStackSize, this.m_instantIntro);
	}

	// Token: 0x1700059D RID: 1437
	// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x001ED40D File Offset: 0x001EB60D
	// (set) Token: 0x06005ED1 RID: 24273 RVA: 0x001ED415 File Offset: 0x001EB615
	[Overridable]
	public float StackingDelay
	{
		get
		{
			return this.m_stackingDelay;
		}
		set
		{
			this.m_stackingDelay = value;
		}
	}

	// Token: 0x1700059E RID: 1438
	// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x001ED41E File Offset: 0x001EB61E
	// (set) Token: 0x06005ED3 RID: 24275 RVA: 0x001ED426 File Offset: 0x001EB626
	[Overridable]
	public float StackingBaseDuration
	{
		get
		{
			return this.m_stackingBaseDuration;
		}
		set
		{
			this.m_stackingBaseDuration = value;
		}
	}

	// Token: 0x1700059F RID: 1439
	// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x001ED42F File Offset: 0x001EB62F
	// (set) Token: 0x06005ED5 RID: 24277 RVA: 0x001ED437 File Offset: 0x001EB637
	[Overridable]
	public float StackingIncrementalDuration
	{
		get
		{
			return this.m_stackingIncrementalDuration;
		}
		set
		{
			this.m_stackingIncrementalDuration = value;
		}
	}

	// Token: 0x170005A0 RID: 1440
	// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x001ED440 File Offset: 0x001EB640
	public int CurrentStackSize
	{
		get
		{
			return this.m_currentStackSize;
		}
	}

	// Token: 0x06005ED7 RID: 24279 RVA: 0x001ED448 File Offset: 0x001EB648
	public bool IsSettled()
	{
		return this.m_currentStackSize == this.m_targetStackSize;
	}

	// Token: 0x06005ED8 RID: 24280 RVA: 0x001ED458 File Offset: 0x001EB658
	public void SetStacks(int stackSize, bool instantaneous = true)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			this.m_targetStackSize = stackSize;
			this.m_instantIntro = instantaneous;
			return;
		}
		if (instantaneous)
		{
			this.m_targetStackSize = stackSize;
			if (stackSize > this.m_currentStackSize)
			{
				this.PlayEventAcrossRange(BoosterStack.BoosterEvent.INSTANT_INTRO, this.m_currentStackSize, stackSize - 1);
			}
			else if (stackSize < this.m_currentStackSize)
			{
				this.PlayEventAcrossRange(BoosterStack.BoosterEvent.INSTANT_OUTRO, this.m_currentStackSize - 1, stackSize);
			}
			this.m_currentStackSize = this.m_targetStackSize;
			return;
		}
		this.AddStacks(stackSize - this.m_targetStackSize);
	}

	// Token: 0x06005ED9 RID: 24281 RVA: 0x001ED4DC File Offset: 0x001EB6DC
	public void AddStacks(int deltaStacks)
	{
		deltaStacks = Math.Max(deltaStacks, -this.m_targetStackSize);
		float num = (float)Math.Abs(deltaStacks) * this.StackingIncrementalDuration;
		if (this.IsSettled())
		{
			this.m_playTime = 0f;
			this.m_startTime = this.StackingDelay;
			this.m_endTime = this.m_startTime + this.StackingBaseDuration + num;
			this.m_startingStackSize = this.m_currentStackSize;
		}
		else
		{
			if (deltaStacks > 0 != this.m_targetStackSize > this.m_currentStackSize)
			{
				this.m_endTime = (this.m_startTime = (this.m_playTime = 0f));
				this.m_startingStackSize = this.m_currentStackSize;
				num = (float)Math.Abs(deltaStacks + this.m_targetStackSize - this.m_currentStackSize) * this.StackingIncrementalDuration;
			}
			this.m_endTime += num;
		}
		this.m_targetStackSize += deltaStacks;
		if (this.m_currentStackSize == this.m_targetStackSize)
		{
			this.m_endTime = (this.m_startTime = 0f);
		}
	}

	// Token: 0x06005EDA RID: 24282 RVA: 0x001ED5E4 File Offset: 0x001EB7E4
	protected void PlayEventAcrossRange(BoosterStack.BoosterEvent ev, int startIdx, int endIdx)
	{
		int i = Math.Min(startIdx, endIdx);
		int num = Math.Max(startIdx, endIdx);
		while (i <= num)
		{
			this.PlayEvent(ev, i);
			i++;
		}
	}

	// Token: 0x06005EDB RID: 24283 RVA: 0x001ED614 File Offset: 0x001EB814
	protected void PlayEvent(BoosterStack.BoosterEvent ev, int atIndex)
	{
		if (atIndex >= this.m_boosters.Count)
		{
			Log.Store.PrintError("BoosterStack::PlayEvent index {0} out of range (max: {1})", new object[]
			{
				atIndex,
				this.m_boosters.Count - 1
			});
			return;
		}
		GameObject gameObject = this.m_boosters[atIndex];
		gameObject.transform.localPosition = this.m_incrementalDisplacement * (float)(atIndex - 1);
		Widget component = gameObject.GetComponent<Widget>();
		if (component != null && !component.IsReady)
		{
			gameObject.SetActive(true);
			return;
		}
		PlayMakerFSM componentInChildren = gameObject.GetComponentInChildren<PlayMakerFSM>();
		if (componentInChildren == null)
		{
			Log.Store.PrintError("No PlayMakerFSM found on booster {0} in BoosterStack {1}!", new object[]
			{
				gameObject,
				this
			});
			return;
		}
		switch (ev)
		{
		case BoosterStack.BoosterEvent.INTRO:
			componentInChildren.SendEvent(this.m_boosterIntroEvent);
			return;
		case BoosterStack.BoosterEvent.OUTRO:
			componentInChildren.SendEvent(this.m_boosterOutroEvent);
			return;
		case BoosterStack.BoosterEvent.INSTANT_INTRO:
			componentInChildren.SendEvent(this.m_boosterInstantIntroEvent);
			return;
		case BoosterStack.BoosterEvent.INSTANT_OUTRO:
			componentInChildren.SendEvent(this.m_boosterInstantOutroEvent);
			return;
		default:
			return;
		}
	}

	// Token: 0x06005EDC RID: 24284 RVA: 0x001ED722 File Offset: 0x001EB922
	protected void OnBoosterReady(int idx)
	{
		if (idx >= this.m_currentStackSize)
		{
			this.PlayEvent(BoosterStack.BoosterEvent.INSTANT_OUTRO, idx);
			return;
		}
		if (idx == this.m_currentStackSize - 1)
		{
			this.PlayEvent(BoosterStack.BoosterEvent.INTRO, idx);
			return;
		}
		this.PlayEvent(BoosterStack.BoosterEvent.INSTANT_INTRO, idx);
	}

	// Token: 0x04004FE6 RID: 20454
	[SerializeField]
	protected Vector3 m_incrementalDisplacement;

	// Token: 0x04004FE7 RID: 20455
	[SerializeField]
	protected int m_stackSize;

	// Token: 0x04004FE8 RID: 20456
	[SerializeField]
	protected GameObject m_boosterContainer;

	// Token: 0x04004FE9 RID: 20457
	[SerializeField]
	protected float m_stackingDelay;

	// Token: 0x04004FEA RID: 20458
	[SerializeField]
	protected float m_stackingBaseDuration = 0.1f;

	// Token: 0x04004FEB RID: 20459
	[SerializeField]
	protected float m_stackingIncrementalDuration = 0.02f;

	// Token: 0x04004FEC RID: 20460
	[SerializeField]
	protected string m_boosterIntroEvent;

	// Token: 0x04004FED RID: 20461
	[SerializeField]
	protected string m_boosterOutroEvent;

	// Token: 0x04004FEE RID: 20462
	[SerializeField]
	protected string m_boosterInstantIntroEvent;

	// Token: 0x04004FEF RID: 20463
	[SerializeField]
	protected string m_boosterInstantOutroEvent;

	// Token: 0x04004FF0 RID: 20464
	private float m_playTime;

	// Token: 0x04004FF1 RID: 20465
	private float m_startTime;

	// Token: 0x04004FF2 RID: 20466
	private float m_endTime;

	// Token: 0x04004FF3 RID: 20467
	private int m_startingStackSize;

	// Token: 0x04004FF4 RID: 20468
	private int m_currentStackSize;

	// Token: 0x04004FF5 RID: 20469
	private int m_targetStackSize;

	// Token: 0x04004FF6 RID: 20470
	private List<GameObject> m_boosters = new List<GameObject>();

	// Token: 0x04004FF7 RID: 20471
	private bool m_instantIntro;

	// Token: 0x020021CE RID: 8654
	protected enum BoosterEvent
	{
		// Token: 0x0400E15E RID: 57694
		INTRO,
		// Token: 0x0400E15F RID: 57695
		OUTRO,
		// Token: 0x0400E160 RID: 57696
		INSTANT_INTRO,
		// Token: 0x0400E161 RID: 57697
		INSTANT_OUTRO
	}
}
