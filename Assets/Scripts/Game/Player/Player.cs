using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        public enum State
        {
            Alive,
            Dead
        }

        [SerializeField]
        private State playerState = State.Alive;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (playerState == State.Dead)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            var controller = GameController.Instance;

            Vector3 tryMove = Vector3.zero;
            if (controller.GetConnectFlag())
            {
                tryMove = ControllerControl(controller);
            }
            else
            {
                tryMove = KeyboardControl();
            }
            gameObject.GetComponent<PlayerAnimController>().ChangeAnim(tryMove);
            _rigidbody.velocity = Vector3.ClampMagnitude(tryMove, 1f) * _moveSpeed;
        }

        /// <summary>
        /// キーボードでの操作
        /// </summary>
        private Vector3 KeyboardControl()
        {
            Vector3 tryMove = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
                tryMove += Vector3Int.left;
            if (Input.GetKey(KeyCode.RightArrow))
                tryMove += Vector3Int.right;
            if (Input.GetKey(KeyCode.UpArrow))
                tryMove += Vector3Int.up;
            if (Input.GetKey(KeyCode.DownArrow))
                tryMove += Vector3Int.down;

            return tryMove;
        }

        /// <summary>
        /// コントローラーの操作
        /// </summary>
        private Vector3 ControllerControl(GameController con)
        {
            Vector3 tryMove = Vector3.zero;

            if (con.Move(Direction.Left))
                tryMove += Vector3Int.left;
            if (con.Move(Direction.Right))
                tryMove += Vector3Int.right;
            if (con.Move(Direction.Front))
                tryMove += Vector3Int.up;
            if (con.Move(Direction.Back))
                tryMove += Vector3Int.down;

            return tryMove;
        }
    }
}