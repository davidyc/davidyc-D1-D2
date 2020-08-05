namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class Category
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int CategoryID { get; set; }

        [DataMember]
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [DataMember(Order =1)]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [OnDeserializing]
        private void SetDefaultDescription(StreamingContext c)
        { 
            Description = "Default description";          
        }
    }
}
