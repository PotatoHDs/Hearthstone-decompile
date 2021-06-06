using System;
using UnityEngine;

public class Carousel : MonoBehaviour
{
	public delegate void ItemPulled(CarouselItem item, int index);

	public delegate void ItemClicked(CarouselItem item, int index);

	public delegate void ItemReleased();

	public delegate void CarouselSettled();

	public delegate void CarouselMoved();

	public delegate void CarouselStartedScrolling();

	public GameObject[] m_bones;

	public GameObject m_centerBone;

	public Vector3 m_centerBoneOffset;

	public Collider m_collider;

	public bool m_useFlyIn;

	public bool m_trackItemHit;

	public bool m_noMouseMovement;

	public int m_maxPosition = 4;

	private int m_intPosition;

	private float m_position;

	private float m_targetPosition;

	private CarouselItem[] m_items;

	private int m_numItems;

	private int m_radius;

	private bool m_touchActive;

	private Vector2 m_touchStart;

	private float m_startX;

	private float m_touchX;

	private float m_velocity;

	private int m_hitIndex;

	private CarouselItem m_hitItem;

	private Vector3 m_hitWorldPosition;

	private float m_hitCarouselPosition;

	private float m_totalMove;

	private float m_moveAdjustment;

	private const float MIN_VELOCITY = 0.03f;

	private const float DRAG = 0.015f;

	private const float SCROLL_THRESHOLD = 10f;

	private int m_momentumCounter;

	private int m_momentumTotal;

	private float[] m_momentumHistory = new float[5];

	private bool m_doFlyIn;

	private float m_flyInState;

	private bool m_settleCallbackCalled;

	private bool m_scrolling;

	private ItemPulled m_itemPulledListener;

	private ItemClicked m_itemClickedListener;

	private ItemReleased m_itemReleasedListener;

	private CarouselSettled m_carouselSettledListener;

	private CarouselStartedScrolling m_carouselStartedScrollingListener;

	private ItemPulled m_itemCrossedCenterPositionListener;

	private void Awake()
	{
		m_numItems = m_bones.Length;
		m_radius = m_numItems / 2;
	}

	private void Start()
	{
		m_trackItemHit = true;
	}

	private void OnDestroy()
	{
		m_itemPulledListener = null;
		m_itemClickedListener = null;
		m_itemReleasedListener = null;
		m_carouselSettledListener = null;
		m_carouselStartedScrollingListener = null;
	}

	public void UpdateUI(bool mouseDown)
	{
		if (m_items == null)
		{
			return;
		}
		bool flag = m_position < 0f || m_position > (float)m_maxPosition;
		if (m_touchActive)
		{
			Vector3 mousePosition = InputCollection.GetMousePosition();
			float x = mousePosition.x;
			float num = x - m_touchX;
			if (!m_scrolling && Math.Abs(m_touchStart.x - x) >= 10f)
			{
				StartScrolling();
			}
			float num2 = num * 4.5f / (float)Screen.width;
			if (m_position < 0f)
			{
				num2 /= 1f + Math.Abs(m_position) * 5f;
			}
			if (!m_noMouseMovement)
			{
				if (m_trackItemHit)
				{
					UniversalInputManager.Get().GetInputPointOnPlane(m_hitWorldPosition, out var point);
					float carouselPosition = GetCarouselPosition(point.x);
					m_position = m_startX + m_hitCarouselPosition - carouselPosition;
				}
				else
				{
					m_position -= num2;
				}
			}
			m_momentumHistory[m_momentumCounter] = num2;
			m_momentumCounter++;
			m_momentumTotal++;
			flag = m_position < 0f || m_position > (float)m_maxPosition;
			if (m_momentumCounter >= m_momentumHistory.Length)
			{
				m_momentumCounter = 0;
			}
			if (m_momentumTotal >= m_momentumHistory.Length)
			{
				m_momentumTotal = m_momentumHistory.Length;
			}
			m_touchX = x;
			float num3 = (float)Screen.height * 0.1f;
			float num4 = (float)Screen.height * 0.275f;
			if (m_itemPulledListener != null && mousePosition.y - m_touchStart.y > num3 && mousePosition.y > num4)
			{
				m_itemPulledListener(m_hitItem, m_hitIndex);
				m_touchActive = false;
				m_velocity = 0f;
				SettlePosition();
			}
			if (!InputCollection.GetMouseButton(0))
			{
				if (!m_noMouseMovement && m_scrolling)
				{
					m_velocity = CalculateVelocity();
					if (m_position < 0f)
					{
						m_targetPosition = 0f;
						m_velocity = 0f;
					}
					else if (m_position >= (float)m_maxPosition)
					{
						m_targetPosition = m_maxPosition;
						m_velocity = 0f;
					}
					else if (Math.Abs(m_velocity) < 0.03f)
					{
						SettlePosition(m_velocity);
						m_velocity = 0f;
					}
				}
				if (m_itemReleasedListener != null)
				{
					m_itemReleasedListener();
				}
				m_touchActive = false;
			}
		}
		CarouselItem itemHit;
		int num5 = MouseHit(out itemHit);
		if (mouseDown && num5 >= 0)
		{
			m_touchActive = true;
			m_touchStart = InputCollection.GetMousePosition();
			m_touchX = m_touchStart.x;
			m_velocity = 0f;
			m_hitIndex = num5;
			m_hitItem = itemHit;
			m_scrolling = false;
			m_settleCallbackCalled = false;
			if (m_trackItemHit)
			{
				UniversalInputManager.Get().GetInputHitInfo(out var hitInfo);
				m_hitWorldPosition = hitInfo.point;
				m_hitCarouselPosition = GetCarouselPosition(m_hitWorldPosition.x);
				m_startX = m_position;
			}
			InitVelocity();
			if (m_itemClickedListener != null)
			{
				m_itemClickedListener(m_hitItem, num5);
			}
		}
		if (!m_touchActive && m_velocity != 0f)
		{
			if (Math.Abs(m_velocity) < 0.03f || flag)
			{
				SettlePosition(m_velocity);
				m_velocity = 0f;
			}
			else
			{
				m_position += m_velocity;
				m_velocity -= 0.015f * (float)Math.Sign(m_velocity);
			}
		}
		if (!m_touchActive && m_targetPosition != m_position && m_velocity == 0f)
		{
			m_position = m_targetPosition * 0.15f + m_position * 0.85f;
			if (!m_settleCallbackCalled && Math.Abs(m_position - m_targetPosition) < 0.1f)
			{
				m_settleCallbackCalled = true;
				if (m_carouselSettledListener != null)
				{
					m_carouselSettledListener();
				}
			}
			if (Math.Abs(m_position - m_targetPosition) < 0.01f)
			{
				m_position = m_targetPosition;
				m_scrolling = false;
			}
		}
		m_intPosition = (int)Mathf.Round(m_position);
		UpdateVisibleItems();
		if (m_doFlyIn)
		{
			float num6 = Math.Min(0.03f, Time.deltaTime);
			m_flyInState += num6 * 8f;
			if (m_flyInState > (float)m_bones.Length)
			{
				m_doFlyIn = false;
			}
		}
	}

