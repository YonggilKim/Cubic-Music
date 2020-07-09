using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text textScore = null;
    [SerializeField] int increaseScore = 10;
    int currentScore = 0;
    [SerializeField] float[] weight = null; // 가중치 값
    [SerializeField] int comboBounusScore = 10;
    Animator myAnim;
    string animScoreUp = "ScoreUp";
    ComboManager theComboManager;
    void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        textScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        //콤보 증가
        theComboManager.IncreaseCombo();
        int currentComboCount = theComboManager.getCurrentCombo();
        int t_bonusComboScore = (currentComboCount / 10) * comboBounusScore;

        //가중치 계산
        int t_increaseScore = increaseScore + t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);
        
        //점수반영
        currentScore += t_increaseScore;        
        textScore.text = string.Format("{0:#,##0}", currentScore);
        
        //판정애니
        myAnim.SetTrigger(animScoreUp);
    }
}
