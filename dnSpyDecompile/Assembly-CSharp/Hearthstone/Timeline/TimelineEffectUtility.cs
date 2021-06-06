using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F8 RID: 4344
	public static class TimelineEffectUtility
	{
		// Token: 0x0600BE68 RID: 48744 RVA: 0x003A0C04 File Offset: 0x0039EE04
		public static Canvas CreateCanvas(string name = null, int sortingOrder = 999)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = ((!string.IsNullOrEmpty(name)) ? name : "Timeline Effect Canvas");
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = sortingOrder;
			CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.referenceResolution = new Vector2(800f, 600f);
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			canvasScaler.matchWidthOrHeight = 0.5f;
			canvasScaler.referencePixelsPerUnit = 100f;
			GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
			return canvas;
		}

		// Token: 0x0600BE69 RID: 48745 RVA: 0x003A0C94 File Offset: 0x0039EE94
		public static RectTransform CreateFillPane(this RectTransform parent, string name = null)
		{
			GameObject gameObject = new GameObject();
			if (!string.IsNullOrEmpty(name))
			{
				gameObject.name = name;
			}
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(parent);
			rectTransform.PositionRectTransform(0f, 0f, 1f, 1f, 0f, 0f, 0f, 0f);
			return rectTransform;
		}

		// Token: 0x0600BE6A RID: 48746 RVA: 0x003A0CF1 File Offset: 0x0039EEF1
		public static Image CreateFillImage(this RectTransform parent, string name, Color color)
		{
			return parent.CreateFillImage(name, null, color);
		}

		// Token: 0x0600BE6B RID: 48747 RVA: 0x003A0CFC File Offset: 0x0039EEFC
		public static Image CreateFillImage(this RectTransform parent, string name = null, Sprite sprite = null)
		{
			return parent.CreateFillImage(name, sprite, Color.white);
		}

		// Token: 0x0600BE6C RID: 48748 RVA: 0x003A0D0C File Offset: 0x0039EF0C
		public static Image CreateFillImage(this RectTransform parent, string name, Sprite sprite, Color color)
		{
			RectTransform rectTransform = parent.CreateFillPane(name);
			Image image = rectTransform.gameObject.AddComponent<Image>();
			image.sprite = sprite;
			image.color = color;
			image.raycastTarget = false;
			AspectRatioFitter aspectRatioFitter = rectTransform.gameObject.AddComponent<AspectRatioFitter>();
			if (sprite != null)
			{
				aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
				aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
			}
			return image;
		}

		// Token: 0x0600BE6D RID: 48749 RVA: 0x003A0D80 File Offset: 0x0039EF80
		public static void PositionRectTransform(this RectTransform rectTransform, float anchorMinX = 0f, float anchorMinY = 0f, float anchorMaxX = 1f, float anchorMaxY = 1f, float offsetMinX = 0f, float offsetMinY = 0f, float widthOrOffsetMaxX = 0f, float heightOrOffsetMaxY = 0f)
		{
			rectTransform.localPosition = Vector3.zero;
			rectTransform.localEulerAngles = Vector3.zero;
			rectTransform.localScale = Vector3.one;
			rectTransform.pivot = new Vector2(Mathf.Lerp(anchorMinX, anchorMaxX, 0.5f), Mathf.Lerp(anchorMinY, anchorMaxY, 0.5f));
			rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
			rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
			rectTransform.offsetMin = new Vector2(offsetMinX, offsetMinY);
			rectTransform.offsetMax = new Vector2(widthOrOffsetMaxX, heightOrOffsetMaxY);
			rectTransform.anchoredPosition = Vector2.zero;
		}

		// Token: 0x04009B02 RID: 39682
		public const int OVERLAY_SORT_ORDER = 999;
	}
}
