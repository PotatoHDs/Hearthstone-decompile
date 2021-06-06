using System;
using UnityEngine;

// Token: 0x02000B01 RID: 2817
public class Carousel : MonoBehaviour
{
	// Token: 0x060095E8 RID: 38376 RVA: 0x003088F5 File Offset: 0x00306AF5
	private void Awake()
	{
		this.m_numItems = this.m_bones.Length;
		this.m_radius = this.m_numItems / 2;
	}

	// Token: 0x060095E9 RID: 38377 RVA: 0x00308913 File Offset: 0x00306B13
	private void Start()
	{
		this.m_trackItemHit = true;
	}

	// Token: 0x060095EA RID: 38378 RVA: 0x0030891C File Offset: 0x00306B1C
	private void OnDestroy()
	{
		this.m_itemPulledListener = null;
		this.m_itemClickedListener = null;
		this.m_itemReleasedListener = null;
		this.m_carouselSettledListener = null;
		this.m_carouselStartedScrollingListener = null;
	}

	// Token: 0x060095EB RID: 38379 RVA: 0x00308944 File Offset: 0x00306B44
	public void UpdateUI(bool mouseDown)
	{
		if (this.m_items == null)
		{
			return;
		}
		bool flag = this.m_position < 0f || this.m_position > (float)this.m_maxPosition;
		if (this.m_touchActive)
		{
			Vector3 mousePosition = InputCollection.GetMousePosition();
			float x = mousePosition.x;
			float num = x - this.m_touchX;
			if (!this.m_scrolling && Math.Abs(this.m_touchStart.x - x) >= 10f)
			{
				this.StartScrolling();
			}
			float num2 = num * 4.5f / (float)Screen.width;
			if (this.m_position < 0f)
			{
				num2 /= 1f + Math.Abs(this.m_position) * 5f;
			}
			if (!this.m_noMouseMovement)
			{
				if (this.m_trackItemHit)
				{
					Vector3 vector;
					UniversalInputManager.Get().GetInputPointOnPlane(this.m_hitWorldPosition, out vector);
					float carouselPosition = this.GetCarouselPosition(vector.x);
					this.m_position = this.m_startX + this.m_hitCarouselPosition - carouselPosition;
				}
				else
				{
					this.m_position -= num2;
				}
			}
			this.m_momentumHistory[this.m_momentumCounter] = num2;
			this.m_momentumCounter++;
			this.m_momentumTotal++;
			flag = (this.m_position < 0f || this.m_position > (float)this.m_maxPosition);
			if (this.m_momentumCounter >= this.m_momentumHistory.Length)
			{
				this.m_momentumCounter = 0;
			}
			if (this.m_momentumTotal >= this.m_momentumHistory.Length)
			{
				this.m_momentumTotal = this.m_momentumHistory.Length;
			}
			this.m_touchX = x;
			float num3 = (float)Screen.height * 0.1f;
			float num4 = (float)Screen.height * 0.275f;
			if (this.m_itemPulledListener != null && mousePosition.y - this.m_touchStart.y > num3 && mousePosition.y > num4)
			{
				this.m_itemPulledListener(this.m_hitItem, this.m_hitIndex);
				this.m_touchActive = false;
				this.m_velocity = 0f;
				this.SettlePosition(0f);
			}
			if (!InputCollection.GetMouseButton(0))
			{
				if (!this.m_noMouseMovement && this.m_scrolling)
				{
					this.m_velocity = this.CalculateVelocity();
					if (this.m_position < 0f)
					{
						this.m_targetPosition = 0f;
						this.m_velocity = 0f;
					}
					else if (this.m_position >= (float)this.m_maxPosition)
					{
						this.m_targetPosition = (float)this.m_maxPosition;
						this.m_velocity = 0f;
					}
					else if (Math.Abs(this.m_velocity) < 0.03f)
					{
						this.SettlePosition(this.m_velocity);
						this.m_velocity = 0f;
					}
				}
				if (this.m_itemReleasedListener != null)
				{
					this.m_itemReleasedListener();
				}
				this.m_touchActive = false;
			}
		}
		CarouselItem hitItem;
		int num5 = this.MouseHit(out hitItem);
		if (mouseDown && num5 >= 0)
		{
			this.m_touchActive = true;
			this.m_touchStart = InputCollection.GetMousePosition();
			this.m_touchX = this.m_touchStart.x;
			this.m_velocity = 0f;
			this.m_hitIndex = num5;
			this.m_hitItem = hitItem;
			this.m_scrolling = false;
			this.m_settleCallbackCalled = false;
			if (this.m_trackItemHit)
			{
				RaycastHit raycastHit;
				UniversalInputManager.Get().GetInputHitInfo(out raycastHit);
				this.m_hitWorldPosition = raycastHit.point;
				this.m_hitCarouselPosition = this.GetCarouselPosition(this.m_hitWorldPosition.x);
				this.m_startX = this.m_position;
			}
			this.InitVelocity();
			if (this.m_itemClickedListener != null)
			{
				this.m_itemClickedListener(this.m_hitItem, num5);
			}
		}
		if (!this.m_touchActive && this.m_velocity != 0f)
		{
			if (Math.Abs(this.m_velocity) < 0.03f || flag)
			{
				this.SettlePosition(this.m_velocity);
				this.m_velocity = 0f;
			}
			else
			{
				this.m_position += this.m_velocity;
				this.m_velocity -= 0.015f * (float)Math.Sign(this.m_velocity);
			}
		}
		if (!this.m_touchActive && this.m_targetPosition != this.m_position && this.m_velocity == 0f)
		{
			this.m_position = this.m_targetPosition * 0.15f + this.m_position * 0.85f;
			if (!this.m_settleCallbackCalled && Math.Abs(this.m_position - this.m_targetPosition) < 0.1f)
			{
				this.m_settleCallbackCalled = true;
				if (this.m_carouselSettledListener != null)
				{
					this.m_carouselSettledListener();
				}
			}
			if (Math.Abs(this.m_position - this.m_targetPosition) < 0.01f)
			{
				this.m_position = this.m_targetPosition;
				this.m_scrolling = false;
			}
		}
		this.m_intPosition = (int)Mathf.Round(this.m_position);
		this.UpdateVisibleItems();
		if (this.m_doFlyIn)
		{
			float num6 = Math.Min(0.03f, Time.deltaTime);
			this.m_flyInState += num6 * 8f;
			if (this.m_flyInState > (float)this.m_bones.Length)
			{
				this.m_doFlyIn = false;
			}
		}
	}

