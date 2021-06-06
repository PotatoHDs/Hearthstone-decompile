using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using UnityEngine;

// Token: 0x0200060E RID: 1550
public class GraphicsResolution : IComparable
{
	// Token: 0x0600569C RID: 22172 RVA: 0x000052CE File Offset: 0x000034CE
	private GraphicsResolution()
	{
	}

	// Token: 0x0600569D RID: 22173 RVA: 0x001C61C1 File Offset: 0x001C43C1
	private GraphicsResolution(int width, int height)
	{
		this.x = width;
		this.y = height;
		this.aspectRatio = (float)this.x / (float)this.y;
	}

	// Token: 0x0600569E RID: 22174 RVA: 0x001C61EC File Offset: 0x001C43EC
	public static GraphicsResolution create(Resolution res)
	{
		return new GraphicsResolution(res.width, res.height);
	}

	// Token: 0x0600569F RID: 22175 RVA: 0x001C6201 File Offset: 0x001C4401
	public static GraphicsResolution create(int width, int height)
	{
		return new GraphicsResolution(width, height);
	}

	// Token: 0x060056A0 RID: 22176 RVA: 0x001C620C File Offset: 0x001C440C
	private static bool add(int width, int height)
	{
		GraphicsResolution item = new GraphicsResolution(width, height);
		if (GraphicsResolution.resolutions_.BinarySearch(item) >= 0)
		{
			return false;
		}
		GraphicsResolution.resolutions_.Add(item);
		GraphicsResolution.resolutions_.Sort();
		return true;
	}

	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x060056A1 RID: 22177 RVA: 0x001C6248 File Offset: 0x001C4448
	public static List<GraphicsResolution> list
	{
		get
		{
			if (GraphicsResolution.resolutions_.Count == 0)
			{
				List<GraphicsResolution> obj = GraphicsResolution.resolutions_;
				lock (obj)
				{
					foreach (Resolution resolution in Screen.resolutions)
					{
						if (GraphicsResolution.IsAspectRatioWithinLimit(resolution.width, resolution.height, false))
						{
							GraphicsResolution.add(resolution.width, resolution.height);
						}
					}
					GraphicsResolution.resolutions_.Reverse();
				}
			}
			return GraphicsResolution.resolutions_;
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x060056A2 RID: 22178 RVA: 0x001C62E4 File Offset: 0x001C44E4
	public static GraphicsResolution current
	{
		get
		{
			return GraphicsResolution.create(Screen.currentResolution);
		}
	}

	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x060056A3 RID: 22179 RVA: 0x001C62F0 File Offset: 0x001C44F0
	// (set) Token: 0x060056A4 RID: 22180 RVA: 0x001C62F8 File Offset: 0x001C44F8
	public int x { get; private set; }

	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x060056A5 RID: 22181 RVA: 0x001C6301 File Offset: 0x001C4501
	// (set) Token: 0x060056A6 RID: 22182 RVA: 0x001C6309 File Offset: 0x001C4509
	public int y { get; private set; }

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x060056A7 RID: 22183 RVA: 0x001C6312 File Offset: 0x001C4512
	// (set) Token: 0x060056A8 RID: 22184 RVA: 0x001C631A File Offset: 0x001C451A
	public float aspectRatio { get; private set; }

	// Token: 0x060056A9 RID: 22185 RVA: 0x001C6324 File Offset: 0x001C4524
	public int CompareTo(object obj)
	{
		GraphicsResolution graphicsResolution = obj as GraphicsResolution;
		if (graphicsResolution == null)
		{
			return 1;
		}
		if (this.x < graphicsResolution.x)
		{
			return -1;
		}
		if (this.x > graphicsResolution.x)
		{
			return 1;
		}
		if (this.y < graphicsResolution.y)
		{
			return -1;
		}
		if (this.y > graphicsResolution.y)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060056AA RID: 22186 RVA: 0x001C6380 File Offset: 0x001C4580
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		GraphicsResolution graphicsResolution = obj as GraphicsResolution;
		return graphicsResolution != null && this.x == graphicsResolution.x && this.y == graphicsResolution.y;
	}

	// Token: 0x060056AB RID: 22187 RVA: 0x001C63BC File Offset: 0x001C45BC
	public override int GetHashCode()
	{
		return (23 * 17 + this.x.GetHashCode()) * 17 + this.y.GetHashCode();
	}

	// Token: 0x060056AC RID: 22188 RVA: 0x001C63EF File Offset: 0x001C45EF
	public static GraphicsResolution GetLargestResolution()
	{
		return GraphicsResolution.list.First<GraphicsResolution>();
	}

	// Token: 0x060056AD RID: 22189 RVA: 0x001C63FC File Offset: 0x001C45FC
	public static bool IsAspectRatioWithinLimit(int width, int height, bool isWindowedMode)
	{
		if (HearthstoneApplication.IsInternal())
		{
			return true;
		}
		if (isWindowedMode)
		{
			height += 63;
		}
		return width >= 400 && height >= 400 && GraphicsResolution.CompareAspectRatio(16, 9, width, height) >= 0 && GraphicsResolution.CompareAspectRatio(width, height, 4, 3) >= 0;
	}

	// Token: 0x060056AE RID: 22190 RVA: 0x001C644C File Offset: 0x001C464C
	public static int CompareAspectRatio(int lWidth, int lHeight, int rWidth, int rHeight)
	{
		float num = (float)lWidth / (float)lHeight;
		float num2 = (float)rWidth / (float)rHeight;
		if (Mathf.Abs(num - num2) < 0.051f)
		{
			return 0;
		}
		if (num > num2)
		{
			return 1;
		}
		return -1;
	}

	// Token: 0x060056AF RID: 22191 RVA: 0x001C6480 File Offset: 0x001C4680
	public static int[] CalcAspectRatioLimit(int x, int y)
	{
		int num = x;
		if (num < 400)
		{
			num = 400;
		}
		int num2 = y;
		if (num2 < 400)
		{
			num2 = 400;
		}
		if (GraphicsResolution.CompareAspectRatio(num, num2, 16, 9) > 0)
		{
			num = (int)((float)num2 * 16f / 9f);
		}
		else if (GraphicsResolution.CompareAspectRatio(num, num2, 16, 9) < 0)
		{
			num = (int)((float)num2 * 4f / 3f);
		}
		return new int[]
		{
			num,
			num2
		};
	}

	// Token: 0x04004A9A RID: 19098
	private const int MIN_ASPECT_RATIO_WIDTH = 4;

	// Token: 0x04004A9B RID: 19099
	private const int MIN_ASPECT_RATIO_HEIGHT = 3;

	// Token: 0x04004A9C RID: 19100
	private const int MAX_ASPECT_RATIO_WIDTH = 16;

	// Token: 0x04004A9D RID: 19101
	private const int MAX_ASPECT_RATIO_HEIGHT = 9;

	// Token: 0x04004A9E RID: 19102
	private const int MIN_WINDOW_WIDTH = 400;

	// Token: 0x04004A9F RID: 19103
	private const int MIN_WINDOW_HEIGHT = 400;

	// Token: 0x04004AA0 RID: 19104
	private const int TASKBAR_HEIGHT = 63;

	// Token: 0x04004AA1 RID: 19105
	private const float ASPECT_RATIO_ERROR_ALLOWANCE = 0.051f;

	// Token: 0x04004AA5 RID: 19109
	public static readonly List<GraphicsResolution> resolutions_ = new List<GraphicsResolution>();
}
