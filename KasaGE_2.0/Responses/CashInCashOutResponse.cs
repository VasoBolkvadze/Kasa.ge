﻿using System.Globalization;
using KasaGE.Core;

namespace KasaGE.Responses
{
	public class CashInCashOutResponse:FiscalResponse
	{
		public CashInCashOutResponse(byte[] buffer) : base(buffer)
		{
			var values = getDataValues();
			if (values.Length == 0) return;
			CashSum = decimal.Parse(values[0], CultureInfo.InvariantCulture);
			CashIn = decimal.Parse(values[1], CultureInfo.InvariantCulture);
			CashOut = decimal.Parse(values[2], CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Cash in safe sum 
		/// </summary>
		public decimal CashSum { get; set; }
		/// <summary>
		/// Total sum of cash in operations 
		/// </summary>
		public decimal CashIn { get; set; }
		/// <summary>
		/// Total sum of cash out operations 
		/// </summary>
		public decimal CashOut { get; set; }
    }
}