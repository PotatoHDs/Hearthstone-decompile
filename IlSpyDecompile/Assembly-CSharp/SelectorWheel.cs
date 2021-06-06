using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class SelectorWheel : MonoBehaviour
{
	private struct Tile
	{
		public GameObject gameObject;

		public int indexOffset;
	}

	[SerializeField]
	protected PegUIElement m_dragRegion;

	[SerializeField]
	protected PegUIElement m_cruiseUpRegion;

	[SerializeField]
	protected PegUIElement m_cruiseDownRegion;

	[SerializeField]
	protected GameObject m_tileBase;

	[SerializeField]
	protected float m_radius = 2f;

	[SerializeField]
	protected float m_flyBraking = 4f;

	[SerializeField]
	protected float m_flyDamping = 1f;

	[SerializeField]
	protected float m_snapDamping = 8f;

	[SerializeField]
	protected float m_snapForce = 40f;

	[SerializeField]
	protected float m_tileSpacing = 1f;

	[SerializeField]
	protected int m_maxTiles = 7;

	[SerializeField]
	protected bool m_globalScrolling = true;

	[SerializeField]
	protected bool m_invertScrolling = true;

	[SerializeField]
	protected float m_scrollingSpeed = 15f;

	[SerializeField]
	protected int m_numTiles = 1;

	[SerializeField]
	protected bool m_topToBottom = true;

	[SerializeField]
	protected float m_cruiseStartSpeed = 4f;

	[SerializeField]
	protected float m_cruiseEndSpeed = 24f;

	[SerializeField]
	protected float m_exponentialCruising = 0.5f;

	private Tile[] m_tiles;

	private float m_velocity;

	private float m_snapInfluence = 1f;

	private float m_wheelPosition;

	private int m_lastSelection;

	private Vector3? m_lastDragPos;

	private float? m_scrollTargetPosition;

	private int m_cruiseDir;

	private List<IDataModel> m_dataModels = new List<IDataModel>();

	public int TileCount
	{
		get
		{
			return m_numTiles;
		}
		protected set
		{
			m_numTiles = value;
			if (m_wheelPosition >= (float)m_numTiles)
			{
				SetIndex((m_numTiles > 0) ? (m_numTiles - 1) : 0);
			}
			LayoutTiles();
		}
	}

	public event Action OnSelectionChanged;

	private void Start()
	{
		m_tiles = new Tile[m_maxTiles];
		for (int i = 0; i < m_maxTiles; i++)
		{
			m_tiles[i] = default(Tile);
			m_tiles[i].indexOffset = i;
			if (i == 0)
			{
				m_tiles[i].gameObject = m_tileBase;
			}
			else
			{
				m_tiles[i].gameObject = UnityEngine.Object.Instantiate(m_tileBase, m_tileBase.transform.parent);
			}
		}
		if (m_dragRegion != null)
		{
			m_dragRegion.AddEventListener(UIEventType.PRESS, delegate
			{
				m_lastDragPos = GetLocalMousePos();
				PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
			});
		}
		InitializeCruiseClicker(m_cruiseUpRegion, 1);
		InitializeCruiseClicker(m_cruiseDownRegion, -1);
		SetIndex(0);
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		UpdateInput(deltaTime);
		float wheelPosition = m_wheelPosition;
		SimulateWheel(deltaTime);
		if (!(Mathf.Abs(m_wheelPosition - wheelPosition) < 0.001f))
		{
			UpdateSelection();
			LayoutTiles();
		}
	}

	public void SetIndex(int index, bool instant = true)
	{
		float positionFromIndex = GetPositionFromIndex(index);
		if (instant)
		{
			m_wheelPosition = positionFromIndex;
			m_velocity = 0f;
			LayoutTiles();
		}
		else
		{
			m_scrollTargetPosition = positionFromIndex;
		}
	}

	public int GetSelectedIndex()
	{
		return GetIndexFromPosition(Mathf.RoundToInt(m_wheelPosition));
	}

	public void SetDataModels(List<IDataModel> dataModels)
	{
		m_dataModels = dataModels;
		TileCount = m_dataModels.Count;
	}

	public IDataModel GetDataModel(int index)
	{
		if (index < 0 || index >= m_dataModels.Count)
		{
			return null;
		}
		return m_dataModels[index];
	}

	private void UpdateInput(float deltaTime)
	{
		Camera camera = GetCamera();
		bool flag = false;
		if (m_globalScrolling)
		{
			flag = true;
		}
		else if (UniversalInputManager.Get() != null && camera != null)
		{
			flag = UniversalInputManager.Get().ForcedInputIsOver(camera, m_dragRegion.gameObject);
		}
		if (flag)
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				float num = Mathf.Sign(axis);
				if (m_invertScrolling)
				{
					num = 0f - num;
				}
				if (!m_scrollTargetPosition.HasValue)
				{
					m_scrollTargetPosition = Mathf.Round(m_wheelPosition);
				}
				float num2 = 1f;
				m_scrollTargetPosition += num * num2;
				m_scrollTargetPosition = Mathf.Clamp(m_scrollTargetPosition.Value, 0f, m_numTiles - 1);
				m_snapInfluence = 1f;
				AbortCruise();
			}
		}
		if (m_lastDragPos.HasValue)
		{
			if (UniversalInputManager.Get().GetMouseButtonUp(0) || UniversalInputManager.Get().WasTouchCanceled())
			{
				m_lastDragPos = null;
				PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
			}
			else
			{
				Vector3 localMousePos = GetLocalMousePos();
				Vector3 vector = localMousePos - m_lastDragPos.Value;
				m_lastDragPos = localMousePos;
				float y = vector.y;
				m_velocity = (0f - y) / m_tileSpacing / deltaTime;
				m_snapInfluence = 0f;
			}
		}
		if (m_cruiseDir != 0)
		{
			if (UniversalInputManager.Get().GetMouseButtonUp(0) || UniversalInputManager.Get().WasTouchCanceled())
			{
				AbortCruise();
			}
			if (Mathf.Sign(m_velocity) == Mathf.Sign(m_cruiseDir))
			{
				float value = Mathf.Abs(m_velocity) * Mathf.Exp(m_exponentialCruising * deltaTime);
				m_velocity = (float)m_cruiseDir * Mathf.Clamp(value, m_cruiseStartSpeed, m_cruiseEndSpeed);
			}
			m_snapInfluence = 1f;
		}
	}

	private void SimulateWheel(float deltaTime)
	{
		float num = 0f;
		float num2 = m_numTiles - 1;
		bool flag = m_lastDragPos.HasValue || m_cruiseDir != 0;
		if (m_scrollTargetPosition.HasValue)
		{
			float num3 = m_scrollTargetPosition.Value - m_wheelPosition;
			float num4 = Mathf.Abs(num3);
			if (num4 >= 0.5f)
			{
				float num5 = 0.5f + num4 / 2f;
				m_velocity = Mathf.Clamp(num3, 0f - num5, num5) * m_scrollingSpeed;
			}
			else
			{
				num2 = (num = m_scrollTargetPosition.Value);
				float num6 = 0.5f * (0.5f - num4);
				if (Mathf.Sign(m_velocity) != Mathf.Sign(num3))
				{
					num6 = 4f / (0.51f - num4);
				}
				m_velocity *= Mathf.Exp((0f - deltaTime) * num6);
				if (Mathf.Abs(m_velocity) < 0.1f)
				{
					m_scrollTargetPosition = null;
				}
			}
		}
		else
		{
			float b = Mathf.Max(m_snapInfluence, 1f / (1f + 4f * Mathf.Abs(m_velocity)));
			float t = (flag ? 1f : (1f - Mathf.Exp((0f - deltaTime) * m_flyBraking)));
			m_snapInfluence = Mathf.Lerp(m_snapInfluence, b, t);
		}
		if (!flag)
		{
			float num7 = (m_wheelPosition % 1f + 1.5f) % 1f - 0.5f;
			float num8 = Mathf.Abs(num7);
			float num9 = -Math.Sign(num7);
			num9 = ((!(num8 <= 0.45f)) ? (num9 * (1f - (num8 - 0.45f) / 0.0500000119f)) : (num9 * (num8 / 0.45f)));
			m_velocity += num9 * deltaTime * m_snapForce * m_snapInfluence;
			m_velocity *= Mathf.Exp((0f - Mathf.Lerp(m_flyDamping, m_snapDamping, m_snapInfluence)) * deltaTime);
		}
		m_wheelPosition += m_velocity * deltaTime;
		num -= 0.49f;
		num2 += 0.49f;
		if ((m_wheelPosition <= num && m_velocity < 0f) || (m_wheelPosition >= num2 && m_velocity > 0f))
		{
			m_wheelPosition = Mathf.Clamp(m_wheelPosition, num, num2);
			m_velocity = 0f;
			AbortCruise();
		}
	}

	private void UpdateSelection()
	{
		int num = Mathf.RoundToInt(m_wheelPosition);
		if (m_lastSelection != num)
		{
			m_lastSelection = num;
			if (this.OnSelectionChanged != null)
			{
				this.OnSelectionChanged();
			}
		}
	}

	private void LayoutTiles()
	{
		Tile[] tiles = m_tiles;
		for (int i = 0; i < tiles.Length; i++)
		{
			Tile tile = tiles[i];
			int num = Mathf.FloorToInt((m_wheelPosition - (float)tile.indexOffset) / (float)m_maxTiles + 0.5f);
			int num2 = tile.indexOffset + num * m_maxTiles;
			if (num2 < 0 || num2 >= m_numTiles)
			{
				tile.gameObject.SetActive(value: false);
				continue;
			}
			tile.gameObject.SetActive(value: true);
			float num3 = ((float)num2 - m_wheelPosition) * m_tileSpacing / m_radius;
			Vector3 localPosition = tile.gameObject.transform.localPosition;
			localPosition.y = m_radius * Mathf.Sin(num3);
			localPosition.z = m_radius * (1f - Mathf.Cos(num3));
			tile.gameObject.transform.localPosition = localPosition;
			tile.gameObject.transform.localEulerAngles = new Vector3(num3 * 180f / (float)Math.PI, 0f, 0f);
			int indexFromPosition = GetIndexFromPosition(num2);
			AssignIndexToTile(tile.gameObject, indexFromPosition);
		}
	}

	protected void AssignIndexToTile(GameObject tile, int index)
	{
		Widget componentInChildren = tile.GetComponentInChildren<Widget>();
		IDataModel dataModel = GetDataModel(index);
		if (componentInChildren != null && dataModel != null)
		{
			componentInChildren.BindDataModel(dataModel);
		}
	}

	private Camera GetCamera()
	{
		return CameraUtils.FindFirstByLayer(base.gameObject.layer);
	}

	private Vector3 GetLocalMousePos()
	{
		Camera camera = GetCamera();
		Vector3 min = m_dragRegion.GetComponent<BoxCollider>().bounds.min;
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		if (plane.Raycast(ray, out var enter))
		{
			return base.transform.InverseTransformPoint(ray.GetPoint(enter));
		}
		return Vector3.zero;
	}

	private void InitializeCruiseClicker(PegUIElement cruiser, int dir)
	{
		if (cruiser == null)
		{
			return;
		}
		cruiser.AddEventListener(UIEventType.PRESS, delegate
		{
			m_cruiseDir = (m_invertScrolling ? (-dir) : dir);
			float a = ((Mathf.Sign(m_velocity) == Mathf.Sign(m_cruiseDir)) ? Mathf.Abs(m_velocity) : 0f);
			m_velocity = (float)m_cruiseDir * Mathf.Max(a, m_cruiseStartSpeed);
			m_scrollTargetPosition = null;
		});
		cruiser.AddEventListener(UIEventType.RELEASE, delegate
		{
			if (m_cruiseDir != 0)
			{
				float value = Mathf.Round(m_wheelPosition + (float)m_cruiseDir * 0.51f);
				m_scrollTargetPosition = Mathf.Clamp(value, 0f, m_numTiles - 1);
				m_cruiseDir = 0;
			}
		});
		PegCursor.Mode mode = ((dir > 0) ? PegCursor.Mode.UPARROW : PegCursor.Mode.DOWNARROW);
		cruiser.SetCursorOver(mode);
		cruiser.SetCursorDown(mode);
	}

	private int GetIndexFromPosition(int position)
	{
		if (!m_topToBottom)
		{
			return position;
		}
		return m_numTiles - 1 - position;
	}

	private float GetPositionFromIndex(int index)
	{
		return m_topToBottom ? (m_numTiles - 1 - index) : index;
	}

	private void AbortCruise()
	{
		m_cruiseDir = 0;
	}
}
