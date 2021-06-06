using System;
using UnityEngine;

// Token: 0x02000A2A RID: 2602
public class Flipbook : MonoBehaviour
{
	// Token: 0x06008BEA RID: 35818 RVA: 0x002CC908 File Offset: 0x002CAB08
	private void Start()
	{
		if (this.m_RandomRateRange)
		{
			this.m_flipbookRate = UnityEngine.Random.Range(this.m_RandomRateMin, this.m_RandomRateMax);
		}
		if (this.m_flipbookRandom)
		{
			this.m_flipbookLastOffset = UnityEngine.Random.Range(0, this.m_flipbookOffsets.Length);
			this.SetIndex(this.m_flipbookLastOffset);
		}
	}

	// Token: 0x06008BEB RID: 35819 RVA: 0x002CC95C File Offset: 0x002CAB5C
	private void Update()
	{
		float num = this.m_flipbookRate;
		if (num == 0f)
		{
			return;
		}
		bool flag = false;
		if (num < 0f)
		{
			num *= -1f;
			flag = true;
		}
		if (this.m_animate)
		{
			if (this.m_flipbookFrame > num)
			{
				int num3;
				if (this.m_flipbookRandom)
				{
					int num2 = 0;
					do
					{
						num3 = UnityEngine.Random.Range(0, this.m_flipbookOffsets.Length);
						num2++;
					}
					while (num3 == this.m_flipbookLastOffset && num2 < 100);
					this.m_flipbookLastOffset = num3;
				}
				else
				{
					if (flag)
					{
						this.m_flipbookLastOffset -= Mathf.FloorToInt(this.m_flipbookFrame / num);
						if (this.m_flipbookLastOffset < 0)
						{
							this.m_flipbookLastOffset = Mathf.FloorToInt((float)(this.m_flipbookOffsets.Length - Mathf.Abs(this.m_flipbookLastOffset)));
							if (this.m_flipbookLastOffset < 0)
							{
								this.m_flipbookLastOffset = this.m_flipbookOffsets.Length - 1;
							}
						}
					}
					else if (!this.m_flipbookReverse)
					{
						if (this.m_reverse)
						{
							if (this.m_flipbookLastOffset >= this.m_flipbookOffsets.Length - 1)
							{
								this.m_flipbookLastOffset = this.m_flipbookOffsets.Length - 1;
								this.m_flipbookReverse = true;
							}
							else
							{
								this.m_flipbookLastOffset++;
							}
						}
						else
						{
							this.m_flipbookLastOffset += Mathf.FloorToInt(this.m_flipbookFrame / num);
							if (this.m_flipbookLastOffset >= this.m_flipbookOffsets.Length)
							{
								this.m_flipbookLastOffset = Mathf.FloorToInt((float)(this.m_flipbookLastOffset - this.m_flipbookOffsets.Length));
								if (this.m_flipbookLastOffset >= this.m_flipbookOffsets.Length)
								{
									this.m_flipbookLastOffset = 0;
								}
							}
						}
					}
					else if (this.m_flipbookLastOffset <= 0)
					{
						this.m_flipbookLastOffset = 1;
						this.m_flipbookReverse = false;
					}
					else
					{
						this.m_flipbookLastOffset -= Mathf.FloorToInt(this.m_flipbookFrame / num);
						if (this.m_flipbookLastOffset < 0)
						{
							this.m_flipbookLastOffset = Mathf.FloorToInt((float)(this.m_flipbookOffsets.Length - Mathf.Abs(this.m_flipbookLastOffset)));
						}
						if (this.m_flipbookLastOffset < 0)
						{
							this.m_flipbookLastOffset = this.m_flipbookOffsets.Length - 1;
						}
					}
					num3 = this.m_flipbookLastOffset;
				}
				this.m_flipbookFrame = 0f;
				this.SetIndex(num3);
			}
			this.m_flipbookFrame += Time.deltaTime * 60f;
		}
	}

	// Token: 0x06008BEC RID: 35820 RVA: 0x002CCBA4 File Offset: 0x002CADA4
	public void SetIndex(int i)
	{
		if (i >= 0 && i < this.m_flipbookOffsets.Length)
		{
			base.GetComponent<Renderer>().GetMaterial().SetTextureOffset("_MainTex", this.m_flipbookOffsets[i]);
			return;
		}
		if (i < 0)
		{
			this.m_flipbookLastOffset = 0;
			return;
		}
		this.m_flipbookLastOffset = this.m_flipbookOffsets.Length;
	}

	// Token: 0x040074CC RID: 29900
	public float m_flipbookRate = 15f;

	// Token: 0x040074CD RID: 29901
	public bool m_flipbookRandom;

	// Token: 0x040074CE RID: 29902
	public Vector2[] m_flipbookOffsets = new Vector2[]
	{
		new Vector2(0f, 0.5f),
		new Vector2(0.5f, 0.5f),
		new Vector2(0f, 0f),
		new Vector2(0.5f, 0f)
	};

	// Token: 0x040074CF RID: 29903
	public bool m_animate = true;

	// Token: 0x040074D0 RID: 29904
	public bool m_reverse = true;

	// Token: 0x040074D1 RID: 29905
	public bool m_RandomRateRange;

	// Token: 0x040074D2 RID: 29906
	public float m_RandomRateMin;

	// Token: 0x040074D3 RID: 29907
	public float m_RandomRateMax;

	// Token: 0x040074D4 RID: 29908
	private float m_flipbookFrame;

	// Token: 0x040074D5 RID: 29909
	private bool m_flipbookReverse;

	// Token: 0x040074D6 RID: 29910
	private int m_flipbookLastOffset;
}
