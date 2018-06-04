using Domain.Base.Repository.Specification;
using Domain.Common.Base.Aggregation;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Exception;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Repository.Specification;
using Domain.Common.Base.Value;
using Domain.Common.Factory.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Domain.Common.Test.Aggregation
{
    [TestClass]
    public class ProductAggregationTest
    {
        private Mock<IShoppingRepository> shoppingQueryRepositoryMock;
        private Mock<IShoppingRepository> emptyShoppingQueryRepositoryMock;
        private Mock<IShoppingRepository> finishedShoppingQueryRepositoryMock;
        private Mock<IShoppingQueryRepositorySpecificationFactory> shoppingQueryRepositorySpecificationFactoryMock;
        private Mock<IShoppingItemRepository> shoppingItemRepositoryMock;
        private Mock<IShoppingItemRepository> shoppingItemRepositoryWithItemMock;
        private Mock<IShoppingItemByIdRepositorySpecificationFactory> shoppingItemByIdRepositorySpecificationFactoryMock;
        private Mock<IProductCategoryEntity> categoryMock;
        private IList<IShoppingEntity> activeShoppings;

        private IList<IProductDetailValue> productDetailDb;
        private IList<IProductPromotionValue> productPromotionDb;

        private readonly long productId = 7;
        private readonly int productQuantity = 9;
        private readonly long shoppingId = 1;
        private Action<IShoppingItemEntity> updateShoppingItemCallback = null;

        [TestInitialize]
        public void Config()
        {
            BuildProductDetailDb();
            BuildProductPromotion();

            activeShoppings = BuildActiveShoppingsList();

            MockShoppingRepository();
            MockEmptyShoppingRepository();
            MockFinishedShoppingRepository();

            shoppingQueryRepositorySpecificationFactoryMock = new Mock<IShoppingQueryRepositorySpecificationFactory>();
            shoppingQueryRepositorySpecificationFactoryMock.Setup(x => x.NewActiveShoppingSpec()).Returns(new Mock<IShoppingRepositorySpecification>().Object);

            shoppingItemByIdRepositorySpecificationFactoryMock = new Mock<IShoppingItemByIdRepositorySpecificationFactory>();
            shoppingItemByIdRepositorySpecificationFactoryMock.Setup(x => x.NewQuerySpec(It.IsAny<IShoppingEntity>(), It.IsAny<long>())).Returns(new Mock<IShoppingItemByIdRepositorySpecification>().Object);
            shoppingItemByIdRepositorySpecificationFactoryMock.Setup(x => x.NewUpdateSpec(It.IsAny<IShoppingEntity>(), It.IsAny<long>())).Returns(new Mock<IShoppingItemByIdRepositorySpecification>().Object);

            categoryMock = new Mock<IProductCategoryEntity>();
            categoryMock.Setup(x => x.Id).Returns(3);
            categoryMock.Setup(x => x.Name).Returns("Lava-roupas");

            MockShoppingItemRepository();
            MockShoppingItemRepositoryWithItem();

        }

        [TestMethod]
        public void ShouldFindActiveShopping()
        {
            var entityFactory = new EntityFactory();
            IProductAggregation product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, 0, 0, false);

            product.IncQuantity(
                shoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                });

            shoppingQueryRepositoryMock
                .Verify(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()), Times.Once());
        }

        [TestMethod]
        public void ShouldCallErrorCallbackByNoActiveShopping()
        {
            var entityFactory = new EntityFactory();
            IProductAggregation product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, 0, 0, false);

            Exception ex = null;

            product.IncQuantity(
                emptyShoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                    ex = err;
                });

            emptyShoppingQueryRepositoryMock
                .Verify(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()), Times.Once());

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(NoActiveShoppingException));

            ex = null;

            product.IncQuantity(
                finishedShoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                    ex = err;
                });

            finishedShoppingQueryRepositoryMock
                .Verify(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()), Times.Once());

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(NoActiveShoppingException));
        }

        [TestMethod]
        public void IncQuantity_ShouldIncreaseAndStoreTheQuantity()
        {
            var entityFactory = new EntityFactory();
            IProductAggregation product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, 0, 0, false);

            product.IncQuantity(
                shoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                });

            Assert.AreEqual(1, product.Quantity);
            shoppingItemRepositoryMock
                .Verify(x => x.Insert(It.IsAny<IShoppingItemEntity>(), It.IsAny<Action<long>>(), It.IsAny<Action<Exception>>()), Times.Once());

            product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, 0, productQuantity, false);
            IShoppingItemEntity updatedItem = null;

            // *******************************************************
            updateShoppingItemCallback = (item) => updatedItem = item;

            product.IncQuantity(
                shoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryWithItemMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                });

            Assert.IsNotNull(updatedItem);
            Assert.IsTrue(updatedItem.Quantity > productQuantity);

        }

        [TestMethod]
        public void IncQuantity_ShouldIncreaseQuantityAndApplyADiscount()
        {
            var initialQuantity = 19;

            var entityFactory = new EntityFactory();
            var product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, 0, initialQuantity, false);
            
            product.IncQuantity(
                shoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryWithItemMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                });

            Assert.AreEqual(initialQuantity + 1, product.Quantity);
            Assert.AreEqual(productPromotionDb[2].Details[1].Discount, product.Discount);
        }

        [TestMethod]
        public void DecQuantity_ShouldDecreaseQuantityAndChangeTheDiscount()
        {
            var initialQuantity = 30;

            var entityFactory = new EntityFactory();
            var product = entityFactory.NewProduct(productId, productDetailDb[6], productPromotionDb[2], categoryMock.Object, productPromotionDb[2].Details[2].Discount, initialQuantity, false);

            product.DecQuantity(
                shoppingQueryRepositoryMock.Object,
                shoppingQueryRepositorySpecificationFactoryMock.Object,
                shoppingItemRepositoryWithItemMock.Object,
                shoppingItemByIdRepositorySpecificationFactoryMock.Object,
                success =>
                {
                }, err =>
                {
                });

            Assert.AreEqual(initialQuantity - 1, product.Quantity);
            Assert.AreEqual(productPromotionDb[2].Details[1].Discount, product.Discount);
        }

        /// <summary>
        /// 
        /// </summary>
        private void MockShoppingItemRepositoryWithItem()
        {
            shoppingItemRepositoryWithItemMock = new Mock<IShoppingItemRepository>();
            shoppingItemRepositoryWithItemMock
                .Setup(x => x.GetById(It.IsAny<IShoppingItemByIdRepositorySpecification>(), It.IsAny<Action<IShoppingItemEntity>>(), It.IsAny<Action<Exception>>()))
                .Callback((IByIdRepositorySpecification<IShoppingItemEntity, long> any, Action<IShoppingItemEntity> success, Action<Exception> error) =>
                {
                    var mock = new Mock<IShoppingItemEntity>();

                    mock.Setup(x => x.Id).Returns(productId);
                    mock.Setup(x => x.ShoppingId).Returns(shoppingId);
                    mock.Setup(x => x.Quantity).Returns(productQuantity);
                    mock.Setup(x => x.Price).Returns(productDetailDb[6].Price);
                    mock.Setup(x => x.Discount).Returns(0);

                    success.Invoke(mock.Object);
                });
            shoppingItemRepositoryWithItemMock
                .Setup(x => x.Update(It.IsAny<IShoppingItemEntity>(), It.IsAny<IShoppingItemByIdRepositorySpecification>(), It.IsAny<Action<IShoppingItemEntity>>(), It.IsAny<Action<System.Exception>>()))
                .Callback((IShoppingItemEntity any, IByIdRepositorySpecification<IShoppingItemEntity, long> spec, Action<IShoppingItemEntity> success, Action<Exception> error) =>
                {
                    if(updateShoppingItemCallback != null)
                    {
                        updateShoppingItemCallback.Invoke(any);
                    }
                    success(any);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void MockShoppingItemRepository()
        {
            shoppingItemRepositoryMock = new Mock<IShoppingItemRepository>();
            shoppingItemRepositoryMock
                .Setup(x => x.GetById(It.IsAny<IShoppingItemByIdRepositorySpecification>(), It.IsAny<Action<IShoppingItemEntity>>(), It.IsAny<Action<Exception>>()))
                .Callback((IByIdRepositorySpecification<IShoppingItemEntity, long> any, Action<IShoppingItemEntity> success, Action<Exception> error) =>
                {
                    success.Invoke(new EntityFactory().EmptyShoppingItem());
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void MockEmptyShoppingRepository()
        {
            emptyShoppingQueryRepositoryMock = new Mock<IShoppingRepository>();
            emptyShoppingQueryRepositoryMock
                .Setup(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()))
                .Callback((IRepositorySpecification<IShoppingEntity, long> any, Action<IList<IShoppingEntity>> success, Action<Exception> error) =>
                {
                    success.Invoke(new List<IShoppingEntity>());
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void MockShoppingRepository()
        {
            shoppingQueryRepositoryMock = new Mock<IShoppingRepository>();
            shoppingQueryRepositoryMock
                .Setup(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()))
                .Callback((IRepositorySpecification<IShoppingEntity, long> any, Action<IList<IShoppingEntity>> success, Action<Exception> error) =>
                {
                    success.Invoke(activeShoppings);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        private void MockFinishedShoppingRepository()
        {
            finishedShoppingQueryRepositoryMock = new Mock<IShoppingRepository>();
            finishedShoppingQueryRepositoryMock
                .Setup(x => x.Find(It.IsAny<IShoppingRepositorySpecification>(), It.IsAny<Action<IList<IShoppingEntity>>>(), It.IsAny<Action<Exception>>()))
                .Callback((IRepositorySpecification<IShoppingEntity, long> any, Action<IList<IShoppingEntity>> success, Action<Exception> error) =>
                {
                    var shoppings = new List<IShoppingEntity>(activeShoppings);

                    var mock = new Mock<IShoppingEntity>();
                    mock.Setup(x => x.Id).Returns(10);
                    mock.Setup(x => x.Creation).Returns(DateTime.Now.AddMinutes(1));
                    mock.Setup(x => x.Creation).Returns(DateTime.Now.AddMinutes(30));
                    mock.Setup(x => x.IsFinished).Returns(true);

                    shoppings[0] = mock.Object;

                    success.Invoke(shoppings);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IList<IShoppingEntity> BuildActiveShoppingsList()
        {
            var result = new List<IShoppingEntity>();
            for (var i = 1; i <= 5; ++i)
            {
                var activeShoppingMock = new Mock<IShoppingEntity>();
                activeShoppingMock.Setup(x => x.Id).Returns(i * 10);
                activeShoppingMock.Setup(x => x.Creation).Returns(DateTime.Now.AddMinutes(i));
                activeShoppingMock.Setup(x => x.Creation).Returns(i == 1 ? DateTime.MinValue : DateTime.Now.AddMinutes(i * 30));

                result.Add(activeShoppingMock.Object);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildProductPromotion()
        {
            productPromotionDb = new List<IProductPromotionValue>();

            var productPromotionDetails = new List<IProductPromotionDetailValue>();
            var productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(1);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(10);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            var productPromotionMock = new Mock<IProductPromotionValue>();
            productPromotionMock.Setup(x => x.Name).Returns("Promoção Oi Me Liga");
            productPromotionMock.Setup(x => x.CategoryId).Returns(2);
            productPromotionMock.Setup(x => x.Details).Returns(productPromotionDetails);
            productPromotionMock.Setup(x => x.Clone()).Returns(productPromotionMock.Object);
            productPromotionDb.Add(productPromotionMock.Object);
            productPromotionDetails = new List<IProductPromotionDetailValue>();
            productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(2);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(10);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(3);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(15);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            productPromotionMock = new Mock<IProductPromotionValue>();
            productPromotionMock.Setup(x => x.Name).Returns("Promoção #100porcentoselfie");
            productPromotionMock.Setup(x => x.CategoryId).Returns(5);
            productPromotionMock.Setup(x => x.Details).Returns(productPromotionDetails);
            productPromotionMock.Setup(x => x.Clone()).Returns(productPromotionMock.Object);
            productPromotionDb.Add(productPromotionMock.Object);
            productPromotionDetails = new List<IProductPromotionDetailValue>();
            productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(10);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(10);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(20);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(20);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            productPromotionDetailMock = new Mock<IProductPromotionDetailValue>();
            productPromotionDetailMock.Setup(x => x.Min).Returns(30);
            productPromotionDetailMock.Setup(x => x.Discount).Returns(30);
            productPromotionDetailMock.Setup(x => x.Clone()).Returns(productPromotionDetailMock.Object);
            productPromotionDetails.Add(productPromotionDetailMock.Object);
            productPromotionMock = new Mock<IProductPromotionValue>();
            productPromotionMock.Setup(x => x.Name).Returns("Promoção de Lavada");
            productPromotionMock.Setup(x => x.CategoryId).Returns(3);
            productPromotionMock.Setup(x => x.Details).Returns(productPromotionDetails);
            productPromotionMock.Setup(x => x.Clone()).Returns(productPromotionMock.Object);
            productPromotionDb.Add(productPromotionMock.Object);

        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildProductDetailDb()
        {
            productDetailDb = new List<IProductDetailValue>();

            var productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("32\" Full HD Flat Smart TV H5103 Series 3");
            productDetailValueMock.Setup(x => x.Description).Returns("Com o Modo futebol, é como se você estivesse realmente no jogo. Ele exibe, de forma precisa e viva, a grama verde do campo e todas as outras cores do estádio. Um poderoso efeito de som multi-surround também permite que você ouça toda a empolgação. Você pode até mesmo ampliar áreas selecionadas da tela para uma melhor visualização. Com apenas o toque de um botão, você pode aproveitar ao máximo o seu esporte favorito com todos os seus amigos.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fda44b12-48f7-11e6-996c-0aad52ea90db.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1466.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(1);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("40\" Full HD Flat Smart TV H5103 Series 5");
            productDetailValueMock.Setup(x => x.Description).Returns("Usando um algoritmo avançado de melhoria da qualidade de imagem, o Wide Color Enhancer Plus da Samsung melhora consideravelmente a qualidade de qualquer imagem e revela detalhes ocultos. Com o Wide Color Enhancer Plus, agora você verá as cores como elas realmente devem ser vistas.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fe41bc44-48f7-11e6-a3ac-0a9a90ee83e3.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1979.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(1);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("65\" UHD 4K Flat Smart TV JU6000 Series 6");
            productDetailValueMock.Setup(x => x.Description).Returns("Sua Smart TV vai possibilitar você baixar e acessar aplicativos e assistir conteúdo de vídeos da internet em uma tela muito melhor. Desfrute do Netflix e You Tube com toda a qualidade de imagem da sua TV.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fd08d6e6-48f7-11e6-886b-0a37f4bea89f.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(10999);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(1);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Galaxy A5 2016");
            productDetailValueMock.Setup(x => x.Description).Returns("Alta performance Hardware de alta qualidade e performance para navegação na internet. O processador Octa-Core permite carregamento de páginas de navegadores instantaneamente, transições de interfaces de maneira suave e multitarefa. O Galaxy A permite também a expansão de memória.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/07b3b1d8-48f8-11e6-aa97-020adee616d7.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1979.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(2);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Galaxy J3 2016");
            productDetailValueMock.Setup(x => x.Description).Returns("O novo Galaxy J apresenta uma frente redesenhada com visual formidável, levando a experiência de visualização a um novo nível. Com uma moldura toda preta e mais fina, de 4.56 mm, a nova estrutura proporciona uma experiência envolvente em tela ampla");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/08da337a-48f8-11e6-9634-02184f9c0531.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(899.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(2);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Galaxy J7 Metal");
            productDetailValueMock.Setup(x => x.Description).Returns("Resistente o suficiente para preservar sua boa aparência. A moldura integrada totalmente metálica e harmoniosamente uniforme protege contra o descascamento, para que você possa ficar tranquilo e admirar seu aparelho por muito tempo.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/0903ec60-48f8-11e6-886b-0a37f4bea89f.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1439.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(2);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Lavadora e Secadora WD103 10.1 kg Branca 127 V");
            productDetailValueMock.Setup(x => x.Description).Returns("A tecnologia revolucionária EcoBubble™ dissolve o sabão com ar e água antes do início do ciclo, gerando bolhas de limpeza que penetram no tecido de forma mais rápida do que o sabão concentrado. Lave as roupas em água fria com a mesma eficiência da água quente. A água fria não agride as roupas e protege o tecido impermeável. *Baseado no teste interno (Samsung Electronics), aparelho Seine Combo de 9 kg, meia carga (4 kg), programa algodão da Samsung (40 ℃) vs programa Super Eco (frio) **Os resultados reais podem variar de acordo com o modelo e as condições ambientais específicas que os consumidores encontram ao operar a máquina.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/0c733874-48f8-11e6-ad8d-02878eee0871.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(2136.6);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(3);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Lavadora e Secadora WD6000 9 kg Branca 127 V");
            productDetailValueMock.Setup(x => x.Description).Returns("Economize energia lavando grandes cargas de roupa a frio com a tecnologia Eco Bubble™. As bolhas dissolvem e ativam o detergente, para que ele penetre as tramas do tecido mais depressa e, assim, limpe de maneira eficaz mesmo com água fria (15 °C).");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/0b65b97a-48f8-11e6-886b-0a37f4bea89f.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(2699.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(3);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Lavadora e Secadora WD7000 15 kg Inox Look 127V");
            productDetailValueMock.Setup(x => x.Description).Returns("A tecnologia Eco Bubble™ utiliza bolhas que rapidamente ativam o sabão, removendo manchas mais facilmente. Soma-se a ela o Bubble Shot™: dois jatos extra de água, que auxiliam na penetração do sabão, antes do enxágue dos resíduos.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/0a233ab0-48f8-11e6-ad8d-02878eee0871.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(4049.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(3);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Samsung Essentials E22");
            productDetailValueMock.Setup(x => x.Description).Returns("Bateria com duração de até 8 horasSurpreenda-se com uma bateria que acompanha o seu ritmo e não te deixa na mão. Até 8 horas* para ficar online ou realizar suas tarefas sem preocupação! E mais, com o novo Battery Life Plus 2.0 a bateria mantém até 90%* da capacidade original mesmo após 100 ciclos de carga (equivalente a 3 anos de uso). * A bateria poderá, dependendo das condições de uso, proporcionar o tempo de utilização e vida útil informados acima");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fb4654d2-48f7-11e6-a3ac-0a9a90ee83e3.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1624.49);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(4);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Samsung Expert X20");
            productDetailValueMock.Setup(x => x.Description).Returns("Performance e tecnologia que proporcionam muita rapidez na realização das suas tarefas, permitindo executar mesmo os softwares e aplicativos mais pesados.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/f99eb282-48f7-11e6-a3ac-0a9a90ee83e3.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(2214.44);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(4);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Samsung Expert X51");
            productDetailValueMock.Setup(x => x.Description).Returns("Muita rapidez na realização das suas tarefas, permitindo executar mesmo os softwares e aplicativos mais pesados. Jogos, vídeos, processamento e edição gráfica com alta performance e qualidade. São as melhores imagens e gráficos em uma tela Full HD de altíssima resolução.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/fa346368-48f7-11e6-ad8d-02878eee0871.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(3521.74);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(4);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Smart Camera NX30");
            productDetailValueMock.Setup(x => x.Description).Returns("A versátil SMART CAMERA Samsung NX30 entrega o desempenho que você precisa. Seu sensor APS-C CMOS de 20.3 MP garante imagens brilhantes mesmo em situações de pouca luz. O sistema de autofoco híbrido de última geração fornece foco preciso e a velocidade do obturador de 1/8000 segundos permite a você capturar objetos em alta velocidade com pouco ou nenhum borrão.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/03522d4a-48f8-11e6-a3ac-0a9a90ee83e3.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1925.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(5);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Smart Camera NX300M");
            productDetailValueMock.Setup(x => x.Description).Returns("Quando você estiver fotografando uma ação de movimento rápido, como um jogador de tênis se preparando para uma jogada junto à rede, capture toda a sequência facilmente com uma série de fotografias incrivelmente nítidas. A SMART CAMERA Samsung NX300M permite efetuar um disparo contínuo a uma velocidade ultrarrápida de 8,6 quadros por segundo, para garantir que você obtenha a fotografia desejada. O avançado sistema de foco automático proporciona máxima nitidez aos objetos em movimento rápido, e um curto intervalo de disparo do obturador ajuda a capturar o momento fugaz antes que o objeto saia do enquadramento.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/04a6af86-48f8-11e6-ad8d-02878eee0871.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(2024.1);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(5);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);
            productDetailValueMock = new Mock<IProductDetailValue>();
            productDetailValueMock.Setup(x => x.Name).Returns("Smart Camera Nxmini 9-27mm");
            productDetailValueMock.Setup(x => x.Description).Returns("Com um corpo sólido de magnésio superior ornado por materiais naturais semelhantes ao couro, a NX mini é requintadamente moderna com um toque perfeito de analógico. Simplesmente elegante.");
            productDetailValueMock.Setup(x => x.Photo).Returns("https://simplest-meuspedidos-arquivos.s3.amazonaws.com/media/imagem_produto/133421/06cfe8d6-48f8-11e6-9634-02184f9c0531.jpeg");
            productDetailValueMock.Setup(x => x.Price).Returns(1530);
            productDetailValueMock.Setup(x => x.CategoryId).Returns(0);
            productDetailValueMock.Setup(x => x.Clone()).Returns(productDetailValueMock.Object);
            productDetailDb.Add(productDetailValueMock.Object);

        }
    }
}
