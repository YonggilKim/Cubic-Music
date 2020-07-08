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
    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
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
                    theEffect.JudgementEffect(x);

                    //점수 증가
                    theScoreManager.IncreaseScore(x);
                    return;
                }
            }
        }
        theEffect.JudgementEffect(timingBoxs.Length);

    }
  
}
