using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNotelist = new List<GameObject>();
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;
    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;
    StageManager theStageManager;
    PlayerController theplayerController;
    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        theplayerController = FindObjectOfType<PlayerController>();

        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public bool CheckTiming()
    {
        Debug.Log("@>> CheckTiming()");
        for (int i = 0; i < boxNotelist.Count; i++)
        {
            float t_notePosX = boxNotelist[i].transform.localPosition.x;
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    string result = null;
                    if (x == 0) result = "Perfect";
                    else if (x == 1) result = "Cool";
                    else if (x == 2) result = "Good";
                    else result = "Bad";
                    Debug.Log(string.Format("Hit {0}",result));

                    //노트 제거
                    boxNotelist[i].GetComponent<Note>().HideNote();
                    boxNotelist.RemoveAt(i);

                    if(x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();// 애니메이션 재생

                    if (CheckCannextPlate())
                    {
                        //점수 증가
                        theScoreManager.IncreaseScore(x);
                        theEffect.JudgementEffect(x);
                        theStageManager.ShowNextPlate();
                    }
                    else 
                    {
                        theEffect.JudgementEffect(5);
                    }
                    return true;
                }
            }
        }
        theComboManager.resetCombo();
        theEffect.JudgementEffect(timingBoxs.Length);
        return false;
    }
    bool CheckCannextPlate()
    {
        if (Physics.Raycast(theplayerController.destPos, Vector3.down, out RaycastHit t_hitinfo, 1.1f))
        {
            if (t_hitinfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitinfo.transform.GetComponent<BasicPlate>();
                if (t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }
}
