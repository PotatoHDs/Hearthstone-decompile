using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
public class SelectorWheel : MonoBehaviour
{
	// Token: 0x06005FCA RID: 24522 RVA: 0x001F3910 File Offset: 0x001F1B10
	private void Start()
	{
		this.m_tiles = new SelectorWheel.Tile[this.m_maxTiles];
		for (int i = 0; i < this.m_maxTiles; i++)
		{
			this.m_tiles[i] = default(SelectorWheel.Tile);
			this.m_tiles[i].indexOffset = i;
			if (i == 0)
			{
				this.m_tiles[i].gameObject = this.m_tileBase;
			}
			else
			{
				this.m_tiles[i].gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_tileBase, this.m_tileBase.transform.parent);
			}
		}
		if (this.m_dragRegion != null)
		{
			this.m_dragRegion.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.m_lastDragPos = new Vector3?(this.GetLocalMousePos());
				PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
			});
		}
		this.InitializeCruiseClicker(this.m_cruiseUpRegion, 1);
		this.InitializeCruiseClicker(this.m_cruiseDownRegion, -1);
		this.SetIndex(0, true);
	}

	// Token: 0x06005FCB RID: 24523 RVA: 0x001F39F4 File Offset: 0x001F1BF4
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		this.UpdateInput(deltaTime);
		float wheelPosition = this.m_wheelPosition;
		this.SimulateWheel(deltaTime);
		if (Mathf.Abs(this.m_wheelPosition - wheelPosition) < 0.001f)
		{
			return;
		}
		this.UpdateSelection();
		this.LayoutTiles();
	}

	// Token: 0x14000042 RID: 66
	// (add) Token: 0x06005FCC RID: 24524 RVA: 0x001F3A40 File Offset: 0x001F1C40
	// (remove) Token: 0x06005FCD RID: 24525 RVA: 0x001F3A78 File Offset: 0x001F1C78
	public event Action OnSelectionChanged;

	// Token: 0x170005BD RID: 1469
	// (get) Token: 0x06005FCE RID: 24526 RVA: 0x001F3AAD File Offset: 0x001F1CAD
	// (set) Token: 0x06005FCF RID: 24527 RVA: 0x001F3AB5 File Offset: 0x001F1CB5
	public int TileCount
	{
		get
		{
			return this.m_numTiles;
		}
		protected set
		{
			this.m_numTiles = value;
			if (this.m_wheelPosition >= (float)this.m_numTiles)
			{
				this.SetIndex((this.m_numTiles > 0) ? (this.m_numTiles - 1) : 0, true);
			}
			this.LayoutTiles();
		}
	}

	// Token: 0x06005FD0 RID: 24528 RVA: 0x001F3AF0 File Offset: 0x001F1CF0
	public void SetIndex(int index, bool instant = true)
	{
		float positionFromIndex = this.GetPositionFromIndex(index);
		if (instant)
		{
			this.m_wheelPosition = positionFromIndex;
			this.m_velocity = 0f;
			this.LayoutTiles();
			return;
		}
		this.m_scrollTargetPosition = new float?(positionFromIndex);
	}

	// Token: 0x06005FD1 RID: 24529 RVA: 0x001F3B2D File Offset: 0x001F1D2D
	public int GetSelectedIndex()
	{
		return this.GetIndexFromPosition(Mathf.RoundToInt(this.m_wheelPosition));
	}

	// Token: 0x06005FD2 RID: 24530 RVA: 0x001F3B40 File Offset: 0x001F1D40
	public void SetDataModels(List<IDataModel> dataModels)
	{
		this.m_dataModels = dataModels;
		this.TileCount = this.m_dataModels.Count;
	}

	// Token: 0x06005FD3 RID: 24531 RVA: 0x001F3B5A File Offset: 0x001F1D5A
	public IDataModel GetDataModel(int index)
	{
		if (index < 0 || index >= this.m_dataModels.Count)
		{
			return null;
		}
		return this.m_dataModels[index];
	}

	// Token: 0x06005FD4 RID: 24532 RVA: 0x001F3B7C File Offset: 0x001F1D7C
	private void UpdateInput(float deltaTime)
	{
		Camera camera = this.GetCamera();
		bool flag = false;
		if (this.m_globalScrolling)
		{
			flag = true;
		}
		else if (UniversalInputManager.Get() != null && camera != null)
		{
			flag = UniversalInputManager.Get().ForcedInputIsOver(camera, this.m_dragRegion.gameObject);
		}
		if (flag)
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f)
			{
				float num = Mathf.Sign(axis);
				if (this.m_invertScrolling)
				{
					num = -num;
				}
				if (this.m_scrollTargetPosition == null)
				{
					this.m_scrollTargetPosition = new float?(Mathf.Round(this.m_wheelPosition));
				}
				float num2 = 1f;
				this.m_scrollTargetPosition += num * num2;
				this.m_scrollTargetPosition = new float?(Mathf.Clamp(this.m_scrollTargetPosition.Value, 0f, (float)(this.m_numTiles - 1)));
				this.m_snapInfluence = 1f;
				this.AbortCruise();
			}
		}
		if (this.m_lastDragPos != null)
		{
			if (UniversalInputManager.Get().GetMouseButtonUp(0) || UniversalInputManager.Get().WasTouchCanceled())
			{
				this.m_lastDragPos = null;
				PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
			}
			else
			{
				Vector3 localMousePos = this.GetLocalMousePos();
				ref Vector3 ptr = localMousePos - this.m_lastDragPos.Value;
				this.m_lastDragPos = new Vector3?(localMousePos);
				float y = ptr.y;
				this.m_velocity = -y / this.m_tileSpacing / deltaTime;
				this.m_snapInfluence = 0f;
			}
		}
		if (this.m_cruiseDir != 0)
		{
			if (UniversalInputManager.Get().GetMouseButtonUp(0) || UniversalInputManager.Get().WasTouchCanceled())
			{
				this.AbortCruise();
			}
			if (Mathf.Sign(this.m_velocity) == Mathf.Sign((float)this.m_cruiseDir))
			{
				float value = Mathf.Abs(this.m_velocity) * Mathf.Exp(this.m_exponentialCruising * deltaTime);
				this.m_velocity = (float)this.m_cruiseDir * Mathf.Clamp(value, this.m_cruiseStartSpeed, this.m_cruiseEndSpeed);
			}
			this.m_snapInfluence = 1f;
		}
	}

	// Token: 0x06005FD5 RID: 24533 RVA: 0x001F3DA0 File Offset: 0x001F1FA0
	private void SimulateWheel(float deltaTime)
	{
		float num = 0f;
		float num2 = (float)(this.m_numTiles - 1);
		bool flag = this.m_lastDragPos != null || this.m_cruiseDir != 0;
		if (this.m_scrollTargetPosition != null)
		{
			float num3 = this.m_scrollTargetPosition.Value - this.m_wheelPosition;
			float num4 = Mathf.Abs(num3);
			if (num4 >= 0.5f)
			{
				float num5 = 0.5f + num4 / 2f;
				this.m_velocity = Mathf.Clamp(num3, -num5, num5) * this.m_scrollingSpeed;
			}
			else
			{
				num = (num2 = this.m_scrollTargetPosition.Value);
				float num6 = 0.5f * (0.5f - num4);
				if (Mathf.Sign(this.m_velocity) != Mathf.Sign(num3))
				{
					num6 = 4f / (0.51f - num4);
				}
				this.m_velocity *= Mathf.Exp(-deltaTime * num6);
				if (Mathf.Abs(this.m_velocity) < 0.1f)
				{
					this.m_scrollTargetPosition = null;
				}
			}
		}
		else
		{
			float b = Mathf.Max(this.m_snapInfluence, 1f / (1f + 4f * Mathf.Abs(this.m_velocity)));
			float t = flag ? 1f : (1f - Mathf.Exp(-deltaTime * this.m_flyBraking));
			this.m_snapInfluence = Mathf.Lerp(this.m_snapInfluence, b, t);
		}
		if (!flag)
		{
			float num7 = (this.m_wheelPosition % 1f + 1.5f) % 1f - 0.5f;
			float num8 = Mathf.Abs(num7);
			float num9 = (float)(-(float)Math.Sign(num7));
			if (num8 <= 0.45f)
			{
				num9 *= num8 / 0.45f;
			}
			else
			{
				num9 *= 1f - (num8 - 0.45f) / 0.050000012f;
			}
			this.m_velocity += num9 * deltaTime * this.m_snapForce * this.m_snapInfluence;
			this.m_velocity *= Mathf.Exp(-Mathf.Lerp(this.m_flyDamping, this.m_snapDamping, this.m_snapInfluence) * deltaTime);
		}
		this.m_wheelPosition += this.m_velocity * deltaTime;
		num -= 0.49f;
		num2 += 0.49f;
		if ((this.m_wheelPosition <= num && this.m_velocity < 0f) || (this.m_wheelPosition >= num2 && this.m_velocity > 0f))
		{
			this.m_wheelPosition = Mathf.Clamp(this.m_wheelPosition, num, num2);
			this.m_velocity = 0f;
			this.AbortCruise();
		}
	}

	// Token: 0x06005FD6 RID: 24534 RVA: 0x001F4038 File Offset: 0x001F2238
	private void UpdateSelection()
	{
		int num = Mathf.RoundToInt(this.m_wheelPosition);
		if (this.m_lastSelection != num)
		{
			this.m_lastSelection = num;
			if (this.OnSelectionChanged != null)
			{
				this.OnSelectionChanged();
			}
		}
	}

	// Token: 0x06005FD7 RID: 24535 RVA: 0x001F4074 File Offset: 0x001F2274
	private void LayoutTiles()
	{
		foreach (SelectorWheel.Tile tile in this.m_tiles)
		{
			int num = Mathf.FloorToInt((this.m_wheelPosition - (float)tile.indexOffset) / (float)this.m_maxTiles + 0.5f);
			int num2 = tile.indexOffset + num * this.m_maxTiles;
			if (num2 < 0 || num2 >= this.m_numTiles)
			{
				tile.gameObject.SetActive(false);
			}
			else
			{
				tile.gameObject.SetActive(true);
				float num3 = ((float)num2 - this.m_wheelPosition) * this.m_tileSpacing / this.m_radius;
				Vector3 localPosition = tile.gameObject.transform.localPosition;
				localPosition.y = this.m_radius * Mathf.Sin(num3);
				localPosition.z = this.m_radius * (1f - Mathf.Cos(num3));
				tile.gameObject.transform.localPosition = localPosition;
				tile.gameObject.transform.localEulerAngles = new Vector3(num3 * 180f / 3.1415927f, 0f, 0f);
				int indexFromPosition = this.GetIndexFromPosition(num2);
				this.AssignIndexToTile(tile.gameObject, indexFromPosition);
			}
		}
	}

	// Token: 0x06005FD8 RID: 24536 RVA: 0x001F41B8 File Offset: 0x001F23B8
	protected void AssignIndexToTile(GameObject tile, int index)
	{
		Widget componentInChildren = tile.GetComponentInChildren<Widget>();
		IDataModel dataModel = this.GetDataModel(index);
		if (componentInChildren != null && dataModel != null)
		{
			componentInChildren.BindDataModel(dataModel, false);
		}
	}

	// Token: 0x06005FD9 RID: 24537 RVA: 0x001F41E8 File Offset: 0x001F23E8
	private Camera GetCamera()
	{
		return CameraUtils.FindFirstByLayer(base.gameObject.layer);
	}

	// Token: 0x06005FDA RID: 24538 RVA: 0x001F41FC File Offset: 0x001F23FC
	private Vector3 GetLocalMousePos()
	{
		Camera camera = this.GetCamera();
		Vector3 min = this.m_dragRegion.GetComponent<BoxCollider>().bounds.min;
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			return base.transform.InverseTransformPoint(ray.GetPoint(distance));
		}
		return Vector3.zero;
	}

	// Token: 0x06005FDB RID: 24539 RVA: 0x001F427C File Offset: 0x001F247C
	private void InitializeCruiseClicker(PegUIElement cruiser, int dir)
	{
		if (cruiser == null)
		{
			return;
		}
		cruiser.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
		{
			this.m_cruiseDir = (this.m_invertScrolling ? (-dir) : dir);
			float a = (Mathf.Sign(this.m_velocity) == Mathf.Sign((float)this.m_cruiseDir)) ? Mathf.Abs(this.m_velocity) : 0f;
			this.m_velocity = (float)this.m_cruiseDir * Mathf.Max(a, this.m_cruiseStartSpeed);
			this.m_scrollTargetPosition = null;
		});
		cruiser.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			if (this.m_cruiseDir == 0)
			{
				return;
			}
			float value = Mathf.Round(this.m_wheelPosition + (float)this.m_cruiseDir * 0.51f);
			this.m_scrollTargetPosition = new float?(Mathf.Clamp(value, 0f, (float)(this.m_numTiles - 1)));
			this.m_cruiseDir = 0;
		});
		PegCursor.Mode mode = (dir > 0) ? PegCursor.Mode.UPARROW : PegCursor.Mode.DOWNARROW;
		cruiser.SetCursorOver(mode);
		cruiser.SetCursorDown(mode);
	}

	// Token: 0x06005FDC RID: 24540 RVA: 0x001F42ED File Offset: 0x001F24ED
	private int GetIndexFromPosition(int position)
	{
		if (!this.m_topToBottom)
		{
			return position;
		}
		return this.m_numTiles - 1 - position;
	}

	// Token: 0x06005FDD RID: 24541 RVA: 0x001F4303 File Offset: 0x001F2503
	private float GetPositionFromIndex(int index)
	{
		return (float)(this.m_topToBottom ? (this.m_numTiles - 1 - index) : index);
	}

	// Token: 0x06005FDE RID: 24542 RVA: 0x001F431B File Offset: 0x001F251B
	private void AbortCruise()
	{
		this.m_cruiseDir = 0;
	}

	// Token: 0x0400505E RID: 20574
	[SerializeField]
	protected PegUIElement m_dragRegion;

	// Token: 0x0400505F RID: 20575
	[SerializeField]
	protected PegUIElement m_cruiseUpRegion;

	// Token: 0x04005060 RID: 20576
	[SerializeField]
	protected PegUIElement m_cruiseDownRegion;

	// Token: 0x04005061 RID: 20577
	[SerializeField]
	protected GameObject m_tileBase;

	// Token: 0x04005062 RID: 20578
	[SerializeField]
	protected float m_radius = 2f;

	// Token: 0x04005063 RID: 20579
	[SerializeField]
	protected float m_flyBraking = 4f;

	// Token: 0x04005064 RID: 20580
	[SerializeField]
	protected float m_flyDamping = 1f;

	// Token: 0x04005065 RID: 20581
	[SerializeField]
	protected float m_snapDamping = 8f;

	// Token: 0x04005066 RID: 20582
	[SerializeField]
	protected float m_snapForce = 40f;

	// Token: 0x04005067 RID: 20583
	[SerializeField]
	protected float m_tileSpacing = 1f;

	// Token: 0x04005068 RID: 20584
	[SerializeField]
	protected int m_maxTiles = 7;

	// Token: 0x04005069 RID: 20585
	[SerializeField]
	protected bool m_globalScrolling = true;

	// Token: 0x0400506A RID: 20586
	[SerializeField]
	protected bool m_invertScrolling = true;

	// Token: 0x0400506B RID: 20587
	[SerializeField]
	protected float m_scrollingSpeed = 15f;

	// Token: 0x0400506C RID: 20588
	[SerializeField]
	protected int m_numTiles = 1;

	// Token: 0x0400506D RID: 20589
	[SerializeField]
	protected bool m_topToBottom = true;

	// Token: 0x0400506E RID: 20590
	[SerializeField]
	protected float m_cruiseStartSpeed = 4f;

	// Token: 0x0400506F RID: 20591
	[SerializeField]
	protected float m_cruiseEndSpeed = 24f;

	// Token: 0x04005070 RID: 20592
	[SerializeField]
	protected float m_exponentialCruising = 0.5f;

	// Token: 0x04005071 RID: 20593
	private SelectorWheel.Tile[] m_tiles;

	// Token: 0x04005072 RID: 20594
	private float m_velocity;

	// Token: 0x04005073 RID: 20595
	private float m_snapInfluence = 1f;

	// Token: 0x04005074 RID: 20596
	private float m_wheelPosition;

	// Token: 0x04005075 RID: 20597
	private int m_lastSelection;

	// Token: 0x04005076 RID: 20598
	private Vector3? m_lastDragPos;

	// Token: 0x04005077 RID: 20599
	private float? m_scrollTargetPosition;

	// Token: 0x04005078 RID: 20600
	private int m_cruiseDir;

	// Token: 0x04005079 RID: 20601
	private List<IDataModel> m_dataModels = new List<IDataModel>();

	// Token: 0x020021EB RID: 8683
	private struct Tile
	{
		// Token: 0x0400E1C1 RID: 57793
		public GameObject gameObject;

		// Token: 0x0400E1C2 RID: 57794
		public int indexOffset;
	}
}
