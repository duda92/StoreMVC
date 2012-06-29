using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Lucene.Net.Documents;

namespace Store.Domain.Entities
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        public Document GetDocument()
        {
            var document = new Document();
            document.Add(new Field("CategoryID", CategoryID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("CategoryName", CategoryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Description", Description, Field.Store.YES, Field.Index.ANALYZED));
            return document;
        }
    }
}
