using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020009BD RID: 2493
public class LightFileInfo
{
	// Token: 0x06008830 RID: 34864 RVA: 0x002BDE7C File Offset: 0x002BC07C
	public LightFileInfo(string path)
	{
		this.FullName = path;
		string[] array = path.Split(FileUtils.FOLDER_SEPARATOR_CHARS);
		this.Name = array[array.Length - 1];
		this.DirectoryName = array[array.Length - 2];
	}

	// Token: 0x17000797 RID: 1943
	// (get) Token: 0x06008831 RID: 34865 RVA: 0x002BDEBC File Offset: 0x002BC0BC
	public string Path
	{
		get
		{
			return this.FullName;
		}
	}

	// Token: 0x06008832 RID: 34866 RVA: 0x002BDEC4 File Offset: 0x002BC0C4
	public static IEnumerable<LightFileInfo> Search(string path, string extension)
	{
		return LightFileInfo.Search(new string[]
		{
			path
		}, extension);
	}

	// Token: 0x06008833 RID: 34867 RVA: 0x002BDED8 File Offset: 0x002BC0D8
	public static IEnumerable<LightFileInfo> Search(string[] paths, string extension)
	{
		List<LightFileInfo> list = new List<LightFileInfo>();
		foreach (string path in paths)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			LightFileInfo._Search(list, path, extension);
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		}
		return list;
	}

	// Token: 0x06008834 RID: 34868 RVA: 0x002BDF14 File Offset: 0x002BC114
	public static void _Search(List<LightFileInfo> matches, string path, string extension)
	{
		foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles(string.Format("*{0}", extension), SearchOption.AllDirectories))
		{
			matches.Add(new LightFileInfo(fileInfo.FullName));
		}
	}

	// Token: 0x0400724C RID: 29260
	public readonly string FullName;

	// Token: 0x0400724D RID: 29261
	public readonly string Name;

	// Token: 0x0400724E RID: 29262
	public readonly string DirectoryName;
}
