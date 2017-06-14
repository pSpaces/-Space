﻿using dotSpace.Interfaces;

namespace dotSpace.Objects.Spaces
{
    /// <summary>
    /// Concrete implementation of a pattern.
    /// Provides the primitives required to define a pattern. 
    /// </summary>
    public sealed class Pattern : IPattern
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the Pattern class.
        /// </summary>
        public Pattern(params object[] fields)
        {
            this.Fields = fields;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values representing the pattern.
        /// </summary>
        public object[] Fields { get; set; }

        /// <summary>
        /// Returns the size of the pattern.
        /// </summary>
        public int Size { get { return Fields.Length; } }

        /// <summary>
        /// Gets or sets the i'th element of the pattern.
        /// </summary>
        public object this[int idx] { get { return this.Fields[idx]; } }

        #endregion

    };
}
