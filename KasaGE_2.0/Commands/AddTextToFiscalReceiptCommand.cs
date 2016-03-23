using KasaGE.Core;

namespace KasaGE.Commands
{
	internal class AddTextToFiscalReceiptCommand : WrappedMessage
	{
		public AddTextToFiscalReceiptCommand(string text)
		{
			Command = 54;
			Data = text + "\t";
		}
		public override int Command { get; set;}
		public override string Data { get; set;}
	}
}