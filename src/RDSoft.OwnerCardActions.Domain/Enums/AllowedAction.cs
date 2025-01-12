using System.Text.Json.Serialization;

namespace RDSoft.OwnerCardActions.Domain.Enums
{

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum AllowedAction
	{
	   Action1,
	   Action2,
	   Action3,
	   Action4,
	   Action5,
	   Action6,
	   Action7,
	   Action8,
	   Action9,
	   Action10,
	   Action11,
	   Action12,
	   Action13
	}
}
