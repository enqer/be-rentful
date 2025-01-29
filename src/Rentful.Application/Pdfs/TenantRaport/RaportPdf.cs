using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Rentful.Application.Pdfs.TenantRaport.Dtos;

namespace Rentful.Application.Pdfs.TenantRaport
{
    public class RaportPdf(TenantRaportDto raport) : IDocument
    {
        [Obsolete]
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(10, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(
                        TextStyle.Default
                            .FontSize(16)
                    );
                    page
                    .Content()
                    .Element(ComposeContent);
                });
        }

        [Obsolete]
        private void ComposeContent(IContainer container)
        {
            container.Grid(grid =>
            {
                grid.Columns(1);

                var containerTop = grid.Item(1).MinimalBox();
                containerTop
                    .MinWidth(4f, Unit.Centimetre)
                    .Grid(GridTop);

                var containerMiddle = grid.Item().MinimalBox();
                containerMiddle
                    .MinWidth(4f, Unit.Centimetre)
                    .MinHeight(5f, Unit.Centimetre)
                    .AlignMiddle()
                    .Grid(GridMiddle);
                var containerBottom = grid.Item(1).MinimalBox();
                containerBottom
                    .MinWidth(4f, Unit.Centimetre)
                    .AlignBottom()
                    .PaddingTop(15)
                    .MinHeight(2f, Unit.Centimetre)
                    .Grid(GridBottom);

            });
        }

        [Obsolete]
        private void GridTop(GridDescriptor grid)
        {
            grid.Columns(12);
            grid.Item(7).Grid(GridHeader);
            grid.Item(5).AlignRight().Grid(GridQr);
        }

        private void GridHeader(GridDescriptor grid)
        {
            grid.Columns(2);
            grid.Item(2)
                .Text(x =>
                {
                    x.Span("Lokator").FontSize(16).Bold().FontColor(Colors.Blue.Medium);
                });
            GetHeaderRow(grid, "Imię i nazwisko:", $"{raport.FirstName} {raport.LastName}");
            GetHeaderRow(grid, "Email:", raport.Email);
            GetHeaderRow(grid, "Numer telefonu:", raport.PhoneNumber);

            grid.Item(2)
                .Text(x =>
                {
                    x.Span("").FontSize(4).Bold();
                });
            grid.Item(2)
                .Text(x =>
                {
                    x.Span("Adres").FontSize(16).Bold().FontColor(Colors.Blue.Medium);
                });
            GetHeaderRow(grid, "Kod pocztowy:", raport.PostalCode);
            GetHeaderRow(grid, "Ulica:", $"{raport.Street} {raport.BuildingNumber}");
            GetHeaderRow(grid, "Miejscowość:", raport.City);
            GetHeaderRow(grid, "Kraj:", raport.Country);
        }
        private void GetHeaderRow(GridDescriptor grid, string label, string value)
        {
            grid.Item(1)
               .Text(x =>
               {
                   x.Span(label).FontSize(10).Bold();
               });
            grid.Item(1)
                .Text(x =>
                {
                    x.Span(value).FontSize(12).Bold().LineHeight(0.8f);
                });
        }


        private void GridQr(GridDescriptor grid)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{raport.QrCode}", QRCodeGenerator.ECCLevel.H);
            var qrCodeBitmap = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCodeBitmap.GetGraphic(20, false);
            grid.Columns(5);
            grid.Item(3).Image(qrCodeAsBitmapByteArr);
        }

        [Obsolete]
        private void GridLeaseAgreementDetails(GridDescriptor grid)
        {
            grid.Columns(1);
            grid.Item(1)
            .Text(x =>
            {
                x.Span("Dane umowy").FontSize(16).Bold().LineHeight(0.8f).FontColor(Colors.Blue.Medium);
            });
            grid.Item(1)
            .Text(x =>
            {
                x.Span("").FontSize(10).Bold().LineHeight(0.8f);
            });
            grid.Item(1)
                .Grid(GridAgreement);

        }

        [Obsolete]
        private void GridLeaseAgreementPayments(GridDescriptor grid)
        {
            grid.Columns(1);
            grid.Item(1)
                .PaddingVertical(10)
                .Text(x =>
                {
                    x.Span("Płatności").FontSize(16).Bold().LineHeight(0.8f).FontColor(Colors.Blue.Medium);
                });
            grid.Item(1)
                .Container()
                .Border(1)
                .PaddingBottom(15)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    table.Cell().Border(1).AlignCenter().Text("Kwota").FontSize(13).Bold();
                    table.Cell().Border(1).AlignCenter().Text("Data").FontSize(13).Bold();
                    raport.Payments.ForEach(x =>
                    {
                        table.Cell().Border(1).AlignCenter().Text(x.Amount).FontSize(10);
                        table.Cell().Border(1).AlignCenter().Text(x.Date).FontSize(10);
                    });
                });

        }

        [Obsolete]
        private void GridLeaseAgreementReports(GridDescriptor grid)
        {
            grid.Columns(1);
            grid.Item(1)
                .PaddingVertical(10)
                .Text(x =>
                {
                    x.Span("Zgłoszenia").FontSize(16).Bold().LineHeight(0.8f).FontColor(Colors.Blue.Medium);
                });
            grid.Item(1)
                .Container()
                .Border(1)
                .PaddingBottom(15)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    table.Cell().Border(1).AlignCenter().Text("Kwota").FontSize(13).Bold();
                    table.Cell().Border(1).AlignCenter().Text("Typ").FontSize(13).Bold();
                    table.Cell().Border(1).AlignCenter().Text("Opis").FontSize(13).Bold();
                    table.Cell().Border(1).AlignCenter().Text("Status").FontSize(13).Bold();
                    raport.Reports.ForEach(x =>
                    {
                        table.Cell().Border(1).AlignCenter().Text(x.Date).FontSize(10);
                        table.Cell().Border(1).AlignCenter().Text(x.Type).FontSize(10);
                        table.Cell().Border(1).AlignCenter().Text(x.Description).FontSize(10);
                        table.Cell().Border(1).AlignCenter().Text(x.Status).FontSize(10);
                    });
                });

        }

        [Obsolete]
        private void GridAgreement(GridDescriptor grid)
        {
            grid.Columns(4);
            grid.Item(1)
                .Grid(GridAgreementPrice);
            grid.Item(1)
                .Grid(GridAgreementRent);
            grid.Item(1)
                .Grid(GridAgreementDeposit);
            grid.Item(1)
                .Grid(GridAgreementBalance);

        }

        private void GridAgreementPrice(GridDescriptor grid)
        {
            grid.Columns(1);
            GridAgreementInfo(grid, "Cena [zł]", raport.Price.ToString());
        }
        private void GridAgreementRent(GridDescriptor grid)
        {
            grid.Columns(1);
            GridAgreementInfo(grid, "Czynsz [zł]", raport.Rent.ToString());
        }
        private void GridAgreementDeposit(GridDescriptor grid)
        {
            grid.Columns(1);
            GridAgreementInfo(grid, "Kaucja [zł]", raport.Deposit.ToString());
        }

        private void GridAgreementBalance(GridDescriptor grid)
        {
            grid.Columns(1);
            GridAgreementInfo(grid, "Wpłacone [zł]", raport.Balance.ToString());
        }

        [Obsolete]
        private void GridBottom(GridDescriptor grid)
        {
            grid.Columns(2);
            grid.Item(2).Container()
                .LineHorizontal(1).LineColor(Colors.Grey.Medium);
            grid.Columns(2);
            grid.Item(1)
                .Container()
                .AlignMiddle()
                .Text(x =>
                {
                    x.Span("Rentful").FontSize(18).Bold().FontColor(Colors.Blue.Medium);
                });
            grid.Item(1).AlignRight().Grid(GridDate);
        }


        private void GridAgreementInfo(GridDescriptor grid, string label, string value)
        {
            grid.Item(1)
                .Container()
                .AlignCenter()
                .AlignMiddle()
                .Text(x =>
                {
                    x.Span(value).FontSize(16).Bold();
                });
            grid.Item(1)
                .AlignCenter()
                .AlignMiddle()
                .Text(x =>
                {
                    x.Span(label).FontSize(12).FontColor(Colors.Grey.Medium); ;
                });
        }

        private void GridDate(GridDescriptor grid)
        {
            grid.Columns(1);
            grid.Item(1)
                .AlignRight()
                .Text(x =>
                {
                    x.Span(raport.PrintDate).FontSize(12).Bold().FontColor(Colors.Blue.Medium);
                });
            grid.Item(1)
                .AlignRight()
                .Text(x =>
                {
                    x.Span("Data wygenerowania").FontSize(10).FontColor(Colors.Grey.Medium); ;
                });

        }

        [Obsolete]
        private void GridMiddle(GridDescriptor grid)
        {
            GridLeaseAgreementDetails(grid);
            GridLeaseAgreementPayments(grid);
            GridLeaseAgreementReports(grid);
        }


        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }
    }
}
