using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class RideFloor : MonoBehaviour
    {
        private BoxCollider2D _collider = null;

        public Collider2D Collider
        {
            get { return _collider ? _collider : _collider = GetComponent<BoxCollider2D>(); }
        }
    }
}