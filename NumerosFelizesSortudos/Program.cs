using NumerosFelizesSortudos.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerosFelizesSortudos
{
    class NumerosFelizesSortudos
    {
        public static List<int> GerarListaNumeros()
        {
            List<int> lista = new List<int>();
            for (int i = 0; i < 100; i++)
                if (VerificarNumero(i)) lista.Add(i);
            return lista;
        }

        public static bool VerificarNumero(int numero)
        {
            bool numeroFeliz = false;
            List<int> listaDigitos = new List<int>();
            listaDigitos = DividirDigitos(numero);
            for (int i = 0; i < 20 && !numeroFeliz; i++)
            {
                int sumaActual = CalcularQuadrados(listaDigitos);
                if (sumaActual != 1)
                    listaDigitos = DividirDigitos(sumaActual);
                else numeroFeliz = true;
            }
            return numeroFeliz;
        }

        public static List<int> DividirDigitos(int digito)
        {
            List<int> digitos = new List<int>();
            while (digito != 0)
            {
                digitos.Add(digito % 10);
                digito = digito / 10;
            }
            return digitos;
        }

        public static int CalcularQuadrados(List<int> listaDigitos)
        {
            int resultado = 0;
            foreach (int elem in listaDigitos) resultado += elem * elem;
            return resultado;
        }

        public List<int> GetNumeroSortudo(int range)
        {
            return ImprimirResultado(FindNumeroSortudo(range));
        }

        public List<int> GetsortudoPrimoNumeros(int range)
        {
            bool[] numeros = FindNumeroSortudo(range);

            for (int i = 0; i < numeros.Length; i++)
            {
                if (numeros[i]) continue;
                numeros[i] = !NumeroPrimo(i + 1);
            }

            return ImprimirResultado(numeros);
        }

        private bool[] FindNumeroSortudo(int range)
        {
            if (range < 1) range = 0;
            bool[] numeros = new bool[range];
            int sortudoContar = 2;

            while (sortudoContar < numeros.Length)
            {
                sortudoContar = NumeroFora(numeros, sortudoContar);
            }

            return numeros;
        }

        private int NumeroFora(bool[] numeros, int sortudoContar)
        {
            int contador = 0;

            for (int i = 0; i < numeros.Length; i++)
            {
                if (numeros[i]) continue;
                contador++;

                if (contador == sortudoContar)
                {
                    numeros[i] = true;
                    contador = 0;
                }
            }

            return GetsortudoContar(numeros, sortudoContar);
        }

        private int GetsortudoContar(bool[] numeros, int pular)
        {
            if (pular >= numeros.Length) return numeros.Length;

            for (int i = pular; i < numeros.Length; i++)
            {
                if (!numeros[i]) return i + 1;
            }

            return numeros.Length;
        }

        private static bool NumeroPrimo(int numero)
        {
            if (numero == 1) return false;

            for (short i = 3; i <= Math.Sqrt(numero); i += 2)
            {
                if (numero % i == 0) return false;
            }

            return true;
        }

        private static List<int> ImprimirResultado(bool[] numeros)
        {
            List<int> resultado = new List<int>();

            for (int i = 0; i < numeros.Length; i++)
            {
                if (!numeros[i]) resultado.Add(i + 1);
            }

            return resultado;
        }

        static void Main(string[] args)
        {
            List<int> recebe = GerarListaNumeros();
            List<NumeroFelizSortudoDto> numeroList = new List<NumeroFelizSortudoDto>();

            for (int i = 0; i < 100; i++)
            {
                NumerosFelizesSortudos sortudo = new NumerosFelizesSortudos();
                NumeroFelizSortudoDto numero = new NumeroFelizSortudoDto();
                numero.numero = i;
                numero.sortudo = sortudo.GetNumeroSortudo(i).Find(n => n == i).ToString() == "0" ? "Não é" : "É";
                numero.feliz = recebe.Find(r => r == i).ToString() == "0" ? "Não é" : "É";
                numeroList.Add(numero);
            }

            numeroList.ForEach(n => Console.WriteLine("Número {0} - {1} Feliz - {2} Sortudo", n.numero, n.feliz, n.sortudo));
            Console.ReadKey();
        }

    }
}

