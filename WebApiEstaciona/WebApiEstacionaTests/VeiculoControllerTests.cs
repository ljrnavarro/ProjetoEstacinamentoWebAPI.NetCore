using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiEstaciona.Controllers;
using WebApiEstaciona.Models;
using WebApiEstaciona.Security;
using Xunit;

namespace WebApiEstacionaTests
{
    public class VeiculoControllerTests
    {
        Autenticacao AutenticacaoAPI;
        Microsoft.AspNetCore.Http.IHttpContextAccessor contexto;

        [Fact]
        public async Task GetVeiculos()
        {
            //Mock IHttpContextAccessor
            AutenticacaoAPI = new Autenticacao(contexto);
            
            // Arrange
            var controller = new VeiculoController(contexto);

            // Act
            IActionResult actionResult = controller.Veiculos();

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetVeiculo()
        {
            int Id = 1;
            
            //Mock IHttpContextAccessor
            AutenticacaoAPI = new Autenticacao(contexto);

            // Arrange
            var controller = new VeiculoController(contexto);

            // Act
            IActionResult actionResult = controller.Get(Id);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            Assert.NotNull(okResult.Value);
        }

    }
}
