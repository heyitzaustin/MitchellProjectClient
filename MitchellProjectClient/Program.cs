using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace MitchellProjectClient
{
    class Program
    {
        ClaimsService.ClaimsServiceClient client = new ClaimsService.ClaimsServiceClient();

        /*
        BASIC CONSOLE APPLICATION TO TEST WEB SERVICES
            */

        static void Main(string[] args)
        {
            Program program = new Program();

            // TEST INSERT CLAIM
            string insertsuccess = program.TestInsert();
            Console.WriteLine("Insertion of create-claim.xml result: "+insertsuccess);

            // TEST READ CLAIM
            XmlDocument doc = new XmlDocument();
            // Read the xml from the webservice
            doc.LoadXml(program.TestRead());
            // Put where you want to save the output xml file here
            string filename = @"C:\Users\Austin Li\Documents\Visual Studio 2015\Projects\MitchellProject\MitchellProject\OutputXML\claim.xml";
            // overwrite file
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            if (!File.Exists(filename))
            {
                Console.WriteLine("Claim Read successful. Output is claim.xml");
                doc.Save(filename);
            }
            else
            {
                Console.WriteLine("Claim Read unsuccessful.");
            }

            // TEST READ VEHICLE FROM SPECIFIC CLAIM
            XmlDocument doc2 = new XmlDocument();
            // Read the xml from the webservice
            doc.LoadXml(program.TestVehicleRead());
            // Put where you want to save the output xml file here
            string filename2 = @"C:\Users\Austin Li\Documents\Visual Studio 2015\Projects\MitchellProject\MitchellProject\OutputXML\vehicle.xml";
            // overwrite file
            if (File.Exists(filename2))
            {
                File.Delete(filename2);
            }
            if (!File.Exists(filename2))
            {
                Console.WriteLine("Vehicle Read successful. Output is temp.vehicle");
                doc.Save(filename2);
            }
            else
            {
                Console.WriteLine("Vehicle Read unsuccessful.");
            }

            // TEST DELETE CLAIM
            string deletesuccess = program.TestDelete();
            Console.WriteLine("Deletion of claim '22c9c23bac142856018ce14a26b6c299' result: " + deletesuccess);


            return;
        }

        public string TestInsert()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Austin Li\\Documents\\Visual Studio 2015\\Projects\\MitchellProject\\MitchellProject\\create-claim.xml");
            string msg =  client.CreateClaimFromXML(doc.OuterXml);
            return msg;
        }

        public string TestRead()
        {
            XmlDocument doc = new XmlDocument();
            string xmldoc = client.readClaim("22c9c23bac142856018ce14a26b6c299");
            return xmldoc;
        }

        public string TestVehicleRead()
        {
            XmlDocument doc = new XmlDocument();
            string xmldoc = client.readVehicleFromClaim("22c9c23bac142856018ce14a26b6c299", "1M8GDM9AXKP042788");
            return xmldoc;
        }

        public string TestDelete()
        {
            string msg = client.deleteClaim("22c9c23bac142856018ce14a26b6c299");
            return msg;
        }
    }
}
