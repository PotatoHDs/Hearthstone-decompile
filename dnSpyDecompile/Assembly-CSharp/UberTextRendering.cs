using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000ABE RID: 2750
public class UberTextRendering
{
	// Token: 0x060092B6 RID: 37558 RVA: 0x002F962A File Offset: 0x002F782A
	public void Init(GameObject parent)
	{
		if (this.m_isInit && this.m_isRenderToTexture)
		{
			this.Destroy();
		}
		if (!this.m_isInit || !this.m_textMeshGameObject)
		{
			this.m_isInit = true;
			this.CreateTextMeshObject(parent);
		}
	}

	// Token: 0x060092B7 RID: 37559 RVA: 0x002F9668 File Offset: 0x002F7868
	public void Destroy()
	{
		if (this.m_textMesh)
		{
			SceneUtils.SafeDestroy(this.m_textMesh.gameObject);
			this.m_textMesh = null;
		}
		if (this.m_textMeshGameObject)
		{
			SceneUtils.SafeDestroy(this.m_textMeshGameObject);
			this.m_textMeshGameObject = null;
		}
		this.m_lastTextMeshText = string.Empty;
		this.m_isInit = false;
		UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
		uberTextMaterialManager.ReleaseMaterial(this.m_mainMaterial);
		uberTextMaterialManager.ReleaseMaterial(this.m_boldMaterial);
	}

	// Token: 0x060092B8 RID: 37560 RVA: 0x002F96E8 File Offset: 0x002F78E8
	public void SetTextMeshRenderToTextureTransform(float offset)
	{
		Vector3 position = new Vector3(-3000f, 3000f, offset);
		this.m_textMeshGameObject.transform.parent = null;
		this.m_textMeshGameObject.transform.position = position;
		this.m_textMeshGameObject.transform.rotation = Quaternion.identity;
		this.SetLayer(UberTextRendering.RENDER_LAYER);
	}

	// Token: 0x060092B9 RID: 37561 RVA: 0x002F974C File Offset: 0x002F794C
	public void SetTextMeshTransform(GameObject parent, Vector3 textCenter, Quaternion textRotation)
	{
		this.m_textMeshGameObject.transform.parent = parent.transform;
		this.m_textMeshGameObject.transform.localPosition = textCenter;
		this.m_textMeshGameObject.transform.localRotation = textRotation;
		this.m_textMeshGameObject.transform.localScale = Vector3.one;
		this.SetLayer(parent.layer);
	}

	// Token: 0x060092BA RID: 37562 RVA: 0x002F97B2 File Offset: 0x002F79B2
	public void SetLayer(int layer)
	{
		if (this.m_textMeshGameObject)
		{
			this.m_textMeshGameObject.layer = layer;
		}
	}

	// Token: 0x060092BB RID: 37563 RVA: 0x002F97D0 File Offset: 0x002F79D0
	public bool HasLayer(int layer)
	{
		bool result = false;
		if (this.m_textMeshGameObject)
		{
			result = (this.m_textMeshGameObject.layer == layer);
		}
		return result;
	}

	// Token: 0x060092BC RID: 37564 RVA: 0x002F97FC File Offset: 0x002F79FC
	public void SetActive(bool active)
	{
		if (this.m_textMeshGameObject)
		{
			this.m_textMeshGameObject.SetActive(active);
		}
	}

	// Token: 0x060092BD RID: 37565 RVA: 0x002F9818 File Offset: 0x002F7A18
	public void ApplyMaterials()
	{
		if (this.m_outlineEnabled)
		{
			this.m_mainMaterial = this.ApplyMaterialIfNeeded(this.m_textMeshRenderer, this.m_mainMaterial, this.m_outlineTextMaterialQuery, 0);
			if (this.m_boldEnabled)
			{
				this.m_outlineBoldMaterialQuery.WithMinimalRenderQueueValue(this.m_mainMaterial.GetRenderQueue());
				this.m_boldMaterial = this.ApplyMaterialIfNeeded(this.m_textMeshRenderer, this.m_boldMaterial, this.m_outlineBoldMaterialQuery, 1);
			}
		}
		else
		{
			this.m_mainMaterial = this.ApplyMaterialIfNeeded(this.m_textMeshRenderer, this.m_mainMaterial, this.m_unlitTextMaterialQuery, 0);
			if (this.m_boldEnabled)
			{
				this.m_boldTextMaterialQuery.WithMinimalRenderQueueValue(this.m_mainMaterial.GetRenderQueue());
				this.m_boldMaterial = this.ApplyMaterialIfNeeded(this.m_textMeshRenderer, this.m_boldMaterial, this.m_boldTextMaterialQuery, 1);
			}
		}
		if (!this.m_boldEnabled && this.m_boldMaterial != null && this.m_boldMaterial.IsStillBound(this.m_textMeshRenderer))
		{
			UberTextMaterialManager.Get().UnboundMaterial(this.m_boldMaterial, this.m_textMeshRenderer);
			this.m_boldMaterial = null;
		}
	}

