using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Updated
    {
        private UsersController? _controller;
        [Fact(DisplayName = "É possivel realizar o Deleted")]
        public async Task E_Possivel_Invocar_a_controller_deleted()
        {
            Mock<IUserService> serviceMock = new Mock<IUserService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);

        }
    }
}