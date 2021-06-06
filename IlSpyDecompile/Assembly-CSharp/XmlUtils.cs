using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class XmlUtils
{
	public static string EscapeXPathSearchString(string search)
	{
		char[] anyOf = new char[2] { '\'', '"' };
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
				string value = ((search[num] != '\'') ? "'\"', " : "\"'\", ");
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

	public static void RemoveAllChildNodes(XmlNode node)
	{
		if (node != null)
		{
			while (node.HasChildNodes)
			{
				node.RemoveChild(node.FirstChild);
			}
		}
	}
}