	// Token: 0x060092BE RID: 37566 RVA: 0x002F992C File Offset: 0x002F7B2C
	public void SetVisible(bool visible)
	{
		if (this.m_textMeshRenderer)
		{
			this.m_textMeshRenderer.enabled = visible;
		}
	}

	// Token: 0x060092BF RID: 37567 RVA: 0x002F9947 File Offset: 0x002F7B47
	public void SetBoldEnabled(bool enabled)
	{
		this.m_boldEnabled = enabled;
	}

	// Token: 0x060092C0 RID: 37568 RVA: 0x002F9950 File Offset: 0x002F7B50
	public void SetOutlineEnabled(bool enabled)
	{
		this.m_outlineEnabled = enabled;
	}

	// Token: 0x060092C1 RID: 37569 RVA: 0x002F9959 File Offset: 0x002F7B59
	public Bounds GetTextMeshBounds()
	{
		if (!this.m_textMeshRenderer)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		return this.m_textMeshRenderer.bounds;
	}

	// Token: 0x060092C2 RID: 37570 RVA: 0x002F9983 File Offset: 0x002F7B83
	public void SetCharacterSize(float characterSize)
	{
		if (!this.m_textMesh || Mathf.Approximately(this.m_textMesh.characterSize, characterSize))
		{
			return;
		}
		this.m_textMesh.characterSize = characterSize;
	}

	// Token: 0x060092C3 RID: 37571 RVA: 0x002F99B2 File Offset: 0x002F7BB2
	public float GetCharacterSize()
	{
		if (!this.m_textMesh)
		{
			return 0f;
		}
		return this.m_textMesh.characterSize;
	}

	// Token: 0x060092C4 RID: 37572 RVA: 0x002F99D2 File Offset: 0x002F7BD2
	public void SetText(string text)
	{
		if (text == null)
		{
			text = string.Empty;
		}
		if (this.m_lastTextMeshText != text)
		{
			this.m_textMesh.text = text;
			this.m_lastTextMeshText = text;
		}
	}

	// Token: 0x060092C5 RID: 37573 RVA: 0x002F99FF File Offset: 0x002F7BFF
	public string GetText()
	{
		return this.m_lastTextMeshText;
	}

	// Token: 0x060092C6 RID: 37574 RVA: 0x002F9A07 File Offset: 0x002F7C07
	public void SetRichText(bool richText)
	{
		if (this.m_textMesh && this.m_textMesh.richText != richText)
		{
			this.m_textMesh.richText = richText;
		}
		this.m_outlineTextMaterialQuery.WithRichText(richText);
	}

	// Token: 0x060092C7 RID: 37575 RVA: 0x002F9A3D File Offset: 0x002F7C3D
	public void SetLineSpacing(float lineSpacing)
	{
		if (!this.m_textMesh || Mathf.Approximately(this.m_textMesh.lineSpacing, lineSpacing))
		{
			return;
		}
		this.m_textMesh.lineSpacing = lineSpacing;
	}

	// Token: 0x060092C8 RID: 37576 RVA: 0x002F9A6C File Offset: 0x002F7C6C
	public bool CanSetFont(Font font)
	{
		return this.m_textMesh && this.m_textMesh.font != font;
	}

	// Token: 0x060092C9 RID: 37577 RVA: 0x002F9A90 File Offset: 0x002F7C90
	public void SetFont(Font font)
	{
		if (!this.m_textMesh || this.m_textMesh.font == font)
		{
			return;
		}
		this.m_textMesh.font = font;
		this.m_fontTexture = font.material.mainTexture;
		this.m_unlitTextMaterialQuery.WithTexture(this.m_fontTexture);
		this.m_boldTextMaterialQuery.WithTexture(this.m_fontTexture);
		this.m_outlineTextMaterialQuery.WithTexture(this.m_fontTexture);
		this.m_outlineBoldMaterialQuery.WithTexture(this.m_fontTexture);
	}

