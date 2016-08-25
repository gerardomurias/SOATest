using System;
using System.Collections.Generic;
using System.Linq;
using SOATest.Contracts;

namespace SOATest.Domain
{
    public class ProductRepository : IProductRepository
    {
        private int _standardReservationTimeoutMinutes = 1;

        public List<Product> DatabaseProducts { get; set; }

        public List<ProductPurchase> DatabaseProductPurchases { get; set; }

        public List<ProductReservation> DatabaseProductReservations { get; set; }

        public ProductRepository()
        {
            InitialiseDatabase();
        }

        private void InitialiseDatabase()
        {
            DatabaseProducts = new List<Product>();
            Product firstProduct = new Product()
            {
                Allocation = 200,
                Description = "Product",
                Id = "13a35876 - ccf1 - 468a - 88b1 - 0acc04422243",
                Name = "A"
            };
            Product secondProduct = new Product()
            {
                Allocation = 200,
                Description = "Product",
                Id = "13a35876 - ccf1 - 468a - 88b1 - 0acc04422244",
                Name = "B"
            };

            DatabaseProducts.Add(firstProduct);
            DatabaseProducts.Add(secondProduct);

            DatabaseProductPurchases = new List<ProductPurchase>();

            DatabaseProductPurchases.Add(new ProductPurchase(" 0ede40e0 - 5a52 - 48b1 - 8578 - de1891c5a7f0 ", firstProduct, 10));
            DatabaseProductPurchases.Add(new ProductPurchase(" 5868144e-e04d - 4c1f - 81d7 - fc671bfc52dd ", firstProduct, 20));
            DatabaseProductPurchases.Add(new ProductPurchase(" 8e6195ac - d448 - 4e28 - 9064 - b3b1b792895e ", secondProduct, 12));
            DatabaseProductPurchases.Add(new ProductPurchase(" f66844e5 - 594b - 44b8 - a0ef - 2a2064ec2f43 ", secondProduct, 32));
            DatabaseProductPurchases.Add(new ProductPurchase(" 0e73c8b3 - f7fa - 455d - ba7f - 7d3f1bc2e469 ", secondProduct, 1));
            DatabaseProductPurchases.Add(new ProductPurchase(" e28a3cb5 - 1d3e-40a1 - be7e - e0fa12b0c763 ", secondProduct, 4));

            DatabaseProductReservations = new List<ProductReservation>();
            DatabaseProductReservations.Add(new ProductReservation(" a2c2a6db - 763c - 4492 - 9974 - 62ab192201fe ", firstProduct, _standardReservationTimeoutMinutes, 10) { HasBeenConfirmed = true });
            DatabaseProductReservations.Add(new ProductReservation(" 37f2e5ac - bbe0 - 48b0 - a3cd - 9c0b47842fa1 ", firstProduct, _standardReservationTimeoutMinutes, 5) { HasBeenConfirmed = false});
            DatabaseProductReservations.Add(new ProductReservation(" b9393ea4 - 6257 - 4dea - a8cb - b78a0c040255 ", firstProduct, _standardReservationTimeoutMinutes, 13) { HasBeenConfirmed = true });
            DatabaseProductReservations.Add(new ProductReservation(" a70ef898 - 5da9 - 4ac1 - 953c - a6420d37b295 ", firstProduct, _standardReservationTimeoutMinutes, 3) { HasBeenConfirmed = false });
            DatabaseProductReservations.Add(new ProductReservation(" 85eaebfa - 4be4 - 407b - 87cc - 9a9ea46d547b ", secondProduct, _standardReservationTimeoutMinutes, 17));
            DatabaseProductReservations.Add(new ProductReservation(" 39d4278e-5643 - 4c43 - 841c - 214c1c3892b0 ", secondProduct, _standardReservationTimeoutMinutes, 3));
            DatabaseProductReservations.Add(new ProductReservation(" 86fff675 - e5e3 - 4e0e - bcce - 36332c4de165 ", secondProduct, _standardReservationTimeoutMinutes, 9));

            firstProduct.PurchasedProducts = (from p in DatabaseProductPurchases where p.Product.Id == firstProduct.Id select p).Cast<IProductPurchase>().ToList();
            firstProduct.ReservedProducts = (from p in DatabaseProductReservations where p.Product.Id == firstProduct.Id select p).Cast<IProductReservation>().ToList();

            secondProduct.PurchasedProducts = (from p in DatabaseProductPurchases where p.Product.Id == secondProduct.Id select p).Cast<IProductPurchase>().ToList();
            secondProduct.ReservedProducts = (from p in DatabaseProductReservations where p.Product.Id == secondProduct.Id select p).Cast<IProductReservation>().ToList();
        }

        public IProduct FindBy(string productId)
        {
            return (from p in DatabaseProducts where p.Id == productId select p).FirstOrDefault();
        }

        public void Save(IProduct product)
        {
            var finalProduct = (Product) product;
            ClearPurchasedAndReservedProducts(finalProduct);
            InsertPurchasedProducts(finalProduct);
            InsertReservedProducts(finalProduct);
        }

        public void Save(Product product)
        {
            ClearPurchasedAndReservedProducts(product);
            InsertPurchasedProducts(product);
            InsertReservedProducts(product);
        }

        private void ClearPurchasedAndReservedProducts(Product product)
        {
            DatabaseProductPurchases.RemoveAll(p => p.Id == product.Id);
            DatabaseProductReservations.RemoveAll(p => p.Id == product.Id);
        }

        private void InsertReservedProducts(Product product)
        {
            DatabaseProductReservations.AddRange(product.ReservedProducts.Cast<ProductReservation>().ToList());
        }

        private void InsertPurchasedProducts(Product product)
        {
            DatabaseProductPurchases.AddRange(product.PurchasedProducts.Cast<ProductPurchase>().ToList());
        }


        #region "Singleton Pattern"

        public static IProductRepository Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            internal static readonly IProductRepository instance = new ProductRepository();

            static Nested() { }
        }

        #endregion
    }
}