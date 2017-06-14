﻿using dotSpace.BaseClasses;
using System;

namespace dotSpace.Objects.Spaces
{

    /// <summary>
    /// Concrete implementation of a tuplespace datastructure.
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class does not impose ordering on the underlying tuples, as they are inserted randomly into the underlying set.
    /// </summary>
    public sealed class RandomSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Returns a random index of where the tuple must be inserted.
        /// </summary>
        protected override int GetIndex(int size)
        {
            return Environment.TickCount % (size+1);
        }

        #endregion
    }
}