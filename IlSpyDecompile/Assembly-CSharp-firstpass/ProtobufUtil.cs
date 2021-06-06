using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public static class ProtobufUtil
{
	public static byte[] ToByteArray(IProtoBuf protobuf)
	{
		byte[] array = new byte[protobuf.GetSerializedSize()];
		MemoryStream stream = new MemoryStream(array);
		protobuf.Serialize(stream);
		return array;
	}

	public static T ParseFrom<T>(byte[] bytes, int offset = 0, int length = -1) where T : IProtoBuf, new()
	{
		if (length == -1)
		{
			length = bytes.Length;
		}
		MemoryStream stream = new MemoryStream(bytes, offset, length);
		T result = new T();
		result.Deserialize(stream);
		return result;
	}

	public static IProtoBuf ParseFromGeneric<T>(byte[] bytes) where T : IProtoBuf, new()
	{
		MemoryStream stream = new MemoryStream(bytes);
		T val = new T();
		val.Deserialize(stream);
		return val;
	}

	public static string ToHumanReadableString(this IProtoBuf proto)
	{
		if (proto == null)
		{
			return "null";
		}
		return ToHumanReadableString_Internal(proto, "", "  ");
	}

	private static string ToHumanReadableString_Internal(IProtoBuf proto, string indent, string indentIncrement)
	{
		StringBuilder stringBuilder = new StringBuilder();
		Type type = proto.GetType();
		foreach (PropertyInfo item in from p in type.GetProperties()
			orderby p.Name
			select p)
		{
			if (!item.Name.StartsWith("Has") && !(item.GetGetMethod() == null))
			{
				FieldInfo field = type.GetField("Has" + item.Name);
				if (!(field != null) || (bool)field.GetValue(proto))
				{
					string arg = ToHumanReadableString_Internal_GetValueString(item.GetValue(proto, null), indent, indentIncrement);
					stringBuilder.Append(indent).Append(indentIncrement);
					stringBuilder.AppendFormat("{0}: {1}\n", item.Name, arg);
				}
			}
		}
		if (stringBuilder.Length == 0)
		{
			return "{ }";
		}
		return $"{{\n{stringBuilder.ToString()}{indent}}}";
	}

	private static string ToHumanReadableString_Internal_GetValueString(object value, string indent, string indentIncrement)
	{
		if (value == null)
		{
			return "null";
		}
		if (value is IProtoBuf)
		{
			value = ToHumanReadableString_Internal(value as IProtoBuf, indent + indentIncrement, indentIncrement);
		}
		else if (!(value is string))
		{
			if (value is Enum)
			{
				value = $"{Convert.ToInt32(value)} {value.ToString()}";
			}
			else if (value is IEnumerable)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (object item in (IEnumerable)value)
				{
					if (num > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.AppendFormat("[{0}] {1}", num, ToHumanReadableString_Internal_GetValueString(item, indent + indentIncrement, indentIncrement));
					num++;
				}
				value = ((num != 0) ? stringBuilder.ToString() : "empty");
			}
		}
		return value.ToString();
	}

	public static T Clone<T>(T protobuf) where T : IProtoBuf, new()
	{
		T result = new T();
		using MemoryStream memoryStream = new MemoryStream(new byte[protobuf.GetSerializedSize()]);
		protobuf.Serialize(memoryStream);
		memoryStream.Position = 0L;
		result.Deserialize(memoryStream);
		return result;
	}
}
