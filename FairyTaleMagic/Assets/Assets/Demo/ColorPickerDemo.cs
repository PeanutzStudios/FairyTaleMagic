#region Includes
using UnityEngine;
#endregion

namespace TS.ColorPicker.Demo
{
    public class ColorPickerDemo : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private GameObject _targetObject;  // Reference to existing object
        [SerializeField] private ColorPicker _colorPicker;

        private SpriteRenderer _renderer;
        private Color _color;
        private RaycastHit2D _hit;

        #endregion

        private void Start()
        {
            _colorPicker.OnChanged.AddListener(ColorPicker_OnChanged);
            _colorPicker.OnSubmit.AddListener(ColorPicker_OnSubmit);
            _colorPicker.OnCancel.AddListener(ColorPicker_OnCancel);

            // Get the SpriteRenderer from the assigned GameObject
            _renderer = _targetObject.GetComponent<SpriteRenderer>();

            if (_renderer != null)
            {
                _color = _renderer.color;
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on the assigned object.");
            }
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (_hit.collider != null && _hit.collider.gameObject == _targetObject)
                {
                    _colorPicker.Open(_color);
                }
            }
        }

        private void ColorPicker_OnChanged(Color color)
        {
            if (_renderer != null)
            {
                _renderer.color = color;
            }
        }
        private void ColorPicker_OnSubmit(Color color)
        {
            _color = color;
        }
        private void ColorPicker_OnCancel()
        {
            if (_renderer != null)
            {
                _renderer.color = _color;
            }
        }
    }
}
