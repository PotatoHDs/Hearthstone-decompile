using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class CancelJournalRequest
	{
		public string journalId;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class CancelJournalRequest {\n");
			stringBuilder.Append("  journalId: ").Append(journalId).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
