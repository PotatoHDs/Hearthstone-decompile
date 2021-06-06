using System;
using System.Linq;
using UnityEngine;

public class TextureOffsetStates : MonoBehaviour
{
	public TextureOffsetState[] m_states;

	private string m_currentState;

	private Material m_originalMaterial;

	public string CurrentState
	{
		get
		{
			return m_currentState;
		}
		set
		{
			TextureOffsetState textureOffsetState = m_states.FirstOrDefault((TextureOffsetState s) => s.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));
			if (textureOffsetState != null)
			{
				m_currentState = value;
				Renderer component = GetComponent<Renderer>();
				if (textureOffsetState.Material == null)
				{
					component.SetMaterial(m_originalMaterial);
				}
				else
				{
					component.SetMaterial(textureOffsetState.Material);
				}
				component.GetMaterial().mainTextureOffset = textureOffsetState.Offset;
			}
		}
	}

	private void Awake()
	{
		m_originalMaterial = GetComponent<Renderer>().GetSharedMaterial();
	}
}
