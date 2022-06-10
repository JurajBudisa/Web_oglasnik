using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_oglasnik.Models;
using System.IO;
using System.Web.Hosting;
using Web_oglasnik.Misc;

namespace Web_oglasnik.Reports
{
    public class KorisniciReport
    {
        public byte[] Podaci { get; private set; }

        private PdfPCell GenerirajCeliju(string sadrzaj, Font font,  BaseColor boja, bool wrap)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(sadrzaj, font));
            c1.BackgroundColor = boja;
            c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            c1.Padding = 5;
            c1.NoWrap = wrap;
            c1.Border = Rectangle.BOTTOM_BORDER;
            c1.BorderColor = BaseColor.LIGHT_GRAY;
            return c1;
        }

        public void ListaKorisnika(List<Korisnik> Korisnici)
        {
            BaseFont bfontZaglavlje = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
            BaseFont bfontText = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            BaseFont bfontPodnozje = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1250, false);

            Font fontZaglavlje = new Font(bfontZaglavlje, 12, Font.NORMAL, BaseColor.DARK_GRAY);
            Font fontZaglavljeBold = new Font(bfontZaglavlje, 12, Font.BOLD, BaseColor.DARK_GRAY);
            Font fontNaslov = new Font(bfontText, 14, Font.BOLDITALIC, BaseColor.DARK_GRAY);
            Font fontTablicaZaglavlje = new Font(bfontText, 10, Font.BOLD, BaseColor.WHITE);
            Font fontTekst = new Font(bfontText, 10, Font.NORMAL, BaseColor.BLACK);

            BaseColor tPozadinaZaglavlje = new BaseColor(11, 65, 121);
            BaseColor tPozadinaSadrzaj = BaseColor.WHITE;

            using (MemoryStream mstream = new MemoryStream())
            {
                using (Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50))
                {
                    PdfWriter.GetInstance(pdfDokument, mstream).CloseStream = false;

                    pdfDokument.Open();

                    PdfPTable tZaglavlje = new PdfPTable(2);
                    tZaglavlje.HorizontalAlignment = Element.ALIGN_LEFT;
                    tZaglavlje.DefaultCell.Border = Rectangle.NO_BORDER;
                    tZaglavlje.WidthPercentage = 100f;
                    float[] sirinaKolonaZag = new float[] { 1f, 3f };
                    tZaglavlje.SetWidths(sirinaKolonaZag);

                    var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Images/MEV_LOGO.jpg"));

                    logo.Alignment = Element.ALIGN_LEFT;
                    logo.ScaleAbsoluteWidth(50);
                    logo.ScaleAbsoluteHeight(50);

                    PdfPCell clogo = new PdfPCell(logo);
                    clogo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(clogo);

                    Paragraph info = new Paragraph();
                    info.Alignment = Element.ALIGN_RIGHT;

                    info.SetLeading(0, 1.2f);
                    info.Add(new Chunk("MEÐIMURSKO VELEUČILIŠTE U ČAKOVCU \n", fontZaglavljeBold));
                    info.Add(new Chunk("Bana Josipa Jelačića 22a \n" + "Čakovec \n", fontZaglavlje));

                    PdfPCell cInfo = new PdfPCell();
                    cInfo.AddElement(info);
                    cInfo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cInfo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(cInfo);

                    pdfDokument.Add(tZaglavlje);

                    Paragraph pNaslov = new Paragraph("POPIS KORISNIKA", fontNaslov);
                    pNaslov.Alignment = Element.ALIGN_CENTER;
                    pNaslov. SpacingBefore = 20;
                    pNaslov. SpacingAfter = 20;
                    pdfDokument.Add(pNaslov);

                    PdfPTable t = new PdfPTable(5); 
                    t.WidthPercentage = 100;
                    t.SetWidths(new float[] { 1, 2, 3, 2, 2 });
                    t.AddCell(GenerirajCeliju("R.br.", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Korisničko ime", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Email", fontTablicaZaglavlje, tPozadinaZaglavlje, true)); 
                    t.AddCell(GenerirajCeliju("Broj mobitela", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Prezime i ime", fontTablicaZaglavlje, tPozadinaZaglavlje, true));

                    int i = 1;
                    foreach (Korisnik k in Korisnici) {
                        t.AddCell(GenerirajCeliju(i.ToString() + ".", fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(k.KorisnickoIme, fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(k.Email, fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(k.Mobitel, fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(k.PrezimeIme, fontTekst, tPozadinaSadrzaj, false));
                        i++;
                    }

                    pdfDokument.Add(t);

                    pNaslov = new Paragraph("Čakovec, " + System.DateTime.Now.ToString("dd.MM.yyyy"), fontTekst);
                    pNaslov.Alignment = Element.ALIGN_RIGHT;
                    pNaslov.SpacingBefore = 30;
                    pdfDokument.Add(pNaslov);
                }

                Podaci = mstream.ToArray();

                using (var reader = new PdfReader(Podaci))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var stamper = new PdfStamper(reader, ms))
                        {
                            int PageCount = reader.NumberOfPages;
                            for(int i=1; i <= PageCount; i++)
                            {
                                Rectangle pageSize = reader.GetPageSize(i);
                                PdfContentByte canvas = stamper.GetOverContent(i);

                                canvas.BeginText();
                                canvas.SetFontAndSize(bfontPodnozje, 10);

                                canvas.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, $"Stranica {i} / {PageCount}", pageSize.Right - 50, 30, 0);
                                canvas.EndText();
                            }
                        }
                        Podaci = ms.ToArray();

                    }
                }
            }
        }
    }
}