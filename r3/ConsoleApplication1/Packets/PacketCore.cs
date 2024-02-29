using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using Storage;
using ConsoleApplication1.Packets;

namespace ConsoleApplication1
{
    public partial class PacketHandler
    {
        private NetworkStream clientStream = NewSocket.clientStream;
        private serverMessage fuseMessage = null;
        private static string Username;
        public static bool HasRegistered;

        public void VERSIONCHECK()
        {
            fuseMessage = new serverMessage("ENCRYPTION_OFF");
            NewSocket.SendData(clientStream, fuseMessage);

            fuseMessage = new serverMessage("SECRET_KEY");
            fuseMessage.AppendInteger(1337);
            NewSocket.SendData(clientStream, fuseMessage);
        }
        public void LOGIN()
        {
            string username = NewSocket.split[2];
            string password = NewSocket.split[3];
            if (HasRegistered == true)
            {
                using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
                {
                    dbClient.AddParamWithValue("username", username);
                    dbClient.AddParamWithValue("password", password);

                    try
                    {
                        string checkdata = dbClient.ReadString("SELECT * FROM members WHERE username = @username AND password = @password");

                        if (checkdata != null)
                        {
                            DataRow dbRow = dbClient.ReadDataRow("SELECT * FROM members WHERE username = @username;");
                            Username = (String)dbRow["username"];
                            HasRegistered = false;
                            fuseMessage = new serverMessage("USEROBJECT");
                            fuseMessage.AppendString("name=" + (String)dbRow["username"]);
                            fuseMessage.AppendString("figure=" + (String)dbRow["figure"]);
                            fuseMessage.AppendString("birthday=" + (String)dbRow["birthday"]);
                            fuseMessage.AppendString("phonenumber=");
                            fuseMessage.AppendString("customData=" + (String)dbRow["motto"]);
                            fuseMessage.AppendString("had_read_agreement=" + (Int32)dbRow["had_read_agreement"]);
                            fuseMessage.AppendString("sex=" + (String)dbRow["sex"]);
                            fuseMessage.AppendString("country=" + (String)dbRow["country"]);
                            fuseMessage.AppendString("has_special_rights=0");
                            fuseMessage.AppendString("badge_type=" + (Int32)dbRow["badge"]);
                            NewSocket.SendData(clientStream, fuseMessage);
                        }
                        else
                        {
                            fuseMessage = new serverMessage("SYSTEMBROADCAST");
                            fuseMessage.AppendString("Wrong username or password.");
                            NewSocket.SendData(clientStream, fuseMessage);
                        }
                    }
                    catch
                    {
                        fuseMessage = new serverMessage("SYSTEMBROADCAST");
                        fuseMessage.AppendString("Wrong username or password.");
                        NewSocket.SendData(clientStream, fuseMessage);
                    }
                }
            }
        }
        public void INFORETRIEVE()
        {
            string username = NewSocket.split[2];
            string password = NewSocket.split[3];

            using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("username", username);
                dbClient.AddParamWithValue("password", password);

                try
                {
                    string checkdata = dbClient.ReadString("SELECT * FROM members WHERE username = @username AND password = @password");

                    if (checkdata != null)
                    {
                        DataRow dbRow = dbClient.ReadDataRow("SELECT * FROM members WHERE username = @username;");
                        Username = (String)dbRow["username"];

                        fuseMessage = new serverMessage("USEROBJECT");
                        fuseMessage.AppendString("name=" + (String)dbRow["username"]);
                        fuseMessage.AppendString("figure=" + (String)dbRow["figure"]);
                        fuseMessage.AppendString("birthday=" + (String)dbRow["birthday"]);
                        fuseMessage.AppendString("phonenumber=");
                        fuseMessage.AppendString("customData=" + (String)dbRow["motto"]);
                        fuseMessage.AppendString("had_read_agreement=" + (Int32)dbRow["had_read_agreement"]);
                        fuseMessage.AppendString("sex=" + (String)dbRow["sex"]);
                        fuseMessage.AppendString("country=" + (String)dbRow["country"]);
                        fuseMessage.AppendString("has_special_rights=0");
                        fuseMessage.AppendString("badge_type=" + (Int32)dbRow["badge"]);
                        NewSocket.SendData(clientStream, fuseMessage);
                    }
                    else
                    {
                        fuseMessage = new serverMessage("SYSTEMBROADCAST");
                        fuseMessage.AppendString("Wrong username or password.");
                        NewSocket.SendData(clientStream, fuseMessage);
                    }
                }
                catch
                {
                    fuseMessage = new serverMessage("SYSTEMBROADCAST");
                    fuseMessage.AppendString("Wrong username or password.");
                    NewSocket.SendData(clientStream, fuseMessage);
                }
            }
        }
        public void INITUNITLISTENER()
        {
            string builder = null;
            string pBuilder = null;

            using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
            {
                DataTable Table = dbClient.ReadDataTable("SELECT * FROM rooms_private");
                DataTable pTable = dbClient.ReadDataTable("SELECT * FROM rooms_public");

                foreach (DataRow Row in Table.Rows)
                {
                    builder = builder + (Char)13 + (Int32)Row["id"] + "/" + (String)Row["name"] + "/" + (String)Row["owner"] + "/" + (String)Row["status"] + "/" + (String)Row["password"] + "/floor1/127.0.0.1/127.0.0.1/90/1/null" + "/" + (String)Row["desc"];
                }

                if (builder != null)
                {
                    fuseMessage = new serverMessage("BUSY_FLAT_RESULTS 1");
                    fuseMessage.Append(builder);
                    NewSocket.SendData(clientStream, fuseMessage);
                }


                foreach (DataRow Row in pTable.Rows)
                {
                    pBuilder = pBuilder + (Char)13 + (String)Row["name"] + "," + (Int32)Row["curr_in"] + "," + (Int32)Row["max_in"] + ",127.0.0.1/127.0.0.1,90," + (String)Row["name"] + " " + (String)Row["name_tolower"] + "," + (Int32)Row["curr_in"] + "," + (Int32)Row["max_in"] + "," + (String)Row["model"];
                }
                if (pBuilder != null)
                {   fuseMessage = new serverMessage("ALLUNITS 1");
                    fuseMessage.Append(pBuilder);
                    NewSocket.SendData(clientStream, fuseMessage);
                }
            }
        }
        public void GETCREDITS()
        {
            using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("username", Username);
                DataRow dbRow = dbClient.ReadDataRow("SELECT console_motto,coins FROM members WHERE username = @username;");

                fuseMessage = new serverMessage("MYPERSISTENTMSG"); //Console Motto
                fuseMessage.AppendString((String)dbRow["console_motto"]);
                NewSocket.SendData(clientStream, fuseMessage);

                fuseMessage = new serverMessage("WALLETBALANCE"); //Your coins
                fuseMessage.AppendInteger((Int32)dbRow["coins"]);
                NewSocket.SendData(clientStream, fuseMessage);

                fuseMessage = new serverMessage("MESSENGERSMSACCOUNT");
                fuseMessage.AppendString("noaccount");
                NewSocket.SendData(clientStream, fuseMessage);

                fuseMessage = new serverMessage("MESSENGERREADY"); //Enable messenger
                NewSocket.SendData(clientStream, fuseMessage);

            }
        }
        public void APPROVENAME()
        {
            using (DatabaseClient dbClient = Program.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("username", NewSocket.split[2]);
                Boolean dbRow = dbClient.ReadBoolean("SELECT * FROM members WHERE username = @username;");

                if (dbRow == true)
                {
                    NewSocket.SendData(clientStream, "BADNAME");
                }
            }
        }
    }
}
