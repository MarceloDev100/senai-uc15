using ProjetoIMCTeste.Classe;
using ProjetoIMCTeste.Enumerador;
using System;
using Xunit;

namespace TestXUnit
{
    public class UnitTest1
    {

        /// <summary>
        /// Permite a realização de uma sequência de testes para verificar lançamento de exceção,
        /// com base em pesos e/ou alturas inválidos.
        /// </summary>
        /// <param name="peso">Peso de um indivíduo em (kg)</param>
        /// <param name="altura">Altura de um indivíduo em (m)</param>
        [Theory]
        [InlineData(0.0, 1.60)]
        [InlineData(-60.0, 1.80)]
        [InlineData(70.0, 0.00)]
        [InlineData(80.0, -1.70)]
        [InlineData(-80.0, -1.70)]
        [InlineData(0.0, 0.00)]
        public void Peso_Ou_Altura_ComValorInvalido_RetornaErro(double peso, double altura)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Operacao.CalcularIMC(peso, altura));
        }

        /// <summary>
        /// Permite a realização de uma sequência de testes para verificar o cálculo do IMC.
        /// </summary>
        /// <param name="peso">Peso do indivíduo em (Kg).</param>
        /// <param name="altura">Altura do indivíduo em (m).</param>
        /// <param name="imc">Valor esperado do IMC.</param>
        [Theory]
        [InlineData(40.0, 1.60, 15.6)]
        [InlineData(70.0, 1.80, 21.6)]
        [InlineData(70.0, 1.58, 28.0)]
        [InlineData(85.0, 1.65, 31.2)]
        [InlineData(80.0, 1.50, 35.6)]
        [InlineData(90.0, 1.50, 40.0)]
        public void CalcularIMCLista(double peso, double altura, double imc)
        {
            var resultado = Operacao.CalcularIMC(peso, altura);

            Assert.Equal(imc, resultado);
        }

       

        /// <summary>
        /// Permite a realização de uma sequência de testes para verificar a classificação do IMC.
        /// </summary>
        /// <param name="imc">IMC calculado.</param>
        /// <param name="categoria">Valor esperado da categoria.</param>
        [Theory]
        [InlineData(15.6, Categoria.AbaixoDoPeso)]
        [InlineData(21.6, Categoria.PesoNormal)]
        [InlineData(28.0, Categoria.Sobrepeso)]
        [InlineData(31.2, Categoria.ObesidadeGrauI)]
        [InlineData(35.6, Categoria.ObesidadeGrauII)]
        [InlineData(40.0, Categoria.ObesidadeGrauIII)]
        public void ClassificarIMCLista(double imc, Categoria categoria)
        {
            var resultado = Operacao.ExibirClassificacao(imc);

            Assert.Equal(categoria, resultado);
        }
    }
}