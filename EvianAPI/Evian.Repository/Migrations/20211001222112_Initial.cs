using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evian.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banco",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banco", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaPaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoCarteira = table.Column<int>(type: "int", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.id);
                    table.ForeignKey(
                        name: "FK_categoria_categoria_CategoriaPaiId",
                        column: x => x.CategoriaPaiId,
                        principalTable: "categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "condicao_parcelamento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtdParcelas = table.Column<int>(type: "int", nullable: true),
                    CondicoesParcelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_condicao_parcelamento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "forma_pagamento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoFormaPagamento = table.Column<int>(type: "int", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forma_pagamento", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    codigo_ibge = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    sigla = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pais", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conta_bancaria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitoAgencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitoConta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BancoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conta_bancaria", x => x.id);
                    table.ForeignKey(
                        name: "FK_conta_bancaria_banco_BancoId",
                        column: x => x.BancoId,
                        principalTable: "banco",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estado",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sigla = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UtcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo_ibge = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    pais_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado", x => x.id);
                    table.ForeignKey(
                        name: "FK_estado_pais_pais_id",
                        column: x => x.pais_id,
                        principalTable: "pais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conciliacao_bancaria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaBancariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Arquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conciliacao_bancaria", x => x.id);
                    table.ForeignKey(
                        name: "FK_conciliacao_bancaria_conta_bancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "conta_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saldo_historico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContaBancariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaldoDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoConsolidado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRecebimentos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPagamentos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saldo_historico", x => x.id);
                    table.ForeignKey(
                        name: "FK_saldo_historico_conta_bancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "conta_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cidade",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    codigo_ibge = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    estado_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cidade", x => x.id);
                    table.ForeignKey(
                        name: "FK_cidade_estado_estado_id",
                        column: x => x.estado_id,
                        principalTable: "estado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conciliacao_bancaria_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConciliacaoBancariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfxLancamentoMD5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusConciliado = table.Column<int>(type: "int", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conciliacao_bancaria_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_conciliacao_bancaria_item_conciliacao_bancaria_ConciliacaoBancariaId",
                        column: x => x.ConciliacaoBancariaId,
                        principalTable: "conciliacao_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPFCNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaisId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente = table.Column<bool>(type: "bit", nullable: false),
                    Fornecedor = table.Column<bool>(type: "bit", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.id);
                    table.ForeignKey(
                        name: "FK_pessoa_cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "cidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pessoa_estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pessoa_pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "pais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conta_financeira",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaFinanceiraRepeticaoPaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContaFinanceiraParcelaPaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValorPrevisto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CondicaoParcelamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormaPagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoContaFinanceira = table.Column<int>(type: "int", nullable: false),
                    StatusContaBancaria = table.Column<int>(type: "int", nullable: false),
                    Repetir = table.Column<bool>(type: "bit", nullable: false),
                    TipoPeriodicidade = table.Column<int>(type: "int", nullable: false),
                    NumeroRepeticoes = table.Column<int>(type: "int", nullable: true),
                    DescricaoParcela = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conta_financeira", x => x.id);
                    table.ForeignKey(
                        name: "FK_conta_financeira_categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categoria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conta_financeira_condicao_parcelamento_CondicaoParcelamentoId",
                        column: x => x.CondicaoParcelamentoId,
                        principalTable: "condicao_parcelamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conta_financeira_conta_financeira_ContaFinanceiraParcelaPaiId",
                        column: x => x.ContaFinanceiraParcelaPaiId,
                        principalTable: "conta_financeira",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_conta_financeira_conta_financeira_ContaFinanceiraRepeticaoPaiId",
                        column: x => x.ContaFinanceiraRepeticaoPaiId,
                        principalTable: "conta_financeira",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_conta_financeira_forma_pagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "forma_pagamento",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conta_financeira_pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "pessoa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conta_financeira_baixa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContaFinanceiraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContaBancariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conta_financeira_baixa", x => x.id);
                    table.ForeignKey(
                        name: "FK_conta_financeira_baixa_conta_bancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "conta_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conta_financeira_baixa_conta_financeira_ContaFinanceiraId",
                        column: x => x.ContaFinanceiraId,
                        principalTable: "conta_financeira",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "movimentacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContaBancariaOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContaBancariaDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContaFinanceiraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_movimentacao_conta_bancaria_ContaBancariaDestinoId",
                        column: x => x.ContaBancariaDestinoId,
                        principalTable: "conta_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movimentacao_conta_bancaria_ContaBancariaOrigemId",
                        column: x => x.ContaBancariaOrigemId,
                        principalTable: "conta_bancaria",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movimentacao_conta_financeira_ContaFinanceiraId",
                        column: x => x.ContaFinanceiraId,
                        principalTable: "conta_financeira",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conciliacao_bancaria_item_conta_financeira",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConciliacaoBancariaItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaFinanceiraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContaFinanceiraBaixaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValorConciliado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_exclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usuario_inclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    usuario_alteracao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    usuario_exclusao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conciliacao_bancaria_item_conta_financeira", x => x.id);
                    table.ForeignKey(
                        name: "FK_conciliacao_bancaria_item_conta_financeira_conciliacao_bancaria_item_ConciliacaoBancariaItemId",
                        column: x => x.ConciliacaoBancariaItemId,
                        principalTable: "conciliacao_bancaria_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conciliacao_bancaria_item_conta_financeira_conta_financeira_baixa_ContaFinanceiraBaixaId",
                        column: x => x.ContaFinanceiraBaixaId,
                        principalTable: "conta_financeira_baixa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_conciliacao_bancaria_item_conta_financeira_conta_financeira_ContaFinanceiraId",
                        column: x => x.ContaFinanceiraId,
                        principalTable: "conta_financeira",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoria_CategoriaPaiId",
                table: "categoria",
                column: "CategoriaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_cidade_estado_id",
                table: "cidade",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_conciliacao_bancaria_ContaBancariaId",
                table: "conciliacao_bancaria",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_conciliacao_bancaria_item_ConciliacaoBancariaId",
                table: "conciliacao_bancaria_item",
                column: "ConciliacaoBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_conciliacao_bancaria_item_conta_financeira_ConciliacaoBancariaItemId",
                table: "conciliacao_bancaria_item_conta_financeira",
                column: "ConciliacaoBancariaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_conciliacao_bancaria_item_conta_financeira_ContaFinanceiraBaixaId",
                table: "conciliacao_bancaria_item_conta_financeira",
                column: "ContaFinanceiraBaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_conciliacao_bancaria_item_conta_financeira_ContaFinanceiraId",
                table: "conciliacao_bancaria_item_conta_financeira",
                column: "ContaFinanceiraId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_bancaria_BancoId",
                table: "conta_bancaria",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_CategoriaId",
                table: "conta_financeira",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_CondicaoParcelamentoId",
                table: "conta_financeira",
                column: "CondicaoParcelamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_ContaFinanceiraParcelaPaiId",
                table: "conta_financeira",
                column: "ContaFinanceiraParcelaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_ContaFinanceiraRepeticaoPaiId",
                table: "conta_financeira",
                column: "ContaFinanceiraRepeticaoPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_FormaPagamentoId",
                table: "conta_financeira",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_PessoaId",
                table: "conta_financeira",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_baixa_ContaBancariaId",
                table: "conta_financeira_baixa",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_conta_financeira_baixa_ContaFinanceiraId",
                table: "conta_financeira_baixa",
                column: "ContaFinanceiraId");

            migrationBuilder.CreateIndex(
                name: "IX_estado_pais_id",
                table: "estado",
                column: "pais_id");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_ContaBancariaDestinoId",
                table: "movimentacao",
                column: "ContaBancariaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_ContaBancariaOrigemId",
                table: "movimentacao",
                column: "ContaBancariaOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacao_ContaFinanceiraId",
                table: "movimentacao",
                column: "ContaFinanceiraId",
                unique: true,
                filter: "[ContaFinanceiraId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_CidadeId",
                table: "pessoa",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_EstadoId",
                table: "pessoa",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_PaisId",
                table: "pessoa",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_saldo_historico_ContaBancariaId",
                table: "saldo_historico",
                column: "ContaBancariaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conciliacao_bancaria_item_conta_financeira");

            migrationBuilder.DropTable(
                name: "movimentacao");

            migrationBuilder.DropTable(
                name: "saldo_historico");

            migrationBuilder.DropTable(
                name: "conciliacao_bancaria_item");

            migrationBuilder.DropTable(
                name: "conta_financeira_baixa");

            migrationBuilder.DropTable(
                name: "conciliacao_bancaria");

            migrationBuilder.DropTable(
                name: "conta_financeira");

            migrationBuilder.DropTable(
                name: "conta_bancaria");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "condicao_parcelamento");

            migrationBuilder.DropTable(
                name: "forma_pagamento");

            migrationBuilder.DropTable(
                name: "pessoa");

            migrationBuilder.DropTable(
                name: "banco");

            migrationBuilder.DropTable(
                name: "cidade");

            migrationBuilder.DropTable(
                name: "estado");

            migrationBuilder.DropTable(
                name: "pais");
        }
    }
}
