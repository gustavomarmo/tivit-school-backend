using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using edu_connect_backend.DTO.Nota;

namespace edu_connect_backend.Service
{
    public class BoletimPdfService
    {
        // Paleta de cores da escola
        private static readonly string ColorPrimary = "#f20024";   // --brand-primary
        private static readonly string ColorSecondary = "#262626"; // --bg-secondary (Cinza Escuro)
        private static readonly string ColorAccent = "#d1001f";    // --brand-secondary
        private static readonly string ColorSuccess = "#27AE60";   // Verde mantido para UX de "Aprovado"
        private static readonly string ColorDanger = "#f20024";    // --brand-primary para "Recuperação"
        private static readonly string ColorRowAlt = "#f4f7f6";    // --bg-primary
        private static readonly string ColorRowWhite = "#FFFFFF";  // Branco padrão
        private static readonly string ColorHeaderBg = "#262626";  // --bg-secondary
        private static readonly string ColorSubHeader = "#929292"; // --border-color / --icon-inactive

        public byte[] GerarPdfBoletim(List<BoletimDTO> boletim, string nomeAluno)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(1.5f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(10).FontColor(Colors.Black));

                    // ── CABEÇALHO ──────────────────────────────────────────────
                    page.Header().Column(col =>
                    {
                        // Faixa superior
                        col.Item().Background(ColorPrimary).Padding(12).Row(row =>
                        {
                            row.RelativeItem().Column(inner =>
                            {
                                inner.Item()
                                    .Text("🎓 Edu Connect")
                                    .FontSize(22).Bold().FontColor(Colors.White);
                                inner.Item()
                                    .Text("Boletim Escolar — Relatório de Desempenho")
                                    .FontSize(11).FontColor("#B8D4F0");
                            });

                            row.ConstantItem(220).AlignRight().Column(inner =>
                            {
                                inner.Item()
                                    .Text($"Aluno: {nomeAluno}")
                                    .FontSize(11).Bold().FontColor(Colors.White).AlignRight();
                                inner.Item()
                                    .Text($"Emitido em: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                    .FontSize(9).FontColor("#B8D4F0").AlignRight();
                                inner.Item()
                                    .Text($"Ano Letivo: {DateTime.Now.Year}")
                                    .FontSize(9).FontColor("#B8D4F0").AlignRight();
                            });
                        });

                        // Faixa dourada decorativa
                        col.Item().Height(4).Background(ColorAccent);
                    });

                    // ── CONTEÚDO ───────────────────────────────────────────────
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        // Legenda de status
                        col.Item().PaddingBottom(8).Row(row =>
                        {
                            row.AutoItem().Background(ColorSuccess).PaddingVertical(4).PaddingHorizontal(6)
                                .Text("✔  Aprovado  (média ≥ 5,0)").FontSize(8).FontColor(Colors.White).Bold();
                            row.ConstantItem(8);
                            row.AutoItem().Background(ColorDanger).PaddingVertical(4).PaddingHorizontal(6)
                                .Text("✘  Recuperação  (média < 5,0)").FontSize(8).FontColor(Colors.White).Bold();
                            row.RelativeItem(); // espaço
                        });

                        // Tabela principal
                        col.Item().Table(table =>
                        {
                            // Definição de colunas
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(3.0f); // Disciplina
                                // 1º Bimestre
                                cols.RelativeColumn(1.0f); // N1
                                cols.RelativeColumn(1.0f); // N2
                                cols.RelativeColumn(1.0f); // Ativ
                                cols.RelativeColumn(1.2f); // Média B1
                                // 2º Bimestre
                                cols.RelativeColumn(1.0f); // N1
                                cols.RelativeColumn(1.0f); // N2
                                cols.RelativeColumn(1.0f); // Ativ
                                cols.RelativeColumn(1.2f); // Média B2
                                // Resultado
                                cols.RelativeColumn(1.4f); // Média Semestral
                                cols.RelativeColumn(1.3f); // Situação
                            });

                            // ── Cabeçalho grupo ──
                            // Linha 1 — Agrupadores
                            table.Header(header =>
                            {
                                // Disciplina
                                header.Cell().RowSpan(2).Background(ColorHeaderBg)
                                    .Padding(6).AlignMiddle().AlignCenter()
                                    .Text("DISCIPLINA").FontSize(9).Bold().FontColor(Colors.White);

                                // 1º Bimestre
                                header.Cell().ColumnSpan(4).Background(ColorSecondary)
                                    .Padding(5).AlignCenter()
                                    .Text("1º BIMESTRE").FontSize(9).Bold().FontColor(Colors.White);

                                // 2º Bimestre
                                header.Cell().ColumnSpan(4).Background(ColorSecondary)
                                    .Padding(5).AlignCenter()
                                    .Text("2º BIMESTRE").FontSize(9).Bold().FontColor(Colors.White);

                                // Resultado
                                header.Cell().RowSpan(2).Background(ColorAccent)
                                    .Padding(6).AlignMiddle().AlignCenter()
                                    .Text("MÉD.\nSEMESTRAL").FontSize(9).Bold().FontColor(Colors.White);

                                header.Cell().RowSpan(2).Background(ColorPrimary)
                                    .Padding(6).AlignMiddle().AlignCenter()
                                    .Text("SITUAÇÃO").FontSize(9).Bold().FontColor(Colors.White);

                                // Linha 2 — Sub-colunas B1
                                foreach (var sub in new[] { "N1", "N2", "Ativ", "Média" })
                                {
                                    header.Cell().Background("#3A8DD4").Padding(4).AlignCenter()
                                        .Text(sub).FontSize(8).Bold().FontColor(Colors.White);
                                }

                                // Sub-colunas B2
                                foreach (var sub in new[] { "N1", "N2", "Ativ", "Média" })
                                {
                                    header.Cell().Background("#3A8DD4").Padding(4).AlignCenter()
                                        .Text(sub).FontSize(8).Bold().FontColor(Colors.White);
                                }
                            });

                            // ── Linhas de dados ──
                            for (int i = 0; i < boletim.Count; i++)
                            {
                                var item = boletim[i];
                                bool isAlt = i % 2 == 1;
                                string rowBg = isAlt ? ColorRowAlt : ColorRowWhite;

                                // Médias calculadas
                                decimal mediaB1 = CalcularMedia(item.N1_N1, item.N1_N2, item.N1_Ativ);
                                decimal mediaB2 = CalcularMedia(item.N2_N1, item.N2_N2, item.N2_Ativ);
                                decimal mediaSemestral = Math.Round((mediaB1 + mediaB2) / 2, 1);
                                bool aprovado = mediaSemestral >= 5.0m;

                                // Disciplina
                                table.Cell().Background(rowBg).BorderBottom(0.5f).BorderColor("#D0D9E8")
                                    .Padding(6).AlignMiddle()
                                    .Text(item.Materia).FontSize(9).Bold();

                                // Notas B1
                                foreach (var val in new[] { item.N1_N1, item.N1_N2, item.N1_Ativ })
                                    CelulaValor(table, val, rowBg);

                                // Média B1
                                CelulaNota(table, mediaB1, rowBg, destaque: true);

                                // Notas B2
                                foreach (var val in new[] { item.N2_N1, item.N2_N2, item.N2_Ativ })
                                    CelulaValor(table, val, rowBg);

                                // Média B2
                                CelulaNota(table, mediaB2, rowBg, destaque: true);

                                // Média Semestral — célula com cor condicional
                                string bgSem = aprovado ? "#E8F8EF" : "#FDECEA";
                                string fgSem = aprovado ? ColorSuccess : ColorDanger;
                                table.Cell().Background(bgSem).BorderBottom(0.5f).BorderColor("#D0D9E8")
                                    .Padding(6).AlignMiddle().AlignCenter()
                                    .Text(mediaSemestral.ToString("F1"))
                                    .FontSize(10).Bold().FontColor(fgSem);

                                // Situação
                                string bgSit = aprovado ? ColorSuccess : ColorDanger;
                                string textoSit = aprovado ? "APROVADO" : "RECUPERAÇÃO";
                                table.Cell().Background(bgSit).BorderBottom(0.5f).BorderColor("#D0D9E8")
                                    .Padding(6).AlignMiddle().AlignCenter()
                                    .Text(textoSit).FontSize(8).Bold().FontColor(Colors.White);
                            }

                            // ── Linha de totais (médias gerais) ──
                            if (boletim.Any())
                            {
                                var mediasB1 = boletim.Select(b => CalcularMedia(b.N1_N1, b.N1_N2, b.N1_Ativ)).ToList();
                                var mediasB2 = boletim.Select(b => CalcularMedia(b.N2_N1, b.N2_N2, b.N2_Ativ)).ToList();
                                decimal mediaGeralB1 = Math.Round(mediasB1.Average(), 1);
                                decimal mediaGeralB2 = Math.Round(mediasB2.Average(), 1);
                                decimal mediaGeralSem = Math.Round((mediaGeralB1 + mediaGeralB2) / 2, 1);
                                bool aprovadoGeral = mediaGeralSem >= 5.0m;

                                // Label
                                table.Cell().ColumnSpan(4).Background(ColorPrimary).Padding(6).AlignRight().AlignMiddle()
                                    .Text("MÉDIA GERAL DA TURMA →").FontSize(9).Bold().FontColor(Colors.White);

                                // Média geral B1
                                table.Cell().Background(ColorPrimary).Padding(6).AlignCenter().AlignMiddle()
                                    .Text(mediaGeralB1.ToString("F1")).FontSize(9).Bold().FontColor(ColorAccent);

                                // 4 colunas vazias B2 (N1 N2 Ativ)
                                for (int k = 0; k < 3; k++)
                                    table.Cell().Background(ColorPrimary).Padding(6).AlignCenter().AlignMiddle()
                                        .Text("—").FontSize(9).FontColor("#6A8DB0");

                                // Média geral B2
                                table.Cell().Background(ColorPrimary).Padding(6).AlignCenter().AlignMiddle()
                                    .Text(mediaGeralB2.ToString("F1")).FontSize(9).Bold().FontColor(ColorAccent);

                                // Média semestral geral
                                table.Cell().Background(aprovadoGeral ? "#27AE60" : "#E74C3C").Padding(6).AlignCenter().AlignMiddle()
                                    .Text(mediaGeralSem.ToString("F1")).FontSize(10).Bold().FontColor(Colors.White);

                                // Situação geral
                                table.Cell().Background(ColorPrimary).Padding(6).AlignCenter().AlignMiddle()
                                    .Text("").FontSize(8).FontColor(Colors.White);
                            }
                        });

