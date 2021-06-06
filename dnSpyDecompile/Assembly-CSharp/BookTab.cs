using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class BookTab : PegUIElement
{
	// Token: 0x06000D59 RID: 3417 RVA: 0x0004C9B5 File Offset: 0x0004ABB5
	public void Init()
	{
		this.SetTabIconsTextureOffset(base.gameObject.GetComponent<Renderer>());
		if (this.m_glowMesh != null)
		{
			this.SetTabIconsTextureOffset(this.m_glowMesh.GetComponent<Renderer>());
		}
		this.SetGlowActive(false);
		this.UpdateNewItemCount(0);
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0004C9F5 File Offset: 0x0004ABF5
	public void SetGlowActive(bool active)
	{
		if (this.m_selected)
		{
			active = true;
		}
		if (this.m_glowMesh != null)
		{
			this.m_glowMesh.SetActive(active);
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004CA1C File Offset: 0x0004AC1C
	public void SetSelected(bool selected)
	{
		if (this.m_selected == selected)
		{
			return;
		}
		this.m_selected = selected;
		this.SetGlowActive(this.m_selected);
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0004CA3B File Offset: 0x0004AC3B
	public void UpdateNewItemCount(int numNewItems)
	{
		this.m_numNewItems = numNewItems;
		this.UpdateNewItemCountVisuals();
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0004CA4A File Offset: 0x0004AC4A
	public void SetTargetLocalPosition(Vector3 targetLocalPos)
	{
		this.m_targetLocalPos = targetLocalPos;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0004CA53 File Offset: 0x0004AC53
	public void SetIsVisible(bool isVisible)
	{
		this.m_isVisible = isVisible;
		this.SetEnabled(this.m_isVisible, false);
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0004CA69 File Offset: 0x0004AC69
	public bool IsVisible()
	{
		return this.m_isVisible;
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0004CA71 File Offset: 0x0004AC71
	public void SetTargetVisibility(bool visible)
	{
		this.m_shouldBeVisible = visible;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0004CA7A File Offset: 0x0004AC7A
	public bool ShouldBeVisible()
	{
		return this.m_shouldBeVisible;
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0004CA82 File Offset: 0x0004AC82
	public bool WillSlide()
	{
		return Mathf.Abs(this.m_targetLocalPos.x - base.transform.localPosition.x) > 0.05f;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0004CAB0 File Offset: 0x0004ACB0
	public void AnimateToTargetPosition(float animationTime, iTween.EaseType easeType)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_targetLocalPos,
			"isLocal",
			true,
			"time",
			animationTime,
			"easetype",
			easeType,
			"name",
			"position",
			"oncomplete",
			"OnMovedToTargetPos",
			"oncompletetarget",
			base.gameObject
		});
		iTween.StopByName(base.gameObject, "position");
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0004CB68 File Offset: 0x0004AD68
	public void SetLargeTab(bool large)
	{
		if (large == this.m_showLargeTab)
		{
			return;
		}
		if (large)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = this.m_SelectedLocalYPos;
			base.transform.localPosition = localPosition;
			Hashtable args = iTween.Hash(new object[]
			{
				"scale",
				this.m_SelectedLocalScale,
				"time",
				BookTab.SELECT_TAB_ANIM_TIME,
				"name",
				"scale"
			});
			iTween.ScaleTo(base.gameObject, args);
			SoundManager.Get().LoadAndPlay("class_tab_click.prefab:d9cb832f0de5c1947a97685e134ba0da", base.gameObject);
		}
		else
		{
			Vector3 localPosition2 = base.transform.localPosition;
			localPosition2.y = this.m_DeselectedLocalYPos;
			base.transform.localPosition = localPosition2;
			iTween.StopByName(base.gameObject, "scale");
			base.transform.localScale = this.m_DeselectedLocalScale;
		}
		this.m_showLargeTab = large;
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0004CC66 File Offset: 0x0004AE66
	protected virtual Vector2 GetTextureOffset()
	{
		return Vector2.zero;
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0004CC70 File Offset: 0x0004AE70
	protected void SetTabIconsTextureOffset(Renderer renderer)
	{
		if (renderer == null || string.IsNullOrEmpty(this.m_IconTextureName))
		{
			return;
		}
		if (this.m_propertyBlock == null)
		{
			this.m_propertyBlock = new MaterialPropertyBlock();
		}
		Vector2 textureOffset = this.GetTextureOffset();
		Vector4 value = new Vector4(1f, 1f, textureOffset.x, textureOffset.y);
		List<Material> sharedMaterials = renderer.GetSharedMaterials();
		for (int i = 0; i < sharedMaterials.Count; i++)
		{
			Material material = sharedMaterials[i];
			if (!(material.mainTexture == null) && material.mainTexture.name.Contains(this.m_IconTextureName))
			{
				renderer.GetPropertyBlock(this.m_propertyBlock, i);
				this.m_propertyBlock.SetVector("_MainTex_ST", value);
				renderer.SetPropertyBlock(this.m_propertyBlock, i);
			}
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0004CD40 File Offset: 0x0004AF40
	private void UpdateNewItemCountVisuals()
	{
		if (this.m_newItemCountText != null)
		{
			this.m_newItemCountText.Text = GameStrings.Format("GLUE_COLLECTION_NEW_CARD_CALLOUT", new object[]
			{
				this.m_numNewItems
			});
		}
		if (this.m_newItemCount != null)
		{
			this.m_newItemCount.SetActive(this.m_numNewItems > 0);
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0004CDA8 File Offset: 0x0004AFA8
	private void OnMovedToTargetPos()
	{
		if (this.m_showLargeTab)
		{
			return;
		}
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = this.m_DeselectedLocalYPos;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x04000918 RID: 2328
	public GameObject m_glowMesh;

	// Token: 0x04000919 RID: 2329
	public GameObject m_newItemCount;

	// Token: 0x0400091A RID: 2330
	public UberText m_newItemCountText;

	// Token: 0x0400091B RID: 2331
	public CollectionUtils.ViewMode m_tabViewMode;

	// Token: 0x0400091C RID: 2332
	public Vector3 m_DeselectedLocalScale = new Vector3(0.44f, 0.44f, 0.44f);

	// Token: 0x0400091D RID: 2333
	public Vector3 m_SelectedLocalScale = new Vector3(0.66f, 0.66f, 0.66f);

	// Token: 0x0400091E RID: 2334
	public float m_SelectedLocalYPos = 0.1259841f;

	// Token: 0x0400091F RID: 2335
	public float m_DeselectedLocalYPos;

	// Token: 0x04000920 RID: 2336
	public string m_IconTextureName;

	// Token: 0x04000921 RID: 2337
	protected int m_numNewItems;

	// Token: 0x04000922 RID: 2338
	protected bool m_selected;

	// Token: 0x04000923 RID: 2339
	protected Vector3 m_targetLocalPos;

	// Token: 0x04000924 RID: 2340
	protected bool m_shouldBeVisible = true;

	// Token: 0x04000925 RID: 2341
	protected bool m_isVisible = true;

	// Token: 0x04000926 RID: 2342
	protected bool m_showLargeTab;

	// Token: 0x04000927 RID: 2343
	protected MaterialPropertyBlock m_propertyBlock;

	// Token: 0x04000928 RID: 2344
	public static readonly float SELECT_TAB_ANIM_TIME = 0.2f;
}
