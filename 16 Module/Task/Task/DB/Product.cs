namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductName", ProductName, typeof(string));
            info.AddValue("SupplierID", SupplierID, typeof(int?));
            info.AddValue("CategoryID", CategoryID, typeof(int?));
            info.AddValue("QuantityPerUnit", QuantityPerUnit, typeof(string));
            info.AddValue("UnitPrice", UnitPrice, typeof(decimal?));
            info.AddValue("UnitsInStock", UnitsInStock, typeof(short?));
            info.AddValue("UnitsOnOrder", UnitsOnOrder, typeof(short?));
            info.AddValue("ReorderLevel", ReorderLevel, typeof(short?));
            info.AddValue("Discontinued", Discontinued, typeof(bool));
            info.AddValue("Category", Category, typeof(Category));
            info.AddValue("Order_Details", Order_Details, typeof(ICollection<Order_Detail>));
            info.AddValue("Supplier", Supplier, typeof(Supplier));

        }

        private Product(SerializationInfo info, StreamingContext context)
        {
            ProductName = (string)info.GetValue("ProductName", typeof(string));
            SupplierID = (int?)info.GetValue("SupplierID", typeof(int?));
            CategoryID = (int?)info.GetValue("CategoryID", typeof(int?));
            QuantityPerUnit = (string)info.GetValue("QuantityPerUnit", typeof(string));
            UnitPrice = (decimal?)info.GetValue("UnitPrice", typeof(decimal?));
            UnitsInStock = (short?)info.GetValue("UnitsInStock", typeof(short?));
            UnitsOnOrder = (short?)info.GetValue("UnitsOnOrder", typeof(short?));
            ReorderLevel = (short?)info.GetValue("ReorderLevel", typeof(short?));
            Discontinued = (bool)info.GetValue("Discontinued", typeof(bool));
            Category = (Category)info.GetValue("Category", typeof(Category));
            Supplier = (Supplier)info.GetValue("Supplier", typeof(Supplier));
            Order_Details = (ICollection<Order_Detail>)info.GetValue("Order_Details", typeof(ICollection<Order_Detail>));
        }
    }
}
