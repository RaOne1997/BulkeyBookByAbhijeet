﻿namespace InstamojoAPI
{
	public class PaymentOrderListResponse
	{

		public int? count { get; set; }
		public string next { get; set; }
		public string previous { get; set; }
		public Orderss[] orders { get; set; }

	}
}
