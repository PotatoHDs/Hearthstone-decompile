public struct CharacterDialog
{
	public int dbfRecordId;

	public int playOrder;

	public bool useInnkeeperQuote;

	public string prefabName;

	public string audioName;

	public bool useAltSpeechBubble;

	public float waitBefore;

	public float waitAfter;

	public bool persistPrefab;

	public bool useAltPosition;

	public float minimumDurationSeconds;

	public float localeExtraSeconds;

	public DbfLocValue bubbleText;
}
