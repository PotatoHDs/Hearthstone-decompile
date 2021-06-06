using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PegasusGame
{
	public class ScriptDebugInformation : IProtoBuf
	{
		public enum PacketID
		{
			ID = 7
		}

		private List<ScriptDebugCall> _Calls = new List<ScriptDebugCall>();

		public int EntityID { get; set; }

		public string EntityName { get; set; }

		public string PowerGUID { get; set; }

		public List<ScriptDebugCall> Calls
		{
			get
			{
				return _Calls;
			}
			set
			{
				_Calls = value;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= EntityID.GetHashCode();
			hashCode ^= EntityName.GetHashCode();
			hashCode ^= PowerGUID.GetHashCode();
			foreach (ScriptDebugCall call in Calls)
			{
				hashCode ^= call.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ScriptDebugInformation scriptDebugInformation = obj as ScriptDebugInformation;
			if (scriptDebugInformation == null)
			{
				return false;
			}
			if (!EntityID.Equals(scriptDebugInformation.EntityID))
			{
				return false;
			}
			if (!EntityName.Equals(scriptDebugInformation.EntityName))
			{
				return false;
			}
			if (!PowerGUID.Equals(scriptDebugInformation.PowerGUID))
			{
				return false;
			}
			if (Calls.Count != scriptDebugInformation.Calls.Count)
			{
				return false;
			}
			for (int i = 0; i < Calls.Count; i++)
			{
				if (!Calls[i].Equals(scriptDebugInformation.Calls[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ScriptDebugInformation Deserialize(Stream stream, ScriptDebugInformation instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ScriptDebugInformation DeserializeLengthDelimited(Stream stream)
		{
			ScriptDebugInformation scriptDebugInformation = new ScriptDebugInformation();
			DeserializeLengthDelimited(stream, scriptDebugInformation);
			return scriptDebugInformation;
		}

		public static ScriptDebugInformation DeserializeLengthDelimited(Stream stream, ScriptDebugInformation instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ScriptDebugInformation Deserialize(Stream stream, ScriptDebugInformation instance, long limit)
		{
			if (instance.Calls == null)
			{
				instance.Calls = new List<ScriptDebugCall>();
			}
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.EntityID = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.EntityName = ProtocolParser.ReadString(stream);
					continue;
				case 26:
					instance.PowerGUID = ProtocolParser.ReadString(stream);
					continue;
				case 34:
					instance.Calls.Add(ScriptDebugCall.DeserializeLengthDelimited(stream));
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ScriptDebugInformation instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.EntityID);
			if (instance.EntityName == null)
			{
				throw new ArgumentNullException("EntityName", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.EntityName));
			if (instance.PowerGUID == null)
			{
				throw new ArgumentNullException("PowerGUID", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PowerGUID));
			if (instance.Calls.Count <= 0)
			{
				return;
			}
			foreach (ScriptDebugCall call in instance.Calls)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, call.GetSerializedSize());
				ScriptDebugCall.Serialize(stream, call);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)EntityID);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(EntityName);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(PowerGUID);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (Calls.Count > 0)
			{
				foreach (ScriptDebugCall call in Calls)
				{
					num++;
					uint serializedSize = call.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 3;
		}
	}
}
