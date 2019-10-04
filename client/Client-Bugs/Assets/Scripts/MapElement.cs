using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Colyseus;
using Colyseus.Schema;


public class MapElement : MonoBehaviour
{
    public Button more, less, del;

	public Text id, qtt;

    private Room<GlobalState> _room;

    public void SetValues(string id, int qtt, Room<GlobalState> room ) {
        this.id.text = id;
        this.qtt.text = qtt.ToString();

        _room = room;

        more.onClick.AddListener(() => {
            room?.Send(new { type = "map", id = id, amount = 1});
        });

        less.onClick.AddListener(() => {
            room?.Send(new { type = "map", id = id, amount = -1});
        });

        del.onClick.AddListener(() => {
            room?.Send(new { type = "map", id = id, amount = 0});
        });
        
        _room.State.items.OnChange += (int value, string key) => {
            if (key == this.id.text) {
                this.qtt.text = value.ToString();
                Debug.Log("change " + key + " " + value);
            }
        };
    }

}
