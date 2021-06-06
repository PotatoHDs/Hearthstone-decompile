using UnityEngine;

public class PuzzleProgressUI : MonoBehaviour
{
	public UberText m_ProgressText;

	public UberText m_PuzzleNameText;

	public UberText m_PuzzleDescriptionText;

	private int m_currentPuzzleProgress;

	private int m_totalPuzzleProgress;

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void UpdateNameAndText(string puzzleName, string puzzleText)
	{
		m_PuzzleNameText.Text = puzzleName;
		m_PuzzleDescriptionText.Text = puzzleText;
	}

	public void UpdateProgressValues(int puzzleProgress, int totalPuzzleProgress)
	{
		m_currentPuzzleProgress = puzzleProgress;
		m_totalPuzzleProgress = totalPuzzleProgress;
		m_ProgressText.Text = $"{puzzleProgress}/{totalPuzzleProgress}";
	}

	public void UpdateText(Entity puzzleEntity)
	{
		m_currentPuzzleProgress = puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS);
		m_totalPuzzleProgress = puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL);
		m_ProgressText.Text = $"{m_currentPuzzleProgress}/{m_totalPuzzleProgress}";
		m_PuzzleNameText.Text = puzzleEntity.GetName();
		m_PuzzleDescriptionText.Text = puzzleEntity.GetCardTextInHand();
	}

	public void IncrementPuzzleProgress()
	{
		UpdateProgressValues(m_currentPuzzleProgress + 1, m_totalPuzzleProgress);
	}
}
