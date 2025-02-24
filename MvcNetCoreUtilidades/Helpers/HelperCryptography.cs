using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCryptography
    {

        public static string Salt { get; set;}

        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for(int i = 0; i <= 30; i++)
            {
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string CifrarContenido(string contenido, bool comparar)
        {
            if(comparar == false)
            {
                Salt = GenerateSalt();

            }
            string contenidoSalt = Salt + contenido;
            SHA256 managed = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);
            for(int i = 1; i <= 4; i++)
            {
                salida = managed.ComputeHash(salida);
            }
            managed.Clear();
            string resulado = encoding.GetString(salida);
            return resulado;
        }

        public static string EncriptarTextoBasico(string contenido)
        {
            byte[] entrada;
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            SHA1 managed = SHA1.Create();
            entrada = encoding.GetBytes(contenido);
            salida = managed.ComputeHash(entrada);
            string resultado = encoding.GetString(salida);
            return resultado;
        }


    }
}
