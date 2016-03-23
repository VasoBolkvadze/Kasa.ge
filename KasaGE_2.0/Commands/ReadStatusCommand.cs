﻿using KasaGE.Core;

namespace KasaGE.Commands
{
	internal class ReadStatusCommand:WrappedMessage
	{
		public ReadStatusCommand()
		{
			Command = 74;
			Data = string.Empty;
		}

		public override int Command { get; set; }
	    public override string Data { get; set; }
	}
}