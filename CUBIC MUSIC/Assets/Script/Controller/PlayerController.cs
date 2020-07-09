using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();

    [SerializeField] float SpinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();
    
    //반동변수
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;
    //기타
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;
    bool canMove = true;

    TimingManager theTimingManager;
    CameraController theCam;
    private void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theTimingManager = FindObjectOfType<TimingManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            if (canMove)
            {
                Calc();

                //판정체크
                if (theTimingManager.CheckTiming())
                {
                    StartAction();
                }
                else 
                { 
                }
            }
        }
        //이동 목표값
    }

    void Calc()
    {
        //방향
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        //이동목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, SpinSpeed);
        destRot = fakeCube.rotation;

    }
    void StartAction()
    {
        StartCoroutine(MoveCo());
        StartCoroutine(Spin());
        StartCoroutine(RecoilCo());
        StartCoroutine(theCam.ZoomCam());

    }

    IEnumerator MoveCo()
    { 
        //Vector3.Distance(transform.position, destPos) != 0
        canMove = false;
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        transform.position = destPos;
        canMove = true;
    }


    IEnumerator Spin()
    {
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, SpinSpeed * Time.deltaTime);
            yield return null;
        }
        realCube.rotation = destRot;
    }
    IEnumerator RecoilCo()
    {
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while (realCube.position.y > 0) 
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }

}
