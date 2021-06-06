using System;
using UnityEngine;

// Token: 0x02000901 RID: 2305
public class PuzzleProgressUI : MonoBehaviour
{
	// Token: 0x0600804A RID: 32842 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600804B RID: 32843 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600804C RID: 32844 RVA: 0x0029B444 File Offset: 0x00299644
	public void UpdateNameAndText(string puzzleName, string puzzleText)
	{
		this.m_PuzzleNameText.Text = puzzleName;
		this.m_PuzzleDescriptionText.Text = puzzleText;
	}

	// Token: 0x0600804D RID: 32845 RVA: 0x0029B45E File Offset: 0x0029965E
	public void UpdateProgressValues(int puzzleProgress, int totalPuzzleProgress)
	{
		this.m_currentPuzzleProgress = puzzleProgress;
		this.m_totalPuzzleProgress = totalPuzzleProgress;
		this.m_ProgressText.Text = string.Format("{0}/{1}", puzzleProgress, totalPuzzleProgress);
	}

	// Token: 0x0600804E RID: 32846 RVA: 0x0029B490 File Offset: 0x00299690
	public void UpdateText(Entity puzzleEntity)
	{
		this.m_currentPuzzleProgress = puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS);
		this.m_totalPuzzleProgress = puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL);
		this.m_ProgressText.Text = string.Format("{0}/{1}", this.m_currentPuzzleProgress, this.m_totalPuzzleProgress);
		this.m_PuzzleNameText.Text = puzzleEntity.GetName();
		this.m_PuzzleDescriptionText.Text = puzzleEntity.GetCardTextInHand();
	}

	// Token: 0x0600804F RID: 32847 RVA: 0x0029B50C File Offset: 0x0029970C
	public void IncrementPuzzleProgress()
	{
		this.UpdateProgressValues(this.m_currentPuzzleProgress + 1, this.m_totalPuzzleProgress);
	}

	// Token: 0x0400693F RID: 26943
	public UberText m_ProgressText;

	// Token: 0x04006940 RID: 26944
	public UberText m_PuzzleNameText;

	// Token: 0x04006941 RID: 26945
	public UberText m_PuzzleDescriptionText;

	// Token: 0x04006942 RID: 26946
	private int m_currentPuzzleProgress;

	// Token: 0x04006943 RID: 26947
	private int m_totalPuzzleProgress;
}
