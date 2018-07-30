using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Sound;

namespace Play
{
	public class Player : MonoBehaviour
	{
		[SerializeField]
		private float _moveSpeed;

		private Rigidbody2D _rigidbody;

		private float _waitCount = 0.2f;

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

		// 向き
		[SerializeField, Extensions.ReadOnly]
		private Direction _direction = Direction.Back;
		public Direction Dir { get { return _direction; } }

		private GameObject _conObj = null;
		private PlayerController _playerController = null;

		[SerializeField]
		private ElementSelector _selector = null;

		void Start()
		{
			_rigidbody = GetComponent<Rigidbody2D>();

			if (!InGameManager.IsInstance())
			{
				return;
			}

			//アニメーション切り替え
			gameObject.GetComponent<PlayerAnimController>().ChangeAnim(PlayerAnimController.ANIMATION_ID.BackWait);

			_conObj = new GameObject();
			_conObj.transform.parent = this.transform;
			_playerController = _conObj.AddComponent<PlayerController>();
			_playerController.TargetCamera = Camera.main;
			_playerController.Material = Resources.Load("punimate") as Material;
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

			_selector.KeyInput();
			if (_selector.IsInput)
			{
				return;
			}

			// 移動処理とポーズ処理
			Vector3 tryMove = Vector3.zero;

			// 入力更新
			_playerController.KeyInput();
			//tryMove = TouchControl();
			tryMove = _playerController.Velocity;

			if (tryMove.x <= -0.5f)
			{
				//left
				tryMove.x = -1.0f;
				_direction = Direction.Left;
			}
			else if (0.5f <= tryMove.x)
			{
				//right
				tryMove.x = 1.0f;
				_direction = Direction.Right;
			}

			if (tryMove.y <= -0.5f)
			{
				//down
				tryMove.y = -1.0f;
				_direction = Direction.Back;
			}
			else if (0.5f <= tryMove.y)
			{
				//up
				tryMove.y = 1.0f;
				_direction = Direction.Front;
			}

			//アニメーション切り替え
			gameObject.GetComponent<PlayerAnimController>().ChangeAnim(_direction, _waitCount);

			//待機時間経過
			if (tryMove == Vector3.zero)
			{
				_waitCount += Time.deltaTime;
			}
			else
			{
				_waitCount = 0;
			}

			//移動
			_rigidbody.velocity = Vector3.ClampMagnitude(tryMove, 1f) * _moveSpeed;
		}
		#region  a

		/// <summary>
		/// タッチでの操作
		/// </summary>
		virtual protected Vector3 TouchControl()
		{
			Vector3 tryMove = Vector3.zero;

			var touchPos = Input.mousePosition;

			var self = this.transform;
			var pos = Camera.main.ScreenToWorldPoint(touchPos);
			var target = new Vector3(pos.x, pos.y, 0.0f);

			var diff = target - self.position;
			Debug.DrawLine(self.position, target);

			var dir = self.position + new Vector3(0.0f, 1.0f, 0.0f);
			Debug.DrawLine(self.position, dir, Color.red);

			var axis = Vector3.Cross(dir, diff);
			var angle = Vector3.Angle(dir, diff) * (axis.z < 0 ? -1 : 1);

			return SetVelocityForRigidbody2D(angle, 1.0f);
		}

		// 速度を設定
		// @param 角度
		// @param 速さ
		public Vector3 SetVelocityForRigidbody2D(float direction, float speed)
		{
			// Setting velocity.
			Vector3 v = Vector3.zero;
			v.x = -Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
			v.y = -Mathf.Sin(Mathf.Deg2Rad * direction) * speed;

			return v;
		}

		#endregion

		/// <summary>
		/// 死亡
		/// </summary>
		public void Dead(bool retry = true)
		{
			if (_playerState == State.Dead) return;

			_playerState = State.Dead;
			_direction = Direction.Back;
			_waitCount = 0.2f;

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
			var renderer = GetComponent<Renderer>();
			renderer.sortingOrder = 0;
		}

		public void Goal()
		{
			var collider = GetComponent<BoxCollider2D>();
			collider.enabled = false;

			var renderer = GetComponent<SpriteRenderer>();
			var layer = renderer.sortingOrder;
			renderer.sortingOrder = layer - 2;
		}
	}
}