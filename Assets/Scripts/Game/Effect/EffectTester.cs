﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{

    public class EffectTester : MonoBehaviour
    {

        [SerializeField]
        private Vector3 StartPos;

        [SerializeField]
        private Vector3 EndPos;


        [SerializeField]
        GameObject obj1;

        [SerializeField]
        GameObject obj2;


        // Use this for initialization
      

        // Update is called once per frame
        void Update()
        {


            GoalTest();

          



        }

        void ConsorlTest()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject player;

                player = GameObject.Find("Player");

                GameObject effect;
                effect = EffectManager.Instance.CreateEffect(EffectID.Console);
                effect.GetComponent<RectTransform>().localPosition = Vector3.zero;
                
            }

        }

        void GoalTest()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject goal;

                goal = GameObject.Find("GOAL");

                GameObject effect;
                effect = EffectManager.Instance.CreateEffect(EffectID.GoalPoint);
                effect.transform.position = goal.transform.position;

            }

        }

        void WaveTest()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPos = obj1.transform.position;
                EndPos = obj2.transform.position;

                GameObject effect;
                effect = EffectManager.Instance.CreateEffect(EffectID.Wave);
                effect.transform.position = StartPos;
                effect.GetComponent<WaveContoller>().setVelocity(EndPos);
            }

            if (Input.GetMouseButtonDown(1))
            {
                StartPos = obj1.transform.position;
                EndPos = obj2.transform.position;

                GameObject effect;
                effect = EffectManager.Instance.CreateEffect(EffectID.Wave);
                effect.transform.position = EndPos;
                effect.GetComponent<WaveContoller>().setVelocity(StartPos);
            }
        }

    }
}