using System;

namespace CC98.Medal.Models
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; } = null!;

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
