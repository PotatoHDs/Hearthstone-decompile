using System;
using System.Text;

namespace com.blizzard.commerce.Model
{
	[Serializable]
	public class FailJournalRequest
	{
		public string journalId;

		public string errorMessage;

		public int errorCode;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("class FailJournalRequest {\n");
			stringBuilder.Append("  journalId: ").Append(journalId).Append("\n");
			stringBuilder.Append("  errorMessage: ").Append(errorMessage).Append("\n");
			stringBuilder.Append("  errorCode: ").Append(errorCode).Append("\n");
			stringBuilder.Append("}\n");
			return stringBuilder.ToString();
		}
	}
}
