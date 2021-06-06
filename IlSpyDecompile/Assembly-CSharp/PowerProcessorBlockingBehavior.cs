using System;
using UnityEngine;

[Serializable]
public class PowerProcessorBlockingBehavior
{
	[Tooltip("If checked, this spell will block power history processing when the spell enters the Death state.")]
	public bool m_OnEnterDeathState;
}