	// Token: 0x060092CA RID: 37578 RVA: 0x002F9B23 File Offset: 0x002F7D23
	public Font GetFont()
	{
		TextMesh textMesh = this.m_textMesh;
		if (textMesh == null)
		{
			return null;
		}
		return textMesh.font;
	}

	// Token: 0x060092CB RID: 37579 RVA: 0x002F9B36 File Offset: 0x002F7D36
	public Texture GetFontTexture()
	{
		return this.m_fontTexture;
	}

	// Token: 0x060092CC RID: 37580 RVA: 0x002F9B3E File Offset: 0x002F7D3E
	public void SetFontSize(int fontSize)
	{
		if (!this.m_textMesh || this.m_textMesh.fontSize == fontSize)
		{
			return;
		}
		this.m_textMesh.fontSize = fontSize;
	}

	// Token: 0x060092CD RID: 37581 RVA: 0x002F9B68 File Offset: 0x002F7D68
	public int GetFontSize()
	{
		if (!this.m_textMesh)
		{
			return 0;
		}
		return this.m_textMesh.fontSize;
	}

	// Token: 0x060092CE RID: 37582 RVA: 0x002F9B84 File Offset: 0x002F7D84
	public TextAnchor GetTextAnchor()
	{
		if (!this.m_textMesh)
		{
			return TextAnchor.MiddleCenter;
		}
		return this.m_textMesh.anchor;
	}

	// Token: 0x060092CF RID: 37583 RVA: 0x002F9BA0 File Offset: 0x002F7DA0
	public void SetTextMeshGameObjectLocalPositionOffset(Vector3 offset)
	{
		this.m_textMeshGameObject.transform.localPosition += offset;
	}

	// Token: 0x060092D0 RID: 37584 RVA: 0x002F9BBE File Offset: 0x002F7DBE
	public void SetTextMeshGameObjectLocalPosition(Vector3 localPosition)
	{
		this.m_textMeshGameObject.transform.localPosition = localPosition;
	}

	// Token: 0x060092D1 RID: 37585 RVA: 0x002F9BD1 File Offset: 0x002F7DD1
	public void SetTextMeshGameObjectPosition(Vector3 position)
	{
		this.m_textMeshGameObject.transform.position = position;
	}

	// Token: 0x060092D2 RID: 37586 RVA: 0x002F9BE4 File Offset: 0x002F7DE4
	public void SetTextMeshGameObjectLocalScale(Vector3 scale)
	{
		this.m_textMeshGameObject.transform.localScale = scale;
	}

	// Token: 0x060092D3 RID: 37587 RVA: 0x002F9BF7 File Offset: 0x002F7DF7
	public void SetTextMeshGameObjectRotation(Quaternion rotation)
	{
		this.m_textMeshGameObject.transform.rotation = rotation;
	}

	// Token: 0x060092D4 RID: 37588 RVA: 0x002F9C0A File Offset: 0x002F7E0A
	public void SetTextMeshGameObjectParent(Transform parent)
	{
		this.m_textMeshGameObject.transform.parent = parent;
	}

	// Token: 0x060092D5 RID: 37589 RVA: 0x002F9C1D File Offset: 0x002F7E1D
	public void SetTextAlignment(UberText.AlignmentOptions alignment)
	{
		if (alignment == UberText.AlignmentOptions.Left)
		{
			this.m_textMesh.alignment = TextAlignment.Left;
			return;
		}
		if (alignment == UberText.AlignmentOptions.Right)
		{
			this.m_textMesh.alignment = TextAlignment.Right;
			return;
		}
		this.m_textMesh.alignment = TextAlignment.Center;
	}

	// Token: 0x060092D6 RID: 37590 RVA: 0x002F9C4C File Offset: 0x002F7E4C
	public void SetTextAnchor(TextAnchor anchor)
	{
		if (!this.m_textMesh || this.m_textMesh.anchor == anchor)
		{
			return;
		}
		this.m_textMesh.anchor = anchor;
	}

	// Token: 0x060092D7 RID: 37591 RVA: 0x002F9C78 File Offset: 0x002F7E78
	public UberTextRendering.TransformBackup BackupTextMeshTransform()
	{
		Transform transform = this.m_textMeshGameObject.transform;
		return new UberTextRendering.TransformBackup
		{
			Parent = transform.parent,
			Position = transform.position,
			Rotation = transform.rotation,
			LocalScale = transform.localScale,
			LocalPosition = transform.localPosition
		};
	}

