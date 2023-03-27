using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoXML
{
    public class xmlUtil
    {
        public bool DesSerializar(ref factura clase, string ficheroXml)
        {
            try
            {
                var objStreamReader = new System.IO.StreamReader(ficheroXml);
                var x = new System.Xml.Serialization.XmlSerializer(clase.GetType());

                clase = (factura)x.Deserialize(objStreamReader);
                objStreamReader.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Serializar(factura clase, string ficheroXmlDestino)
        {
            try
            {
                var objStreamWriter = new System.IO.StreamWriter(ficheroXmlDestino, false, System.Text.Encoding.GetEncoding("UTF-8"));
                var x = new System.Xml.Serialization.XmlSerializer(clase.GetType());

                x.Serialize(objStreamWriter, clase);
                objStreamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
