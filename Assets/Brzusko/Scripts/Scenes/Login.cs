using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Brzusko.Events;

namespace Brzusko.Scenes
{
    public class Login : MonoBehaviour
    {
        private static Login _instance;
        public static Login Instance => _instance;

        [SerializeField]
        private CinemachineVirtualCamera _loginView;
        
        [SerializeField]
        private CinemachineVirtualCamera _playerCamera;

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private AboutWindow _playerAccountEditor;

        private PlayerCredentials _credentials;
        private LoadingScreen _loadingScreen;
        private LoginScreen _loginScreen;
        private CrosshairWIndow _crosshair;

        private async Task Start()
        {
            _instance = this;
            
            GetReferences();
            ConnectEvents();

            if(!_credentials) return;

            await InitSequence();
        }

        private void OnDestroy()
        {
            _instance = null;
            DisconnectEvents();
        }

        private void OnRegisterComplete(object sender, BasicMassage mess)
        {
            _loginScreen.DisplayMassage(mess.Message);
        }

        private void OnRegisterFail(object sender, BasicMassage mess)
        {
            _loginScreen.DisplayMassage(mess.Message);
        }

        private void OnLoginComplete(object sender, BasicMassage mess)
        {
            _player.SetActive(true);
            _loadingScreen.Active = false;
            _loginScreen.Active = false;
            SwtichCamera(true);
            _crosshair.Active = true;
        }

        private void OnLoginFail(object sender, BasicMassage mess)
        {
            if(!_loginScreen.gameObject.activeSelf)
            {
                _loadingScreen.Active = false;
                _loginScreen.Active = true;
            }

            _loginScreen.DisplayMassage(mess.Message);
        }

        private void OnLogout(object sender, BasicMassage mess)
        {
            _player.SetActive(false);
            SwtichCamera(false);
            _crosshair.Active = false;
            _loginScreen.Active = true;
        }

        private void ConnectEvents()
        {
            var playerCred = PlayerCredentials.Instance;
            playerCred.RegisterComplete += OnRegisterComplete;
            playerCred.RegisterFailed += OnRegisterFail;
            playerCred.LoginComplete += OnLoginComplete;
            playerCred.LoginFailed += OnLoginFail;
            playerCred.LogoutComplete += OnLogout;
        }

        private void DisconnectEvents()
        {
            var playerCred = PlayerCredentials.Instance;
            playerCred.RegisterComplete -= OnRegisterComplete;
            playerCred.RegisterFailed -= OnRegisterFail;
            playerCred.LoginComplete -= OnLoginComplete;
            playerCred.LoginFailed -= OnLoginFail;
            playerCred.LogoutComplete -= OnLogout;
        }

        private void GetReferences()
        {
            var ui = UIReferenceHandler.Instance;
            _credentials = PlayerCredentials.Instance;
            if(!ui || !_credentials) return;

            _loadingScreen = ui.LoadingScreen;
            _loginScreen = ui.LoginScreen;
            _crosshair = ui.Crosshair;
        }

        private async Task InitSequence()
        {
            _loadingScreen.ChangeLoadingInfo("Trying to login autologin.");
            _player.SetActive(false);
            _playerAccountEditor.Active = true;

            if(_credentials.KeysExist())
            {
                await _credentials.LoginRef();
                return;
            }

            _loadingScreen.ChangeLoadingInfo("Failed to log in, openning login scene!");
            await Task.Delay(1000);
            _loadingScreen.Active = false;
            _loginScreen.Active = true;
        }
        public void SwtichCamera(bool toPlayer) => _loginView.Priority = toPlayer ? 1 : 3;

    }
}

