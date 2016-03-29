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

        // IMPORTANT: OUTPUT PATH OF XML FILES HERE
        string directory_path = "C:\\Users\\Austin Li\\Documents\\Visual Studio 2015\\Projects\\MitchellProjectClient\\MitchellProjectClient\\OutputXML\\";
        // IMPORTANT: PATH OF XML FILE TO BE INSERTED HERE
        string create_xml_path = "C:\\Users\\Austin Li\\Documents\\Visual Studio 2015\\Projects\\MitchellProjectClient\\MitchellProjectClient\\create-claim.xml";


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
            string filename = program.directory_path+"claim.xml";
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

            // TEST GET CLAIMS IN DATE RANGE
            XmlDocument doc3 = new XmlDocument();
            // Read the xml from the webservice
            doc.LoadXml(program.testDateClaims());
            // Put where you want to save the output xml file here
            string filename3 = program.directory_path + "claimsindaterange.xml";
            // overwrite file
            if (File.Exists(filename3))
            {
                File.Delete(filename3);
            }
            if (!File.Exists(filename3))
            {
                Console.WriteLine("Date Range Claim Read successful. Output is claimsindaterange.xml");
                doc.Save(filename3);
            }
            else
            {
                Console.WriteLine("Date Range Claim Read unsuccessful.");
            }

            // TEST READ VEHICLE FROM SPECIFIC CLAIM
            XmlDocument doc2 = new XmlDocument();
            // Read the xml from the webservice
            doc.LoadXml(program.TestVehicleRead());
            // Put where you want to save the output xml file here
            string filename2 = program.directory_path + "vehicle.xml";
            // overwrite file
            if (File.Exists(filename2))
            {
                File.Delete(filename2);
            }
            if (!File.Exists(filename2))
            {
                Console.WriteLine("Vehicle Read successful. Output is vehicle.xml");
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


        /*

        TEMPORARY UNIT TESTS
            
            */
        // CREATING A CLAIM
        public string TestInsert()
        {
            XmlDocument doc = new XmlDocument();
            // FILE PATH OF XML TO BE INSERTED
            doc.Load(create_xml_path);
            string msg =  client.CreateClaimFromXML(doc.OuterXml);
            return msg;
        }

        // READING A CLAIM
        public string TestRead()
        {
            XmlDocument doc = new XmlDocument();
            string xmldoc = client.readClaim("22c9c23bac142856018ce14a26b6c299");
            return xmldoc;
        }

        // READING A SPECIFIC VEHICLE FROM A CLAIM
        public string TestVehicleRead()
        {
            XmlDocument doc = new XmlDocument();
            string xmldoc = client.readVehicleFromClaim("22c9c23bac142856018ce14a26b6c299", "1M8GDM9AXKP042788");
            return xmldoc;
        }

        // DELETE CLAIM
        public string TestDelete()
        {
            string msg = client.deleteClaim("22c9c23bac142856018ce14a26b6c299");
            return msg;
        }

        // FIND CLAIMS IN DATE RANGE
        public string testDateClaims()
        {
            string startdate = "2010-07-10T17:19:13.676-07:00";
            string enddate = "2015-07-10T17:19:13.676-07:00";
            string[] xml_list = client.getClaimsInDateRange(startdate, enddate);
            string full_xml = "";
            foreach (string xml in xml_list)
            {
                full_xml += xml;
            }
            return full_xml;

        }
    }
}
