using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
    public class RideFloor : ElementBase

    {

        void Awake()
        {
            _type = ElementType.Action;
        }

        public override void Initialize()
        {
        }

        private BoxCollider2D _collider = null;

        public Collider2D Collider
        {
            get { return _collider ? _collider : _collider = GetComponent<BoxCollider2D>(); }
        }
    }
}