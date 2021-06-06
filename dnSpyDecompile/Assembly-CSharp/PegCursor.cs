using System;
using UnityEngine;

// Token: 0x02000AD8 RID: 2776
public class PegCursor : MonoBehaviour
{
	// Token: 0x060093D8 RID: 37848 RVA: 0x002FFFAE File Offset: 0x002FE1AE
	private void Awake()
	{
		PegCursor.s_instance = this;
	}

	// Token: 0x060093D9 RID: 37849 RVA: 0x002FFFB6 File Offset: 0x002FE1B6
	private void OnDestroy()
	{
		PegCursor.s_instance = null;
	}

	// Token: 0x060093DA RID: 37850 RVA: 0x002FFFBE File Offset: 0x002FE1BE
	public static PegCursor Get()
	{
		return PegCursor.s_instance;
	}

	// Token: 0x060093DB RID: 37851 RVA: 0x002FFFC5 File Offset: 0x002FE1C5
	public void Show()
	{
		Cursor.visible = true;
	}

	// Token: 0x060093DC RID: 37852 RVA: 0x002FFFCD File Offset: 0x002FE1CD
	public void Hide()
	{
		Cursor.visible = false;
	}

	// Token: 0x060093DD RID: 37853 RVA: 0x002FFFD8 File Offset: 0x002FE1D8
	public void SetMode(PegCursor.Mode mode)
	{
		bool flag = false;
		if (this.m_currentMode == PegCursor.Mode.WAITING && mode != PegCursor.Mode.STOPWAITING)
		{
			if (mode != PegCursor.Mode.DOWN)
			{
				if (mode == PegCursor.Mode.UP)
				{
					if (flag)
					{
						Cursor.SetCursor(this.m_cursorWaiting64, this.m_cursorWaitingHotspot64, CursorMode.Auto);
						return;
					}
					Cursor.SetCursor(this.m_cursorWaiting, this.m_cursorWaitingHotspot, CursorMode.Auto);
				}
				return;
			}
			if (flag)
			{
				Cursor.SetCursor(this.m_cursorWaitingDown64, this.m_cursorWaitingDownHotspot64, CursorMode.Auto);
				return;
			}
			Cursor.SetCursor(this.m_cursorWaitingDown, this.m_cursorWaitingDownHotspot, CursorMode.Auto);
			return;
		}
		else
		{
			if (this.m_currentMode == PegCursor.Mode.DRAG && mode != PegCursor.Mode.STOPDRAG)
			{
				return;
			}
			this.m_currentMode = mode;
			if (flag)
			{
				switch (mode)
				{
				case PegCursor.Mode.UP:
					Cursor.SetCursor(this.m_cursorUp64, this.m_cursorUpHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.DOWN:
					Cursor.SetCursor(this.m_cursorDown64, this.m_cursorDownHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.OVER:
					Cursor.SetCursor(this.m_cursorUp64, this.m_cursorUpHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.DRAG:
					Cursor.SetCursor(this.m_cursorDrag64, this.m_cursorDragHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.STOPDRAG:
				case PegCursor.Mode.STOPWAITING:
					Cursor.SetCursor(this.m_cursorUp64, this.m_cursorUpHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.WAITING:
					Cursor.SetCursor(this.m_cursorWaiting64, this.m_cursorWaitingHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.LEFTARROW:
					Cursor.SetCursor(this.m_leftArrow64, this.m_leftArrowHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.RIGHTARROW:
					Cursor.SetCursor(this.m_rightArrow64, this.m_rightArrowHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.UPARROW:
					Cursor.SetCursor(this.m_upArrow64, this.m_upArrowHotspot64, CursorMode.Auto);
					return;
				case PegCursor.Mode.DOWNARROW:
					Cursor.SetCursor(this.m_downArrow64, this.m_downArrowHotspot64, CursorMode.Auto);
					return;
				default:
					return;
				}
			}
			else
			{
				switch (mode)
				{
				case PegCursor.Mode.UP:
					Cursor.SetCursor(this.m_cursorUp, this.m_cursorUpHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.DOWN:
					Cursor.SetCursor(this.m_cursorDown, this.m_cursorDownHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.OVER:
					Cursor.SetCursor(this.m_cursorUp, this.m_cursorUpHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.DRAG:
					Cursor.SetCursor(this.m_cursorDrag, this.m_cursorDragHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.STOPDRAG:
				case PegCursor.Mode.STOPWAITING:
					Cursor.SetCursor(this.m_cursorUp, this.m_cursorUpHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.WAITING:
					Cursor.SetCursor(this.m_cursorWaiting, this.m_cursorWaitingHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.LEFTARROW:
					Cursor.SetCursor(this.m_leftArrow, this.m_leftArrowHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.RIGHTARROW:
					Cursor.SetCursor(this.m_rightArrow, this.m_rightArrowHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.UPARROW:
					Cursor.SetCursor(this.m_upArrow, this.m_upArrowHotspot, CursorMode.Auto);
					return;
				case PegCursor.Mode.DOWNARROW:
					Cursor.SetCursor(this.m_downArrow, this.m_downArrowHotspot, CursorMode.Auto);
					return;
				default:
					return;
				}
			}
		}
	}

	// Token: 0x060093DE RID: 37854 RVA: 0x00300249 File Offset: 0x002FE449
	public PegCursor.Mode GetMode()
	{
		return this.m_currentMode;
	}

	// Token: 0x060093DF RID: 37855 RVA: 0x00300251 File Offset: 0x002FE451
	public GameObject GetExplosionPrefab()
	{
		return this.m_explosionPrefab;
	}

	// Token: 0x04007BF3 RID: 31731
	public Texture2D m_cursorUp;

	// Token: 0x04007BF4 RID: 31732
	public Vector2 m_cursorUpHotspot = Vector2.zero;

	// Token: 0x04007BF5 RID: 31733
	public Texture2D m_cursorDown;

	// Token: 0x04007BF6 RID: 31734
	public Vector2 m_cursorDownHotspot = Vector2.zero;

	// Token: 0x04007BF7 RID: 31735
	public Texture2D m_cursorDrag;

	// Token: 0x04007BF8 RID: 31736
	public Vector2 m_cursorDragHotspot = Vector2.zero;

	// Token: 0x04007BF9 RID: 31737
	public Texture2D m_cursorOver;

	// Token: 0x04007BFA RID: 31738
	public Vector2 m_cursorOverHotspot = Vector2.zero;

	// Token: 0x04007BFB RID: 31739
	public Texture2D m_cursorWaiting;

	// Token: 0x04007BFC RID: 31740
	public Vector2 m_cursorWaitingHotspot = Vector2.zero;

	// Token: 0x04007BFD RID: 31741
	public Texture2D m_cursorWaitingDown;

	// Token: 0x04007BFE RID: 31742
	public Vector2 m_cursorWaitingDownHotspot = Vector2.zero;

	// Token: 0x04007BFF RID: 31743
	public Texture2D m_cursorWaitingUp;

	// Token: 0x04007C00 RID: 31744
	public Vector2 m_cursorWaitingUpHotspot = Vector2.zero;

	// Token: 0x04007C01 RID: 31745
	public Texture2D m_leftArrow;

	// Token: 0x04007C02 RID: 31746
	public Vector2 m_leftArrowHotspot = Vector2.zero;

	// Token: 0x04007C03 RID: 31747
	public Texture2D m_rightArrow;

	// Token: 0x04007C04 RID: 31748
	public Vector2 m_rightArrowHotspot = Vector2.zero;

	// Token: 0x04007C05 RID: 31749
	public Texture2D m_upArrow;

	// Token: 0x04007C06 RID: 31750
	public Vector2 m_upArrowHotspot = Vector2.zero;

	// Token: 0x04007C07 RID: 31751
	public Texture2D m_downArrow;

	// Token: 0x04007C08 RID: 31752
	public Vector2 m_downArrowHotspot = Vector2.zero;

	// Token: 0x04007C09 RID: 31753
	public Texture2D m_cursorUp64;

	// Token: 0x04007C0A RID: 31754
	public Vector2 m_cursorUpHotspot64 = Vector2.zero;

	// Token: 0x04007C0B RID: 31755
	public Texture2D m_cursorDown64;

	// Token: 0x04007C0C RID: 31756
	public Vector2 m_cursorDownHotspot64 = Vector2.zero;

	// Token: 0x04007C0D RID: 31757
	public Texture2D m_cursorDrag64;

	// Token: 0x04007C0E RID: 31758
	public Vector2 m_cursorDragHotspot64 = Vector2.zero;

	// Token: 0x04007C0F RID: 31759
	public Texture2D m_cursorOver64;

	// Token: 0x04007C10 RID: 31760
	public Vector2 m_cursorOverHotspot64 = Vector2.zero;

	// Token: 0x04007C11 RID: 31761
	public Texture2D m_cursorWaiting64;

	// Token: 0x04007C12 RID: 31762
	public Vector2 m_cursorWaitingHotspot64 = Vector2.zero;

	// Token: 0x04007C13 RID: 31763
	public Texture2D m_cursorWaitingDown64;

	// Token: 0x04007C14 RID: 31764
	public Vector2 m_cursorWaitingDownHotspot64 = Vector2.zero;

	// Token: 0x04007C15 RID: 31765
	public Texture2D m_cursorWaitingUp64;

	// Token: 0x04007C16 RID: 31766
	public Vector2 m_cursorWaitingUpHotspot64 = Vector2.zero;

	// Token: 0x04007C17 RID: 31767
	public Texture2D m_leftArrow64;

	// Token: 0x04007C18 RID: 31768
	public Vector2 m_leftArrowHotspot64 = Vector2.zero;

	// Token: 0x04007C19 RID: 31769
	public Texture2D m_rightArrow64;

	// Token: 0x04007C1A RID: 31770
	public Vector2 m_rightArrowHotspot64 = Vector2.zero;

	// Token: 0x04007C1B RID: 31771
	public Texture2D m_upArrow64;

	// Token: 0x04007C1C RID: 31772
	public Vector2 m_upArrowHotspot64 = Vector2.zero;

	// Token: 0x04007C1D RID: 31773
	public Texture2D m_downArrow64;

	// Token: 0x04007C1E RID: 31774
	public Vector2 m_downArrowHotspot64 = Vector2.zero;

	// Token: 0x04007C1F RID: 31775
	public GameObject m_explosionPrefab;

	// Token: 0x04007C20 RID: 31776
	private Texture2D m_cursorTexture;

	// Token: 0x04007C21 RID: 31777
	private PegCursor.Mode m_currentMode;

	// Token: 0x04007C22 RID: 31778
	private static PegCursor s_instance;

	// Token: 0x02002710 RID: 10000
	public enum Mode
	{
		// Token: 0x0400F33F RID: 62271
		UP,
		// Token: 0x0400F340 RID: 62272
		DOWN,
		// Token: 0x0400F341 RID: 62273
		OVER,
		// Token: 0x0400F342 RID: 62274
		DRAG,
		// Token: 0x0400F343 RID: 62275
		STOPDRAG,
		// Token: 0x0400F344 RID: 62276
		WAITING,
		// Token: 0x0400F345 RID: 62277
		STOPWAITING,
		// Token: 0x0400F346 RID: 62278
		LEFTARROW,
		// Token: 0x0400F347 RID: 62279
		RIGHTARROW,
		// Token: 0x0400F348 RID: 62280
		UPARROW,
		// Token: 0x0400F349 RID: 62281
		DOWNARROW,
		// Token: 0x0400F34A RID: 62282
		NONE
	}
}
