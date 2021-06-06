using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pixelplacement/iTweenPath")]
public class iTweenPath : MonoBehaviour
{
	public string pathName = "";

	public Color pathColor = Color.cyan;

	public List<Vector3> nodes = new List<Vector3>
	{
		Vector3.zero,
		Vector3.zero
	};

	public static Map<string, iTweenPath> paths = new Map<string, iTweenPath>();

	public bool initialized;

	public string initialName = "";

	public bool pathVisible = true;

	private void OnEnable()
	{
		string key = FixupPathName(pathName);
		if (!paths.ContainsKey(key))
		{
			paths.Add(key, this);
		}
	}

	private void OnDisable()
	{
		paths.Remove(FixupPathName(pathName));
	}

	private void OnDrawGizmosSelected()
	{
		if (pathVisible && nodes.Count > 0)
		{
			iTween.DrawPath(nodes.ToArray(), pathColor);
		}
	}

	public static Vector3[] GetPath(string requestedName)
	{
		requestedName = FixupPathName(requestedName);
		if (paths.ContainsKey(requestedName))
		{
			return paths[requestedName].nodes.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	public static Vector3[] GetPathReversed(string requestedName)
	{
		requestedName = FixupPathName(requestedName);
		if (paths.ContainsKey(requestedName))
		{
			List<Vector3> range = paths[requestedName].nodes.GetRange(0, paths[requestedName].nodes.Count);
			range.Reverse();
			return range.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	public static string FixupPathName(string name)
	{
		return name.ToLower();
	}
}
