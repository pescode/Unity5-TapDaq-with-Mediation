using System;
using System.Collections.Generic;

namespace Tapdaq {
	public class TDAds {
		/*
		//
		// Static Fields
		//
		public const string Version = "8.5.1";

		private static TDAds tapDaq;

		//
		// Fields
		//
		private Dictionary<string, string> startParams;

		private Dictionary<string, object> startOptions;

		//
		// Properties
		//
		private bool initialized { get; set; }

		private Settings settings { get; set; }

		//
		// Constructors
		//
		private TDAds () {
			this.startOptions = new Dictionary<string, object> (4);
			this.settings = new Settings ();
		}

		//
		// Static Methods
		//
		public static TDAds With (string appId) {
			if (string.IsNullOrEmpty (appId)) {
				throw new ArgumentException ("App ID cannot be null nor empty");
			}
			if (TDAds.tapDaq == null) {
				TDAds.tapDaq = new Fyber ();
			}
			if (!TDAds.tapDaq.initialized) {
				TDAds.tapDaq.startOptions ["appId"] = appId;
			}
			return TDAds.tapDaq;
		}

		//
		// Methods
		//
		public Settings Start () {
			if (!this.initialized) {
				if (this.startParams != null) {
					this.startOptions ["parameters"] = this.startParams;
				}
				string json = JsonMapper.ToJson (this.startOptions);
				PluginBridge.Start (json);
			}
			return this.settings;
		}

		public TDAds WithManualPrecaching () {
			if (!this.initialized) {
				CacheManager.PauseDownloads ();
				this.startOptions ["startVideoPrecaching"] = false;
			}
			return this;
		}

		public TDAds WithParameters (Dictionary<string, string> parameters) {
			if (!this.initialized) {
				this.startParams = new Dictionary<string, string> (parameters);
			}
			return this;
		}

		public TDAds WithSecurityToken (string securityToken) {
			if (!this.initialized) {
				this.startOptions ["securityToken"] = securityToken;
			}
			return this;
		}

		public TDAds WithUserId (string userId) {
			if (!this.initialized) {
				this.startOptions ["userId"] = userId;
			}
			return this;
		}
		*/
	}
}

