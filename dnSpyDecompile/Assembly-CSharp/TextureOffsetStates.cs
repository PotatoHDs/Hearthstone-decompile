using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000AA0 RID: 2720
public class TextureOffsetStates : MonoBehaviour
{
	// Token: 0x06009116 RID: 37142 RVA: 0x002F0DB7 File Offset: 0x002EEFB7
	private void Awake()
	{
		this.m_originalMaterial = base.GetComponent<Renderer>().GetSharedMaterial();
	}

	// Token: 0x17000830 RID: 2096
	// (get) Token: 0x06009117 RID: 37143 RVA: 0x002F0DCA File Offset: 0x002EEFCA
	// (set) Token: 0x06009118 RID: 37144 RVA: 0x002F0DD4 File Offset: 0x002EEFD4
	public string CurrentState
	{
		get
		{
			return this.m_currentState;
		}
		set
		{
			TextureOffsetState textureOffsetState = this.m_states.FirstOrDefault((TextureOffsetState s) => s.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
			if (textureOffsetState != null)
			{
				this.m_currentState = value;
				Renderer component = base.GetComponent<Renderer>();
				if (textureOffsetState.Material == null)
				{
					component.SetMaterial(this.m_originalMaterial);
				}
				else
				{
					component.SetMaterial(textureOffsetState.Material);
				}
				component.GetMaterial().mainTextureOffset = textureOffsetState.Offset;
			}
		}
	}

	// Token: 0x040079E1 RID: 31201
	public TextureOffsetState[] m_states;

	// Token: 0x040079E2 RID: 31202
	private string m_currentState;

	// Token: 0x040079E3 RID: 31203
	private Material m_originalMaterial;
}
