﻿using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;
using System.IO;

namespace LambentLight.Managers
{
    /// <summary>
    /// Identifies the type of a resource.
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// A simple script.
        /// </summary>
        Script = 0,
        /// <summary>
        /// A completely different gamemode.
        /// </summary>
        GameMode = 1,
        /// <summary>
        /// A new loading screen.
        /// </summary>
        LoadingScreen = 2
    }

    /// <summary>
    /// The specific version information of a resource.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// The readable version. This can be either semantic versioning or the git commit.
        /// </summary>
        [JsonProperty("version")]
        public string ReadableVersion { get; set; }
        /// <summary>
        /// The place where we can download the resource with the specific version.
        /// </summary>
        [JsonProperty("download")]
        public string Download { get; set; }
        /// <summary>
        /// The Path of the resource inside of the compressed files.
        /// This take precedence over the Resource Path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// The compression used for the download.
        /// </summary>
        [JsonProperty("compression")]
        public CompressionType Compression { get; set; }
        /// <summary>
        /// The extension of the file based on the compression type.
        /// </summary>
        public string Extension
        {
            get
            {
                switch (Compression)
                {
                    case CompressionType.Zip:
                        return ".zip";
                    case CompressionType.SevenZip:
                        return ".7z";
                    case CompressionType.Rar:
                        return ".rar";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the readable version of the resource.
        /// </summary>
        /// <returns>The readable version of the resource.</returns>
        public override string ToString() => ReadableVersion;
    }

    /// <summary>
    /// Class that contains the informatioon of a specific downloadable resource.
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// The readable name of the resource.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// The creator of the resource.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }
        /// <summary>
        /// The name of the folder of the resource.
        /// </summary>
        [JsonProperty("folder")]
        public string Folder { get; set; }
        /// <summary>
        /// The Path of the resource inside of the compressed files.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// The type of resource. Only used for the UI.
        /// </summary>
        [JsonProperty("type")]
        public ResourceType Type { get; set; }
        /// <summary>
        /// The URL of the license page.
        /// </summary>
        [JsonProperty("license")]
        public string License { get; set; }
        /// <summary>
        /// The list of requirements of the resource.
        /// </summary>
        [JsonProperty("requires")]
        public List<string> Requires { get; set; }
        /// <summary>
        /// The resource configuration instructions by the author.
        /// </summary>
        [JsonProperty("instructions")]
        public string ConfigInstructions { get; set; }
        /// <summary>
        /// A list with the versions of the resource.
        /// </summary>
        [JsonProperty("versions")]
        public List<Version> Versions { get; set; }

        /// <summary>
        /// Checks if a specific resource is installed on a Data Folder.
        /// </summary>
        /// <param name="folder">The folder to check against to.</param>
        /// <returns>true if the resource is installed, false otherwise.</returns>
        public bool IsInstalledIn(DataFolder folder)
        {
            // Format the path of the folder
            string Location = System.IO.Path.Combine(folder.Resources, Folder);
            // Return if the folder exists
            return Directory.Exists(Location);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Class that manages the resources of the installer.
    /// </summary>
    public static class ResourceManager
    {
        /// <summary>
        /// The logger for our current class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// All of the resources that are available for installing.
        /// </summary>
        public static List<Resource> Resources = new List<Resource>();

        /// <summary>
        /// Refreshes the list of resources.
        /// </summary>
        public static void Refresh()
        {
            // Create a temporary list of resources
            Resources = Downloader.DownloadJSON<List<Resource>>(Properties.Settings.Default.Resources, new BuildConverter());
            // Log what we have just done
            Logger.Debug("The list of resources has been updated");
        }
    }
}
