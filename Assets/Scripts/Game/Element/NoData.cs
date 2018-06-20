using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;


namespace Play.Element
{

    public class NoData : ElementBase
    {


        void Awake()
        {
            _type = ElementType.Action;
        }


        public override void Initialize()
        {

        }


        private void Update()
        {
            Act();
        }


        private void Act()
        { }
    }
}