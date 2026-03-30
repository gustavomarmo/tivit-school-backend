using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using edu_connect_backend.DTO;

namespace edu_connect_backend.Service
{
    public class BoletimPdfService
    {
        public byte[] GerarPdfBoletim(List<BoletimDTO> boletim, string nomeAluno)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text($"Boletim Escolar - {nomeAluno}")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().ColumnSpan(1).Element(BlockHeader).Text("Disciplina");
                                header.Cell().ColumnSpan(3).Element(BlockHeader).AlignCenter().Text("1º Bimestre");
                                header.Cell().ColumnSpan(3).Element(BlockHeader).AlignCenter().Text("2º Bimestre");

                                header.Cell().Element(BlockSubHeader).Text("");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("N1");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("N2");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("Ativ");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("N1");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("N2");
                                header.Cell().Element(BlockSubHeader).AlignCenter().Text("Ativ");
                            });

                            foreach (var item in boletim)
                            {
                                table.Cell().Element(BlockCell).Text(item.Materia);
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N1_N1.ToString("F1"));
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N1_N2.ToString("F1"));
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N1_Ativ.ToString("F1"));
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N2_N1.ToString("F1"));
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N2_N2.ToString("F1"));
                                table.Cell().Element(BlockCell).AlignCenter().Text(item.N2_Ativ.ToString("F1"));
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Gerado automaticamente em ");
                            x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        });
                });
            });

            return documento.GeneratePdf();
        }
        static IContainer BlockHeader(IContainer container)
        {
            return container
                .Border(1)
                .Background(Colors.Grey.Lighten3)
                .Padding(5);
        }

        static IContainer BlockSubHeader(IContainer container)
        {
            return container
                .Border(1)
                .Background(Colors.Grey.Lighten4)
                .Padding(5);
        }

        static IContainer BlockCell(IContainer container)
        {
            return container
                .Border(1)
                .Padding(5);
        }
    }
}