using ProjetoIMCTeste.Enumerador;

namespace ProjetoIMCTeste.Classe
{
    public class Operacao
    {
        /// <summary>
        /// Calcula o Índice de Massa Corporal (IMC).
        /// </summary>
        /// <param name="peso">Peso na unidade - Kg.</param>
        /// <param name="altura">Altura na unidade - m.</param>
        /// <returns>Retorna o IMC calculado</returns>
        public static double CalcularIMC(double peso, double altura)
        {
            if(peso <= 0.0 || altura <= 0.0)
            {
                throw new ArgumentOutOfRangeException("Peso ou altura não devem ser menor ou igual a zero");
            }
            return Math.Round((peso / (Math.Pow(altura, 2.0))), 1);
        }

        /// <summary>
        /// Exibe a classificação de acordo com o IMC calculado.
        /// </summary>
        /// <param name="imc">IMC calculado.</param>
        /// <returns>Retorna um enum da categoria do IMC.</returns>
        public static Categoria ExibirClassificacao(double imc)
        {
            Categoria categoria;

            if (imc < 18.5)
                categoria = Categoria.AbaixoDoPeso;
            else if (imc <= 24.9)
                categoria = Categoria.PesoNormal;
            else if (imc <= 29.9)
                categoria = Categoria.Sobrepeso;
            else if (imc <= 34.9)
                categoria = Categoria.ObesidadeGrauI;
            else if (imc <= 39.9)
                categoria = Categoria.ObesidadeGrauII;
            else
                categoria = Categoria.ObesidadeGrauIII;

            return categoria;

        }
    }
}
