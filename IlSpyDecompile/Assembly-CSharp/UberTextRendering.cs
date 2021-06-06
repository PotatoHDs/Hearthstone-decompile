using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class UberTextRendering
{
	public struct TransformBackup
	{
		public Transform Parent;

		public Vector3 Position;

		public Vector3 LocalPosition;

		public Quaternion Rotation;

		public Vector3 LocalScale;
	}

	private static int RENDER_LAYER = 28;

	private TextMesh m_textMesh;

	private MeshRenderer m_textMeshRenderer;

	private GameObject m_textMeshGameObject;

	private bool m_isInit;

	private bool m_isRenderToTexture;

	private string m_lastTextMeshText = string.Empty;

	private Texture m_fontTexture;

	private UnlitTextMaterialQuery m_unlitTextMaterialQuery = new UnlitTextMaterialQuery();

	private BoldTextMaterialQuery m_boldTextMaterialQuery = new BoldTextMaterialQuery();

	private OutlineTextMaterialQuery m_outlineTextMaterialQuery = new OutlineTextMaterialQuery();

	private OutlineBoldTextMaterialQuery m_outlineBoldMaterialQuery = new OutlineBoldTextMaterialQuery();

	private List<Material> m_textMeshRendererMaterials = new List<Material>();

	private UberTextMaterial m_mainMaterial;

	private UberTextMaterial m_boldMaterial;

	private PopupRenderer m_popupRendererComponent;

	private bool m_boldEnabled;

	private bool m_outlineEnabled;

	public void Init(GameObject parent)
	{
		if (m_isInit && m_isRenderToTexture)
		{
			Destroy();
		}
		if (!m_isInit || !m_textMeshGameObject)
		{
			m_isInit = true;
			CreateTextMeshObject(parent);
		}
	}

	public void Destroy()
	{
		if ((bool)m_textMesh)
		{
			SceneUtils.SafeDestroy(m_textMesh.gameObject);
			m_textMesh = null;
		}
		if ((bool)m_textMeshGameObject)
		{
			SceneUtils.SafeDestroy(m_textMeshGameObject);
			m_textMeshGameObject = null;
		}
		m_lastTextMeshText = string.Empty;
		m_isInit = false;
		UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
		uberTextMaterialManager.ReleaseMaterial(m_mainMaterial);
		uberTextMaterialManager.ReleaseMaterial(m_boldMaterial);
	}

	public void SetTextMeshRenderToTextureTransform(float offset)
	{
		Vector3 position = new Vector3(-3000f, 3000f, offset);
		m_textMeshGameObject.transform.parent = null;
		m_textMeshGameObject.transform.position = position;
		m_textMeshGameObject.transform.rotation = Quaternion.identity;
		SetLayer(RENDER_LAYER);
	}

	public void SetTextMeshTransform(GameObject parent, Vector3 textCenter, Quaternion textRotation)
	{
		m_textMeshGameObject.transform.parent = parent.transform;
		m_textMeshGameObject.transform.localPosition = textCenter;
		m_textMeshGameObject.transform.localRotation = textRotation;
		m_textMeshGameObject.transform.localScale = Vector3.one;
		SetLayer(parent.layer);
	}

	public void SetLayer(int layer)
	{
		if ((bool)m_textMeshGameObject)
		{
			m_textMeshGameObject.layer = layer;
		}
	}

	public bool HasLayer(int layer)
	{
		bool result = false;
		if ((bool)m_textMeshGameObject)
		{
			result = m_textMeshGameObject.layer == layer;
		}
		return result;
	}

	public void SetActive(bool active)
	{
		if ((bool)m_textMeshGameObject)
		{
			m_textMeshGameObject.SetActive(active);
		}
	}

	public void ApplyMaterials()
	{
		if (m_outlineEnabled)
		{
			m_mainMaterial = ApplyMaterialIfNeeded(m_textMeshRenderer, m_mainMaterial, m_outlineTextMaterialQuery, 0);
			if (m_boldEnabled)
			{
				m_outlineBoldMaterialQuery.WithMinimalRenderQueueValue(m_mainMaterial.GetRenderQueue());
				m_boldMaterial = ApplyMaterialIfNeeded(m_textMeshRenderer, m_boldMaterial, m_outlineBoldMaterialQuery, 1);
			}
		}
		else
		{
			m_mainMaterial = ApplyMaterialIfNeeded(m_textMeshRenderer, m_mainMaterial, m_unlitTextMaterialQuery, 0);
			if (m_boldEnabled)
			{
				m_boldTextMaterialQuery.WithMinimalRenderQueueValue(m_mainMaterial.GetRenderQueue());
				m_boldMaterial = ApplyMaterialIfNeeded(m_textMeshRenderer, m_boldMaterial, m_boldTextMaterialQuery, 1);
			}
		}
		if (!m_boldEnabled && m_boldMaterial != null && m_boldMaterial.IsStillBound(m_textMeshRenderer))
		{
			UberTextMaterialManager.Get().UnboundMaterial(m_boldMaterial, m_textMeshRenderer);
			m_boldMaterial = null;
		}
	}

	public void SetVisible(bool visible)
	{
		if ((bool)m_textMeshRenderer)
		{
			m_textMeshRenderer.enabled = visible;
		}
	}

	public void SetBoldEnabled(bool enabled)
	{
		m_boldEnabled = enabled;
	}

	public void SetOutlineEnabled(bool enabled)
	{
		m_outlineEnabled = enabled;
	}

	public Bounds GetTextMeshBounds()
	{
		if (!m_textMeshRenderer)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		return m_textMeshRenderer.bounds;
	}

	public void SetCharacterSize(float characterSize)
	{
		if ((bool)m_textMesh && !Mathf.Approximately(m_textMesh.characterSize, characterSize))
		{
			m_textMesh.characterSize = characterSize;
		}
	}

	public float GetCharacterSize()
	{
		if (!m_textMesh)
		{
			return 0f;
		}
		return m_textMesh.characterSize;
	}

	public void SetText(string text)
	{
		if (text == null)
		{
			text = string.Empty;
		}
		if (m_lastTextMeshText != text)
		{
			m_textMesh.text = text;
			m_lastTextMeshText = text;
		}
	}

	public string GetText()
	{
		return m_lastTextMeshText;
	}

	public void SetRichText(bool richText)
	{
		if ((bool)m_textMesh && m_textMesh.richText != richText)
		{
			m_textMesh.richText = richText;
		}
		m_outlineTextMaterialQuery.WithRichText(richText);
	}

	public void SetLineSpacing(float lineSpacing)
	{
		if ((bool)m_textMesh && !Mathf.Approximately(m_textMesh.lineSpacing, lineSpacing))
		{
			m_textMesh.lineSpacing = lineSpacing;
		}
	}

	public bool CanSetFont(Font font)
	{
		if (!m_textMesh)
		{
			return false;
		}
		return m_textMesh.font != font;
	}

	public void SetFont(Font font)
	{
		if ((bool)m_textMesh && !(m_textMesh.font == font))
		{
			m_textMesh.font = font;
			m_fontTexture = font.material.mainTexture;
			m_unlitTextMaterialQuery.WithTexture(m_fontTexture);
			m_boldTextMaterialQuery.WithTexture(m_fontTexture);
			m_outlineTextMaterialQuery.WithTexture(m_fontTexture);
			m_outlineBoldMaterialQuery.WithTexture(m_fontTexture);
		}
	}

	public Font GetFont()
	{
		return m_textMesh?.font;
	}

	public Texture GetFontTexture()
	{
		return m_fontTexture;
	}

	public void SetFontSize(int fontSize)
	{
		if ((bool)m_textMesh && m_textMesh.fontSize != fontSize)
		{
			m_textMesh.fontSize = fontSize;
		}
	}

	public int GetFontSize()
	{
		if (!m_textMesh)
		{
			return 0;
		}
		return m_textMesh.fontSize;
	}

	public TextAnchor GetTextAnchor()
	{
		if (!m_textMesh)
		{
			return TextAnchor.MiddleCenter;
		}
		return m_textMesh.anchor;
	}

	public void SetTextMeshGameObjectLocalPositionOffset(Vector3 offset)
	{
		m_textMeshGameObject.transform.localPosition += offset;
	}

	public void SetTextMeshGameObjectLocalPosition(Vector3 localPosition)
	{
		m_textMeshGameObject.transform.localPosition = localPosition;
	}

	public void SetTextMeshGameObjectPosition(Vector3 position)
	{
		m_textMeshGameObject.transform.position = position;
	}

	public void SetTextMeshGameObjectLocalScale(Vector3 scale)
	{
		m_textMeshGameObject.transform.localScale = scale;
	}

	public void SetTextMeshGameObjectRotation(Quaternion rotation)
	{
		m_textMeshGameObject.transform.rotation = rotation;
	}

	public void SetTextMeshGameObjectParent(Transform parent)
	{
		m_textMeshGameObject.transform.parent = parent;
	}

	public void SetTextAlignment(UberText.AlignmentOptions alignment)
	{
		switch (alignment)
		{
		case UberText.AlignmentOptions.Left:
			m_textMesh.alignment = TextAlignment.Left;
			break;
		case UberText.AlignmentOptions.Right:
			m_textMesh.alignment = TextAlignment.Right;
			break;
		default:
			m_textMesh.alignment = TextAlignment.Center;
			break;
		}
	}

	public void SetTextAnchor(TextAnchor anchor)
	{
		if ((bool)m_textMesh && m_textMesh.anchor != anchor)
		{
			m_textMesh.anchor = anchor;
		}
	}

	public TransformBackup BackupTextMeshTransform()
	{
		Transform transform = m_textMeshGameObject.transform;
		TransformBackup result = default(TransformBackup);
		result.Parent = transform.parent;
		result.Position = transform.position;
		result.Rotation = transform.rotation;
		result.LocalScale = transform.localScale;
		result.LocalPosition = transform.localPosition;
		return result;
	}

	public void SetTextColor(Color color)
	{
		m_unlitTextMaterialQuery.WithColor(color);
		m_boldTextMaterialQuery.WithColor(color);
		m_outlineTextMaterialQuery.WithColor(color);
		m_outlineBoldMaterialQuery.WithColor(color);
	}

	public void SetOutlineColor(Color color)
	{
		m_outlineTextMaterialQuery.WithOutlineColor(color);
		m_outlineBoldMaterialQuery.WithOutlineColor(color);
	}

	public void SetAmbientLightBlend(float ambientLightBlend)
	{
		m_unlitTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		m_boldTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		m_outlineTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		m_outlineBoldMaterialQuery.WithLightingBlend(ambientLightBlend);
	}

	public void SetOutlineOffset(Vector2 offset)
	{
		m_outlineTextMaterialQuery.WithOutlineOffsetX(offset.x);
		m_outlineTextMaterialQuery.WithOutlineOffsetY(offset.y);
	}

	public void SetTexelSize(Vector2 texelSize)
	{
		m_outlineTextMaterialQuery.WithTexelSizeX(texelSize.x);
		m_outlineTextMaterialQuery.WithTexelSizeY(texelSize.y);
	}

	public void SetBoldOffset(Vector2 boldOffset)
	{
		m_boldTextMaterialQuery.WithBoldOffsetX(boldOffset.x);
		m_boldTextMaterialQuery.WithBoldOffsetY(boldOffset.y);
		m_outlineBoldMaterialQuery.WithBoldOffsetX(boldOffset.x);
		m_outlineBoldMaterialQuery.WithBoldOffsetY(boldOffset.y);
	}

	public void SetOutlineBoldOffset(Vector2 offset)
	{
		m_outlineBoldMaterialQuery.WithOutlineOffsetX(offset.x);
		m_outlineBoldMaterialQuery.WithOutlineOffsetY(offset.y);
	}

	public MeshRenderer GetTextMeshRenderer()
	{
		return m_textMeshRenderer;
	}

	public void SetLocale(Locale locale)
	{
		m_outlineTextMaterialQuery.WithLocale(locale);
	}

	public PopupRenderer GetPopupRenderer()
	{
		if (!m_popupRendererComponent)
		{
			m_popupRendererComponent = m_textMeshGameObject.AddComponent<PopupRenderer>();
		}
		return m_popupRendererComponent;
	}

	public void DisablePopupRenderer()
	{
		if ((bool)m_popupRendererComponent)
		{
			m_popupRendererComponent.DisablePopupRendering();
		}
	}

	public void SetRenderQueueIncrement(int value)
	{
		if ((bool)m_textMesh)
		{
			m_textMeshRenderer.sortingOrder = value;
		}
		m_unlitTextMaterialQuery.WithIncrementRenderQueue(value);
		m_boldTextMaterialQuery.WithIncrementRenderQueue(value);
		m_outlineTextMaterialQuery.WithIncrementRenderQueue(value);
		m_outlineBoldMaterialQuery.WithIncrementRenderQueue(value);
	}

	private void CreateTextMeshObject(GameObject parent)
	{
		if ((bool)m_textMeshGameObject)
		{
			return;
		}
		string text = "UberText_RenderObject_" + parent.name;
		for (int num = parent.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = parent.transform.GetChild(num);
			if ((bool)child && child.gameObject.name == text)
			{
				SceneUtils.SafeDestroy(child.gameObject);
			}
		}
		m_textMeshGameObject = new GameObject();
		m_textMeshGameObject.name = text;
		SceneUtils.SetHideFlags(m_textMeshGameObject, HideFlags.HideAndDontSave);
		m_textMeshRenderer = m_textMeshGameObject.AddComponent<MeshRenderer>();
		m_textMesh = m_textMeshGameObject.AddComponent<TextMesh>();
		SetText(string.Empty);
	}

	private UberTextMaterial ApplyMaterialIfNeeded(Renderer renderer, UberTextMaterial currentMaterial, UberTextMaterialQuery query, int materialIndex)
	{
		if (currentMaterial == null || !currentMaterial.HasQuery(query))
		{
			UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
			if (currentMaterial != null)
			{
				uberTextMaterialManager.ReleaseMaterial(currentMaterial);
			}
			currentMaterial = uberTextMaterialManager.FetchMaterial(query);
			Material material = currentMaterial.Acquire();
			m_textMeshRendererMaterials.Clear();
			renderer.GetSharedMaterials(m_textMeshRendererMaterials);
			if (m_textMeshRendererMaterials.Count <= materialIndex)
			{
				m_textMeshRendererMaterials.Add(material);
				renderer.SetSharedMaterials(m_textMeshRendererMaterials.ToArray());
			}
			else
			{
				List<Material> sharedMaterials = renderer.GetSharedMaterials();
				m_textMeshRendererMaterials[materialIndex] = material;
				sharedMaterials[materialIndex] = material;
				renderer.SetSharedMaterials(sharedMaterials);
			}
		}
		else if (!currentMaterial.IsStillBound(renderer))
		{
			currentMaterial.Rebound(renderer, materialIndex);
		}
		return currentMaterial;
	}
}