	// Token: 0x060095EC RID: 38380 RVA: 0x00308E64 File Offset: 0x00307064
	public bool MouseOver()
	{
		CarouselItem carouselItem;
		return this.MouseHit(out carouselItem) >= 0;
	}

	// Token: 0x060095ED RID: 38381 RVA: 0x00308E80 File Offset: 0x00307080
	private float GetCarouselPosition(float x)
	{
		if (x < this.m_bones[0].transform.position.x)
		{
			return 0f;
		}
		if (x > this.m_bones[this.m_bones.Length - 1].transform.position.x)
		{
			return (float)this.m_bones.Length - 1f;
		}
		float num = this.m_bones[0].transform.position.x;
		for (int i = 1; i < this.m_bones.Length; i++)
		{
			float x2 = this.m_bones[i].transform.position.x;
			if (x >= num && x <= x2)
			{
				return (float)i + (x - num) / (x2 - num);
			}
			num = x2;
		}
		return 0f;
	}

	// Token: 0x060095EE RID: 38382 RVA: 0x00308F40 File Offset: 0x00307140
	private int MouseHit(out CarouselItem itemHit)
	{
		int result = -1;
		itemHit = null;
		if (this.m_items == null || this.m_items.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < this.m_items.Length; i++)
		{
			CarouselItem carouselItem = this.m_items[i];
			RaycastHit raycastHit;
			if (carouselItem.GetGameObject() != null && UniversalInputManager.Get().InputIsOver(carouselItem.GetGameObject(), out raycastHit))
			{
				itemHit = carouselItem;
				result = i;
				break;
			}
		}
		return result;
	}

	// Token: 0x060095EF RID: 38383 RVA: 0x00308FAB File Offset: 0x003071AB
	public void SetListeners(Carousel.ItemPulled pulled, Carousel.ItemClicked clicked, Carousel.ItemReleased released, Carousel.CarouselSettled settled = null, Carousel.CarouselStartedScrolling scrolling = null, Carousel.ItemPulled crossedCenterPosition = null)
	{
		this.m_itemPulledListener = pulled;
		this.m_itemClickedListener = clicked;
		this.m_itemReleasedListener = released;
		this.m_carouselSettledListener = settled;
		this.m_carouselStartedScrollingListener = scrolling;
		this.m_itemCrossedCenterPositionListener = crossedCenterPosition;
	}

