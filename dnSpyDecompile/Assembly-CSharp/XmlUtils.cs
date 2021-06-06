using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

// Token: 0x02000A04 RID: 2564
public class XmlUtils
{
	// Token: 0x06008B04 RID: 35588 RVA: 0x002C770C File Offset: 0x002C590C
	public static string EscapeXPathSearchString(string search)
	{
		char[] anyOf = new char[]
		{
			'\'',
			'"'
		};
		StringBuilder stringBuilder = new StringBuilder();
		int num = search.IndexOfAny(anyOf);
		if (num == -1)
		{
			stringBuilder.Append('\'');
			stringBuilder.Append(search);
			stringBuilder.Append('\'');
		}
		else
		{
			stringBuilder.Append("concat(");
			int num2 = 0;
			while (num != -1)
			{
				stringBuilder.Append('\'');
				stringBuilder.Append(search, num2, num - num2);
				stringBuilder.Append("', ");
				string value;
				if (search[num] == '\'')
				{
					value = "\"'\", ";
				}
				else
				{
					value = "'\"', ";
				}
				stringBuilder.Append(value);
				num2 = num + 1;
				num = search.IndexOfAny(anyOf, num + 1);
			}
			stringBuilder.Append('\'');
			stringBuilder.Append(search, num2, search.Length - num2);
			stringBuilder.Append("')");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06008B05 RID: 35589 RVA: 0x002C77F0 File Offset: 0x002C59F0
	public static XmlDocument LoadXmlDocFromTextAsset(TextAsset asset)
	{
		string text = null;
		using (StringReader stringReader = new StringReader(asset.text))
		{
			text = stringReader.ReadToEnd();
		}
		if (text == null)
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(text);
		return xmlDocument;
	}

	// Token: 0x06008B06 RID: 35590 RVA: 0x002C7840 File Offset: 0x002C5A40
	public static void RemoveAllChildNodes(XmlNode node)
	{
		if (node == null)
		{
			return;
		}
		while (node.HasChildNodes)
		{
			node.RemoveChild(node.FirstChild);
		}
	}
}
