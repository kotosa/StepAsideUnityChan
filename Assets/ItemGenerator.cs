using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject m_cCarPrefab;
    public GameObject m_cCoinPrefab;
    public GameObject m_cConePrefab;
    private int m_iStartPosZ = -160;
    private int m_iEndPosZ = 120;
    private float posRangeX = 3.4f;

	// Use this for initialization
	void Start () {
        for(int i = m_iStartPosZ; i < m_iEndPosZ; i += 15)
        {
            int l_iItem = Random.Range(0, 10);
            if(l_iItem <= 1)
            {
                // コーンをX軸方向に一直線に並べる
                for(float j = -1; j <= 1; j +=0.4f)
                {
                    GameObject cone = Instantiate(m_cConePrefab) as GameObject;
                    cone.transform.position = new Vector3(j * 4, cone.transform.position.y, i);
                }
            }
            else
            {
                for(int j = -1; j < 2; j++)
                {
                    int item = Random.Range(1, 11);
                    int offsetZ = Random.Range(-5, 6);
                    if (1 <= item && item <= 6)
                    {
                        GameObject coin = Instantiate(m_cCoinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRangeX * j, coin.transform.position.y, i);
                    }
                    else if(7 <= item && item <= 9)
                    {
                        GameObject car = Instantiate(m_cCarPrefab) as GameObject;
                        car.transform.position = new Vector3(posRangeX * j, car.transform.position.y, i);
                    }
                }

            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
