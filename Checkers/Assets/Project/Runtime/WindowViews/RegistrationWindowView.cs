using Popups;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.WindowViews
{
    internal sealed class RegistrationWindowView: PopupView, IRegistrationWindowView
    {
        [SerializeField] private TMP_InputField _nickNameInputField;
        [SerializeField] private TMP_InputField _emailInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _registerButton;

        public UnityEvent OnGoBackRequest => _backButton.onClick;
        public UnityEvent OnRegisterRequest => _registerButton.onClick;

#if UNITY_EDITOR

        private void Start()
        {
            //_nickNameInputField.text = "babaYaga";
            //_emailInputField.text = "yaga@izbushka.com";
            //_passwordInputField.text = "223344";
        }

#endif

        public string GetNickname() => _nickNameInputField.text;

        public string GetEmail() => _emailInputField.text;

        public string GetPassword() => _passwordInputField.text;
    }
}