	// Token: 0x060095F0 RID: 38384 RVA: 0x00308FDC File Offset: 0x003071DC
	private void InitVelocity()
	{
		for (int i = 0; i < this.m_momentumHistory.Length; i++)
		{
			this.m_momentumHistory[i] = 0f;
		}
		this.m_momentumCounter = 0;
		this.m_momentumTotal = 0;
	}

	// Token: 0x060095F1 RID: 38385 RVA: 0x00309018 File Offset: 0x00307218
	private float CalculateVelocity()
	{
		float num = 0f;
		for (int i = 0; i < this.m_momentumTotal; i++)
		{
			num += this.m_momentumHistory[i];
		}
		return -0.9f * num / (float)this.m_momentumTotal;
	}

	// Token: 0x060095F2 RID: 38386 RVA: 0x00309058 File Offset: 0x00307258
	private float DistanceFromSettle()
	{
		float num = this.m_position - (float)((int)this.m_position);
		if (num > 0.5f)
		{
			return 1f - num;
		}
		return num;
	}

	// Token: 0x060095F3 RID: 38387 RVA: 0x00309088 File Offset: 0x00307288
	private void SettlePosition(float bias = 0f)
	{
		float num;
		if (bias > 0.001f)
		{
			num = Mathf.Round(this.m_position + 0.5f);
		}
		else if (bias < -0.001f)
		{
			num = Mathf.Round(this.m_position - 0.5f);
		}
		else
		{
			num = Mathf.Round(this.m_position);
		}
		num = Math.Max(0f, num);
		num = Math.Min((float)this.m_maxPosition, num);
		this.m_targetPosition = num;
	}

	// Token: 0x060095F4 RID: 38388 RVA: 0x003090FC File Offset: 0x003072FC
	private void UpdateVisibleItems()
	{
		float position = this.m_position;
		int num = Mathf.FloorToInt(position);
		float num2 = position - (float)num;
		float num3 = 1f - num2;
		int num4 = 0;
		for (int i = 0; i < this.m_items.Length; i++)
		{
			int num5 = i - num + this.m_radius - 1;
			int num6 = i - num + this.m_radius;
			if (num5 < 0 || num6 >= this.m_bones.Length)
			{
				this.m_items[i].Hide();
			}
			else
			{
				this.m_items[i].Show(this);
				if (this.m_items[i].IsLoaded())
				{
					Vector3 localPosition = this.m_bones[num5].transform.localPosition;
					Vector3 localPosition2 = this.m_bones[num6].transform.localPosition;
					Vector3 localScale = this.m_bones[num5].transform.localScale;
					Vector3 localScale2 = this.m_bones[num6].transform.localScale;
					Quaternion localRotation = this.m_bones[num5].transform.localRotation;
					Quaternion localRotation2 = this.m_bones[num6].transform.localRotation;
					Vector3 vector = new Vector3(localPosition.x * num2 + localPosition2.x * num3, localPosition.y * num2 + localPosition2.y * num3, localPosition.z * num2 + localPosition2.z * num3);
					Vector3 localScale3 = new Vector3(localScale.x * num2 + localScale2.x * num3, localScale.y * num2 + localScale2.y * num3, localScale.z * num2 + localScale2.z * num3);
					Quaternion localRotation3 = new Quaternion(localRotation.x * num2 + localRotation2.x * num3, localRotation.y * num2 + localRotation2.y * num3, localRotation.z * num2 + localRotation2.z * num3, localRotation.w * num2 + localRotation2.w * num3);
					if (this.m_doFlyIn)
					{
						float num7 = 1f;
						if (num4 >= (int)this.m_flyInState + 1)
						{
							num7 = 0f;
						}
						else if (num4 >= (int)this.m_flyInState)
						{
							num7 = this.m_flyInState - (float)Math.Floor((double)this.m_flyInState);
						}
						float num8 = 1f - num7;
						Vector3 vector2 = new Vector3(81f, 9.4f, 4f);
						this.m_items[i].GetGameObject().transform.localPosition = new Vector3(num7 * vector.x + num8 * vector2.x, num7 * vector.y + num8 * vector2.y, num7 * vector.z + num8 * vector2.z);
					}
					else
					{
						Vector3 localPosition3 = this.m_items[i].GetGameObject().transform.localPosition;
						this.m_items[i].GetGameObject().transform.localPosition = vector;
						if (this.m_centerBone != null && this.m_itemCrossedCenterPositionListener != null)
						{
							Vector3 vector3 = this.m_centerBone.transform.localPosition + this.m_centerBoneOffset;
							bool flag;
							if (vector.x == vector3.x)
							{
								flag = (localPosition3.x != vector.x);
							}
							else
							{
								flag = ((localPosition3.x <= vector3.x && vector.x >= vector3.x) || (localPosition3.x >= vector3.x && vector.x <= vector3.x));
							}
							if (flag)
							{
								this.m_itemCrossedCenterPositionListener(this.m_items[i], i);
							}
						}
					}
					this.m_items[i].GetGameObject().transform.localScale = localScale3;
					this.m_items[i].GetGameObject().transform.localRotation = localRotation3;
				}
				num4++;
			}
		}
	}

