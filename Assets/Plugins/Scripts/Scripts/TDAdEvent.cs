using System;

namespace Tapdaq {
	public class TDAdEvent {
		public readonly string adType;
		public readonly string message;

		public TDAdEvent (string adType, string message) {
			this.adType = adType;
			this.message = message;
		}
	}
}