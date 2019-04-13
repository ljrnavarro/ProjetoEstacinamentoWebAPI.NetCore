using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiEstaciona.Controllers;
using WebApiEstaciona.Models;
using WebApiEstaciona.Security;
using Xunit;

namespace WebApiEstacionaTests
{
    public class EstacionamentoControllerTests
    {
        Autenticacao AutenticacaoAPI;
        Microsoft.AspNetCore.Http.IHttpContextAccessor contexto;

        [Fact]
        public async Task Test_Estaciona()
        {

            var estacionar = new EstacionamentoModel
            {
                Id = 0,
                Estado = "A",
                IdVaga = 108,
                DataHora_Entrada = DateTime.Now,
                DataHora_Saida = DateTime.MinValue,
                IdVeiculo = 2
            };

            var content = JsonConvert.SerializeObject(estacionar);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");


            // Act
            var result = new EstacionamentoController(contexto).Novo(estacionar);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Test_Pagar()
        {
            // Act
            var result = new EstacionamentoController(contexto).Pagar(11);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }
    }
}
