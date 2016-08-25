using System;
using SOATest.Contracts;
using SOATest.Domain;

namespace SOATest.Service
{
    public class ProductService : IProductService
    {
        private readonly IMessageRepositoryFactory _messageRepositoryFactory;
        private readonly IProductRepositoryFactory _productRepositoryFactory;
        private readonly IMessageRepository _messageRepository;
        private readonly IProductRepository _productRepository;

        public ProductService(IMessageRepositoryFactory messageRepositoryFactory, IProductRepositoryFactory productRepositoryFactory)
        {
            if (messageRepositoryFactory == null) throw new ArgumentNullException("MessageRepositoryFactory");
            if (productRepositoryFactory == null) throw new ArgumentNullException("ProductRepositoryFactory");

            _messageRepositoryFactory = messageRepositoryFactory;
            _productRepositoryFactory = productRepositoryFactory;

            _messageRepository = _messageRepositoryFactory.Create();
            _productRepository = _productRepositoryFactory.Create();
        }

        public IProductReservationResponse ReserveProduct(IReserveProductRequest productReservationRequest)
        {
            ProductReservationResponse reserveProductResponse = new ProductReservationResponse();

            try
            {
                var product = (Product)_productRepository.FindBy(productReservationRequest.ProductId);

                if (product != null)
                {
                    IProductReservation productReservation = null;

                    if (product.CanReserveProduct(productReservationRequest.ProductQuantity))
                    {
                        productReservation = product.Reserve(productReservationRequest.ProductId, productReservationRequest.ProductQuantity);
                        _productRepository.Save(product);
                        reserveProductResponse.ProductId = productReservation.Product.Id.ToString();
                        reserveProductResponse.Expiration = productReservation.Expiry;
                        reserveProductResponse.ProductName = productReservation.Product.Name;
                        reserveProductResponse.ProductQuantity = productReservation.Quantity;
                        reserveProductResponse.ReservationId = productReservation.Id.ToString();
                    }
                }
                else
                {
                    throw new ResourceNotFoundException($"No product with Id {productReservationRequest.ProductId} was found");
                }
            }
            catch (Exception ex)
            {
                reserveProductResponse.Exception = ex;
            }

            return reserveProductResponse;
        }

        public IPurchaseProductResponse PurchaseProduct(IPurchaseProductRequest productPurchaseRequest)
        {
            PurchaseProductResponse purchaseProductResponse = new PurchaseProductResponse();
            try
            {
                if (_messageRepository.IsUniqueRequest(productPurchaseRequest.CorrelationId))
                {
                    Product product = (Product)_productRepository.FindBy(productPurchaseRequest.ProductId);
                    if (product != null)
                    {
                        IProductPurchase productPurchase = null;
                        if (product.ReservationIsValid(productPurchaseRequest.ReservationId))
                        {
                            productPurchase = product.ConfirmPurchaseWith(productPurchaseRequest.ReservationId);
                            _productRepository.Save(product);
                            purchaseProductResponse.ProductId = productPurchase.Product.Id.ToString();
                            purchaseProductResponse.PurchaseId = productPurchase.Id.ToString();
                            purchaseProductResponse.ProductQuantity = productPurchase.ProductQuantity;
                            purchaseProductResponse.ProductName = productPurchase.Product.Name;
                        }
                        else
                        {
                            throw new ResourceNotFoundException($"invalid or expired reservation id: {productPurchaseRequest.ReservationId}");
                        }

                        _messageRepository.SaveResponse(productPurchaseRequest.CorrelationId, purchaseProductResponse);
                    }
                    else
                    {
                        throw new ResourceNotFoundException($"no product with id {productPurchaseRequest.ProductId} was found");
                    }
                }
                else
                {
                    purchaseProductResponse = _messageRepository.RetrieveResponseFor<PurchaseProductResponse>(productPurchaseRequest.CorrelationId);
                }
            }
            catch (Exception ex)
            {
                purchaseProductResponse.Exception = ex;
            }
            return purchaseProductResponse;
        }
    }
}