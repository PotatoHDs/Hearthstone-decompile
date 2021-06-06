using UnityEngine;

public interface IChatLogUI
{
	bool IsShowing { get; }

	GameObject GameObject { get; }

	BnetPlayer Receiver { get; }

	void ShowForPlayer(BnetPlayer player);

	void Hide();

	void GoBack();
}