	// Token: 0x060092D8 RID: 37592 RVA: 0x002F9CDB File Offset: 0x002F7EDB
	public void SetTextColor(Color color)
	{
		this.m_unlitTextMaterialQuery.WithColor(color);
		this.m_boldTextMaterialQuery.WithColor(color);
		this.m_outlineTextMaterialQuery.WithColor(color);
		this.m_outlineBoldMaterialQuery.WithColor(color);
	}

	// Token: 0x060092D9 RID: 37593 RVA: 0x002F9D11 File Offset: 0x002F7F11
	public void SetOutlineColor(Color color)
	{
		this.m_outlineTextMaterialQuery.WithOutlineColor(color);
		this.m_outlineBoldMaterialQuery.WithOutlineColor(color);
	}

	// Token: 0x060092DA RID: 37594 RVA: 0x002F9D2D File Offset: 0x002F7F2D
	public void SetAmbientLightBlend(float ambientLightBlend)
	{
		this.m_unlitTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		this.m_boldTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		this.m_outlineTextMaterialQuery.WithLightingBlend(ambientLightBlend);
		this.m_outlineBoldMaterialQuery.WithLightingBlend(ambientLightBlend);
	}

	// Token: 0x060092DB RID: 37595 RVA: 0x002F9D63 File Offset: 0x002F7F63
	public void SetOutlineOffset(Vector2 offset)
	{
		this.m_outlineTextMaterialQuery.WithOutlineOffsetX(offset.x);
		this.m_outlineTextMaterialQuery.WithOutlineOffsetY(offset.y);
	}

	// Token: 0x060092DC RID: 37596 RVA: 0x002F9D89 File Offset: 0x002F7F89
	public void SetTexelSize(Vector2 texelSize)
	{
		this.m_outlineTextMaterialQuery.WithTexelSizeX(texelSize.x);
		this.m_outlineTextMaterialQuery.WithTexelSizeY(texelSize.y);
	}

	// Token: 0x060092DD RID: 37597 RVA: 0x002F9DB0 File Offset: 0x002F7FB0
	public void SetBoldOffset(Vector2 boldOffset)
	{
		this.m_boldTextMaterialQuery.WithBoldOffsetX(boldOffset.x);
		this.m_boldTextMaterialQuery.WithBoldOffsetY(boldOffset.y);
		this.m_outlineBoldMaterialQuery.WithBoldOffsetX(boldOffset.x);
		this.m_outlineBoldMaterialQuery.WithBoldOffsetY(boldOffset.y);
	}

	// Token: 0x060092DE RID: 37598 RVA: 0x002F9E05 File Offset: 0x002F8005
	public void SetOutlineBoldOffset(Vector2 offset)
	{
		this.m_outlineBoldMaterialQuery.WithOutlineOffsetX(offset.x);
		this.m_outlineBoldMaterialQuery.WithOutlineOffsetY(offset.y);
	}

	// Token: 0x060092DF RID: 37599 RVA: 0x002F9E2B File Offset: 0x002F802B
	public MeshRenderer GetTextMeshRenderer()
	{
		return this.m_textMeshRenderer;
	}

	// Token: 0x060092E0 RID: 37600 RVA: 0x002F9E33 File Offset: 0x002F8033
	public void SetLocale(Locale locale)
	{
		this.m_outlineTextMaterialQuery.WithLocale(locale);
	}

	// Token: 0x060092E1 RID: 37601 RVA: 0x002F9E42 File Offset: 0x002F8042
	public PopupRenderer GetPopupRenderer()
	{
		if (!this.m_popupRendererComponent)
		{
			this.m_popupRendererComponent = this.m_textMeshGameObject.AddComponent<PopupRenderer>();
		}
		return this.m_popupRendererComponent;
	}

	// Token: 0x060092E2 RID: 37602 RVA: 0x002F9E68 File Offset: 0x002F8068
	public void DisablePopupRenderer()
	{
		if (this.m_popupRendererComponent)
		{
			this.m_popupRendererComponent.DisablePopupRendering();
		}
	}

	// Token: 0x060092E3 RID: 37603 RVA: 0x002F9E84 File Offset: 0x002F8084
	public void SetRenderQueueIncrement(int value)
	{
		if (this.m_textMesh)
		{
			this.m_textMeshRenderer.sortingOrder = value;
		}
		this.m_unlitTextMaterialQuery.WithIncrementRenderQueue(value);
		this.m_boldTextMaterialQuery.WithIncrementRenderQueue(value);
		this.m_outlineTextMaterialQuery.WithIncrementRenderQueue(value);
		this.m_outlineBoldMaterialQuery.WithIncrementRenderQueue(value);
	}