	// Token: 0x060095F5 RID: 38389 RVA: 0x003094F0 File Offset: 0x003076F0
	public void Initialize(CarouselItem[] items, int position = 0)
	{
		if (this.m_items != null)
		{
			this.ClearItems();
		}
		this.m_items = items;
		this.m_position = (this.m_targetPosition = (float)position);
		this.m_intPosition = position;
		this.DoFlyIn();
	}

	// Token: 0x060095F6 RID: 38390 RVA: 0x00309530 File Offset: 0x00307730
	public void ClearItems()
	{
		if (this.m_items == null)
		{
			return;
		}
		CarouselItem[] items = this.m_items;
		for (int i = 0; i < items.Length; i++)
		{
			items[i].Clear();
		}
	}

	// Token: 0x060095F7 RID: 38391 RVA: 0x00309564 File Offset: 0x00307764
	public bool AreVisibleItemsLoaded()
	{
		for (int i = 0; i < this.m_numItems; i++)
		{
			int num = this.m_intPosition + i - this.m_radius;
			if (num >= 0 && !this.m_items[num].IsLoaded())
			{
				Debug.Log("Not loaded " + num);
				return false;
			}
		}
		return true;
	}

	// Token: 0x060095F8 RID: 38392 RVA: 0x003095BD File Offset: 0x003077BD
	public int GetCurrentIndex()
	{
		return this.m_intPosition;
	}

	// Token: 0x060095F9 RID: 38393 RVA: 0x003095C5 File Offset: 0x003077C5
	public int GetTargetPosition()
	{
		return (int)this.m_targetPosition;
	}

	// Token: 0x060095FA RID: 38394 RVA: 0x003095D0 File Offset: 0x003077D0
	public CarouselItem GetCurrentItem()
	{
		if (this.m_items == null)
		{
			return null;
		}
		int num = Mathf.Clamp(this.m_intPosition, 0, this.m_items.Length - 1);
		return this.m_items[num];
	}

	// Token: 0x060095FB RID: 38395 RVA: 0x00309606 File Offset: 0x00307806
	public void SetPosition(int n, bool animate = false)
	{
		this.m_targetPosition = (float)n;
		if (!animate)
		{
			this.m_position = this.m_targetPosition;
		}
		else
		{
			this.StartScrolling();
			this.m_settleCallbackCalled = false;
		}
		this.DoFlyIn();
	}

	// Token: 0x060095FC RID: 38396 RVA: 0x00309634 File Offset: 0x00307834
	public bool IsScrolling()
	{
		return this.m_scrolling;
	}

	// Token: 0x060095FD RID: 38397 RVA: 0x0030963C File Offset: 0x0030783C
	private void DoFlyIn()
	{
		if (this.m_useFlyIn)
		{
			this.m_doFlyIn = true;
			this.m_flyInState = 0f;
		}
	}

	// Token: 0x060095FE RID: 38398 RVA: 0x00309658 File Offset: 0x00307858
	private void StartScrolling()
	{
		this.m_scrolling = true;
		if (this.m_carouselStartedScrollingListener != null)
		{
			this.m_carouselStartedScrollingListener();
		}
	}

	// Token: 0x04007D9F RID: 32159
	public GameObject[] m_bones;

	// Token: 0x04007DA0 RID: 32160
	public GameObject m_centerBone;

	// Token: 0x04007DA1 RID: 32161
	public Vector3 m_centerBoneOffset;

	// Token: 0x04007DA2 RID: 32162
	public Collider m_collider;

