using UnityEngine;

public interface GameMenuInterface
{
	bool GameMenuIsShown();

	void GameMenuShow();

	void GameMenuHide();

	void GameMenuShowOptionsMenu();

	GameObject GameMenuGetGameObject();
}
