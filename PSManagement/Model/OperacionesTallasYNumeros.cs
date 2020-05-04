using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSManagement.Model
{
    public partial class numeroscalzado
    {
        public int GetTotalItems()
        {
            return C36 + C37 + C38 + C39 + C40 + C41 + C42 + C43 + C44 + C45 + C46 + C47;
        }

        public int GetCantidadNumero(string numero)
        {
            switch (numero)
            {
                case "Número 36":
                    return C36;

                case "Número 37":
                    return C37;

                case "Número 38":
                    return C38;

                case "Número 39":
                    return C39;

                case "Número 40":
                    return C40;

                case "Número 41":
                    return C41;

                case "Número 42":
                    return C42;

                case "Número 43":
                    return C43;

                case "Número 44":
                    return C44;

                case "Número 45":
                    return C45;

                case "Número 46":
                    return C46;

                case "Número 47":
                    return C47;

                default:
                    return -1;
            }
        }

        public bool SetNumeros(string numero, int cantidad)
        {
            try
            {
                switch (numero)
                {
                    case "Número 36":
                        this.C36 = cantidad;
                        break;

                    case "Número 37":
                        this.C37 = cantidad;
                        break;

                    case "Número 38":
                        this.C38 = cantidad;
                        break;

                    case "Número 39":
                        this.C39 = cantidad;
                        break;

                    case "Número 40":
                        this.C40 = cantidad;
                        break;

                    case "Número 41":
                        this.C41 = cantidad;
                        break;

                    case "Número 42":
                        this.C42 = cantidad;
                        break;

                    case "Número 43":
                        this.C43 = cantidad;
                        break;

                    case "Número 44":
                        this.C44 = cantidad;
                        break;

                    case "Número 45":
                        this.C45 = cantidad;
                        break;

                    case "Número 46":
                        this.C46 = cantidad;
                        break;

                    case "Número 47":
                        this.C47 = cantidad;
                        break;

                    default:
                        break;
                }
                this.TotalCantidadArticulo = GetTotalItems();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SumaNumero(string numero)
        {
            switch (numero)
            {
                case "Número 36":
                    C36++;
                    break;

                case "Número 37":
                    C37++;
                    break;

                case "Número 38":
                    C38++;
                    break;

                case "Número 39":
                    C39++;
                    break;

                case "Número 40":
                    C40++;
                    break;

                case "Número 41":
                    C41++;
                    break;

                case "Número 42":
                    C42++;
                    break;

                case "Número 43":
                    C43++;
                    break;

                case "Número 44":
                    C44++;
                    break;

                case "Número 45":
                    C45++;
                    break;

                case "Número 46":
                    C46++;
                    break;

                case "Número 47":
                    C47++;
                    break;

                default:
                    break;
            }
            this.TotalCantidadArticulo = GetTotalItems();
        }

        public void RestaNumero(string numero)
        {
            switch (numero)
            {
                case "Número 36":
                    C36--;
                    break;

                case "Número 37":
                    C37--;
                    break;

                case "Número 38":
                    C38--;
                    break;

                case "Número 39":
                    C39--;
                    break;

                case "Número 40":
                    C40--;
                    break;

                case "Número 41":
                    C41--;
                    break;

                case "Número 42":
                    C42--;
                    break;

                case "Número 43":
                    C43--;
                    break;

                case "Número 44":
                    C44--;
                    break;

                case "Número 45":
                    C45--;
                    break;

                case "Número 46":
                    C46--;
                    break;

                case "Número 47":
                    C47--;
                    break;

                default:
                    break;
            }
            this.TotalCantidadArticulo = GetTotalItems();
        }
    }

    public partial class tallastextiles
    {
        public int GetTotalItems()
        {
            return XXS + XS + S + M + L + XL + XXL;
        }

        public int GetCantidadTalla(string talla)
        {
            switch (talla)
            {
                case "Talla XXS":
                    return XXS;

                case "Talla XS":
                    return XS;

                case "Talla S":
                    return S;

                case "Talla M":
                    return M;

                case "Talla L":
                    return L;

                case "Talla XL":
                    return XL;

                case "Talla XXL":
                    return XXL;

                default:
                    return -1;
            }
        }

        public bool SetTallas(string talla, int cantidad)
        {
            try
            {
                switch (talla)
                {
                    case "Talla XXS":
                        this.XXS = cantidad;
                        break;

                    case "Talla XS":
                        this.XS = cantidad;
                        break;

                    case "Talla S":
                        this.S = cantidad;
                        break;

                    case "Talla M":
                        this.M = cantidad;
                        break;

                    case "Talla L":
                        this.L = cantidad;
                        break;

                    case "Talla XL":
                        this.XL = cantidad;
                        break;

                    case "Talla XXL":
                        this.XXL = cantidad;
                        break;

                    default:
                        return false;
                }
                this.TotalCantidadArticulo = GetTotalItems();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SumaTalla(string talla)
        {
            switch (talla)
            {
                case "Talla XXS":
                    XXS++;
                    break;

                case "Talla XS":
                    XS++;
                    break;

                case "Talla S":
                    S++;
                    break;

                case "Talla M":
                    M++;
                    break;

                case "Talla L":
                    L++;
                    break;

                case "Talla XL":
                    XL++;
                    break;

                case "Talla XXL":
                    XXL++;
                    break;

                default:
                    break;
            }
            this.TotalCantidadArticulo = GetTotalItems();
        }

        public void RestaTalla(string talla)
        {
            switch (talla)
            {
                case "Talla XXS":
                    XXS--;
                    break;

                case "Talla XS":
                    XS--;
                    break;

                case "Talla S":
                    S--;
                    break;

                case "Talla M":
                    M--;
                    break;

                case "Talla L":
                    L--;
                    break;

                case "Talla XL":
                    XL--;
                    break;

                case "Talla XXL":
                    XXL--;
                    break;

                default:
                    break;
            }
            this.TotalCantidadArticulo = GetTotalItems();
        }

    }
}
