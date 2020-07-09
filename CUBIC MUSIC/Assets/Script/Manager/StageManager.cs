using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null;
    [SerializeField] float OffsetY = -5;
    [SerializeField] float plateSpeed = 10;
    Transform[] stagePlates;
    // Start is called before the first frame update
    int stepCount = 0;
    int totalPlateCount = 0;
    void Start()
    {
        stagePlates = stage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x, stagePlates[i].position.y + OffsetY, stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
            StartCoroutine(MovePlateCo(stepCount++));
    }
    IEnumerator MovePlateCo(int num)
    {
        stagePlates[num].gameObject.SetActive(true);

        Vector3 t_destPos = new Vector3(stagePlates[num].position.x,
                                        stagePlates[num].position.y - OffsetY,
                                        stagePlates[num].position.z);
        while (Vector3.SqrMagnitude(stagePlates[num].position - t_destPos) >= 0.001f)
        {
            stagePlates[num].position = Vector3.Lerp(stagePlates[num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }
        stagePlates[num].position = t_destPos;
    }
}