	// Token: 0x060092E4 RID: 37604 RVA: 0x002F9EE0 File Offset: 0x002F80E0
	private void CreateTextMeshObject(GameObject parent)
	{
		if (!this.m_textMeshGameObject)
		{
			string text = "UberText_RenderObject_" + parent.name;
			for (int i = parent.transform.childCount - 1; i >= 0; i--)
			{
				Transform child = parent.transform.GetChild(i);
				if (child && child.gameObject.name == text)
				{
					SceneUtils.SafeDestroy(child.gameObject);
				}
			}
			this.m_textMeshGameObject = new GameObject();
			this.m_textMeshGameObject.name = text;
			SceneUtils.SetHideFlags(this.m_textMeshGameObject, HideFlags.HideAndDontSave);
			this.m_textMeshRenderer = this.m_textMeshGameObject.AddComponent<MeshRenderer>();
			this.m_textMesh = this.m_textMeshGameObject.AddComponent<TextMesh>();
			this.SetText(string.Empty);
		}
	}

	// Token: 0x060092E5 RID: 37605 RVA: 0x002F9FAC File Offset: 0x002F81AC
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
			this.m_textMeshRendererMaterials.Clear();
			renderer.GetSharedMaterials(this.m_textMeshRendererMaterials);
			if (this.m_textMeshRendererMaterials.Count <= materialIndex)
			{
				this.m_textMeshRendererMaterials.Add(material);
				renderer.SetSharedMaterials(this.m_textMeshRendererMaterials.ToArray());
			}
			else
			{
				List<Material> sharedMaterials = renderer.GetSharedMaterials();
				this.m_textMeshRendererMaterials[materialIndex] = material;
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

	// Token: 0x04007B02 RID: 31490
	private static int RENDER_LAYER = 28;

	// Token: 0x04007B03 RID: 31491
	private TextMesh m_textMesh;

	// Token: 0x04007B04 RID: 31492
	private MeshRenderer m_textMeshRenderer;

	// Token: 0x04007B05 RID: 31493
	private GameObject m_textMeshGameObject;

	// Token: 0x04007B06 RID: 31494
	private bool m_isInit;

	// Token: 0x04007B07 RID: 31495
	private bool m_isRenderToTexture;

	// Token: 0x04007B08 RID: 31496
	private string m_lastTextMeshText = string.Empty;

	// Token: 0x04007B09 RID: 31497
	private Texture m_fontTexture;

	// Token: 0x04007B0A RID: 31498
	private UnlitTextMaterialQuery m_unlitTextMaterialQuery = new UnlitTextMaterialQuery();

	// Token: 0x04007B0B RID: 31499
	private BoldTextMaterialQuery m_boldTextMaterialQuery = new BoldTextMaterialQuery();

	// Token: 0x04007B0C RID: 31500
	private OutlineTextMaterialQuery m_outlineTextMaterialQuery = new OutlineTextMaterialQuery();

	// Token: 0x04007B0D RID: 31501
	private OutlineBoldTextMaterialQuery m_outlineBoldMaterialQuery = new OutlineBoldTextMaterialQuery();

	// Token: 0x04007B0E RID: 31502
	private List<Material> m_textMeshRendererMaterials = new List<Material>();

	// Token: 0x04007B0F RID: 31503
	private UberTextMaterial m_mainMaterial;

	// Token: 0x04007B10 RID: 31504
	private UberTextMaterial m_boldMaterial;

	// Token: 0x04007B11 RID: 31505
	private PopupRenderer m_popupRendererComponent;

	// Token: 0x04007B12 RID: 31506
	private bool m_boldEnabled;

	// Token: 0x04007B13 RID: 31507
	private bool m_outlineEnabled;

	// Token: 0x020026EF RID: 9967
	public struct TransformBackup
	{
		// Token: 0x0400F2A3 RID: 62115
		public Transform Parent;

		// Token: 0x0400F2A4 RID: 62116
		public Vector3 Position;

		// Token: 0x0400F2A5 RID: 62117
		public Vector3 LocalPosition;

		// Token: 0x0400F2A6 RID: 62118
		public Quaternion Rotation;

		// Token: 0x0400F2A7 RID: 62119
		public Vector3 LocalScale;
	}
}
