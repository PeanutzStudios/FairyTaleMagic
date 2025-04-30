#if UNITY_EDITOR
using UnityEditor;
#endif

#region includes
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#endregion

namespace TS.ColorPicker
{
    public class HairOutlineColor : MonoBehaviour
    {
        #region Variables

        [Header("References")]
        [SerializeField] private HsbPicker _hsbPicker;
        [SerializeField] private Image _colorResult;
        [SerializeField] private InputHex _inputHex;

        [Header("Events")]
        public UnityEvent<Color> OnChanged;
        public UnityEvent<Color> OnSubmit;
        public UnityEvent OnCancel;

        private InputColorChannels _inputRgb;
        private Color _currentColor = Color.white;
        private Texture2D _screenTexture;
        private GameObject[] colorPickerObjects;

        public static bool IsColorPickerOpen { get; private set; } = false; // Track if color picker is open

        #endregion

        private void Awake()
        {
            _inputRgb = GetComponent<InputColorChannels>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_inputRgb == null) { throw new System.Exception("Missing InputColorChannels"); }
#endif
        }

        private void Start()
        {
            _hsbPicker.ValueChanged = HsbPicker_ValueChanged;
            _inputRgb.ValueChanged = InputColorChannels_RGB_ValueChanged;
            _inputHex.ValueChanged = InputHex_ValueChanged;

            enabled = false;

            // Find all objects with the tag "BGColor"
            colorPickerObjects = GameObject.FindGameObjectsWithTag("frontHairoutline");

            // Load the saved color when the game starts
            FrontHairOutlineStorage.Instance.LoadColor();
            Color savedColor = FrontHairOutlineStorage.Instance.SelectedColor;
            UpdateColor(savedColor);
        }

        public void Open()
        {
            IsColorPickerOpen = true; // Set flag when opening
            Open(FrontHairOutlineStorage.Instance.SelectedColor);
        }

        public void Open(Color color)
        {
            IsColorPickerOpen = true; // Set flag when opening
            Enable(true);
            UpdateColor(color);
        }

        private void Update()
        {
            if (_screenTexture == null)
                return;

            var mousePosition = Input.mousePosition;
            var color = _screenTexture.GetPixel((int)mousePosition.x, (int)mousePosition.y);

            UpdateColor(color);

            if (Input.anyKeyDown)
            {
                Destroy(_screenTexture);
                enabled = false;
            }
        }

        private void HsbPicker_ValueChanged(HsbPicker sender, float hue, float saturation, float brightness)
        {
            var color = Color.HSVToRGB(hue, saturation, brightness);
            SetCurrentColor(color);
        }

        private void InputColorChannels_RGB_ValueChanged(InputColorChannels sender, Color color)
        {
            SetCurrentColor(color);
        }

        private void InputHex_ValueChanged(InputHex sender, Color color)
        {
            SetCurrentColor(color);
        }

        private void Enable(bool enable)
        {
            gameObject.SetActive(enable);
        }

        private void UpdateColor(Color color)
        {
            SetCurrentColor(color);
        }

        private void SetCurrentColor(Color color)
        {
            _currentColor = color;
            _colorResult.color = _currentColor;

            // Save the color using ColorStorage
            FrontHairOutlineStorage.Instance.SaveColor(color);

            // Update objects with tag "BGColor"
            foreach (var obj in colorPickerObjects)
            {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = _currentColor;
                }

                var image = obj.GetComponent<Image>();
                if (image != null)
                {
                    image.color = _currentColor;
                }
            }

            OnChanged?.Invoke(color);
        }

        private IEnumerator EnableScreenPicker_Coroutine()
        {
            yield return new WaitForEndOfFrame();

            _screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            _screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            _screenTexture.Apply();

            enabled = true;
        }

        public void UI_Button_Picker()
        {
            StartCoroutine(EnableScreenPicker_Coroutine());
        }

        public void UI_Button_Apply()
        {
            OnSubmit?.Invoke(_currentColor);
            IsColorPickerOpen = false; // Reset flag when closing
            Enable(false);
        }

        public void UI_Button_Cancel()
        {
            OnCancel?.Invoke();
            IsColorPickerOpen = false; // Reset flag when closing
            Enable(false);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ColorPickerForBackground))]
        public class ColorPickerEditor : Editor
        {
            private ColorPickerForBackground _target;
            private Color _color;

            private void OnEnable()
            {
                _target = (ColorPickerForBackground)target;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Editor");

                if (GUILayout.Button("Open"))
                {
                    _target.Open();
                }

                EditorGUILayout.BeginHorizontal();
                _color = EditorGUILayout.ColorField(_color);
                if (GUILayout.Button("Open with Color"))
                {
                    _target.Open(_color);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
#endif
    }
}
