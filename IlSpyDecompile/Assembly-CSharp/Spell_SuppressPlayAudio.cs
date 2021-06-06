using UnityEngine;

public class Spell_SuppressPlayAudio : Spell
{
	public override void SetSource(GameObject go)
	{
		m_source = go;
		if (!(m_source == null))
		{
			Card component = m_source.GetComponent<Card>();
			if (component != null)
			{
				component.SuppressPlaySounds(suppress: true);
			}
		}
	}
}
