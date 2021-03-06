﻿using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] UnityEngine.UI.Text txtCombo = null;
    
    int currentCombo = 0;

    Animator myAnim;
    string animCombo = "Combo";
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }


    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if (currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);
            myAnim.SetTrigger(animCombo);

        }
    }
    public void resetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public int getCurrentCombo()
    {
        return currentCombo;
    }
}
