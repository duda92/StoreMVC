using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Lucene.Net.Documents;

namespace Store.Domain.Entities
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        public Document GetDocument()
        {
            var document = new Document();
            document.Add(new Field("ProductID", ProductID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("ProductName", ProductName, Field.Store.YES, Field.Index.ANALYZED));
            if (string.IsNullOrEmpty(Description))
                document.Add(new Field("Description", string.Empty, Field.Store.YES, Field.Index.ANALYZED));
            else
                document.Add(new Field("Description", Description, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }
    }
}