	public bool MouseOver()
	{
		CarouselItem itemHit;
		return MouseHit(out itemHit) >= 0;
	}

	private float GetCarouselPosition(float x)
	{
		if (x < m_bones[0].transform.position.x)
		{
			return 0f;
		}
		if (x > m_bones[m_bones.Length - 1].transform.position.x)
		{
			return (float)m_bones.Length - 1f;
		}
		float num = m_bones[0].transform.position.x;
		for (int i = 1; i < m_bones.Length; i++)
		{
			float x2 = m_bones[i].transform.position.x;
			if (x >= num && x <= x2)
			{
				return (float)i + (x - num) / (x2 - num);
			}
			num = x2;
		}
		return 0f;
	}

	private int MouseHit(out CarouselItem itemHit)
	{
		int result = -1;
		itemHit = null;
		if (m_items == null || m_items.Length == 0)
		{
			return -1;
		}
		for (int i = 0; i < m_items.Length; i++)
		{
			CarouselItem carouselItem = m_items[i];
			if (carouselItem.GetGameObject() != null && UniversalInputManager.Get().InputIsOver(carouselItem.GetGameObject(), out var _))
			{
				itemHit = carouselItem;
				result = i;
				break;
			}
		}
		return result;
	}

	public void SetListeners(ItemPulled pulled, ItemClicked clicked, ItemReleased released, CarouselSettled settled = null, CarouselStartedScrolling scrolling = null, ItemPulled crossedCenterPosition = null)
	{
		m_itemPulledListener = pulled;
		m_itemClickedListener = clicked;
		m_itemReleasedListener = released;
		m_carouselSettledListener = settled;
		m_carouselStartedScrollingListener = scrolling;
		m_itemCrossedCenterPositionListener = crossedCenterPosition;
	}

	private void InitVelocity()
	{
		for (int i = 0; i < m_momentumHistory.Length; i++)
		{
			m_momentumHistory[i] = 0f;
		}
		m_momentumCounter = 0;
		m_momentumTotal = 0;
	}

	private float CalculateVelocity()
	{
		float num = 0f;
		for (int i = 0; i < m_momentumTotal; i++)
		{
			num += m_momentumHistory[i];
		}
		return -0.9f * num / (float)m_momentumTotal;
	}

	private float DistanceFromSettle()
	{
		float num = m_position - (float)(int)m_position;
		if (num > 0.5f)
		{
			return 1f - num;
		}
		return num;
	}

	private void SettlePosition(float bias = 0f)
	{
		float val = ((bias > 0.001f) ? Mathf.Round(m_position + 0.5f) : ((!(bias < -0.001f)) ? Mathf.Round(m_position) : Mathf.Round(m_position - 0.5f)));
		val = Math.Max(0f, val);
		val = (m_targetPosition = Math.Min(m_maxPosition, val));
	}

