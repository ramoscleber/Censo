using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportExportDadosCenso
{
    public static class HelperCSV
    {
        public static DataTable RetornaDataTableArquivoCSV(string caminho)
        {
            DataTable dt = new DataTable("ArquivoCsv");//Nome do DataTable
            DataRow dr = null;//Linha para adicionar ao DataTable
            StreamReader stream = new StreamReader(caminho, Encoding.UTF7);//Cria um objeto do tipo stream para receber um determinado arquivo
            string[] nomeColuna = null;
            string[] itemColuna = null;
            string linha = null;
            bool temColuna = false;//Vai ser usada para controlar se nome da coluna ja foi criado

            while ((linha = stream.ReadLine()) != null)//Percorre um determinado arquivo e atribui o valor a linha
            {
                itemColuna = linha.Split(';');//Cria um array de string para receber os itens da coluna

                if (temColuna == false)//Verifica se ja foi criado coluna do DataTable
                {
                    nomeColuna = linha.Split(';');//Pega o nome das colunas

                    for (int i = 0; i < nomeColuna.Length; i++)//Percorre o array para pegar a posição é atribuir o nome a coluna do dt
                    {
                        dt.Columns.Add(nomeColuna[i]);
                    }
                }
                else//Adiciona items no DataTable
                {
                    dr = dt.NewRow();//Cria uma nova linha para o DataTable

                    for (int j = 0; j < nomeColuna.Length; j++)
                    {
                        //Recebe o nome da coluna     <-Atribui o valor item ao nome da coluna     
                        dr[nomeColuna[j].ToString()] = itemColuna[j].ToString().Trim();
                    }

                    dt.Rows.Add(dr);//DataTable recebe os valores da linha
                }

                temColuna = true;
            }
            return dt;
        }
    }

}
