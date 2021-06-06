using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone.Timeline
{
	public static class TimelineEffectUtility
	{
		public const int OVERLAY_SORT_ORDER = 999;

		public static Canvas CreateCanvas(string name = null, int sortingOrder = 999)
		{
			GameObject obj = new GameObject
			{
				name = ((!string.IsNullOrEmpty(name)) ? name : "Timeline Effect Canvas")
			};
			Canvas canvas = obj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = sortingOrder;
			CanvasScaler canvasScaler = obj.AddComponent<CanvasScaler>();
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.referenceResolution = new Vector2(800f, 600f);
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			canvasScaler.matchWidthOrHeight = 0.5f;
			canvasScaler.referencePixelsPerUnit = 100f;
			GraphicRaycaster graphicRaycaster = obj.AddComponent<GraphicRaycaster>();
			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
			return canvas;
		}

		public static RectTransform CreateFillPane(this RectTransform parent, string name = null)
		{
			GameObject gameObject = new GameObject();
			if (!string.IsNullOrEmpty(name))
			{
				gameObject.name = name;
			}
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(parent);
			rectTransform.PositionRectTransform();
			return rectTransform;
		}

		public static Image CreateFillImage(this RectTransform parent, string name, Color color)
		{
			return parent.CreateFillImage(name, null, color);
		}

		public static Image CreateFillImage(this RectTransform parent, string name = null, Sprite sprite = null)
		{
			return parent.CreateFillImage(name, sprite, Color.white);
		}

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
	}
}