                        // Observações
                        col.Item().PaddingTop(14).Row(row =>
                        {
                            row.RelativeItem().Background("#F4F8FD").Border(0.5f).BorderColor("#C5D6EC")
                                .Padding(8).Column(obs =>
                                {
                                    obs.Item().Text("Observações:").FontSize(9).Bold().FontColor(ColorPrimary);
                                    obs.Item().PaddingTop(4)
                                        .Text("• A média bimestral é calculada pela fórmula: (N1 + N2 + Ativ) / 3")
                                        .FontSize(8).FontColor("#555");
                                    obs.Item()
                                        .Text("• A média semestral é a média aritmética entre os dois bimestres.")
                                        .FontSize(8).FontColor("#555");
                                    obs.Item()
                                        .Text("• Alunos com média semestral inferior a 5,0 serão encaminhados para recuperação.")
                                        .FontSize(8).FontColor("#555");
                                });
                        });
                    });

                    // ── RODAPÉ ────────────────────────────────────────────────
                    page.Footer().Column(col =>
                    {
                        col.Item().Height(3).Background(ColorAccent);
                        col.Item().Background(ColorPrimary).Padding(6).Row(row =>
                        {
                            row.RelativeItem()
                                .Text("Edu Connect — Sistema de Gestão Escolar")
                                .FontSize(8).FontColor("#B8D4F0");
                            row.AutoItem()
                                .Text(x =>
                                {
                                    x.Span("Documento gerado automaticamente em ")
                                        .FontSize(8).FontColor("#B8D4F0");
                                    x.Span(DateTime.Now.ToString("dd/MM/yyyy 'às' HH:mm"))
                                        .FontSize(8).Bold().FontColor(Colors.White);
                                });
                            row.ConstantItem(16);
                            row.AutoItem().Text(x =>
                            {
                                x.Span("Pág. ").FontSize(8).FontColor("#B8D4F0");
                                x.CurrentPageNumber().FontSize(8).Bold().FontColor(Colors.White);
                                x.Span(" / ").FontSize(8).FontColor("#B8D4F0");
                                x.TotalPages().FontSize(8).Bold().FontColor(Colors.White);
                            });
                        });
                    });
                });
            });

            return documento.GeneratePdf();
        }

        private static decimal CalcularMedia(decimal n1, decimal n2, decimal ativ)
            => Math.Round((n1 + n2 + ativ) / 3, 1);

        private static void CelulaValor(TableDescriptor table, decimal valor, string rowBg)
        {
            string cor = valor < 5.0m ? "#C0392B" : "#2C3E50";
            table.Cell().Background(rowBg).BorderBottom(0.5f).BorderColor("#D0D9E8")
                .Padding(6).AlignCenter().AlignMiddle()
                .Text(valor.ToString("F1")).FontSize(9).FontColor(cor);
        }

        private static void CelulaNota(TableDescriptor table, decimal media, string rowBg, bool destaque = false)
        {
            string bg = destaque ? (media >= 5.0m ? "#DFF5EA" : "#FDECEA") : rowBg;
            string cor = media >= 5.0m ? "#1E8449" : "#C0392B";
            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor("#D0D9E8")
                .Padding(6).AlignCenter().AlignMiddle()
                .Text(media.ToString("F1")).FontSize(9).Bold().FontColor(cor);
        }
    }
}