	// Token: 0x04007DA3 RID: 32163
	public bool m_useFlyIn;

	// Token: 0x04007DA4 RID: 32164
	public bool m_trackItemHit;

	// Token: 0x04007DA5 RID: 32165
	public bool m_noMouseMovement;

	// Token: 0x04007DA6 RID: 32166
	public int m_maxPosition = 4;

	// Token: 0x04007DA7 RID: 32167
	private int m_intPosition;

	// Token: 0x04007DA8 RID: 32168
	private float m_position;

	// Token: 0x04007DA9 RID: 32169
	private float m_targetPosition;

	// Token: 0x04007DAA RID: 32170
	private CarouselItem[] m_items;

	// Token: 0x04007DAB RID: 32171
	private int m_numItems;

	// Token: 0x04007DAC RID: 32172
	private int m_radius;

	// Token: 0x04007DAD RID: 32173
	private bool m_touchActive;

	// Token: 0x04007DAE RID: 32174
	private Vector2 m_touchStart;

	// Token: 0x04007DAF RID: 32175
	private float m_startX;

	// Token: 0x04007DB0 RID: 32176
	private float m_touchX;

	// Token: 0x04007DB1 RID: 32177
	private float m_velocity;

	// Token: 0x04007DB2 RID: 32178
	private int m_hitIndex;

	// Token: 0x04007DB3 RID: 32179
	private CarouselItem m_hitItem;

	// Token: 0x04007DB4 RID: 32180
	private Vector3 m_hitWorldPosition;

	// Token: 0x04007DB5 RID: 32181
	private float m_hitCarouselPosition;

	// Token: 0x04007DB6 RID: 32182
	private float m_totalMove;

	// Token: 0x04007DB7 RID: 32183
	private float m_moveAdjustment;

	// Token: 0x04007DB8 RID: 32184
	private const float MIN_VELOCITY = 0.03f;

	// Token: 0x04007DB9 RID: 32185
	private const float DRAG = 0.015f;

	// Token: 0x04007DBA RID: 32186
	private const float SCROLL_THRESHOLD = 10f;

	// Token: 0x04007DBB RID: 32187
	private int m_momentumCounter;

	// Token: 0x04007DBC RID: 32188
	private int m_momentumTotal;

	// Token: 0x04007DBD RID: 32189
	private float[] m_momentumHistory = new float[5];

	// Token: 0x04007DBE RID: 32190
	private bool m_doFlyIn;

	// Token: 0x04007DBF RID: 32191
	private float m_flyInState;

	// Token: 0x04007DC0 RID: 32192
	private bool m_settleCallbackCalled;

	// Token: 0x04007DC1 RID: 32193
	private bool m_scrolling;

	// Token: 0x04007DC2 RID: 32194
	private Carousel.ItemPulled m_itemPulledListener;

	// Token: 0x04007DC3 RID: 32195
	private Carousel.ItemClicked m_itemClickedListener;

	// Token: 0x04007DC4 RID: 32196
	private Carousel.ItemReleased m_itemReleasedListener;

	// Token: 0x04007DC5 RID: 32197
	private Carousel.CarouselSettled m_carouselSettledListener;

	// Token: 0x04007DC6 RID: 32198
	private Carousel.CarouselStartedScrolling m_carouselStartedScrollingListener;

	// Token: 0x04007DC7 RID: 32199
	private Carousel.ItemPulled m_itemCrossedCenterPositionListener;

	// Token: 0x02002746 RID: 10054
	// (Invoke) Token: 0x06013960 RID: 80224
	public delegate void ItemPulled(CarouselItem item, int index);

	// Token: 0x02002747 RID: 10055
	// (Invoke) Token: 0x06013964 RID: 80228
	public delegate void ItemClicked(CarouselItem item, int index);

	// Token: 0x02002748 RID: 10056
	// (Invoke) Token: 0x06013968 RID: 80232
	public delegate void ItemReleased();

	// Token: 0x02002749 RID: 10057
	// (Invoke) Token: 0x0601396C RID: 80236
	public delegate void CarouselSettled();

	// Token: 0x0200274A RID: 10058
	// (Invoke) Token: 0x06013970 RID: 80240
	public delegate void CarouselMoved();

	// Token: 0x0200274B RID: 10059
	// (Invoke) Token: 0x06013974 RID: 80244
	public delegate void CarouselStartedScrolling();
}
