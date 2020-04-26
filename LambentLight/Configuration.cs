﻿using Newtonsoft.Json;

namespace LambentLight
{
    /// <summary>
    /// Class used for the program configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The page that contains the download list of the CFX Builds.
        /// </summary>
        [JsonProperty("download_builds")]
        public string DownloadBuilds { get; set; } = "https://raw.githubusercontent.com/LambentLight/Builds/master/builds.json";

        /// <summary>
        /// The Server License key.
        /// </summary>
        [JsonProperty("token_cfx")]
        public string CFXLicense { get; set; } = "";
        /// <summary>
        /// The Steam API Token for Steam Identifiers.
        /// </summary>
        [JsonProperty("token_steam")]
        public string SteamKey { get; set; } = "";
    }
}
