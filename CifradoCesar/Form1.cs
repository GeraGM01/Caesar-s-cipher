using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CifradoCesar
{
    public partial class Form1 : Form
    {
        private List<string> alfabeto = new List<string>();
        public Form1()
        {
            InitializeComponent();
            inicializaAlfabeto();
        }

        public void inicializaAlfabeto()
        {
            for (byte a = 65; a <= 90; a++)
            {
                alfabeto.Add(((char)a).ToString());
                //Console.WriteLine((char)a);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCifrada.Text = "";
            string mensajeCifrado = "";
            List<string> intervaloC = new List<string>();
            string c = txtC.Text;
            txtPalabra.Text = txtPalabra.Text.ToUpper();
            string mensaje = txtPalabra.Text;
            char[] delimitador = { ',', '(', ')' };
            string[] trozos = c.Split(delimitador);
            for (int i = 0; i < trozos.Length; i++)
            {
                if (trozos[i] != "")
                {
                    intervaloC.Add(trozos[i]);
                }
            }
            //Si es monoalfabetico y es positivo
            if((intervaloC.ElementAt(0) == "m" || intervaloC.ElementAt(0) == "M") && Convert.ToInt32(intervaloC.ElementAt(1)) > 0)
            {
                //Inmediatamente hacemos una copia del alfabeto en una lista auxiliar
                List<string> alfabetoAux = new List<string>(alfabeto);
                //Apuntamos al final de la lista del alfabeto
                int indexFinal = alfabeto.Count;
                int indexInicial = indexFinal - Convert.ToInt32(intervaloC.ElementAt(1));
                //Reemplazamos los ultimos n digitos de acuerdo al intervalo
                int itemAlfabeto = 0;
                for (int i = indexInicial; i < alfabeto.Count(); i++)
                {
                    alfabetoAux[i] = alfabeto[itemAlfabeto];
                    //alfabetoAux.Insert(i, alfabeto[itemAlfabeto]);
                    itemAlfabeto++;
                }
                //Actualizamos los valores restantes del alfabeto iniciando desde la siguiente letra que le sigue al item
                for (int i = 0; i < indexInicial; i++)
                {
                    alfabetoAux[i] = alfabeto[itemAlfabeto];
                    //alfabetoAux.Insert(i, alfabeto[itemAlfabeto]);
                    itemAlfabeto++;
                }

                //Aqui comienza la traduccion para tener el mensaje cifrado

                //buscamos el indice de la letra en nuestro alfabeto original y lo empatamos con el alfabeto que generamos 
                foreach(char caracter in mensaje)
                {
                    string cAux = caracter.ToString();
                    int indexOG = getIndexAlfabeto(cAux);
                    string s = alfabeto.ElementAt(indexOG);
                    if(indexOG != -1)
                    {
                        int indexAux = getIndexAlfabeto(s);
                        string sAux = alfabetoAux.ElementAt(indexAux);
                        mensajeCifrado += sAux;
                    }
                }
                txtCifrada.Text = txtCifrada.Text + mensajeCifrado;
            }
            //Caso monoalfabetico y negativo
            else if((intervaloC.ElementAt(0) == "m" || intervaloC.ElementAt(0) == "M") && Convert.ToInt32(intervaloC.ElementAt(1)) < 0)
            {
                //Inmediatamente hacemos una copia del alfabeto en una lista auxiliar
                List<string> alfabetoAux = new List<string>(alfabeto);

                //Apuntamos al inicio del alfabeto
                int indexInicial = 0;
                int indexFinal = Convert.ToInt32(intervaloC.ElementAt(1)) * -1;
                int indexInicialAux = alfabeto.Count() + Convert.ToInt32(intervaloC.ElementAt(1));

                //Reemplazamos los primeros n digitos de acuerdo al intervalo
                int itemAlfabeto = indexInicialAux;
                for (int i = indexInicial; i < indexFinal; i++)
                {
                    alfabetoAux[i] = alfabeto[itemAlfabeto];
                    //alfabetoAux.Insert(i, alfabeto[itemAlfabeto]);
                    itemAlfabeto++;
                }
                //Actualizamos los valores restantes del alfabeto iniciando desde la siguiente letra que le sigue al item
                int j = 0;
                for (int i = indexFinal; i < alfabeto.Count(); i++)
                {
                    alfabetoAux[i] = alfabeto[j];
                    //alfabetoAux.Insert(i, alfabeto[itemAlfabeto]);
                    j++;
                }

                //Aqui comienza la traduccion para tener el mensaje cifrado

                //buscamos el indice de la letra en nuestro alfabeto original y lo empatamos con el alfabeto que generamos 
                foreach (char caracter in mensaje)
                {
                    string cAux = caracter.ToString();
                    int indexOG = getIndexAlfabeto(cAux);
                    string s = alfabeto.ElementAt(indexOG);
                    if (indexOG != -1)
                    {
                        int indexAux = getIndexAlfabeto(s);
                        string sAux = alfabetoAux.ElementAt(indexAux);
                        mensajeCifrado += sAux;
                    }
                }
                txtCifrada.Text = txtCifrada.Text + mensajeCifrado;
            }
        }

        public int getIndexAlfabeto(string letra)
        {
            for (int i = 0; i < alfabeto.Count(); i++)
            {
                string s = alfabeto[i];
                if(letra == s)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
