﻿using KasaGE.Core;

namespace KasaGE.Commands
{
	internal class GetLastFiscalEntryInfoCommand : WrappedMessage
	{
		public GetLastFiscalEntryInfoCommand(int type)
		{
			Command = 64;
			Data = type + "\t";
		}
		public override int Command { get; }
		public override string Data { get; }
	}
	public enum FiscalEntryInfoType
	{
		CashDebit = 0,
		CashCredit = 1,
		CashFreeDebit = 2,
		CashFreeCredit = 3
	}
}