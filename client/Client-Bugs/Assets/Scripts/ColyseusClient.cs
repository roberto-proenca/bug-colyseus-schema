using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Colyseus;

public class ColyseusClient : MonoBehaviour {

	// UI Buttons are attached through Unity Inspector
	public Button
		mConnectButton,
		mJoinOrCreateButton;
	
	public Text mSessionIdText;

	public GameObject mapUnit;


	public string roomName = "demo";

	public Transform parentMap;

	private Client _client;
	private Room<GlobalState> _room;


	private void Start () {
		mConnectButton.onClick.AddListener(ConnectToServer);
		mJoinOrCreateButton.onClick.AddListener(JoinOrCreateRoom);
	}

	private void ConnectToServer ()
	{
		/*
		 * Get Colyseus endpoint from InputField
		 */
		const string endpoint = "ws://localhost:2567";

		Debug.Log("Connecting to " + endpoint);

		/*
		 * Connect into Colyeus Server
		 */
		_client = ColyseusManager.Instance.CreateClient(endpoint);
	}

	private async void JoinOrCreateRoom()
	{
		_room = await _client.JoinOrCreate<GlobalState>(roomName, new Dictionary<string, object>());

		mSessionIdText.text = "sessionId: " + _room.SessionId;

		PlayerPrefs.SetString("roomId", _room.Id);
		PlayerPrefs.SetString("sessionId", _room.SessionId);
		PlayerPrefs.Save();

		_room.OnLeave += (code) => Debug.Log("ROOM: ON LEAVE");
		_room.OnError += Debug.LogError;
		_room.OnStateChange += OnStateChangeHandler;
	}

	private void OnStateChangeHandler (GlobalState state, bool isFirstState)
	{
		Debug.Log("State has been updated!");

		if (!isFirstState) return;
		
		foreach (string key in state.items.Keys)
		{
			Debug.Log("-1-> " + key + " - " + state.items[key]);
			var obj = Instantiate(mapUnit, parentMap);

			obj.GetComponent<MapElement>().SetValues(key, state.items[key], _room);
		}
	}
}
