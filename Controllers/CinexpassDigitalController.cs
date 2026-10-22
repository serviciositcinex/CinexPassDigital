// Decompiled with JetBrains decompiler
// Type: webApiCinexpassDigital.Controllers.CinexpassDigitalController
// Assembly: webApi - Cinexpass Digital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6879F7D-F6C7-4F6D-A234-14D332A50BC7
// Assembly location: C:\cinex\restApiCinexpassImagen\app.publish\bin\webApi - Cinexpass Digital.dll

using BarcodeLib;
using dllConnect.ADO_Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Http;
using webApiCinexpassDigital.Models;


namespace webApiCinexpassDigital.Controllers
{

public class CinexpassDigitalController : ApiController
{
  private StringBuilder arreglo = new StringBuilder();
  private List<webApiCinexpassDigital.Models.Coordenadas> _Coordenadas = new List<webApiCinexpassDigital.Models.Coordenadas>();
  private Document doc = new Document(PageSize.LETTER, 0.0f, 0.0f, 0.0f, 0.0f);
  private string vRuta = "";
  private string vNombre = "";

  public CinexpassDigitalController()
  {
    Conexion.connectString = ConfigurationManager.AppSettings["OFC"];
  }

  [HttpGet]
  public string Version() => "V 1.2";

  [HttpGet]
  public IEnumerable<Client> AllClient()
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

  [HttpGet]
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

  [HttpGet]
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
          nombre = row[0].ToString(),
          nOrderRef = row[1].ToString(),
          correlativo = row[2].ToString(),
          codigo = row[3].ToString(),
          fecha_exp = row[4].ToString(),
          lVoucherTypeID = row[5].ToString(),
          cod_barra = row[6].ToString(),
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

