using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Main
{
	public class PhoneScreen : MonoBehaviour
	{
		[SerializeField]
		private Image _stagePanel = null;

		[SerializeField]
		private Vector3 _initPos = Vector3.zero;

		[SerializeField]
		private Vector2 _popOffSet = Vector2.zero;

		// 作成するパネルの数
		public int STAGE_NUM = 7;

		[SerializeField]
		private List<Image> _panelList = new List<Image>();

		private RectTransform _rectTransform = null;

		private Vector2 _panelRect = Vector2.zero;

		private int _selectIndex = 0;
		public int SelectIndex
		{
			get { return _selectIndex; }
		}

		private Tween _moveTween = null;

		public void SetUp()
		{
			_panelRect = _stagePanel.rectTransform.sizeDelta;

			_rectTransform = GetComponent<RectTransform>();

			PanelSlide(0);
		}

		/// <summary>
		/// パネルの作成
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Image CreatePanel(Vector3 pos, int index)
		{
			var obj = Instantiate(_stagePanel, pos, Quaternion.identity);
			obj.rectTransform.SetParent(_rectTransform);
			_panelList.Add(obj);

			obj.rectTransform.localScale = Vector3.one;
			obj.rectTransform.localPosition = pos;

			var text = obj.GetComponentInChildren<Text>();
			text.text = "STAGE " + (index + 1);

			return obj;
		}

		private Image MovePanel(Vector3 pos, int index)
		{
			var obj = _panelList[index];

			obj.rectTransform.localScale = Vector3.one;
			_moveTween = obj.rectTransform.DOLocalMove(pos, 0.15f)
								.OnComplete(() => _moveTween = null);

			return obj;
		}

		/// <summary>
		/// パネルのセットアップ
		/// </summary>
		private void PanelSetup()
		{
			bool create = false;
			if (_panelList.Count == 0)
			{
				create = true;
			}

			for (int i = 0; i < STAGE_NUM; i++)
			{
				var offSet = _popOffSet;
				var posX = 0.0f;

				if (i == _selectIndex)
				{
					posX += offSet.x;
				}

				var posY = i * (_panelRect.y + offSet.y);
				var pos = new Vector3(_initPos.x - posX, _initPos.y - posY, _initPos.z);

				if (create) CreatePanel(pos, i);
				else MovePanel(pos, i);
			}
		}

		/// <summary>
		/// パネルを次に移動
		/// </summary>
		public int PanelNext()
		{
			return PanelSlide(1);
		}

		/// <summary>
		/// パネルを前に移動
		/// </summary>
		public int PanelBefore()
		{
			return PanelSlide(-1);
		}

		/// <summary>
		/// パネルのスライド移動
		/// </summary>
		/// <param name="c"></param>
		private int PanelSlide(int c)
		{
			if ((_moveTween != null) ||
				(!IndexIsContained(_selectIndex + c)))
			{
				return -1;
			}

			var margin = _panelRect.y + _popOffSet.y;
			_initPos.y += (margin * c);
			_selectIndex += c;
			PanelSetup();

			return _selectIndex;
		}

		private bool IndexIsContained(int index)
		{
			var count = STAGE_NUM;
			if ((index < 0) || (count <= index))
			{
				return false;
			}
			return true;
		}

	}
}