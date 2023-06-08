using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public partial class SceneNetworkManager : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _runner;
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
        private NetworkEvents NetworkEvents { get; set; }

        private void Awake()
        {
            NetworkEvents = GetComponent<NetworkEvents>();
            NetworkEvents.PlayerJoined.AddListener(OnPlayerJoined);
            NetworkEvents.PlayerLeft.AddListener(OnPlayerLeft);
            
        }

        public async void StartGame(GameMode mode)
        {
            _runner.ProvideInput = true;
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartGame(GameMode.Host);
            }

            if (GUI.Button(new Rect(0, 80, 200, 40), "Shared"))
            {
                StartGame(GameMode.Shared);
            }

            if (GUI.Button(new Rect(0, 40, 200, 40), "client"))
            {
                StartGame(GameMode.Client);
            }
        }

        private float spawnX;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                Vector3 spawnPosition = new Vector3(spawnX, 0, 0);
                spawnX += 3;
                NetworkObject networkPlayerObject =
                    runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
                _spawnedCharacters.Add(player, networkPlayerObject);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }
        
    }
}