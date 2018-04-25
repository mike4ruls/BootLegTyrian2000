using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public enum CameraMode
    {
        TopDown,
        CockPit
    };

    public CameraMode camMode;
    public Player player;
    public float height;
    public float seperationDist;
    public float topDownAngle;
    public float cockPitAngle;
    public float rotSpeed;
    public float hubWorldSpeed;

    public float xPosTopDownConstraint;
    public float xNegTopDownConstraint;
    public float zPosTopDownConstraint;
    public float zNegTopDownConstraint;

    public float xPosCockPitConstraint;
    public float xNegCockPitConstraint;
    public float zPosCockPitConstraint;
    public float zNegCockPitConstraint;

    public bool inHUBWorld;

    Vector3 center;

    float curAngle;

    CameraMode prevCamMode;

    int MAX_CAM_SHAKE_ENTRIES = 20;

    int camShakeCount = 0;
    List<CamShakeInfo> shakeList;
    float totalShakeAmmount = 0.0f;
    Vector3 camShakePos;

    // Use this for initialization
    void Start () {
        camShakePos = new Vector3(0.0f, 0.0f, 0.0f);
        if (!inHUBWorld)
        {
            center = new Vector3(0.0f, 0.0f, player.transform.position.z);
        }
        else
        {
            center = transform.position;
        }
        shakeList = new List<CamShakeInfo>();
        InitCamShakeEntries();

        switch (camMode)
        {
            case CameraMode.TopDown:
                {
                    player.xPosConstraint = xPosTopDownConstraint;
                    player.xNegConstraint = xNegTopDownConstraint;
                    player.zPosConstraint = zPosTopDownConstraint;
                    player.zNegConstraint = zNegTopDownConstraint;

                    prevCamMode = CameraMode.CockPit;

                    curAngle = topDownAngle;
                    break;
                }
            case CameraMode.CockPit:
                {
                    player.xPosConstraint = xPosCockPitConstraint;
                    player.xNegConstraint = xNegCockPitConstraint;
                    player.zPosConstraint = zPosCockPitConstraint;
                    player.zNegConstraint = zNegCockPitConstraint;

                    prevCamMode = CameraMode.TopDown;

                    curAngle = cockPitAngle;
                    break;
                }
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateCamShake(Time.deltaTime);

        if (!inHUBWorld)
        {
            Vector3 newPos = (player.transform.position - center) / seperationDist;
            newPos = new Vector3(newPos.x, height, newPos.z);
            transform.position = center + newPos + camShakePos;
        }
        else
        {
            Vector3 distFromPlayer = player.transform.position - transform.position;

            if (camMode == CameraMode.TopDown)
            {
                if (distFromPlayer.x < -55.0f)
                {
                    transform.Translate(hubWorldSpeed * -Time.deltaTime, 0.0f, 0.0f);
                    center = transform.position;
                }
                else if (distFromPlayer.x > 55.0f)
                {
                    transform.Translate(hubWorldSpeed * Time.deltaTime, 0.0f, 0.0f);
                    center = transform.position;
                }
                if (distFromPlayer.z < -20.0f)
                {
                    center = new Vector3(center.x, center.y, center.z + (hubWorldSpeed * -Time.deltaTime));
                }
                else if (distFromPlayer.z > 20.0f)
                {
                    center = new Vector3(center.x, center.y, center.z + (hubWorldSpeed * Time.deltaTime));
                }
            }
            else if (camMode == CameraMode.CockPit)
            {
                if (distFromPlayer.x < -55.0f)
                {
                    transform.Translate(35.0f * -Time.deltaTime, 0.0f, 0.0f);
                    center = transform.position;
                }
                else if (distFromPlayer.x > 55.0f)
                {
                    transform.Translate(35.0f * Time.deltaTime, 0.0f, 0.0f);
                    center = transform.position;
                }
                if (distFromPlayer.z < 9.0f)
                {
                    center = new Vector3(center.x, center.y, center.z + (35.0f * -Time.deltaTime));
                }
                else if (distFromPlayer.z > 45.0f)
                {
                    center = new Vector3(center.x, center.y, center.z + (35.0f * Time.deltaTime));
                }
            }

            Vector3 newPos = new Vector3(center.x, height, center.z);
            transform.position = newPos + camShakePos;
        }
        CheckCamMode();
	}
    void UpdateCamShake(float dt)
    {
        if (camShakeCount == 0) { camShakePos = new Vector3(0.0f, 0.0f, 0.0f); return; }

        float rangeX = Random.Range(-1,1) * totalShakeAmmount;
        float rangeY = Random.Range(-1,1) * totalShakeAmmount;
        float rangeZ = Random.Range(-1,1) * totalShakeAmmount;
        camShakePos = new Vector3(rangeX, rangeY, rangeZ );
        int shakeCount = 0;

        for (int i = 0; i < MAX_CAM_SHAKE_ENTRIES; i++)
        {
            if (shakeCount >= camShakeCount) break;
            if (!shakeList[i].active) continue;

            shakeCount++;

            shakeList[i].CountDown(dt);

            if (shakeList[i].timerCD <= 0.0f)
            {
                totalShakeAmmount -= shakeList[i].shakeAmmount;
                shakeList[i].SetActive(false);
                camShakeCount--;
                shakeCount--;
            }
        }
    }

    public void ShakeCamera(CamShakeInfo newInfo)
    {
        totalShakeAmmount += newInfo.shakeAmmount;

        int pos = FindFreeCamShakeEntry();

        if (pos < 0) return;

        camShakeCount++;

        shakeList[pos].SetTimerCD(newInfo.timerCD);
        shakeList[pos].SetShakeAmmount(newInfo.shakeAmmount);
        shakeList[pos].SetActive(true);
    }

   public void ShakeCamera(float shkTime, float shkAmnt)
    {
        totalShakeAmmount += shkAmnt;

        int pos = FindFreeCamShakeEntry();

        if (pos < 0) return;

        camShakeCount++;

        shakeList[pos].SetTimerCD(shkTime);
        shakeList[pos].SetShakeAmmount(shkAmnt);
        shakeList[pos].SetActive(true);
    }
    void InitCamShakeEntries()
    {
        for (int i = 0; i < MAX_CAM_SHAKE_ENTRIES; i++)
        {
            CamShakeInfo newInfo = new CamShakeInfo(0.0f, 0.0f);
            shakeList.Add(newInfo);
        }
    }

    int FindFreeCamShakeEntry()
    {
        for (int i = 0; i < MAX_CAM_SHAKE_ENTRIES; i++)
        {
            if (!shakeList[i].active)
            {
                return i;
            }
        }
        return -1;
    }
    void CheckCamMode()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            prevCamMode = camMode;
            camMode = (camMode == CameraMode.TopDown) ? CameraMode.CockPit : CameraMode.TopDown;
        }

        if (prevCamMode != camMode)
        {
            Vector3 eulerRot = transform.rotation.eulerAngles;
            switch (camMode)
            {
                case CameraMode.TopDown:
                    {
                        eulerRot = new Vector3(eulerRot.x + (rotSpeed * Time.deltaTime), eulerRot.y, eulerRot.z);
                        if (eulerRot.x >= topDownAngle)
                        {
                            eulerRot = new Vector3(topDownAngle, eulerRot.y, eulerRot.z);
                            prevCamMode = camMode;
                        }
                        transform.rotation = Quaternion.Euler(eulerRot);

                        float lerp = (eulerRot.x - cockPitAngle) / (topDownAngle - cockPitAngle);

                        player.xPosConstraint = Mathf.Lerp(xPosCockPitConstraint, xPosTopDownConstraint, lerp);
                        player.xNegConstraint = Mathf.Lerp(xNegCockPitConstraint, xNegTopDownConstraint, lerp);
                        player.zPosConstraint = Mathf.Lerp(zPosCockPitConstraint, zPosTopDownConstraint, lerp);
                        player.zNegConstraint = Mathf.Lerp(zNegCockPitConstraint, zNegTopDownConstraint, lerp);

                        break;
                    }
                case CameraMode.CockPit:
                    {
                        eulerRot = new Vector3(eulerRot.x - (rotSpeed * Time.deltaTime), eulerRot.y, eulerRot.z);
                        if (eulerRot.x <= cockPitAngle)
                        {
                            eulerRot = new Vector3(cockPitAngle, eulerRot.y, eulerRot.z);
                            prevCamMode = camMode;
                        }
                        transform.rotation = Quaternion.Euler(eulerRot);

                        float lerp = (eulerRot.x - topDownAngle) / (cockPitAngle - topDownAngle);

                        player.xPosConstraint = Mathf.Lerp(xPosTopDownConstraint, xPosCockPitConstraint, lerp);
                        player.xNegConstraint = Mathf.Lerp(xNegTopDownConstraint, xNegCockPitConstraint, lerp);
                        player.zPosConstraint = Mathf.Lerp(zPosTopDownConstraint, zPosCockPitConstraint, lerp);
                        player.zNegConstraint = Mathf.Lerp(zNegTopDownConstraint, zNegCockPitConstraint, lerp);

                        break;
                    }
            }
        }
        }
}