	private void UpdateVisibleItems()
	{
		float position = m_position;
		int num = Mathf.FloorToInt(position);
		float num2 = position - (float)num;
		float num3 = 1f - num2;
		int num4 = 0;
		for (int i = 0; i < m_items.Length; i++)
		{
			int num5 = i - num + m_radius - 1;
			int num6 = i - num + m_radius;
			if (num5 < 0 || num6 >= m_bones.Length)
			{
				m_items[i].Hide();
				continue;
			}
			m_items[i].Show(this);
			if (m_items[i].IsLoaded())
			{
				Vector3 localPosition = m_bones[num5].transform.localPosition;
				Vector3 localPosition2 = m_bones[num6].transform.localPosition;
				Vector3 localScale = m_bones[num5].transform.localScale;
				Vector3 localScale2 = m_bones[num6].transform.localScale;
				Quaternion localRotation = m_bones[num5].transform.localRotation;
				Quaternion localRotation2 = m_bones[num6].transform.localRotation;
				Vector3 localPosition3 = new Vector3(localPosition.x * num2 + localPosition2.x * num3, localPosition.y * num2 + localPosition2.y * num3, localPosition.z * num2 + localPosition2.z * num3);
				Vector3 localScale3 = new Vector3(localScale.x * num2 + localScale2.x * num3, localScale.y * num2 + localScale2.y * num3, localScale.z * num2 + localScale2.z * num3);
				Quaternion localRotation3 = new Quaternion(localRotation.x * num2 + localRotation2.x * num3, localRotation.y * num2 + localRotation2.y * num3, localRotation.z * num2 + localRotation2.z * num3, localRotation.w * num2 + localRotation2.w * num3);
				if (m_doFlyIn)
				{
					float num7 = 1f;
					if (num4 >= (int)m_flyInState + 1)
					{
						num7 = 0f;
					}
					else if (num4 >= (int)m_flyInState)
					{
						num7 = m_flyInState - (float)Math.Floor(m_flyInState);
					}
					float num8 = 1f - num7;
					Vector3 vector = new Vector3(81f, 9.4f, 4f);
					m_items[i].GetGameObject().transform.localPosition = new Vector3(num7 * localPosition3.x + num8 * vector.x, num7 * localPosition3.y + num8 * vector.y, num7 * localPosition3.z + num8 * vector.z);
				}
				else
				{
					Vector3 localPosition4 = m_items[i].GetGameObject().transform.localPosition;
					m_items[i].GetGameObject().transform.localPosition = localPosition3;
					if (m_centerBone != null && m_itemCrossedCenterPositionListener != null)
					{
						bool flag = false;
						Vector3 vector2 = m_centerBone.transform.localPosition + m_centerBoneOffset;
						if ((localPosition3.x != vector2.x) ? ((localPosition4.x <= vector2.x && localPosition3.x >= vector2.x) || (localPosition4.x >= vector2.x && localPosition3.x <= vector2.x)) : (localPosition4.x != localPosition3.x))
						{
							m_itemCrossedCenterPositionListener(m_items[i], i);
						}
					}
				}
				m_items[i].GetGameObject().transform.localScale = localScale3;
				m_items[i].GetGameObject().transform.localRotation = localRotation3;
			}
			num4++;
		}
	}

	public void Initialize(CarouselItem[] items, int position = 0)
	{
		if (m_items != null)
		{
			ClearItems();
		}
		m_items = items;
		m_position = (m_targetPosition = position);
		m_intPosition = position;
		DoFlyIn();
	}

	public void ClearItems()
	{
		if (m_items != null)
		{
			CarouselItem[] items = m_items;
			for (int i = 0; i < items.Length; i++)
			{
				items[i].Clear();
			}
		}
	}

	public bool AreVisibleItemsLoaded()
	{
		for (int i = 0; i < m_numItems; i++)
		{
			int num = m_intPosition + i - m_radius;
			if (num >= 0 && !m_items[num].IsLoaded())
			{
				Debug.Log("Not loaded " + num);
				return false;
			}
		}
		return true;
	}

	public int GetCurrentIndex()
	{
		return m_intPosition;
	}

	public int GetTargetPosition()
	{
		return (int)m_targetPosition;
	}

	public CarouselItem GetCurrentItem()
	{
		if (m_items == null)
		{
			return null;
		}
		int num = Mathf.Clamp(m_intPosition, 0, m_items.Length - 1);
		return m_items[num];
	}

	public void SetPosition(int n, bool animate = false)
	{
		m_targetPosition = n;
		if (!animate)
		{
			m_position = m_targetPosition;
		}
		else
		{
			StartScrolling();
			m_settleCallbackCalled = false;
		}
		DoFlyIn();
	}

	public bool IsScrolling()
	{
		return m_scrolling;
	}

	private void DoFlyIn()
	{
		if (m_useFlyIn)
		{
			m_doFlyIn = true;
			m_flyInState = 0f;
		}
	}

	private void StartScrolling()
	{
		m_scrolling = true;
		if (m_carouselStartedScrollingListener != null)
		{
			m_carouselStartedScrollingListener();
		}
	}
}
