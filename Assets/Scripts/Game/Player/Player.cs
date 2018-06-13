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
        private State _playerState = State.Alive;

        public State PlayerState
        {
            get { return _playerState; }
        }

        private bool _terrain = true;

        // 向き
        [SerializeField, Extensions.ReadOnly]
        private Direction _direction = Direction.Front;
        public Direction Dir { get { return _direction; } }

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!InGameManager.IsInstance())
            {
                return;
            }

            if (InGameManager.Instance.GameState != InGameManager.State.Play)
            {
                // ゲームが開始していないときは移動しない
                return;
            }

            // 死んでいるときは動かない
            if (PlayerState == State.Dead)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            var controller = GameController.Instance;

            // 移動処理とポーズ処理
            Vector3 tryMove = Vector3.zero;
            if (controller.GetConnectFlag())
            {
                tryMove = ControllerControl(controller);

               
                    //アニメーション切り替え
                    gameObject.GetComponent<PlayerAnimController>().ChangeAnim(tryMove);
                  
            }
            else
            {
                tryMove = KeyboardControl();
        
                    //アニメーション切り替え
                    gameObject.GetComponent<PlayerAnimController>().ChangeAnim(tryMove);
                  
            }
            _rigidbody.velocity = Vector3.ClampMagnitude(tryMove, 1f) * _moveSpeed;
        }

        /// <summary>
        /// キーボードでの操作
        /// </summary>
        virtual protected Vector3 KeyboardControl()
        {
            Vector3 tryMove = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                tryMove += Vector3Int.left;
                _direction = Direction.Left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                tryMove += Vector3Int.right;
                _direction = Direction.Right;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                tryMove += Vector3Int.up;
                _direction = Direction.Back;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                tryMove += Vector3Int.down;
                _direction = Direction.Front;
            }

            return tryMove;
        }

        /// <summary>
        /// コントローラーの操作
        /// </summary>
        virtual protected Vector3 ControllerControl(GameController con)
        {
            Vector3 tryMove = Vector3.zero;

            if (con.Move(Direction.Left))
            {
                tryMove += Vector3Int.left;
                _direction = Direction.Left;
            }
            if (con.Move(Direction.Right))
            {
                tryMove += Vector3Int.right;
                _direction = Direction.Right;
            }
            if (con.Move(Direction.Front))
            {
                tryMove += Vector3Int.up;
                _direction = Direction.Back;
            }
            if (con.Move(Direction.Back))
            {
                tryMove += Vector3Int.down;
                _direction = Direction.Front;
            }

            return tryMove;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public void Dead(bool retry = true)
        {
            _playerState = State.Dead;
            
            if (retry)
            {
                InGameManager.Instance.StageOver();
            }
        }

        /// <summary>
        /// 復活
        /// </summary>
        public void Reborn()
        {
            _playerState = State.Alive;
            //復帰時のアニメーション変更（デフォルト下向き）
            gameObject.GetComponent<PlayerAnimController>().ChangeAnim(PlayerAnimController.ANIMATION_ID.Back);
        }
    }
}