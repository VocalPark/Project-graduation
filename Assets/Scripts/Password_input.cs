﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Password_input Scene에서 비밀번호 입력을 위한 스크립트(160526)
/// </summary>
public class Password_input : MonoBehaviour
{
    public GameObject aimTexture;
    public AudioClip input;
    private AudioSource source = null;
    GUITexture aim;

    Vector3 ViewportPosition = new Vector3(0.5f, 0.5f, 0.0f);

    int[] a = new int[4];
    public static int cnt;
    public static bool End;
    

    void rayCasting(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag.Equals("Keys"))
            {
                source.PlayOneShot(input);
                GameObject tmp = null;
                tmp = hit.transform.gameObject;
                if (tmp.GetComponentInChildren<TextMesh>().text == "C")
                {
                    if (cnt > 0) cnt--;
                }
                else
                {
                    a[cnt] = int.Parse(tmp.GetComponentInChildren<TextMesh>().text);
                    cnt++;
                }

                string add_tmp = "";
                for (int i = 0; i < cnt; i++)
                {
                    add_tmp = add_tmp + a[i].ToString() + " ";
                }
                for (int i = cnt; i < 4; i++)
                {
                    add_tmp = add_tmp + "_ ";
                }
                this.GetComponentInChildren<TextMesh>().text = add_tmp;

                if (cnt > 3)
                    SendPassword();
            }
        }
    }


    void SendPassword()
    {
        for (int i = 0; i < 4; i++)
        {
            if (a[i] != CNextStage.Password[i])
            {
                print(a[i]);
                print(CNextStage.Password[i]);
                CNextStage.OnPlay = false;
                End = true;
                return;
            }
        }
        CNextStage.OnPlay = true;
        End = true;
        return;
    }

    // Use this for initialization
    void Start()
    {
        aim = aimTexture.GetComponent<GUITexture>();
        aim.enabled = true;
        source = GetComponent<AudioSource>();
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ViewportPointToRay(ViewportPosition);

            rayCasting(ray);
        }
        //Vector3 aim2 = Camera.main.ViewportToWorldPoint(ViewportPosition);
        //aim.z = 0;
        //aimTexture.transform.position = aim2;
    }

}
