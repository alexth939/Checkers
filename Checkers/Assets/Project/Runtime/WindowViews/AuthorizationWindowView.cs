﻿using System;
using Popups;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.WindowViews
{
    internal sealed class AuthorizationWindowView: PopupView, IAuthorizationWindowView
    {
        [SerializeField] private TMP_InputField _emailInputField;
        [SerializeField] private TMP_InputField _passwordInputField;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _signInButon;

        public UnityEvent OnGoBackRequest => _backButton.onClick;
        public UnityEvent OnAuthorizeRequest => _signInButon.onClick;

        private void Start()
        {
            //(_emailInputField.text, _passwordInputField.text) = new CredentialsLoader();
            //_emailInputField.text = "tester@tester.com";
            //_passwordInputField.text = "tester";
        }

        public string GetEmail() => _emailInputField.text;

        public string GetPassword() => _passwordInputField.text;

        // todo implement me
        internal void PlayConnectingAnimation()
        {
            throw new NotImplementedException();
        }

        internal void StopConnectingAnimation()
        {
            throw new NotImplementedException();
        }
    }
}
