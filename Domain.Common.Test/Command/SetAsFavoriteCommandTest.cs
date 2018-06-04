using Domain.Base.Repository.Specification;
using Domain.Common.Base.Entity;
using Domain.Common.Base.Factory.Specification;
using Domain.Common.Base.Repository;
using Domain.Common.Base.Repository.Specification;
using Domain.Common.Factory.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Test.Command
{
    [TestClass]
    public class SetAsFavoriteCommandTest
    {
        [TestMethod]
        public void Exec_shouldCallTheRepositoryUpdateMethodWithCorrectParam()
        {
            var repositoryMock = new Mock<IFavoriteProductCommandRepository>();
            var repositorySpecFactory = new Mock<IFavoriteProductByIdRepositorySpecificationFactory>();

            var productId = 0L;
            var favorite = true;

            var correctProductId = false;
            var correctFavorite = false;

            repositoryMock.Setup(x => x.Update(It.IsAny<IFavoriteProductEntity>(), It.IsAny<IFavoriteProductRepositoryByIdSpecification>(), It.IsAny<Action<IFavoriteProductEntity>>(), It.IsAny<Action<Exception>>()))
                .Callback((IFavoriteProductEntity entity, IByIdRepositorySpecification<IFavoriteProductEntity, long> spec, Action<IFavoriteProductEntity> success, Action<Exception> error ) =>
                {
                    correctProductId = entity.Id == productId;
                    correctFavorite = entity.IsFavorite == favorite;
                });

            var command = new SetAsFavoriteCommandFactory(repositoryMock.Object, repositorySpecFactory.Object);

            command.NewCommand(productId, favorite).Exec(() => { });

            Assert.IsTrue(correctProductId);
            Assert.IsTrue(correctFavorite);

        }
    }
}
