using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A45 RID: 2629
[AddComponentMenu("Pixelplacement/iTweenPath")]
public class iTweenPath : MonoBehaviour
{
	// Token: 0x06008E40 RID: 36416 RVA: 0x002DE378 File Offset: 0x002DC578
	private void OnEnable()
	{
		string key = iTweenPath.FixupPathName(this.pathName);
		if (!iTweenPath.paths.ContainsKey(key))
		{
			iTweenPath.paths.Add(key, this);
		}
	}

	// Token: 0x06008E41 RID: 36417 RVA: 0x002DE3AA File Offset: 0x002DC5AA
	private void OnDisable()
	{
		iTweenPath.paths.Remove(iTweenPath.FixupPathName(this.pathName));
	}

	// Token: 0x06008E42 RID: 36418 RVA: 0x002DE3C2 File Offset: 0x002DC5C2
	private void OnDrawGizmosSelected()
	{
		if (this.pathVisible && this.nodes.Count > 0)
		{
			iTween.DrawPath(this.nodes.ToArray(), this.pathColor);
		}
	}

	// Token: 0x06008E43 RID: 36419 RVA: 0x002DE3F0 File Offset: 0x002DC5F0
	public static Vector3[] GetPath(string requestedName)
	{
		requestedName = iTweenPath.FixupPathName(requestedName);
		if (iTweenPath.paths.ContainsKey(requestedName))
		{
			return iTweenPath.paths[requestedName].nodes.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	// Token: 0x06008E44 RID: 36420 RVA: 0x002DE440 File Offset: 0x002DC640
	public static Vector3[] GetPathReversed(string requestedName)
	{
		requestedName = iTweenPath.FixupPathName(requestedName);
		if (iTweenPath.paths.ContainsKey(requestedName))
		{
			List<Vector3> range = iTweenPath.paths[requestedName].nodes.GetRange(0, iTweenPath.paths[requestedName].nodes.Count);
			range.Reverse();
			return range.ToArray();
		}
		Debug.Log("No path with that name (" + requestedName + ") exists! Are you sure you wrote it correctly?");
		return null;
	}

	// Token: 0x06008E45 RID: 36421 RVA: 0x002DE4AF File Offset: 0x002DC6AF
	public static string FixupPathName(string name)
	{
		return name.ToLower();
	}

	// Token: 0x0400763C RID: 30268
	public string pathName = "";

	// Token: 0x0400763D RID: 30269
	public Color pathColor = Color.cyan;

	// Token: 0x0400763E RID: 30270
	public List<Vector3> nodes = new List<Vector3>
	{
		Vector3.zero,
		Vector3.zero
	};

	// Token: 0x0400763F RID: 30271
	public static Map<string, iTweenPath> paths = new Map<string, iTweenPath>();

	// Token: 0x04007640 RID: 30272
	public bool initialized;

	// Token: 0x04007641 RID: 30273
	public string initialName = "";

	// Token: 0x04007642 RID: 30274
	public bool pathVisible = true;
}
