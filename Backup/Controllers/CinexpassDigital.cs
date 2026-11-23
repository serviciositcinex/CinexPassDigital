// Decompiled with JetBrains decompiler
// Type: webApiCinexpassDigital.Controllers.CinexpassDigital
// Assembly: webApi - Cinexpass Digital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6879F7D-F6C7-4F6D-A234-14D332A50BC7
// Assembly location: C:\cinex\restApiCinexpassImagen\app.publish\bin\webApi - Cinexpass Digital.dll

using BarcodeLib;
using dllConnect.ADO_Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using webApiCinexpassDigital.Models;


namespace webApiCinexpassDigital.Controllers
{
    public class CinexpassDigital
    {
        private StringBuilder arreglo = new StringBuilder();

        public IEnumerable<Client> ClientAll()
        {
            DataTable dataTable = new DataTable();
            List<Client> clientList = new List<Client>();
            try
            {
                ADO_V2 adoV2 = new ADO_V2();
                adoV2.SP = "Lee_Vouchers_Clientes";
                adoV2.leer();
                foreach (DataRow row in adoV2.DS.Tables[0].Rows)
                    clientList.Add(new Client()
                    {
                        id = row[0].ToString(),
                        cliente = row[1].ToString()
                    });
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                dataTable = (DataTable) null;
            }
            return (IEnumerable<Client>) clientList;
        }

        public IEnumerable<webApiCinexpassDigital.Models.ClientVouchers> ClientVouchers(string valor1)
        {
            DataTable dataTable = new DataTable();
            List<webApiCinexpassDigital.Models.ClientVouchers> clientVouchersList = new List<webApiCinexpassDigital.Models.ClientVouchers>();
            try
            {
                ADO_V2 adoV2 = new ADO_V2();
                adoV2.SP = "Lee_Vouchers_Clientes_Orden";
                adoV2.AgregarParametro("@pe_clienteId", ADO_V2.enumTipo_de_Datos._varchar, (object) valor1);
                adoV2.leer();
                foreach (DataRow row in adoV2.DS.Tables[0].Rows)
                    clientVouchersList.Add(new webApiCinexpassDigital.Models.ClientVouchers()
                    {
                        Cliente = row[0].ToString(),
                        IDCategoria = row[1].ToString(),
                        Categoria = row[2].ToString(),
                        IDVoucher = row[3].ToString(),
                        Tipo = row[4].ToString(),
                        IDOrden = row[5].ToString(),
                        FchOrden = row[6].ToString(),
                        Referencia = row[7].ToString(),
                        IDOrdenItem = row[8].ToString(),
                        Aprobada = row[9].ToString(),
                        Desde = row[10].ToString(),
                        Hasta = row[11].ToString(),
                        clienteid = row[12].ToString()
                    });
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                dataTable = (DataTable) null;
            }
            return (IEnumerable<webApiCinexpassDigital.Models.ClientVouchers>) clientVouchersList;
        }

        public IEnumerable<webApiCinexpassDigital.Models.ClientVouchersDetails> ClientVouchersDetails(
          string valor1,
          string valor2,
          string valor3,
          string valor4)
        {
            DataTable dataTable = new DataTable();
            List<webApiCinexpassDigital.Models.ClientVouchersDetails> clientVouchersDetailsList = new List<webApiCinexpassDigital.Models.ClientVouchersDetails>();
            try
            {
                ADO_V2 adoV2 = new ADO_V2();
                adoV2.SP = "Lee_Vouchers_Clientes_OrdenDetalle";
                adoV2.AgregarParametro("@pe_ordenId", ADO_V2.enumTipo_de_Datos._varchar, (object) valor1);
                adoV2.AgregarParametro("@pe_tipoVoucher", ADO_V2.enumTipo_de_Datos._varchar, (object) valor2);
                adoV2.AgregarParametro("@pe_voucherDesde", ADO_V2.enumTipo_de_Datos._varchar, (object) valor3);
                adoV2.AgregarParametro("@pe_voucherHasta", ADO_V2.enumTipo_de_Datos._varchar, (object) valor4);
                adoV2.leer();
                foreach (DataRow row in adoV2.DS.Tables[0].Rows)
                    clientVouchersDetailsList.Add(new webApiCinexpassDigital.Models.ClientVouchersDetails()
                    {
                        cod_barra = row[0].ToString(),
                        codigo = row[1].ToString(),
                        nombre = row[2].ToString(),
                        nOrderRef = row[3].ToString(),
                        correlativo = row[4].ToString(),
                        fecha_exp = row[5].ToString(),
                        lVoucherTypeID = row[6].ToString(),
                        id = row[7].ToString()
                    });
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                dataTable = (DataTable) null;
            }
            return (IEnumerable<webApiCinexpassDigital.Models.ClientVouchersDetails>) clientVouchersDetailsList;
        }

