﻿using System.IO;

namespace LambentLight.Builds
{
    /// <summary>
    /// Class that contains the information of a specific CFX build.
    /// </summary>
    public class Build
    {
        #region Public Properties

        /// <summary>
        /// The ID of the build. This can be either the folder name or SHA1 hash.
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// The local folder where the build can be located.
        /// </summary>
        public string Folder { get; private set; }
        /// <summary>
        /// The location of the CFX Server executable.
        /// </summary>
        public string Executable { get; private set; }
        /// <summary>
        /// If the server executable is present.
        /// </summary>
        public bool IsExecutablePresent => File.Exists(Executable);
        /// <summary>
        /// If the folder for the build exists.
        /// </summary>
        public bool IsFolderPresent => File.Exists(Folder);

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a Build to use with LambentLight
        /// </summary>
        /// <param name="identifier">The folder name or Upstream identifier.</param>
        public Build(string identifier)
        {
            // Set our identifier
            ID = identifier;
            // And the other properties that we need
            Folder = Path.Combine(Locations.BuildsForOS, ID);
            Executable = Path.Combine(Folder, "FXServer.exe");
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the string representation of a build.
        /// </summary>
        public override string ToString() => ID + " " + (IsExecutablePresent ? "(Ready to Use)" : "(Requires Download)");
        /// <summary>
        /// Gets the Hash of the Build Identifier.
        /// </summary>
        public override int GetHashCode() => ID.GetHashCode();
        /// <summary>
        /// Checks if the compared object has the same ID as the current one.
        /// </summary>
        public override bool Equals(object obj) => obj is Build && ID == ((Build)obj).ID;

        #endregion
    }
}
