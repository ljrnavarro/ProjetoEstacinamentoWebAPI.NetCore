using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiEstaciona.Controllers;
using WebApiEstaciona.Security;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using WebApiEstaciona.Models;

namespace WebApiEstacionaTests
{
    public class VagaControllerTests
    {
        Autenticacao AutenticacaoAPI;
        Microsoft.AspNetCore.Http.IHttpContextAccessor contexto;

        [Fact]
        public void GetVagasQuantidade()
        {
            //Mock IHttpContextAccessor
            AutenticacaoAPI = new Autenticacao(contexto);
            
            // Arrange
            var controller = new VagaController(contexto);

            // Act
            IActionResult actionResult = controller.QuantVagasDisponiveis();

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetVagasOcupadas()
        {
            
            //Mock IHttpContextAccessor
            AutenticacaoAPI = new Autenticacao(contexto);

            // Arrange
            var controller = new VagaController(contexto);

            // Act
            IActionResult actionResult = controller.Disponiveis();

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            Assert.NotNull(okResult.Value);
        }

    }
}