        public string ClientVouchersDetails_dt(
          string valor1,
          string valor2,
          string valor3,
          string valor4)
        {
            DataTable dataTable = new DataTable();
            List<webApiCinexpassDigital.Models.ClientVouchersDetails> clientVouchersDetailsList = new List<webApiCinexpassDigital.Models.ClientVouchersDetails>();
            string str = "Proceso finalizado con errores";
            try
            {
                ADO_V2 adoV2 = new ADO_V2();
                adoV2.SP = "Lee_Vouchers_Clientes_OrdenDetalle";
                adoV2.AgregarParametro("@pe_ordenId", ADO_V2.enumTipo_de_Datos._varchar, (object) valor1);
                adoV2.AgregarParametro("@pe_tipoVoucher", ADO_V2.enumTipo_de_Datos._varchar, (object) valor2);
                adoV2.AgregarParametro("@pe_voucherDesde", ADO_V2.enumTipo_de_Datos._varchar, (object) valor3);
                adoV2.AgregarParametro("@pe_voucherHasta", ADO_V2.enumTipo_de_Datos._varchar, (object) valor4);
                adoV2.leer();
                foreach (DataRow row in adoV2.DS.Tables[0].Rows)
                {
                    clientVouchersDetailsList.Add(new webApiCinexpassDigital.Models.ClientVouchersDetails()
                    {
                        cod_barra = row[0].ToString(),
                        codigo = row[1].ToString(),
                        nombre = row[2].ToString(),
                        nOrderRef = row[3].ToString(),
                        correlativo = row[4].ToString(),
                        fecha_exp = row[5].ToString(),
                        lVoucherTypeID = row[6].ToString(),
                        id = row[7].ToString()
                    });
                    this.CreateImage(row[2].ToString(), row[5].ToString(), row[1].ToString(), row[4].ToString(), $"{row[2].ToString()} - {row[1].ToString()}");
                    this.arreglo = this.Log_Codigo(this.arreglo, row[1].ToString());
                    File.WriteAllText($"{Environment.CurrentDirectory}\\img\\{row[2].ToString()} codigos asignados.txt", this.arreglo.ToString());
                }
                str = "Proceso finalizado correctamente";
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                dataTable = (DataTable) null;
            }
            return str;
        }

        private void CreateImage(
          string name,
          string date,
          string codebar,
          string controlnumber,
          string nameArchive)
        {
            PointF point1 = new PointF(120f, 25f);
            PointF point2 = new PointF(120f, 40f);
            PointF point3 = new PointF(210f, 70f);
            PointF point4 = new PointF(100f, 136f);
            string filename = $"{Environment.CurrentDirectory}\\img\\{nameArchive}.png";
            Bitmap backImage = (Bitmap) Image.FromFile(Environment.CurrentDirectory + "\\img\\diamante_back.png");
            using (Graphics graphics = Graphics.FromImage((Image) backImage))
            {
                Font font1 = new Font("Arial", 10f);
                Font font2 = new Font("Arial", 7f);
                Font font3 = new Font("Arial", 10f);
                graphics.DrawString(name, font2, Brushes.Black, point2);
                graphics.DrawString(controlnumber, font1, Brushes.Black, point1);
                graphics.DrawString(date, font1, Brushes.Black, point3);
                graphics.DrawString(codebar, font3, Brushes.Black, point4);
            }
            Image barcode = this.GenerateBarcode(codebar);
            this.OverlayImages((Image) backImage, barcode, 13, 75).Save(filename, ImageFormat.Png);
        }

        private Image GenerateBarcode(string stringbarCode)
        {
            Barcode barcode = new Barcode();
            int Width = 290;
            int Height = 120;
            Color black = Color.Black;
            Color transparent = Color.Transparent;
            string StringToEncode = stringbarCode;
            return barcode.Encode(TYPE.EAN13, StringToEncode, black, transparent, Width, Height);
        }

        private Image OverlayImages(Image backImage, Image topImage, int topPosX = 0, int topPosY = 0)
        {
            if (backImage == null)
                throw new ArgumentNullException(nameof (backImage));
            if (topImage == null)
                throw new ArgumentNullException(nameof (topImage));
            if (topImage.Width > backImage.Width || topImage.Height > backImage.Height)
                throw new ArgumentException("Image bounds are greater than background image.", nameof (topImage));
            topPosX += Convert.ToInt32(backImage.Width / 2 - topImage.Width / 2);
            topPosY += Convert.ToInt32(backImage.Height / 2 - topImage.Height / 2);
            Bitmap bitmap = new Bitmap(backImage.Width, backImage.Height);
            using (Graphics graphics = Graphics.FromImage((Image) bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(backImage, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                graphics.DrawImage(topImage, new Rectangle(topPosX, topPosY, 250, 20), new Rectangle(0, 0, topImage.Width, topImage.Height), GraphicsUnit.Pixel);
                graphics.Save();
                return (Image) bitmap;
            }
        }

        private StringBuilder Log_Codigo(StringBuilder arreglo, string texto)
        {
            StringBuilder stringBuilder = arreglo;
            stringBuilder.Append(texto + Environment.NewLine);
            return stringBuilder;
        }
    }
}
