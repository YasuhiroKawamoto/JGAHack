using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.MapEvent
{
    public class TextEvent : EventBase
    {
        [SerializeField]
        private bool _isDead = false;

        protected override void ColliderSetting()
        {
            _onEnter += (Collider2D other) =>
            {
                if (other.GetComponent<Player>())
                {
                    Tutrial.TutrialManager.Instance.NextStep();
                    if (_isDead) Destroy(this);
                }
            };
        }
    }
}