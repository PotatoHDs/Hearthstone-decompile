using UnityEngine;

public class DrawWireframe : MonoBehaviour
{
	private void OnPreRender()
	{
		GL.wireframe = true;
	}

	private void OnPostRender()
	{
		GL.wireframe = false;
	}
}
