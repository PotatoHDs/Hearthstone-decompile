using UnityEngine;

public class AdventureUITestScene : MonoBehaviour
{
	private void Start()
	{
		PegUI.Get().AddInputCamera(Box.Get().m_Camera.GetComponent<Camera>());
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(Box.Get().m_Camera.GetComponent<Camera>());
		}
	}
}
