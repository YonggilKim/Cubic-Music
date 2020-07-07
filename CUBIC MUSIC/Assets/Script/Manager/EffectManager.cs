using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";
    // Start is called before the first frame update
    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage= null;
    [SerializeField] Sprite[] judgementSprite = null;

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
    public void JudgementEffect(int param)
    {
        judgementImage.sprite = judgementSprite[param];
        judgementAnimator.SetTrigger(hit); 
    }
}
