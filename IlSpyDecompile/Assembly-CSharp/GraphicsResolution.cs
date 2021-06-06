using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using UnityEngine;

public class GraphicsResolution : IComparable
{
	private const int MIN_ASPECT_RATIO_WIDTH = 4;

	private const int MIN_ASPECT_RATIO_HEIGHT = 3;

	private const int MAX_ASPECT_RATIO_WIDTH = 16;

	private const int MAX_ASPECT_RATIO_HEIGHT = 9;

	private const int MIN_WINDOW_WIDTH = 400;

	private const int MIN_WINDOW_HEIGHT = 400;

	private const int TASKBAR_HEIGHT = 63;

	private const float ASPECT_RATIO_ERROR_ALLOWANCE = 0.051f;

	public static readonly List<GraphicsResolution> resolutions_ = new List<GraphicsResolution>();

	public static List<GraphicsResolution> list
	{
		get
		{
			if (resolutions_.Count == 0)
			{
				lock (resolutions_)
				{
					Resolution[] resolutions = Screen.resolutions;
					for (int i = 0; i < resolutions.Length; i++)
					{
						Resolution resolution = resolutions[i];
						if (IsAspectRatioWithinLimit(resolution.width, resolution.height, isWindowedMode: false))
						{
							add(resolution.width, resolution.height);
						}
					}
					resolutions_.Reverse();
				}
			}
			return resolutions_;
		}
	}

	public static GraphicsResolution current => create(Screen.currentResolution);

	public int x { get; private set; }

	public int y { get; private set; }

	public float aspectRatio { get; private set; }

	private GraphicsResolution()
	{
	}

	private GraphicsResolution(int width, int height)
	{
		x = width;
		y = height;
		aspectRatio = (float)x / (float)y;
	}

	public static GraphicsResolution create(Resolution res)
	{
		return new GraphicsResolution(res.width, res.height);
	}

	public static GraphicsResolution create(int width, int height)
	{
		return new GraphicsResolution(width, height);
	}

	private static bool add(int width, int height)
	{
		GraphicsResolution item = new GraphicsResolution(width, height);
		if (resolutions_.BinarySearch(item) >= 0)
		{
			return false;
		}
		resolutions_.Add(item);
		resolutions_.Sort();
		return true;
	}

	public int CompareTo(object obj)
	{
		GraphicsResolution graphicsResolution = obj as GraphicsResolution;
		if (graphicsResolution == null)
		{
			return 1;
		}
		if (x < graphicsResolution.x)
		{
			return -1;
		}
		if (x > graphicsResolution.x)
		{
			return 1;
		}
		if (y < graphicsResolution.y)
		{
			return -1;
		}
		if (y > graphicsResolution.y)
		{
			return 1;
		}
		return 0;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		GraphicsResolution graphicsResolution = obj as GraphicsResolution;
		if (graphicsResolution == null)
		{
			return false;
		}
		if (x == graphicsResolution.x)
		{
			return y == graphicsResolution.y;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (23 * 17 + x.GetHashCode()) * 17 + y.GetHashCode();
	}

	public static GraphicsResolution GetLargestResolution()
	{
		return list.First();
	}

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
		if (width >= 400 && height >= 400 && CompareAspectRatio(16, 9, width, height) >= 0)
		{
			return CompareAspectRatio(width, height, 4, 3) >= 0;
		}
		return false;
	}

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
		if (CompareAspectRatio(num, num2, 16, 9) > 0)
		{
			num = (int)((float)num2 * 16f / 9f);
		}
		else if (CompareAspectRatio(num, num2, 16, 9) < 0)
		{
			num = (int)((float)num2 * 4f / 3f);
		}
		return new int[2] { num, num2 };
	}
}
