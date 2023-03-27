using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXML
{
    public static class GenerateXML
    {
        public static string XmlFactura()
        {
            factura fac = new()
            {
                id = facturaID.comprobante,
                version = "1.1.0"
            };
            //datos del emisor
            Random rnd = new Random();
            int randonNum = rnd.Next(10000000, 99999999);
            string claveAcceso = "260320230113132722451001001000000001" + randonNum.ToString() + "1";
            fac.infoTributaria = new infoTributaria
            {
                ambiente = "1",
                tipoEmision = "1",
                razonSocial = "PRUEBAS SERVICIO DE RENTAS INTERNAS",
                nombreComercial = "PRUEBAS SERVICIO DE RENTAS INTERNAS",
                ruc = "1313272245",
                claveAcceso = claveAcceso + digitoVerificador(claveAcceso),
                codDoc = "01",
                estab = "001",
                ptoEmi = "001",
                secuencial = "000000001",
                dirMatriz = "Ciudadela La Lorena"
            };
            //datos de factura
            fac.infoFactura = new facturaInfoFactura();
            fac.infoFactura.fechaEmision = "26/03/2023";
            fac.infoFactura.contribuyenteEspecial = "12345";
            fac.infoFactura.obligadoContabilidad = obligadoContabilidad.NO;
            fac.infoFactura.dirEstablecimiento = "La aurora";
            //cliente
            fac.infoFactura.tipoIdentificacionComprador = "05";
            fac.infoFactura.razonSocialComprador = "Victor Maldonado";
            fac.infoFactura.identificacionComprador = "0919328948";
            fac.infoFactura.direccionComprador = "Quito";
            fac.infoFactura.totalSinImpuestos = 8.45M;
            //facturaInfoFacturaTotalImpuesto[] fTotalConImpuesto = new facturaInfoFacturaTotalImpuesto[1];
            fac.infoFactura.totalConImpuestos = new facturaInfoFacturaTotalImpuesto[1];
            fac.infoFactura.totalConImpuestos[0] = new facturaInfoFacturaTotalImpuesto
            {
                codigo = "2",
                codigoPorcentaje = "2",
                baseImponible = 8.45M,
                valor = 1.01M
            };         

            //fac.infoFactura.totalConImpuestos = fTotalConImpuesto;
            fac.infoFactura.propina = 0;
            fac.infoFactura.importeTotal = 9.46M;
            fac.infoFactura.moneda = "USD";
            fac.infoFactura.pagos = new pagosPago[1];
            fac.infoFactura.pagos[0] = new pagosPago
            {
                formaPago = "01",
                total = 9.46M
            };

            fac.detalles = new facturaDetalle[1];
            fac.detalles[0] = new facturaDetalle
            {
                codigoPrincipal = "02151",
                descripcion = "eucerin crema",
                cantidad = 1.00M,
                precioUnitario = 8.45M,
                descuento = 0.00M,
                precioTotalSinImpuesto = 8.45M,
                
            };
            fac.detalles[0].impuestos = new impuesto[1];
            fac.detalles[0].impuestos[0] = new impuesto
            {
                codigo = "2",
                codigoPorcentaje = "2",
                baseImponible = 8.45M,
                valor = 9.46M
            };

            xmlUtil util = new xmlUtil();
            util.Serializar(fac, "D:\\xmlGenerados\\"+ fac.infoTributaria.claveAcceso+".xml");



            return "prueba";
        }
        private static string digitoVerificador(string claveAcceso48)
        {

            var clave1 = claveAcceso48.ToCharArray();
            int suma = 0, factor = 7;

            foreach (var item in clave1)
            {

                suma = suma + Convert.ToInt32(item.ToString()) * factor;
                factor = factor - 1;
                if (factor == 1)
                    factor = 7;

            }
            var digitoverificador = (suma % 11);
            digitoverificador = 11 - digitoverificador;
            if (digitoverificador == 11)
                digitoverificador = 0;
            else if (digitoverificador == 10)
                digitoverificador = 1;

            return digitoverificador.ToString(); ;


        }
    }
}
