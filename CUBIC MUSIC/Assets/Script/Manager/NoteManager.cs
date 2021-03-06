﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d; // 사소한 오차에도 문제가 생기기때문에 플롯보다 오차가 더 작은 double 사용
    [SerializeField] Transform tfNoteappear = null;
    TimingManager theTimingmanager;
    EffectManager theEffectManager;
    ComboManager theComboManager;
    private void Start()
    {
        theTimingmanager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theComboManager = FindObjectOfType<ComboManager>();

    }
    // Update is called once per frame
    void Update()
    {
        // 60 / 120 = 1beat per 0.5 seconds
        // 60s / bpm = 1beat 시간
        currentTime += Time.deltaTime;
        if(currentTime >= 60d / bpm)
        {
            //GameObject t_note = Instantiate(goNote, tfNoteappear.position, Quaternion.identity);
            ////t_note.transform.SetParent(this.transform);
            //currentTime -= 60d/bpm; // 0으로 초기화하면 안됨 시간차 손실
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
            t_note.transform.position = tfNoteappear.position;
            t_note.SetActive(true);
            theTimingmanager.boxNotelist.Add(t_note);
            currentTime -= 60d / bpm;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag()) 
            {
                // 노트가 맨끝까지 가서 미스 되는경우
                theEffectManager.JudgementEffect(4);
                theComboManager.resetCombo();
            }
            theTimingmanager.boxNotelist.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
    }
}
