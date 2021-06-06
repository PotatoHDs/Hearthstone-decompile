using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

// Token: 0x0200000A RID: 10
public static class ProtobufUtil
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003360 File Offset: 0x00001560
	public static byte[] ToByteArray(IProtoBuf protobuf)
	{
		byte[] array = new byte[protobuf.GetSerializedSize()];
		MemoryStream stream = new MemoryStream(array);
		protobuf.Serialize(stream);
		return array;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003388 File Offset: 0x00001588
	public static T ParseFrom<T>(byte[] bytes, int offset = 0, int length = -1) where T : IProtoBuf, new()
	{
		if (length == -1)
		{
			length = bytes.Length;
		}
		MemoryStream stream = new MemoryStream(bytes, offset, length);
		T result = Activator.CreateInstance<T>();
		result.Deserialize(stream);
		return result;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000033BC File Offset: 0x000015BC
	public static IProtoBuf ParseFromGeneric<T>(byte[] bytes) where T : IProtoBuf, new()
	{
		MemoryStream stream = new MemoryStream(bytes);
		T t = Activator.CreateInstance<T>();
		t.Deserialize(stream);
		return t;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x000033EA File Offset: 0x000015EA
	public static string ToHumanReadableString(this IProtoBuf proto)
	{
		if (proto == null)
		{
			return "null";
		}
		return ProtobufUtil.ToHumanReadableString_Internal(proto, "", "  ");
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003408 File Offset: 0x00001608
	private static string ToHumanReadableString_Internal(IProtoBuf proto, string indent, string indentIncrement)
	{
		StringBuilder stringBuilder = new StringBuilder();
		Type type = proto.GetType();
		foreach (PropertyInfo propertyInfo in from p in type.GetProperties()
		orderby p.Name
		select p)
		{
			if (!propertyInfo.Name.StartsWith("Has") && !(propertyInfo.GetGetMethod() == null))
			{
				FieldInfo field = type.GetField("Has" + propertyInfo.Name);
				if (!(field != null) || (bool)field.GetValue(proto))
				{
					string arg = ProtobufUtil.ToHumanReadableString_Internal_GetValueString(propertyInfo.GetValue(proto, null), indent, indentIncrement);
					stringBuilder.Append(indent).Append(indentIncrement);
					stringBuilder.AppendFormat("{0}: {1}\n", propertyInfo.Name, arg);
				}
			}
		}
		if (stringBuilder.Length == 0)
		{
			return "{ }";
		}
		return string.Format("{{\n{0}{1}}}", stringBuilder.ToString(), indent);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003528 File Offset: 0x00001728
	private static string ToHumanReadableString_Internal_GetValueString(object value, string indent, string indentIncrement)
	{
		if (value == null)
		{
			return "null";
		}
		if (value is IProtoBuf)
		{
			value = ProtobufUtil.ToHumanReadableString_Internal(value as IProtoBuf, indent + indentIncrement, indentIncrement);
		}
		else if (!(value is string))
		{
			if (value is Enum)
			{
				value = string.Format("{0} {1}", Convert.ToInt32(value), value.ToString());
			}
			else if (value is IEnumerable)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (object value2 in ((IEnumerable)value))
				{
					if (num > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.AppendFormat("[{0}] {1}", num, ProtobufUtil.ToHumanReadableString_Internal_GetValueString(value2, indent + indentIncrement, indentIncrement));
					num++;
				}
				if (num == 0)
				{
					value = "empty";
				}
				else
				{
					value = stringBuilder.ToString();
				}
			}
		}
		return value.ToString();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000363C File Offset: 0x0000183C
	public static T Clone<T>(T protobuf) where T : IProtoBuf, new()
	{
		T result = Activator.CreateInstance<T>();
		using (MemoryStream memoryStream = new MemoryStream(new byte[protobuf.GetSerializedSize()]))
		{
			protobuf.Serialize(memoryStream);
			memoryStream.Position = 0L;
			result.Deserialize(memoryStream);
		}
		return result;
	}
}