  [HttpGet]
  public IEnumerable<Respuestas> ClientVouchersDetails_Image(
    string valor1,
    string valor2,
    string valor3,
    string valor4,
    string valor5)
  {
    DataTable dataTable = new DataTable();
    List<webApiCinexpassDigital.Models.ClientVouchersDetails> clientVouchersDetailsList = new List<webApiCinexpassDigital.Models.ClientVouchersDetails>();
    string str = "Proceso finalizado con errores";
    List<Respuestas> respuestasList = new List<Respuestas>();
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
          cod_barra = row["codigo"].ToString(),
          codigo = row["codigo"].ToString(),
          nombre = row["nombre"].ToString(),
          nOrderRef = row["nOrderRef"].ToString(),
          correlativo = row["correlativo"].ToString(),
          fecha_exp = row["fecha_exp"].ToString(),
          lVoucherTypeID = row["lVoucherTypeID"].ToString(),
          id = row["lVoucherNumber"].ToString()
        });
        this.CreateImage(row["nombre"].ToString(), row["fecha_exp"].ToString(), row["codigo"].ToString(), row["correlativo"].ToString(), row["nOrderRef"].ToString(), $"{row["nOrderRef"].ToString()} - {row["correlativo"].ToString()}", row["lVoucherTypeID"].ToString(), valor5);
      }
      respuestasList.Add(new Respuestas()
      {
        ruta = this.vRuta,
        nombre_carpeta = this.vNombre,
        procesado = true
      });
    }
    catch (Exception ex)
    {
      string message = ex.Message;
      str = ex.StackTrace;
      this.log(ex.StackTrace);
      respuestasList.Add(new Respuestas()
      {
        ruta = (string) null,
        nombre_carpeta = (string) null,
        procesado = false
      });
      dataTable = (DataTable) null;
    }
    return (IEnumerable<Respuestas>) respuestasList;
  }

  private void CreateImage(
    string name,
    string date,
    string codebar,
    string controlnumber,
    string orderId,
    string nameArchive)
  {
    PointF point1 = new PointF(120f, 25f);
    PointF point2 = new PointF(120f, 40f);
    PointF point3 = new PointF(210f, 70f);
    PointF point4 = new PointF(100f, 136f);
    string pathOutput = $"{ConfigurationManager.AppSettings["ruta_destino"]}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}";
    this.CreateDirectoryOutput(pathOutput);
    string filename1 = ConfigurationManager.AppSettings["ruta_plantilla"] + "\\diamante.png";
    string filename2 = $"{pathOutput}/{nameArchive}.png";
    Bitmap backImage = new Bitmap(System.Drawing.Image.FromFile(filename1), new Size(304, 199));
    using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) backImage))
    {
      System.Drawing.Font font1 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font2 = new System.Drawing.Font("Arial", 7f);
      System.Drawing.Font font3 = new System.Drawing.Font("Arial", 10f);
      graphics.DrawString(name, font2, Brushes.Black, point2);
      graphics.DrawString(controlnumber, font1, Brushes.Black, point1);
      graphics.DrawString(date, font1, Brushes.Black, point3);
      graphics.DrawString(codebar, font3, Brushes.Black, point4);
    }
    System.Drawing.Image barcode = this.GenerateBarcode(codebar);
    System.Drawing.Image image = this.OverlayImages((System.Drawing.Image) backImage, barcode, 13, 75);
    this.arreglo = this.Log_Codigo(this.arreglo, codebar);
    File.WriteAllText($"{pathOutput}\\{name} codigos asignados.txt", this.arreglo.ToString());
    image.Save(filename2, ImageFormat.Png);
  }

  private void CreateImage(
    string name,
    string date,
    string codebar,
    string controlnumber,
    string orderId,
    string nameArchive,
    string tipoCinexpass,
    string directorio)
  {
    try
    {
      PointF? nullable1 = new PointF?();
      PointF? nullable2 = new PointF?();
      PointF? nullable3 = new PointF?();
      PointF? nullable4 = new PointF?();
      string str1;
      if (tipoCinexpass != null && tipoCinexpass.Length == 3)
      {
        switch (tipoCinexpass[1])
        {
          case '0':
            if (tipoCinexpass == "206")
            {
              nullable1 = new PointF?(new PointF(170f, 53f));
              nullable2 = new PointF?(new PointF(170f, 65f));
              nullable3 = new PointF?(new PointF(270f, 112f));
              nullable4 = new PointF?(new PointF(110f, 192f));
              str1 = "diamante";
              goto label_34;
            }
            break;
          case '1':
            switch (tipoCinexpass)
            {
              case "214":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "combo";
                goto label_34;
              case "212":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "verde";
                goto label_34;
              case "210":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "naranja";
                goto label_34;
              case "216":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "naranja";
                goto label_34;
              case "213":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "4DX";
                goto label_34;
            }
            break;
          case '2':
            switch (tipoCinexpass)
            {
              case "222":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "estudiante";
                goto label_34;
              case "226":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "3D";
                goto label_34;
              case "227":
                nullable1 = new PointF?(new PointF(180f, 53f));
                nullable2 = new PointF?(new PointF(180f, 65f));
                nullable3 = new PointF?(new PointF(272f, 109f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "ComboDuoVentas";
                goto label_34;
            }
            break;
          case '3':
            switch (tipoCinexpass)
            {
              case "235":
                nullable1 = new PointF?(new PointF(170f, 75f));
                nullable2 = new PointF?(new PointF(170f, 88f));
                nullable3 = new PointF?(new PointF(270f, 125f));
                nullable4 = new PointF?(new PointF(110f, 202f));
                str1 = "cotillon";
                goto label_34;
              case "238":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 110f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "Vacaciones";
                goto label_34;
              case "239":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 110f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "Vip";
                goto label_34;
              case "237":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 110f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "4DX_COMBO";
                goto label_34;
            }
            break;
          case '4':
            if (tipoCinexpass == "249")
            {
              nullable1 = new PointF?(new PointF(170f, 53f));
              nullable2 = new PointF?(new PointF(170f, 65f));
              nullable3 = new PointF?(new PointF(270f, 112f));
              nullable4 = new PointF?(new PointF(110f, 192f));
              str1 = "naranja";
              goto label_34;
            }
            break;
          case '6':
            switch (tipoCinexpass)
            {
              case "265":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "COCOSETTE";
                goto label_34;
              case "267":
                nullable1 = new PointF?(new PointF(180f, 53f));
                nullable2 = new PointF?(new PointF(180f, 65f));
                nullable3 = new PointF?(new PointF(272f, 109f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "ComboPopTequeMercadeo";
                goto label_34;
              case "268":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "naranja";
                goto label_34;
            }
            break;
          case '7':
            if (tipoCinexpass == "278")
            {
              nullable1 = new PointF?(new PointF(170f, 38f));
              nullable2 = new PointF?(new PointF(170f, 52f));
              nullable3 = new PointF?(new PointF(290f, 110f));
              nullable4 = new PointF?(new PointF(120f, 205f));
              str1 = "ComboPOPMercadeo";
              goto label_34;
            }
            break;
          case '8':
            switch (tipoCinexpass)
            {
              case "280":
                nullable1 = new PointF?(new PointF(170f, 38f));
                nullable2 = new PointF?(new PointF(170f, 52f));
                nullable3 = new PointF?(new PointF(290f, 110f));
                nullable4 = new PointF?(new PointF(110f, 205f));
                str1 = "ComboDuoMercadeo";
                goto label_34;
              case "282":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "naranja";
                goto label_34;
              case "288":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "COCOSETTE";
                goto label_34;
              case "284":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 112f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "4DX";
                goto label_34;
              case "286":
                nullable1 = new PointF?(new PointF(170f, 53f));
                nullable2 = new PointF?(new PointF(170f, 65f));
                nullable3 = new PointF?(new PointF(270f, 110f));
                nullable4 = new PointF?(new PointF(110f, 192f));
                str1 = "Vip";
                goto label_34;
            }
            break;
        }
      }
      nullable1 = new PointF?(new PointF(170f, 53f));
      nullable2 = new PointF?(new PointF(170f, 65f));
      nullable3 = new PointF?(new PointF(270f, 112f));
      nullable4 = new PointF?(new PointF(110f, 192f));
      str1 = "naranja";
label_34:
      string pathOutput = $"{ConfigurationManager.AppSettings["ruta_destino"]}\\{directorio}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}\\{str1}";
      string str2 = $"{ConfigurationManager.AppSettings["ruta_destino"]}\\{directorio}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}";
      this.log("pathOutput -> " + str2);
      this.CreateDirectoryOutput(pathOutput);
      string filename1 = $"{ConfigurationManager.AppSettings["ruta_plantilla"]}\\{str1}.png";
      string filename2 = $"{pathOutput}/{nameArchive}.png";
      Bitmap backImage = new Bitmap(System.Drawing.Image.FromFile(filename1), new Size(411, 295));
      using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) backImage))
      {
        System.Drawing.Font font1 = new System.Drawing.Font("Arial", 10f);
        System.Drawing.Font font2 = new System.Drawing.Font("Arial", 12f);
        System.Drawing.Font font3 = new System.Drawing.Font("Arial", 10f);
        System.Drawing.Font font4 = new System.Drawing.Font("Arial", 15f);
        if (name.Length > 25)
          name = name.Substring(0, 25);
        graphics.DrawString(name.Length < 25 ? name : name.Substring(0, 25), font3, Brushes.Black, nullable2.Value);
        graphics.DrawString(controlnumber, font1, Brushes.Black, nullable1.Value);
        graphics.DrawString(date, font2, Brushes.Black, nullable3.Value);
        graphics.DrawString(codebar, font4, Brushes.Black, nullable4.Value);
      }
      System.Drawing.Image barcode = this.GenerateBarcode(codebar);
      System.Drawing.Image image;
      switch (tipoCinexpass)
      {
        case "235":
          image = this.OverlayImages((System.Drawing.Image) backImage, barcode, -5, 90);
          break;
        case "227":
          image = this.OverlayImages((System.Drawing.Image) backImage, barcode, -5, 80 /*0x50*/);
          break;
        default:
          image = this.OverlayImages((System.Drawing.Image) backImage, barcode, -5, 80 /*0x50*/);
          break;
      }
      this.arreglo = this.Log_Codigo(this.arreglo, codebar);
      File.WriteAllText($"{pathOutput}\\{name} {str1} codigos asignados.txt", this.arreglo.ToString());
      this.vRuta = str2;
      this.vNombre = $"{directorio}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}";
      image.Save(filename2, ImageFormat.Png);
    }
    catch (Exception ex)
    {
      this.log(ex.StackTrace);
    }
  }

  private System.Drawing.Image GenerateBarcode(string stringbarCode)
  {
    BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
    int Width = 290;
    int Height = 120;
    Color black = Color.Black;
    Color transparent = Color.Transparent;
    string StringToEncode = stringbarCode;
    return barcode.Encode(TYPE.CODE128, StringToEncode, black, transparent, Width, Height);
  }

  private System.Drawing.Image OverlayImages(
    System.Drawing.Image backImage,
    System.Drawing.Image topImage,
    int topPosX = 0,
    int topPosY = 0)
  {
    if (backImage == null)
      throw new ArgumentNullException(nameof (backImage));
    if (topImage == null)
      throw new ArgumentNullException(nameof (topImage));
    if (topImage.Width > backImage.Width || topImage.Height > backImage.Height)
      throw new ArgumentException("Los límites de la imagen son mayores que la imagen de fondo.", nameof (topImage));
    topPosX += Convert.ToInt32(backImage.Width / 2 - topImage.Width / 2);
    topPosY += Convert.ToInt32(backImage.Height / 2 - topImage.Height / 2);
    Bitmap bitmap = new Bitmap(backImage.Width, backImage.Height);
    using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap))
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(backImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
      graphics.DrawImage(topImage, new System.Drawing.Rectangle(topPosX, topPosY, 300, 25), new System.Drawing.Rectangle(0, 0, topImage.Width, topImage.Height), GraphicsUnit.Pixel);
      graphics.Save();
      return (System.Drawing.Image) bitmap;
    }
  }

  private System.Drawing.Image OverlayImages1(
    System.Drawing.Image backImage,
    System.Drawing.Image topImage,
    int topPosX = 0,
    int topPosY = 0)
  {
    if (backImage == null)
      throw new ArgumentNullException(nameof (backImage));
    if (topImage == null)
      throw new ArgumentNullException(nameof (topImage));
    if (topImage.Width > backImage.Width || topImage.Height > backImage.Height)
      throw new ArgumentException("Los límites de la imagen son mayores que la imagen de fondo.", nameof (topImage));
    topPosX += Convert.ToInt32(backImage.Width / 2 - topImage.Width / 2);
    topPosY += Convert.ToInt32(backImage.Height / 2 - topImage.Height / 2);
    Bitmap bitmap = new Bitmap(backImage.Width, backImage.Height);
    using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap))
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(backImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
      graphics.DrawImage(topImage, new System.Drawing.Rectangle(topPosX, topPosY, 300, 25), new System.Drawing.Rectangle(0, 0, topImage.Width, topImage.Height), GraphicsUnit.Pixel);
      graphics.Save();
      return (System.Drawing.Image) bitmap;
    }
  }

  private StringBuilder Log_Codigo(StringBuilder arreglo, string texto)
  {
    StringBuilder stringBuilder = arreglo;
    stringBuilder.Append(texto + Environment.NewLine);
    return stringBuilder;
  }

  private void CreateDirectoryOutput(string pathOutput)
  {
    try
    {
      if (Directory.Exists(pathOutput))
        return;
      Directory.CreateDirectory(pathOutput);
    }
    catch (Exception ex)
    {
      this.log("CreateDirectoryOutput -> " + ex.StackTrace);
    }
  }

  public byte[] ImageToByteArray(System.Drawing.Image imageIn)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      imageIn.Save((Stream) memoryStream, imageIn.RawFormat);
      return memoryStream.ToArray();
    }
  }

  private static List<webApiCinexpassDigital.Models.Coordenadas> Coordenadas()
  {
    return new List<webApiCinexpassDigital.Models.Coordenadas>()
    {
      new webApiCinexpassDigital.Models.Coordenadas()
      {
        tipo = "diamante",
        x = 1,
        y = 1,
        pos_x = 280,
        pos_y = 250
      },
      new webApiCinexpassDigital.Models.Coordenadas()
      {
        tipo = "naranja",
        x = 1,
        y = 1,
        pos_x = 40,
        pos_y = 40
      },
      new webApiCinexpassDigital.Models.Coordenadas()
      {
        tipo = "combopop",
        x = 1,
        y = 1,
        pos_x = 40,
        pos_y = 40
      }
    };
  }

  public static webApiCinexpassDigital.Models.Coordenadas LeerCoordenas(string tipoBoleto)
  {
    return CinexpassDigitalController.Coordenadas().Find((Predicate<webApiCinexpassDigital.Models.Coordenadas>) (p => p.tipo == tipoBoleto));
  }

  [HttpGet]
  public string ClientVouchersDetails_PDF(
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
      DataTable table = adoV2.DS.Tables[0];
      int num1 = 3;
      int num2 = 1;
      int num3 = 0;
      Bitmap bitmap = new Bitmap(888, 1155);
      webApiCinexpassDigital.Models.Coordenadas coordenadas = CinexpassDigitalController.LeerCoordenas("diamante");
      System.Drawing.Image image = (System.Drawing.Image) null;
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Jpeg);
      Bitmap backImage = (Bitmap) System.Drawing.Image.FromStream((Stream) memoryStream);
      System.Drawing.Font font1 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font2 = new System.Drawing.Font("Arial", 7f);
      System.Drawing.Font font3 = new System.Drawing.Font("Arial", 10f);
      Graphics graphics = Graphics.FromImage((System.Drawing.Image) backImage);
      graphics.Clear(Color.LightGray);
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        PointF point1 = new PointF((float) (120 + num3), 15f);
        PointF point2 = new PointF((float) (120 + num3), 30f);
        PointF point3 = new PointF((float) (200 + num3), 65f);
        PointF point4 = new PointF((float) (90 + num3), 132f);
        graphics.DrawString(row["nombre"].ToString(), font2, Brushes.Black, point2);
        graphics.DrawString(row["correlativo"].ToString(), font1, Brushes.Black, point1);
        graphics.DrawString(row["fecha_exp"].ToString(), font1, Brushes.Black, point3);
        graphics.DrawString(row["codigo"].ToString(), font3, Brushes.Black, point4);
        System.Drawing.Image barcode = this.GenerateBarcode(row["codigo"].ToString());
        image = this.OverlayImages((System.Drawing.Image) backImage, barcode, num3 - 277, -410);
        backImage = (Bitmap) image;
        string filename = $"F:\\temp\\pdf\\prueba_Pdf_{num2.ToString()}.png";
        backImage.Save(filename, ImageFormat.Png);
        ++num2;
        num3 += coordenadas.pos_x;
        if (num2 > num1)
        {
          num2 = 1;
          num3 = 0;
        }
        graphics = Graphics.FromImage((System.Drawing.Image) backImage);
      }
      string filename1 = "F:\\temp\\pdf\\prueba_Pdf.png";
      image.Save(filename1, ImageFormat.Png);
      str = "Proceso finalizado correctamente";
    }
    catch (Exception ex)
    {
      string message = ex.Message;
      dataTable = (DataTable) null;
    }
    return str;
  }

  public static byte[] ImageToByte(System.Drawing.Image img)
  {
    return (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));
  }

  [HttpGet]
  public string ClientVouchersDetails_Pdf1(
    string valor1,
    string valor2,
    string valor3,
    string valor4)
  {
    DataTable dataTable = new DataTable();
    List<webApiCinexpassDigital.Models.ClientVouchersDetails> clientVouchersDetailsList = new List<webApiCinexpassDigital.Models.ClientVouchersDetails>();
    string str = "Proceso finalizado con errores";
    bool cerrarArchivo = false;
    int num1 = 0;
    int x = 0;
    int y = 1;
    int num2 = 3;
    int num3 = 6;
    Document pdf = new Document();
    try
    {
      ADO_V2 adoV2 = new ADO_V2();
      adoV2.SP = "Lee_Vouchers_Clientes_OrdenDetalle";
      adoV2.AgregarParametro("@pe_ordenId", ADO_V2.enumTipo_de_Datos._varchar, (object) valor1);
      adoV2.AgregarParametro("@pe_tipoVoucher", ADO_V2.enumTipo_de_Datos._varchar, (object) valor2);
      adoV2.AgregarParametro("@pe_voucherDesde", ADO_V2.enumTipo_de_Datos._varchar, (object) valor3);
      adoV2.AgregarParametro("@pe_voucherHasta", ADO_V2.enumTipo_de_Datos._varchar, (object) valor4);
      adoV2.leer();
      DataTable table = adoV2.DS.Tables[0];
      int count = table.Rows.Count;
      foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
      {
        clientVouchersDetailsList.Add(new webApiCinexpassDigital.Models.ClientVouchersDetails()
        {
          cod_barra = row["codigo"].ToString(),
          codigo = row["codigo"].ToString(),
          nombre = row["nombre"].ToString(),
          nOrderRef = row["nOrderRef"].ToString(),
          correlativo = row["correlativo"].ToString(),
          fecha_exp = row["fecha_exp"].ToString(),
          lVoucherTypeID = row["lVoucherTypeID"].ToString(),
          id = row["lVoucherNumber"].ToString()
        });
        ++num1;
        if (num1 == count)
          cerrarArchivo = true;
        if (x < num2)
        {
          ++x;
        }
        else
        {
          x = 1;
          if (y == num3)
            y = 1;
          else
            ++y;
        }
        pdf = this.CreatePDF(row["nombre"].ToString(), row["fecha_exp"].ToString(), row["codigo"].ToString(), row["correlativo"].ToString(), row["nOrderRef"].ToString(), $"{row["nOrderRef"].ToString()} - {row["correlativo"].ToString()}", row["lVoucherTypeID"].ToString(), cerrarArchivo, pdf, x, y);
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

  private void CreatePDF(
    string name,
    string date,
    string codebar,
    string controlnumber,
    string orderId,
    string nameArchive,
    string tipoCinexpass)
  {
    PointF? nullable1 = new PointF?();
    PointF? nullable2 = new PointF?();
    PointF? nullable3 = new PointF?();
    PointF? nullable4 = new PointF?();
    string str1 = "";
    switch (tipoCinexpass)
    {
      case "206":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "diamante";
        break;
      case "214":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "combo";
        break;
      case "212":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "verde";
        break;
      case "210":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "naranja";
        break;
    }
    string pathOutput = $"{ConfigurationManager.AppSettings["ruta_destino"]}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}";
    this.CreateDirectoryOutput(pathOutput);
    string filename = $"{ConfigurationManager.AppSettings["ruta_plantilla"]}\\{str1}.png";
    string str2 = $"{pathOutput}/{nameArchive}.png";
    Bitmap backImage = new Bitmap(System.Drawing.Image.FromFile(filename), new Size(411, 295));
    using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) backImage))
    {
      System.Drawing.Font font1 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font2 = new System.Drawing.Font("Arial", 12f);
      System.Drawing.Font font3 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font4 = new System.Drawing.Font("Arial", 15f);
      if (name.Length > 25)
        name = name.Substring(0, 25);
      graphics.DrawString(name.Length < 25 ? name : name.Substring(0, 25), font3, Brushes.Black, nullable2.Value);
      graphics.DrawString(controlnumber, font1, Brushes.Black, nullable1.Value);
      graphics.DrawString(date, font2, Brushes.Black, nullable3.Value);
      graphics.DrawString(codebar, font4, Brushes.Black, nullable4.Value);
    }
    System.Drawing.Image barcode = this.GenerateBarcode(codebar);
    System.Drawing.Image image = this.OverlayImages((System.Drawing.Image) backImage, barcode, -5, 80 /*0x50*/);
    this.arreglo = this.Log_Codigo(this.arreglo, codebar);
    File.WriteAllText($"{pathOutput}\\{name} {str1} codigos asignados.txt", this.arreglo.ToString());
    Document document = new Document(PageSize.LETTER);
    string path = $"{pathOutput}\\{name} {str1}.pdf";
    PdfWriter.GetInstance(document, (Stream) new FileStream(path, FileMode.Create));
    document.Open();
    iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(image, ImageFormat.Png);
    instance.BorderWidth = 0.0f;
    instance.Alignment = 2;
    float num = 150f / instance.Width;
    instance.ScalePercent(num * 100f);
    document.Add((IElement) instance);
    document.Close();
  }

  private Document CreatePDF(
    string name,
    string date,
    string codebar,
    string controlnumber,
    string orderId,
    string nameArchive,
    string tipoCinexpass,
    bool cerrarArchivo,
    Document pdf,
    int x,
    int y)
  {
    PointF? nullable1 = new PointF?();
    PointF? nullable2 = new PointF?();
    PointF? nullable3 = new PointF?();
    PointF? nullable4 = new PointF?();
    string str1 = "";
    int absoluteX = 5;
    int absoluteY = 0;
    switch (tipoCinexpass)
    {
      case "206":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "diamante";
        break;
      case "214":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "combo";
        break;
      case "212":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "verde";
        break;
      case "210":
        nullable1 = new PointF?(new PointF(170f, 53f));
        nullable2 = new PointF?(new PointF(170f, 65f));
        nullable3 = new PointF?(new PointF(270f, 112f));
        nullable4 = new PointF?(new PointF(110f, 192f));
        str1 = "naranja";
        break;
    }
    string pathOutput = $"{ConfigurationManager.AppSettings["ruta_destino"]}\\{DateTime.Now.ToShortDateString().Replace("/", "")}_{name}_{orderId}";
    this.CreateDirectoryOutput(pathOutput);
    string filename = $"{ConfigurationManager.AppSettings["ruta_plantilla"]}\\{str1}.png";
    string str2 = $"{pathOutput}/{nameArchive}.png";
    Bitmap backImage = new Bitmap(System.Drawing.Image.FromFile(filename), new Size(800, 600));
    using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) backImage))
    {
      System.Drawing.Font font1 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font2 = new System.Drawing.Font("Arial", 12f);
      System.Drawing.Font font3 = new System.Drawing.Font("Arial", 10f);
      System.Drawing.Font font4 = new System.Drawing.Font("Arial", 15f);
      if (name.Length > 25)
        name = name.Substring(0, 25);
      graphics.DrawString(name.Length < 25 ? name : name.Substring(0, 25), font3, Brushes.Black, nullable2.Value);
      graphics.DrawString(controlnumber, font1, Brushes.Black, nullable1.Value);
      graphics.DrawString(date, font2, Brushes.Black, nullable3.Value);
      graphics.DrawString(codebar, font4, Brushes.Black, nullable4.Value);
    }
    System.Drawing.Image barcode = this.GenerateBarcode(codebar);
    System.Drawing.Image image = this.OverlayImages1((System.Drawing.Image) backImage, barcode, -5, 80 /*0x50*/);
    this.arreglo = this.Log_Codigo(this.arreglo, codebar);
    File.WriteAllText($"{pathOutput}\\{name} {str1} codigos asignados.txt", this.arreglo.ToString());
    if (!this.doc.IsOpen())
    {
      File.Delete($"{pathOutput}\\{name} {str1}.pdf");
      PdfWriter.GetInstance(this.doc, (Stream) new FileStream($"{pathOutput}\\{name} {str1}.pdf", FileMode.OpenOrCreate));
      this.doc.Open();
    }
    iTextSharp.text.Image instance = iTextSharp.text.Image.GetInstance(image, ImageFormat.Png);
    instance.BorderWidth = 0.0f;
    switch (x)
    {
      case 1:
        absoluteX = 5;
        break;
      case 2:
        absoluteX = 160 /*0xA0*/;
        break;
      case 3:
        absoluteX = 315;
        break;
    }
    switch (y)
    {
      case 1:
        absoluteY = 680;
        break;
      case 2:
        absoluteY = 570;
        break;
      case 3:
        absoluteY = 460;
        break;
      case 4:
        absoluteY = 350;
        break;
      case 5:
        absoluteY = 240 /*0xF0*/;
        break;
      case 6:
        absoluteY = 130;
        break;
      case 7:
        absoluteY = 20;
        break;
      case 8:
        absoluteY = -90;
        break;
      case 9:
        absoluteY = -200;
        break;
    }
    instance.SetAbsolutePosition((float) absoluteX, (float) absoluteY);
    float num1 = 100f / instance.Width;
    instance.ScalePercent(num1 * 100f);
    float num2 = num1 * 100f;
    this.doc.Add((IElement) instance);
    if (x == 3 && y == 6)
    {
      this.doc.NewPage();
      x = 1;
      y = 1;
    }
    if (cerrarArchivo)
      this.doc.Close();
    return this.doc;
  }

  private void log(string valor)
  {
    try
    {
      string path1 = "C:\\WEB\\Log\\";
      if (!Directory.Exists(path1))
        Directory.CreateDirectory(path1);
      string path2 = path1 + "Log_Cinexpass_Digital.txt";
      string path3 = path2;
      string[] strArray = new string[6];
      DateTime now = DateTime.Now;
      strArray[0] = now.ToShortDateString();
      strArray[1] = " ";
      now = DateTime.Now;
      strArray[2] = now.ToShortTimeString();
      strArray[3] = " --> ";
      strArray[4] = valor;
      strArray[5] = Environment.NewLine;
      string contents = string.Concat(strArray);
      File.AppendAllText(path3, contents);
      File.AppendAllText(path2, "--------------------------------------------------------------------" + Environment.NewLine);
    }
    catch (Exception ex)
    {
      File.AppendAllText("C:\\WEB\\Log\\Log_Cinexpass_Digital.txt", $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} --> {ex.StackTrace}{Environment.NewLine}");
    }
  }
    }
}
