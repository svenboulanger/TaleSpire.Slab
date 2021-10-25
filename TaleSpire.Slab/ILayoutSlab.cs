using System.Collections.Generic;

namespace TaleSpire.Slab
{
    /// <summary>
    /// A slab that supports layouts.
    /// </summary>
    /// <seealso cref="ISlab"/>
    public interface ILayoutSlab : ISlab
    {
        /// <summary>
        /// Gets the layouts embedded in the slab.
        /// </summary>
        public IReadOnlyList<ILayout> Layouts { get; }
    }
}
