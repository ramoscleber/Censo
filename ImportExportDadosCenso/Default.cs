using ImportExportDadosCenso.Modelo;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportExportDadosCenso
{
    public partial class Default : Form
    {
        public static string exeLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6);

        DataTable dt = new DataTable();
        int contadorRegistro;
        int contadorRegistroVinculoAluno;
        string[] pegaLinhaRegistro = null;

        string serverName = "localhost";//"10.0.105.126";    //localhost
        string port = "5433";                 //porta default
        string userName = "postgres";  //singu_0_6_0";      //nome do administrador
        string password = "m@ster30"; //"51ngu:dt!";        //senha do administrador
        string databaseName = "singu_0_6_0";  //nome do banco de dados

        NpgsqlConnection pgsqlConnection;
        string connString = null;

        StreamReader file;
        string linha = null;

        string[] getLinha = null;

        public Default()
        {
            InitializeComponent();
            connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                 serverName, port, userName, password, databaseName);
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder colunas = new StringBuilder();

                this.gvArquivosCsv.DataSource = null;
                this.gvArquivosCsv.Rows.Clear();
                contadorRegistro = 0;
                contadorRegistroVinculoAluno = 0;
                pbRegistro.Visible = false;
                pbAluno.Visible = false;
                pbVinculoAlunoCurso.Visible = false;
                lblTotalRegistros.Visible = false;
                lblTotalLinhasRegistroAluno.Visible = false;
                lblTotalVinculosAluno.Visible = false;

                if (dlgOpen.ShowDialog() == DialogResult.OK)//Condição se arquivo foi selecionado
                {

                    dt = HelperCSV.RetornaDataTableArquivoCSV(dlgOpen.FileName);

                    txtPath.Text = dlgOpen.FileName;

                    file = new StreamReader(dlgOpen.FileName);

                    while ((linha = file.ReadLine()) != null)
                    {
                        getLinha = linha.Split('|');

                        if (getLinha[0].ToString() != "40")
                        {
                            MessageBox.Show("Arquivo não corresponde ao\n LEIAUTE DE MIGRAÇÃO – ARQUIVO ALUNO", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (dt.Columns.Count != 0)
                            {
                                gvArquivosCsv.DataSource = dt;
                                btnInserirRegistro.Enabled = true;

                                pbRegistro.Visible = true;
                                lblTotalLinhasRegistroAluno.Visible = true;
                                lblTotalLinhasRegistroAluno.Text = string.Format("Total de registros: {0}", dt.Rows.Count);

                                //Pecorrendo o datatable - somente para teste
                                foreach (DataRow item in dt.Rows)
                                {
                                    pegaLinhaRegistro = item[0].ToString().Split('|');

                                    if (pegaLinhaRegistro[0].ToString().Equals("41"))
                                    {
                                        contadorRegistro++;
                                    }
                                    if (pegaLinhaRegistro[0].ToString().Equals("42"))
                                    {
                                        contadorRegistroVinculoAluno++;
                                    }
                                }

                                pbAluno.Visible = true;
                                pbVinculoAlunoCurso.Visible = true;
                                lblTotalRegistros.Visible = true;
                                lblTotalVinculosAluno.Visible = true;
                                lblTotalRegistros.Text = string.Format("Total de alunos: {0}", contadorRegistro.ToString());
                                lblTotalVinculosAluno.Text = string.Format("Total de vinculo aluno curso: {0}", contadorRegistroVinculoAluno.ToString());
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ocorreu um erro ao importar o arquivo.\nVerifique se o arquivo esta de acordo com o modelo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnInserirRegistro.Enabled = false;
                txtPath.Clear();
            }
        }

        private void btnInserirRegistro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma inclusão desses registro para a base de dados? ", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RegistroAluno ra = new RegistroAluno();
                VinculoAlunoCurso vac = new VinculoAlunoCurso();

                Default frmDefault = new Default();

                int contadorRegistroIncluidos = 0;

                btnInserirRegistro.Text = "Inserindo registro...";

                file = new StreamReader(dlgOpen.FileName);

                pgsqlConnection = new NpgsqlConnection(connString);

                //Abra a conexão com o PgSQL                  
                pgsqlConnection.Open();

                while ((linha = file.ReadLine()) != null)
                {
                    getLinha = linha.Split('|');

                    if (getLinha[0].ToString() != "40")
                    {
                        //Recebe dados para gravar na taabela de registo aluno
                        if (getLinha[0].ToString().Equals("41"))
                        {
                            ra.Tipo_registro_01 = Convert.ToInt32(getLinha[0]);
                            ra.Id_aluno_inep_02 = getLinha[1];
                            ra.Nome_03 = getLinha[2].ToString();
                            ra.Cpf_04 = getLinha[3].ToString();
                            ra.Doc_estrangeiro_passaporte_05 = !string.IsNullOrEmpty(getLinha[4]) ? getLinha[4] : "NULL";
                            ra.Data_nascimento_06 = ConvertStringData(getLinha[5]);
                            ra.Sexo_07 = getLinha[6];
                            ra.Cor_raca_08 = getLinha[7];
                            ra.Nome_mae_09 = getLinha[8];
                            ra.Nacionalidade_10 = getLinha[9];
                            ra.Uf_nascimento_11 = !string.IsNullOrEmpty(getLinha[10]) ? getLinha[10] : "NULL";
                            ra.Municipio_nascimento_12 = !string.IsNullOrEmpty(getLinha[11]) ? getLinha[11] : "NULL";
                            ra.Pais_origem_13 = getLinha[12].ToString();
                            ra.Aluno_deficiencia_14 = !string.IsNullOrEmpty(getLinha[13]) ? getLinha[13] : "NULL";
                            ra.Tipo_def_cegueira_15 = !string.IsNullOrEmpty(getLinha[14]) ? getLinha[14] : "NULL";
                            ra.Tipo_def_baixa_visao_16 = !string.IsNullOrEmpty(getLinha[15]) ? getLinha[15] : "NULL";
                            ra.Tipo_def_surdez_17 = !string.IsNullOrEmpty(getLinha[16]) ? getLinha[16] : "NULL";
                            ra.Tipo_def_auditiva_18 = !string.IsNullOrEmpty(getLinha[17]) ? getLinha[17] : "NULL";
                            ra.Tipo_def_fisica_19 = !string.IsNullOrEmpty(getLinha[18]) ? getLinha[18] : "NULL";
                            ra.Tipo_def_surdocegueira_20 = !string.IsNullOrEmpty(getLinha[19]) ? getLinha[19] : "NULL";
                            ra.Tipo_def_multipla_21 = !string.IsNullOrEmpty(getLinha[20]) ? getLinha[20] : "Null";
                            ra.Tipo_def_intelectual_22 = !string.IsNullOrEmpty(getLinha[21]) ? getLinha[21] : "NULL";
                            ra.Tipo_def_autismo_23 = !string.IsNullOrEmpty(getLinha[22]) ? getLinha[22] : "NULL";
                            ra.Tipo_def_asperger_24 = !string.IsNullOrEmpty(getLinha[23]) ? getLinha[23] : "NULL";
                            ra.Tipo_def_rett_25 = !string.IsNullOrEmpty(getLinha[24]) ? getLinha[24] : "NULL";
                            ra.Tipo_def_tgi_26 = !string.IsNullOrEmpty(getLinha[25]) ? getLinha[25] : "NULL";
                            ra.Tipo_def_superdotacao_27 = !string.IsNullOrEmpty(getLinha[26]) ? getLinha[26] : "NULL";
                            ra.DataCriacao = DateTime.Now.ToString("MM/dd/yyyy");
                            ra.DataAlteracao = DateTime.Now.ToString("MM/dd/yyyy");
                            ra.Usuario = "87295849220";
                            ra.Censo = (DateTime.Now.Year - 1).ToString();
                            ra.Numero_Matricula = "NULL";

                            //Chama método para inserir registro aluno
                            InserirRegistroAluno(ra);

                            contadorRegistroIncluidos++;
                        }

                        //Recebe dados para gravar na taabela de vinculo aluno curso
                        if (getLinha[0].ToString().Equals("42"))
                        {
                            //Atribui a variavel o ultimo registro incluido na base de dados referente ao registro aluno
                            vac.id_registro_aluno_00 = Convert.ToInt32(GetUltimoRegistroIdRegistroAluno());
                            vac.tipo_registro_01 = Convert.ToInt32(getLinha[0]);
                            vac.semestre_referencia_02 = !string.IsNullOrEmpty(getLinha[1]) ? getLinha[1] : "NULL";
                            vac.codigo_curso_inep_03 = !string.IsNullOrEmpty(getLinha[2]) ? getLinha[2] : "NULL";
                            vac.cod_polo_distancia_04 = !string.IsNullOrEmpty(getLinha[3]) ? getLinha[3] : "NULL";
                            vac.id_aluno_ies_05 = !string.IsNullOrEmpty(getLinha[4]) ? getLinha[4] : "NULL";
                            vac.turno_aluno_06 = !string.IsNullOrEmpty(getLinha[5]) ? getLinha[5] : "NULL";
                            vac.situacao_vinculo_aluno_curso_07 = !string.IsNullOrEmpty(getLinha[6]) ? getLinha[6] : "NULL";
                            vac.curso_origem_08 = !string.IsNullOrEmpty(getLinha[7]) ? getLinha[7] : "NULL";
                            vac.semestre_conclusao_curso_09 = !string.IsNullOrEmpty(getLinha[8]) ? getLinha[8] : "NULL";
                            vac.aluno_parfor_10 = !string.IsNullOrEmpty(getLinha[9]) ? getLinha[9] : "NULL";
                            vac.semestre_ingresso_curso_11 = !string.IsNullOrEmpty(getLinha[10]) ? getLinha[10] : "NULL";
                            vac.tp_escola_medio_12 = !string.IsNullOrEmpty(getLinha[11]) ? getLinha[11] : "NULL";
                            vac.forma_ingresso_vestibular_13 = !string.IsNullOrEmpty(getLinha[12]) ? getLinha[12] : "NULL";
                            vac.forma_ingresso_enem_14 = !string.IsNullOrEmpty(getLinha[13]) ? getLinha[13] : "NULL";
                            vac.forma_ingresso_seriada_15 = !string.IsNullOrEmpty(getLinha[14]) ? getLinha[14] : "NULL";
                            vac.forma_ingresso_simplificada_16 = !string.IsNullOrEmpty(getLinha[15]) ? getLinha[15] : "NULL";
                            vac.forma_ingresso_egresso_bili_17 = !string.IsNullOrEmpty(getLinha[16]) ? getLinha[16] : "NULL";
                            vac.forma_ingresso_pecg_18 = !string.IsNullOrEmpty(getLinha[17]) ? getLinha[17] : "NULL";
                            vac.forma_ingresso_transf_exofficio_19 = !string.IsNullOrEmpty(getLinha[18]) ? getLinha[18] : "NULL";
                            vac.forma_ingresso_judicial_20 = !string.IsNullOrEmpty(getLinha[19]) ? getLinha[19] : "NULL";
                            vac.forma_ingresso_remanescentes_21 = !string.IsNullOrEmpty(getLinha[20]) ? getLinha[20] : "NULL";
                            vac.forma_ingresso_especiais_22 = !string.IsNullOrEmpty(getLinha[21]) ? getLinha[21] : "NULL";
                            vac.mobilidade_academica_23 = !string.IsNullOrEmpty(getLinha[22]) ? getLinha[22] : "NULL";
                            vac.tp_mobilidade_academica_24 = !string.IsNullOrEmpty(getLinha[23]) ? getLinha[23] : "NULL";
                            vac.id_ies_destino_25 = !string.IsNullOrEmpty(getLinha[24]) ? getLinha[24] : "NULL";
                            vac.tp_mobilidade_academica_inter_26 = !string.IsNullOrEmpty(getLinha[25]) ? getLinha[25] : "NULL";
                            vac.pais_destino_27 = !string.IsNullOrEmpty(getLinha[26]) ? getLinha[26] : "NULL";
                            vac.prog_reserva_vagas_28 = !string.IsNullOrEmpty(getLinha[27]) ? getLinha[27] : "NULL";
                            vac.prog_reserva_vagas_etnico_29 = !string.IsNullOrEmpty(getLinha[28]) ? getLinha[28] : "NULL";
                            vac.prog_reserva_vagas_def_30 = !string.IsNullOrEmpty(getLinha[29]) ? getLinha[29] : "NULL";
                            vac.prog_reserva_vagas_esc_publica_31 = !string.IsNullOrEmpty(getLinha[30]) ? getLinha[30] : "NULL";
                            vac.prog_reserva_vagas_renda_32 = !string.IsNullOrEmpty(getLinha[31]) ? getLinha[31] : "NULL";
                            vac.prog_reserva_vagas_outros_33 = !string.IsNullOrEmpty(getLinha[32]) ? getLinha[32] : "NULL";
                            vac.financiamento_estudantil_34 = !string.IsNullOrEmpty(getLinha[33]) ? getLinha[33] : "NULL";
                            vac.fin_est_reemb_fies_35 = !string.IsNullOrEmpty(getLinha[34]) ? getLinha[34] : "NULL";
                            vac.fin_est_reemb_est_36 = !string.IsNullOrEmpty(getLinha[35]) ? getLinha[35] : "NULL";
                            vac.fin_est_reemb_muni_37 = !string.IsNullOrEmpty(getLinha[36]) ? getLinha[36] : "NULL";
                            vac.fin_est_reemb_ies_38 = !string.IsNullOrEmpty(getLinha[37]) ? getLinha[37] : "NULL";
                            vac.fin_est_reemb_externas_39 = !string.IsNullOrEmpty(getLinha[38]) ? getLinha[38] : "NULL";
                            vac.tp_fin_nao_reemb_prouni_int_40 = !string.IsNullOrEmpty(getLinha[39]) ? getLinha[39] : "NULL";
                            vac.tp_fin_nao_reemb_prouni_par_41 = !string.IsNullOrEmpty(getLinha[40]) ? getLinha[40] : "NULL";
                            vac.tp_fin_nao_reemb_externas_42 = !string.IsNullOrEmpty(getLinha[41]) ? getLinha[41] : "NULL";
                            vac.tp_fin_nao_reemb_estadual_43 = !string.IsNullOrEmpty(getLinha[42]) ? getLinha[42] : "NULL";
                            vac.tp_fin_nao_reemb_ies_44 = !string.IsNullOrEmpty(getLinha[43]) ? getLinha[43] : "NULL";
                            vac.tp_fin_nao_reemb_municipal_45 = !string.IsNullOrEmpty(getLinha[44]) ? getLinha[44] : "NULL";
                            vac.apoio_social_46 = !string.IsNullOrEmpty(getLinha[45]) ? getLinha[45] : "NULL";
                            vac.tp_apoio_alimentacao_47 = !string.IsNullOrEmpty(getLinha[46]) ? getLinha[46] : "NULL";
                            vac.tp_apoio_moradia_48 = !string.IsNullOrEmpty(getLinha[47]) ? getLinha[47] : "NULL";
                            vac.tp_apoio_transporte_49 = !string.IsNullOrEmpty(getLinha[48]) ? getLinha[48] : "NULL";
                            vac.tp_apoio_material_50 = !string.IsNullOrEmpty(getLinha[49]) ? getLinha[49] : "NULL";
                            vac.tp_apoio_bolsa_trabalho_51 = !string.IsNullOrEmpty(getLinha[50]) ? getLinha[50] : "NULL";
                            vac.tp_apoio_bolsa_permanencia_52 = !string.IsNullOrEmpty(getLinha[51]) ? getLinha[51] : "NULL";
                            vac.atividade_extracurricular_53 = !string.IsNullOrEmpty(getLinha[52]) ? getLinha[52] : "NULL";
                            vac.ativ_pesquisa_54 = !string.IsNullOrEmpty(getLinha[53]) ? getLinha[53] : "NULL";
                            vac.bolsa_pesquisa_55 = !string.IsNullOrEmpty(getLinha[54]) ? getLinha[54] : "NULL";
                            vac.ativ_extensao_56 = !string.IsNullOrEmpty(getLinha[55]) ? getLinha[55] : "NULL";
                            vac.bolsa_extensao_57 = !string.IsNullOrEmpty(getLinha[56]) ? getLinha[56] : "NULL";
                            vac.ativ_monitoria_58 = !string.IsNullOrEmpty(getLinha[57]) ? getLinha[57] : "NULL";
                            vac.bolsa_monitoria_59 = !string.IsNullOrEmpty(getLinha[58]) ? getLinha[58] : "NULL";
                            vac.ativ_estagio_nao_obrig_60 = !string.IsNullOrEmpty(getLinha[59]) ? getLinha[59] : "NULL";
                            vac.bolsa_estagio_nao_obrig_61 = !string.IsNullOrEmpty(getLinha[60]) ? getLinha[60] : "NULL";
                            vac.ch_total_62 = !string.IsNullOrEmpty(getLinha[61]) ? getLinha[61] : "NULL";
                            vac.ch_integralizada_63 = !string.IsNullOrEmpty(getLinha[62]) ? getLinha[62] : "NULL";

                            //Chama método para inserir registo na tabela vinculo aluno curso
                            InserirVinculoAlunoCurso(vac);

                            contadorRegistroIncluidos++;
                        }
                    }
                }

                MessageBox.Show("Foram incluidos " + contadorRegistroIncluidos + " registros com sucesso", "Mensagem de sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInserirRegistro.Enabled = false;
                btnInserirRegistro.Text = "Inserir Registro";
                pgsqlConnection.Close();
            }
        }

        public void InserirRegistroAluno(RegistroAluno ra)
        {
            try
            {
                StringBuilder sbCmdInserir = new StringBuilder();

                sbCmdInserir.Append("Insert Into censo.registro_aluno(");
                sbCmdInserir.Append("tipo_registro_01,");
                sbCmdInserir.Append("id_aluno_inep_02,");
                sbCmdInserir.Append("nome_03,");
                sbCmdInserir.Append("cpf_04,");
                sbCmdInserir.Append("doc_estrangeiro_passaporte_05,");
                sbCmdInserir.Append("data_nascimento_06,");
                sbCmdInserir.Append("sexo_07,");
                sbCmdInserir.Append("cor_raca_08,");
                sbCmdInserir.Append("nome_mae_09,");
                sbCmdInserir.Append("nacionalidade_10,");
                sbCmdInserir.Append("uf_nascimento_11,");
                sbCmdInserir.Append("municipio_nascimento_12,");
                sbCmdInserir.Append("pais_origem_13,");
                sbCmdInserir.Append("aluno_deficiencia_14,");
                sbCmdInserir.Append("tipo_def_cegueira_15,");
                sbCmdInserir.Append("tipo_def_baixa_visao_16,");
                sbCmdInserir.Append("tipo_def_surdez_17,");
                sbCmdInserir.Append("tipo_def_auditiva_18,");
                sbCmdInserir.Append("tipo_def_fisica_19,");
                sbCmdInserir.Append("tipo_def_surdocegueira_20,");
                sbCmdInserir.Append("tipo_def_multipla_21,");
                sbCmdInserir.Append("tipo_def_intelectual_22,");
                sbCmdInserir.Append("tipo_def_autismo_23,");
                sbCmdInserir.Append("tipo_def_asperger_24,");
                sbCmdInserir.Append("tipo_def_rett_25,");
                sbCmdInserir.Append("tipo_def_tgi_26,");
                sbCmdInserir.Append("tipo_def_superdotacao_27,");
                sbCmdInserir.Append("data_criacao,");
                sbCmdInserir.Append("data_alteracao,");
                sbCmdInserir.Append("usuario,");
                sbCmdInserir.Append("censo,");
                sbCmdInserir.Append("numero_matricula)");

                sbCmdInserir.Append("values('");
                sbCmdInserir.Append(ra.Tipo_registro_01);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Id_aluno_inep_02);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Nome_03);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Cpf_04);
                sbCmdInserir.Append("',");
                sbCmdInserir.Append(ra.Doc_estrangeiro_passaporte_05);
                sbCmdInserir.Append(",'");
                sbCmdInserir.Append(ra.Data_nascimento_06);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Sexo_07);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Cor_raca_08);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Nome_mae_09);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Nacionalidade_10);
                sbCmdInserir.Append("',");
                sbCmdInserir.Append(ra.Uf_nascimento_11);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Municipio_nascimento_12);
                sbCmdInserir.Append(",'");
                sbCmdInserir.Append(ra.Pais_origem_13);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Aluno_deficiencia_14);
                sbCmdInserir.Append("',");
                sbCmdInserir.Append(ra.Tipo_def_cegueira_15);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_baixa_visao_16);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_surdez_17);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_auditiva_18);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_fisica_19);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_surdocegueira_20);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_multipla_21);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_intelectual_22);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_autismo_23);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_asperger_24);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_rett_25);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_tgi_26);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(ra.Tipo_def_superdotacao_27);
                sbCmdInserir.Append(",'");
                sbCmdInserir.Append(ra.DataCriacao);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.DataAlteracao);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Usuario);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Censo);
                sbCmdInserir.Append("','");
                sbCmdInserir.Append(ra.Numero_Matricula);
                sbCmdInserir.Append("')");

                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(sbCmdInserir.ToString(), pgsqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message, "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InserirVinculoAlunoCurso(VinculoAlunoCurso vac)
        {
            try
            {

                StringBuilder sbCmdInserir = new StringBuilder();

                //sbCmdInserir.Append("Insert Into censo.vinculo_aluno_curso(");
              
                sbCmdInserir.Append("tipo_registro_01,");
                sbCmdInserir.Append("semestre_referencia_02,");
                sbCmdInserir.Append("codigo_curso_inep_03,");
                sbCmdInserir.Append("cod_polo_distancia_04,");
                sbCmdInserir.Append("id_aluno_ies_05,");
                sbCmdInserir.Append("turno_aluno_06,");
                sbCmdInserir.Append("situacao_vinculo_aluno_curso_07,");
                sbCmdInserir.Append("curso_origem_08,");
                sbCmdInserir.Append("semestre_conclusao_curso_09,");
                sbCmdInserir.Append("aluno_parfor_10,");
                sbCmdInserir.Append("semestre_ingresso_curso_11,");
                sbCmdInserir.Append("tp_escola_medio_12,");
                sbCmdInserir.Append("forma_ingresso_vestibular_13,");
                sbCmdInserir.Append("forma_ingresso_enem_14,");
                sbCmdInserir.Append("forma_ingresso_seriada_15,");
                sbCmdInserir.Append("forma_ingresso_simplificada_16,");
                sbCmdInserir.Append("forma_ingresso_egresso_bili_17,");
                sbCmdInserir.Append("forma_ingresso_pecg_18,");
                sbCmdInserir.Append("forma_ingresso_transf_exofficio_19,");
                sbCmdInserir.Append("forma_ingresso_judicial_20,");
                sbCmdInserir.Append("forma_ingresso_remanescentes_21,");
                sbCmdInserir.Append("forma_ingresso_especiais_22,");
                sbCmdInserir.Append("mobilidade_academica_23,");
                sbCmdInserir.Append("tp_mobilidade_academica_24,");
                sbCmdInserir.Append("id_ies_destino_25,");
                sbCmdInserir.Append("tp_mobilidade_academica_inter_26,");
                sbCmdInserir.Append("pais_destino_27,");
                sbCmdInserir.Append("prog_reserva_vagas_28,");
                sbCmdInserir.Append("prog_reserva_vagas_etnico_29,");
                sbCmdInserir.Append("prog_reserva_vagas_def_30,");
                sbCmdInserir.Append("prog_reserva_vagas_esc_publica_31,");
                sbCmdInserir.Append("prog_reserva_vagas_renda_32,");
                sbCmdInserir.Append("prog_reserva_vagas_outros_33,");
                sbCmdInserir.Append("financiamento_estudantil_34,");
                sbCmdInserir.Append("fin_est_reemb_fies_35,");
                sbCmdInserir.Append("fin_est_reemb_est_36,");
                sbCmdInserir.Append("fin_est_reemb_muni_37,");
                sbCmdInserir.Append("fin_est_reemb_ies_38,");
                sbCmdInserir.Append("fin_est_reemb_externas_39,");
                sbCmdInserir.Append("tp_fin_nao_reemb_prouni_int_40,");
                sbCmdInserir.Append("tp_fin_nao_reemb_prouni_par_41,");
                sbCmdInserir.Append("tp_fin_nao_reemb_externas_42,");
                sbCmdInserir.Append("tp_fin_nao_reemb_estadual_43,");
                sbCmdInserir.Append("tp_fin_nao_reemb_ies_44,");
                sbCmdInserir.Append("tp_fin_nao_reemb_municipal_45,");
                sbCmdInserir.Append("apoio_social_46,");
                sbCmdInserir.Append("tp_apoio_alimentacao_47,");
                sbCmdInserir.Append("tp_apoio_moradia_48,");
                sbCmdInserir.Append("tp_apoio_transporte_49,");
                sbCmdInserir.Append("tp_apoio_material_50,");
                sbCmdInserir.Append("tp_apoio_bolsa_trabalho_51,");
                sbCmdInserir.Append("tp_apoio_bolsa_permanencia_52,");
                sbCmdInserir.Append("atividade_extracurricular_53,");
                sbCmdInserir.Append("ativ_pesquisa_54,");
                sbCmdInserir.Append("bolsa_pesquisa_55,");
                sbCmdInserir.Append("ativ_extensao_56,");
                sbCmdInserir.Append("bolsa_extensao_57,");
                sbCmdInserir.Append("ativ_monitoria_58,");
                sbCmdInserir.Append("bolsa_monitoria_59,");
                sbCmdInserir.Append("ativ_estagio_nao_obrig_60,");
                sbCmdInserir.Append("bolsa_estagio_nao_obrig_61,");
                sbCmdInserir.Append("ch_total_62,");
                sbCmdInserir.Append("ch_integralizada_63,");
                sbCmdInserir.Append("id_registro_aluno_00)");

                sbCmdInserir.Append("values(");
                sbCmdInserir.Append(vac.tipo_registro_01);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.semestre_referencia_02);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.codigo_curso_inep_03);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.cod_polo_distancia_04);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.id_aluno_ies_05);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.turno_aluno_06);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.situacao_vinculo_aluno_curso_07);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.curso_origem_08);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.semestre_conclusao_curso_09);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.aluno_parfor_10);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.semestre_ingresso_curso_11);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_escola_medio_12);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_vestibular_13);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_enem_14);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_seriada_15);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_simplificada_16);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_egresso_bili_17);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_pecg_18);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_transf_exofficio_19);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_judicial_20);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_remanescentes_21);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.forma_ingresso_especiais_22);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.mobilidade_academica_23);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_mobilidade_academica_24);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.id_ies_destino_25);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_mobilidade_academica_inter_26);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.pais_destino_27);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_28);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_etnico_29);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_def_30);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_esc_publica_31);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_renda_32);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.prog_reserva_vagas_outros_33);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.financiamento_estudantil_34);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.fin_est_reemb_fies_35);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.fin_est_reemb_est_36);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.fin_est_reemb_muni_37);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.fin_est_reemb_ies_38);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.fin_est_reemb_externas_39);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_prouni_int_40);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_prouni_par_41);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_externas_42);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_estadual_43);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_ies_44);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_fin_nao_reemb_municipal_45);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.apoio_social_46);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_alimentacao_47);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_moradia_48);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_transporte_49);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_material_50);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_bolsa_trabalho_51);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.tp_apoio_bolsa_permanencia_52);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.atividade_extracurricular_53);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ativ_pesquisa_54);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.bolsa_pesquisa_55);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ativ_extensao_56);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.bolsa_extensao_57);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ativ_monitoria_58);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.bolsa_monitoria_59);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ativ_estagio_nao_obrig_60);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.bolsa_estagio_nao_obrig_61);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ch_total_62);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.ch_integralizada_63);
                sbCmdInserir.Append(",");
                sbCmdInserir.Append(vac.id_registro_aluno_00);
                sbCmdInserir.Append(")");

                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(sbCmdInserir.ToString(), pgsqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message, "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int? GetUltimoRegistroIdRegistroAluno()
        {
            int? id_registro_aluno = null;

            try
            {
                DataTable dtRegistro = new DataTable();

                string cmdSeleciona = "select a.id_registro_aluno_00 from censo.registro_aluno a order by a.id_registro_aluno_00 desc LIMIT 1";

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                {
                    Adpt.Fill(dtRegistro);

                    if (dtRegistro.Rows.Count != 0)
                    {
                        foreach (DataRow item in dtRegistro.Rows)
                        {
                            id_registro_aluno = Convert.ToInt32(item["id_registro_aluno_00"]);
                        }
                    }
                }

            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id_registro_aluno;
        }

        private String ConvertStringData(string data)
        {
            string newdt;
            string ano;
            string mes;
            string dia;

            dia = data.Substring(0, 2);
            mes = data.Substring(2, 2);
            ano = data.Substring(4);

            newdt = (mes + "/" + dia + "/" + ano);

            return newdt;
        }


        public void CriaArquivoLeiauteCenso()
        {
            pCarregando.Visible = true;
            pCarregando.Refresh();

            FileInfo arquivo = new FileInfo(exeLocation + "\\Exportacao\\Censo_" + (DateTime.Now.Year - 1).ToString() + ".txt");

            //Cria o arquivo
            using (FileStream fs = arquivo.Create()) { }

            StringBuilder write = new StringBuilder();

            //Inclui cabeçalho ao arquivo
            write.AppendLine("40|699|4| Data e hora criacao do arquivo: " + DateTime.Now.ToString());//Nome no arquivo

            pgsqlConnection = new NpgsqlConnection(connString);

            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            DataTable dt41 = new DataTable();
            DataTable dt42_1 = new DataTable();
            DataTable dt42_2 = new DataTable();

            dt41 = Lista41();
            dt42_1 = Lista42_1();
            dt42_2 = Lista42_2();

            pgsqlConnection.Close();

            string linha41;
            string linha42_1;
            string linha42_2;

            foreach (DataRow row41 in dt41.Rows)
            {
                
                //Incluir o cabeçalho 41
                linha41 =
                       row41["1"]  + "|" + row41["2"]  + "|" + row41["3"] + "|"  + row41["4"] + "|"  + row41["5"]  + "|" + row41["6"] + "|"  + row41["7"]  + "|" + row41["8"]  + "|" + row41["9"] + "|"  + row41["10"] + "|"
                     + row41["11"] + "|" + row41["12"] + "|" + row41["13"] + "|" + row41["14"] + "|" + row41["15"] + "|" + row41["16"] + "|" + row41["17"] + "|" + row41["18"] + "|" + row41["19"] + "|" + row41["20"] + "|"
                     + row41["21"] + "|" + row41["22"] + "|" + row41["23"] + "|" + row41["24"] + "|" + row41["25"] + "|" + row41["26"] + "|" + row41["27"];

                write.AppendLine(linha41);

                //Verifica se existe linha 42 semestre 1 - atraves do cpf da linha 41
                foreach (DataRow row42_1 in dt42_1.Rows)
                {
                    if (row41["4"].Equals(row42_1["0"]))
                    {
                        linha42_1 =
                              row42_1["1"] + "|" + row42_1["2"] + "|" + row42_1["3"] + "|" + row42_1["4"] + "|" + row42_1["5"] + "|" + row42_1["6"] + "|" + row42_1["7"] + "|" + row42_1["8"] + "|" + row42_1["9"] + "|" + row42_1["10"] + "|"
                            + row42_1["11"] + "|" + row42_1["12"] + "|" + row42_1["13"] + "|" + row42_1["14"] + "|" + row42_1["15"] + "|" + row42_1["16"] + "|" + row42_1["17"] + "|" + row42_1["18"] + "|" + row42_1["19"] + "|" + row42_1["20"] + "|"
                            + row42_1["21"] + "|" + row42_1["22"] + "|" + row42_1["23"] + "|" + row42_1["24"] + "|" + row42_1["25"] + "|" + row42_1["26"] + "|" + row42_1["27"] + "|" + row42_1["28"] + "|" + row42_1["29"] + "|" + row42_1["30"] + "|"
                            + row42_1["31"] + "|" + row42_1["32"] + "|" + row42_1["33"] + "|" + row42_1["34"] + "|" + row42_1["35"] + "|" + row42_1["36"] + "|" + row42_1["37"] + "|" + row42_1["38"] + "|" + row42_1["39"] + "|" + row42_1["40"] + "|"
                            + row42_1["41"] + "|" + row42_1["42"] + "|" + row42_1["43"] + "|" + row42_1["44"] + "|" + row42_1["45"] + "|" + row42_1["46"] + "|" + row42_1["47"] + "|" + row42_1["48"] + "|" + row42_1["49"] + "|" + row42_1["50"] + "|"
                            + row42_1["51"] + "|" + row42_1["52"] + "|" + row42_1["53"] + "|" + row42_1["54"] + "|" + row42_1["55"] + "|" + row42_1["56"] + "|" + row42_1["57"] + "|" + row42_1["58"] + "|" + row42_1["59"] + "|" + row42_1["60"] + "|"
                            + row42_1["61"] + "|" + row42_1["62"] + "|" + row42_1["63"];

                        write.AppendLine(linha42_1);
                        break;
                    }
                }

                //Verifica se existe linha 42 semestre 2 atraves do cpf da linha 41
                foreach (DataRow row42_2 in dt42_2.Rows)
                {
                    if (row41["4"].Equals(row42_2["0"]))
                    {
                        linha42_2 =
                              row42_2["1"] + "|" + row42_2["2"] + "|" + row42_2["3"] + "|" + row42_2["4"] + "|" + row42_2["5"] + "|" + row42_2["6"] + "|" + row42_2["7"] + "|" + row42_2["8"] + "|" + row42_2["9"] + "|" + row42_2["10"] + "|"
                            + row42_2["11"] + "|" + row42_2["12"] + "|" + row42_2["13"] + "|" + row42_2["14"] + "|" + row42_2["15"] + "|" + row42_2["16"] + "|" + row42_2["17"] + "|" + row42_2["18"] + "|" + row42_2["19"] + "|" + row42_2["20"] + "|"
                            + row42_2["21"] + "|" + row42_2["22"] + "|" + row42_2["23"] + "|" + row42_2["24"] + "|" + row42_2["25"] + "|" + row42_2["26"] + "|" + row42_2["27"] + "|" + row42_2["28"] + "|" + row42_2["29"] + "|" + row42_2["30"] + "|"
                            + row42_2["31"] + "|" + row42_2["32"] + "|" + row42_2["33"] + "|" + row42_2["34"] + "|" + row42_2["35"] + "|" + row42_2["36"] + "|" + row42_2["37"] + "|" + row42_2["38"] + "|" + row42_2["39"] + "|" + row42_2["40"] + "|"
                            + row42_2["41"] + "|" + row42_2["42"] + "|" + row42_2["43"] + "|" + row42_2["44"] + "|" + row42_2["45"] + "|" + row42_2["46"] + "|" + row42_2["47"] + "|" + row42_2["48"] + "|" + row42_2["49"] + "|" + row42_2["50"] + "|"
                            + row42_2["51"] + "|" + row42_2["52"] + "|" + row42_2["53"] + "|" + row42_2["54"] + "|" + row42_2["55"] + "|" + row42_2["56"] + "|" + row42_2["57"] + "|" + row42_2["58"] + "|" + row42_2["59"] + "|" + row42_2["60"] + "|"
                            + row42_2["61"] + "|" + row42_2["62"] + "|" + row42_2["63"];

                        write.AppendLine(linha42_2);
                        break;
                    }
                }
            }

            string[] texto = { write.ToString() };

            //Escreve as informações no arquivo txt, salvo no diretorio expecificado
            File.WriteAllLines(exeLocation + "\\Exportacao\\Censo_" + (DateTime.Now.Year - 1).ToString() + ".txt", texto);

            MessageBox.Show("Arquivo gerado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ExibirResultadoArquivoCriado()
        {
            try
            {
                StringBuilder colunas = new StringBuilder();

                this.gvArquivosCsv.DataSource = null;
                this.gvArquivosCsv.Rows.Clear();
                contadorRegistro = 0;
                contadorRegistroVinculoAluno = 0;
                pbRegistro.Visible = false;
                pbAluno.Visible = false;
                pbVinculoAlunoCurso.Visible = false;
                lblTotalRegistros.Visible = false;
                lblTotalLinhasRegistroAluno.Visible = false;
                lblTotalVinculosAluno.Visible = false;

                dt = HelperCSV.RetornaDataTableArquivoCSV(exeLocation + "\\Exportacao\\Censo_" + (DateTime.Now.Year - 1).ToString() + ".txt");

                file = new StreamReader(exeLocation + "\\Exportacao\\Censo_" + (DateTime.Now.Year - 1).ToString() + ".txt");

                while ((linha = file.ReadLine()) != null)
                {
                    getLinha = linha.Split('|');

                    if (getLinha[0].ToString() != "40")
                    {
                        MessageBox.Show("Arquivo não corresponde ao\n LEIAUTE DE MIGRAÇÃO – ARQUIVO ALUNO", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (dt.Columns.Count != 0)
                        {

                            gvArquivosCsv.DataSource = dt;
                            btnInserirRegistro.Enabled = true;

                            pbRegistro.Visible = true;
                            lblTotalLinhasRegistroAluno.Visible = true;
                            lblTotalLinhasRegistroAluno.Text = string.Format("Total de registros: {0}", dt.Rows.Count);

                            //Pecorrendo o datatable - somente para teste
                            foreach (DataRow item in dt.Rows)
                            {
                                pegaLinhaRegistro = item[0].ToString().Split('|');

                                if (pegaLinhaRegistro[0].ToString().Equals("41"))
                                {
                                    contadorRegistro++;
                                }
                                if (pegaLinhaRegistro[0].ToString().Equals("42"))
                                {
                                    contadorRegistroVinculoAluno++;
                                }
                            }

                            pbAluno.Visible = true;
                            pbVinculoAlunoCurso.Visible = true;
                            lblTotalRegistros.Visible = true;
                            lblTotalVinculosAluno.Visible = true;
                            lblTotalRegistros.Text = string.Format("Total de alunos: {0}", contadorRegistro.ToString());
                            lblTotalVinculosAluno.Text = string.Format("Total de vinculo aluno curso: {0}", contadorRegistroVinculoAluno.ToString());
                        }
                    }
                    break;
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ocorreu um erro ao importar o arquivo.\nVerifique se o arquivo esta de acordo com o modelo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnInserirRegistro.Enabled = false;
            }
        }


        public DataTable Lista41()
        {
            DataTable dtRegistro = new DataTable();
            try
            {
                string cmdSeleciona = "select * from public.censo_2018_41";

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                {
                    Adpt.Fill(dtRegistro);
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtRegistro;
        }

        public DataTable Lista42_1()
        {
            DataTable dtRegistro = new DataTable();

            try
            {
                string cmdSeleciona = "select * from public.censo_2018_42_1";

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                {
                    Adpt.Fill(dtRegistro);
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtRegistro;
        }

        public DataTable Lista42_2()
        {
            DataTable dtRegistro = new DataTable();

            try
            {
                string cmdSeleciona = "select * from public.censo_2018_42_2";

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                {
                    Adpt.Fill(dtRegistro);
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtRegistro;
        }

        private void btnGerarArquivoCSV_Click_1(object sender, EventArgs e)
        {
                CriaArquivoLeiauteCenso();
            ExibirResultadoArquivoCriado();
        }

        private void btnAtualizarNomeDataNasc_Click(object sender, EventArgs e)
        {
            string cpf;
            string dataNascimento;
            string nomeCorreto;
            string cmdUpdate;

            pgsqlConnection = new NpgsqlConnection(connString);

            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            DataTable listaNomeDataNasci = new DataTable();

            try
            {
                string cmdSeleciona = "select * from public.censo_2018_41";

                using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, pgsqlConnection))
                {
                    Adpt.Fill(listaNomeDataNasci);

                    foreach (DataRow row in listaNomeDataNasci.Rows)
                    {
                        cpf = row["4"].ToString();
                        dataNascimento = row["6"].ToString().Replace("/", "");

                        cmdUpdate = "update public.censo_2018_41_nao_importado set \"6\" = '" + dataNascimento + "' where \"4\" = '" + cpf + "'";

                        using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdUpdate.ToString(), pgsqlConnection))
                        {
                            pgsqlcommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            pgsqlConnection.Close();
        }

        private void Default_Load(object sender, EventArgs e)
        {

        }
    }
}
