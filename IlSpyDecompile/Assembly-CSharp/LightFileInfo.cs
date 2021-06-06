using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LightFileInfo
{
	public readonly string FullName;

	public readonly string Name;

	public readonly string DirectoryName;

	public string Path => FullName;

	public LightFileInfo(string path)
	{
		FullName = path;
		string[] array = path.Split(FileUtils.FOLDER_SEPARATOR_CHARS);
		Name = array[array.Length - 1];
		DirectoryName = array[array.Length - 2];
	}

	public static IEnumerable<LightFileInfo> Search(string path, string extension)
	{
		return Search(new string[1] { path }, extension);
	}

	public static IEnumerable<LightFileInfo> Search(string[] paths, string extension)
	{
		List<LightFileInfo> list = new List<LightFileInfo>();
		foreach (string path in paths)
		{
			_ = Time.realtimeSinceStartup;
			_Search(list, path, extension);
			_ = Time.realtimeSinceStartup;
		}
		return list;
	}

	public static void _Search(List<LightFileInfo> matches, string path, string extension)
	{
		FileInfo[] files = new DirectoryInfo(path).GetFiles($"*{extension}", SearchOption.AllDirectories);
		foreach (FileInfo fileInfo in files)
		{
			matches.Add(new LightFileInfo(fileInfo.FullName));
		}
	}
}
