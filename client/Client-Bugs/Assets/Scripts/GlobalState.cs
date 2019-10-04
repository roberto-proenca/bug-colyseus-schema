using Colyseus.Schema;

public class GlobalState : Schema {
	[Type(0, "string")]
	public string status;

	[Type(1, "map", "int8")]
	public MapSchema<int> items = new MapSchema<int>();
}