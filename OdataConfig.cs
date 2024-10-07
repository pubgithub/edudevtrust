using BookLibraryApi.Models;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace BookLibraryApi
{
    public sealed class OdataConfig
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            return builder.GetEdmModel();
        }
    }
}