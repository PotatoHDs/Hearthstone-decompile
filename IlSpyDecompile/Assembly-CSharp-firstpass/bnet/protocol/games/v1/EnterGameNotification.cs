using System.Collections.Generic;
using System.IO;
using bnet.protocol.games.v1.Types;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	public class EnterGameNotification : IProtoBuf
	{
		public bool HasGameHandle;

		private bnet.protocol.games.v2.GameHandle _GameHandle;

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

		private List<bnet.protocol.games.v2.Assignment> _Assignments = new List<bnet.protocol.games.v2.Assignment>();

		public bnet.protocol.games.v2.GameHandle GameHandle
		{
			get
			{
				return _GameHandle;
			}
			set
			{
				_GameHandle = value;
				HasGameHandle = value != null;
			}
		}

		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return _PrivacyLevel;
			}
			set
			{
				_PrivacyLevel = value;
				HasPrivacyLevel = true;
			}
		}

		public List<bnet.protocol.games.v2.Assignment> Assignments
		{
			get
			{
				return _Assignments;
			}
			set
			{
				_Assignments = value;
			}
		}

		public List<bnet.protocol.games.v2.Assignment> AssignmentsList => _Assignments;

		public int AssignmentsCount => _Assignments.Count;

		public bool IsInitialized => true;

		public void SetGameHandle(bnet.protocol.games.v2.GameHandle val)
		{
			GameHandle = val;
		}

		public void SetPrivacyLevel(PrivacyLevel val)
		{
			PrivacyLevel = val;
		}

		public void AddAssignments(bnet.protocol.games.v2.Assignment val)
		{
			_Assignments.Add(val);
		}

		public void ClearAssignments()
		{
			_Assignments.Clear();
		}

		public void SetAssignments(List<bnet.protocol.games.v2.Assignment> val)
		{
			Assignments = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGameHandle)
			{
				num ^= GameHandle.GetHashCode();
			}
			if (HasPrivacyLevel)
			{
				num ^= PrivacyLevel.GetHashCode();
			}
			foreach (bnet.protocol.games.v2.Assignment assignment in Assignments)
			{
				num ^= assignment.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EnterGameNotification enterGameNotification = obj as EnterGameNotification;
			if (enterGameNotification == null)
			{
				return false;
			}
			if (HasGameHandle != enterGameNotification.HasGameHandle || (HasGameHandle && !GameHandle.Equals(enterGameNotification.GameHandle)))
			{
				return false;
			}
			if (HasPrivacyLevel != enterGameNotification.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(enterGameNotification.PrivacyLevel)))
			{
				return false;
			}
			if (Assignments.Count != enterGameNotification.Assignments.Count)
			{
				return false;
			}
			for (int i = 0; i < Assignments.Count; i++)
			{
				if (!Assignments[i].Equals(enterGameNotification.Assignments[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static EnterGameNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EnterGameNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EnterGameNotification Deserialize(Stream stream, EnterGameNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EnterGameNotification DeserializeLengthDelimited(Stream stream)
		{
			EnterGameNotification enterGameNotification = new EnterGameNotification();
			DeserializeLengthDelimited(stream, enterGameNotification);
			return enterGameNotification;
		}

		public static EnterGameNotification DeserializeLengthDelimited(Stream stream, EnterGameNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EnterGameNotification Deserialize(Stream stream, EnterGameNotification instance, long limit)
		{
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
			if (instance.Assignments == null)
			{
				instance.Assignments = new List<bnet.protocol.games.v2.Assignment>();
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
				case 10:
					if (instance.GameHandle == null)
					{
						instance.GameHandle = bnet.protocol.games.v2.GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.games.v2.GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 16:
					instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
					continue;
				case 26:
					instance.Assignments.Add(bnet.protocol.games.v2.Assignment.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, EnterGameNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				bnet.protocol.games.v2.GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
			}
			if (instance.Assignments.Count <= 0)
			{
				return;
			}
			foreach (bnet.protocol.games.v2.Assignment assignment in instance.Assignments)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, assignment.GetSerializedSize());
				bnet.protocol.games.v2.Assignment.Serialize(stream, assignment);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGameHandle)
			{
				num++;
				uint serializedSize = GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasPrivacyLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrivacyLevel);
			}
			if (Assignments.Count > 0)
			{
				foreach (bnet.protocol.games.v2.Assignment assignment in Assignments)
				{
					num++;
					uint serializedSize2 = assignment.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
