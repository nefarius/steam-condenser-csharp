using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public enum SteamPacketTypes : byte
	{
		A2A_PING						= 0x69,  // 'i'
		A2A_ACK							= 0x6A,  // 'j'
		A2S_INFO						= 0x54,  // 'T'
		A2S_PLAYER						= 0x55,  // 'U'
		A2S_RULES						= 0x56,  // 'V'
		A2S_SERVERQUERY_GETCHALLENGE	= 0x57,  // 'W'
		S2C_CHALLENGE					= 0x41,  // 'A'
		S2A_INFO						= 0x43,  // 'C'
		S2A_INFO_DETAILED				= 0x6D,  // 'm'
		S2A_INFO2						= 0x49,  // 'I'
		S2A_PLAYER						= 0x44,  // 'D'
		S2A_RULES						= 0x45,  // 'E'
		A2M_GET_SERVERS_BATCH2			= 0x31,  // '1'
		M2A_SERVER_BATCH				= 0x66,  // 'f'
	}
}
