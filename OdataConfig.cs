using BookLibraryApi.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace BookLibraryApi
{
    /// <summary>
    /// Provides configuration for OData in the Book Library API.
    /// </summary>
    public sealed class OdataConfig
    {
        /// <summary>
        /// Builds and returns the Entity Data Model (EDM) for OData.
        /// </summary>
        /// <returns>The EDM model.</returns>
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            return builder.GetEdmModel();
        }
    }
}
