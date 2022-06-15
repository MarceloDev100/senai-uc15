﻿using Chapter.WebApi.Controllers;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace TestXUnit.Controllers
{  
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Arrange
            var fakeRepository = new Mock<IUsuarioRepository>();

            fakeRepository.Setup(x => x.Login(It.IsAny<string>(),
                It.IsAny<string>())).Returns((Usuario) null);

            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "email@email.com";
            dadosLogin.Senha = "1234";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            var resultado = controller.Login(dadosLogin);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);

        }

        [Fact]
        public void LoginController_Retornar_Token()
        {
            //Arrange
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "email@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "0";

            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(),
                It.IsAny<string>())).Returns(usuarioRetorno);

            string issuerValidacao = "chapter.webapi";

            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "email@email.com";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            OkObjectResult resultado = (OkObjectResult) controller.Login(dadosLogin);

            string token = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt  = jwtHandler.ReadJwtToken(token);

            //Assert
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
    }
}
