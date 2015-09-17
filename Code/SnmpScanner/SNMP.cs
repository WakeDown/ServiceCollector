using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner
{
    public static class Snmp
    {
        internal static string RunProgram(string ipHost, string oid)
        {
            string exeName = "snmpget.exe";
            int timeoutSeconds = 0;

            string argsLine = String.Format(" -v1 -r 1 -t 0.025 -c public {0} {1}", ipHost, oid);
            StreamReader outputStream = StreamReader.Null;
            string strOutput = "";
            bool bSuccess = false;

            try
            {
                Process newProcess = new Process();
                newProcess.StartInfo.FileName = exeName;
                newProcess.StartInfo.Arguments = argsLine;
                newProcess.StartInfo.UseShellExecute = false;
                newProcess.StartInfo.CreateNoWindow = true;
                newProcess.StartInfo.RedirectStandardOutput = true;
                newProcess.Start();

                if (0 == timeoutSeconds)
                {
                    outputStream = newProcess.StandardOutput;
                    strOutput = outputStream.ReadToEnd();
                    newProcess.WaitForExit();
                }
                else
                {
                    bSuccess = newProcess.WaitForExit(timeoutSeconds * 1000);

                    if (bSuccess)
                    {
                        outputStream = newProcess.StandardOutput;
                        strOutput = outputStream.ReadToEnd();
                    }
                    else
                    {
                        strOutput = String.Format("Timed out at {0} seconds waiting for {1} to exit.", timeoutSeconds, exeName);
                    }
                }
            }
            catch (Exception e)
            {
                throw (new Exception(String.Format("An error occurred running {0}.", exeName), e));
            }
            finally
            {
                outputStream.Close();
            }

            return strOutput;
        }

        //public static string GetValue(string host, string oid)
        //{
        //    // Prepare target
        //    UdpTarget target = new UdpTarget((IPAddress)new IpAddress(host));
        //    // Create a SET PDU
        //    Pdu pdu = new Pdu(PduType.Set);

        //    pdu.VbList.Add(new Oid(oid), new Counter32());

        //    // Set sysLocation.0 to a new string
        //    //pdu.VbList.Add(new Oid("1.3.6.1.2.1.1.6.0"), new OctetString("Some other value"));
        //    //// Set a value to integer
        //    //pdu.VbList.Add(new Oid("1.3.6.1.2.1.67.1.1.1.1.5.0"), new Integer32(500));
        //    //// Set a value to unsigned integer
        //    //pdu.VbList.Add(new Oid("1.3.6.1.2.1.67.1.1.1.1.6.0"), new UInteger32(101));
        //    // Set Agent security parameters
        //    AgentParameters aparam = new AgentParameters(SnmpVersion.Ver1, new OctetString("private"));



        //    // Response packet
        //    SnmpV2Packet response;
            
            
        //        // Send request and wait for response
        //    try
        //    {
        //        response = target.Request(pdu, aparam) as SnmpV2Packet;
        //    }
        //    catch (Exception exception)
        //    {
        //        return null;
        //    }
            
        //    // Make sure we received a response
        //    if (response == null)
        //    {
        //        //Console.WriteLine("Error in sending SNMP request.");
        //    }
        //    else
        //    {
        //        // Check if we received an SNMP error from the agent
        //        if (response.Pdu.ErrorStatus != 0)
        //        {
        //            //Console.WriteLine(String.Format("SNMP agent returned ErrorStatus {0} on index {1}",
        //            //  response.Pdu.ErrorStatus, response.Pdu.ErrorIndex));
        //        }
        //        else
        //        {
        //            // Everything is ok. Agent will return the new value for the OID we changed
        //            //Console.WriteLine(String.Format("Agent response {0}: {1}",
        //            //  response.Pdu[0].Oid.ToString(), ));
        //        }

        //        return response.Pdu[0].Value.ToString();
        //    }

        //    return null;
        //}

        //public byte[] get(string request, string host, string community, string mibstring)
        //{
        //    byte[] packet = new byte[1024];
        //    byte[] mib = new byte[1024];
        //    int snmplen;
        //    int comlen = community.Length;
        //    string[] mibvals = mibstring.Split('.');
        //    int miblen = mibvals.Length;
        //    int cnt = 0, temp, i;
        //    int orgmiblen = miblen;
        //    int pos = 0;

        //    // Convert the string MIB into a byte array of integer values
        //    // Unfortunately, values over 128 require multiple bytes
        //    // which also increases the MIB length
        //    for (i = 0; i < orgmiblen; i++)
        //    {
        //        temp = Convert.ToInt16(mibvals[i]);
        //        if (temp > 127)
        //        {
        //            mib[cnt] = Convert.ToByte(128 + (temp / 128));
        //            mib[cnt + 1] = Convert.ToByte(temp - ((temp / 128) * 128));
        //            cnt += 2;
        //            miblen++;
        //        }
        //        else
        //        {
        //            mib[cnt] = Convert.ToByte(temp);
        //            cnt++;
        //        }
        //    }
        //    snmplen = 29 + comlen + miblen - 1;  //Length of entire SNMP packet

        //    //The SNMP sequence start
        //    packet[pos++] = 0x30; //Sequence start
        //    packet[pos++] = Convert.ToByte(snmplen - 2);  //sequence size

        //    //SNMP version
        //    packet[pos++] = 0x02; //Integer type
        //    packet[pos++] = 0x01; //length
        //    packet[pos++] = 0x00; //SNMP version 1

        //    //Community name
        //    packet[pos++] = 0x04; // String type
        //    packet[pos++] = Convert.ToByte(comlen); //length
        //    //Convert community name to byte array
        //    byte[] data = Encoding.ASCII.GetBytes(community);
        //    for (i = 0; i < data.Length; i++)
        //    {
        //        packet[pos++] = data[i];
        //    }

        //    //Add GetRequest or GetNextRequest value
        //    if (request == "get")
        //        packet[pos++] = 0xA0;
        //    else
        //        packet[pos++] = 0xA1;

        //    packet[pos++] = Convert.ToByte(20 + miblen - 1); //Size of total MIB

        //    //Request ID
        //    packet[pos++] = 0x02; //Integer type
        //    packet[pos++] = 0x04; //length
        //    packet[pos++] = 0x00; //SNMP request ID
        //    packet[pos++] = 0x00;
        //    packet[pos++] = 0x00;
        //    packet[pos++] = 0x01;

        //    //Error status
        //    packet[pos++] = 0x02; //Integer type
        //    packet[pos++] = 0x01; //length
        //    packet[pos++] = 0x00; //SNMP error status

        //    //Error index
        //    packet[pos++] = 0x02; //Integer type
        //    packet[pos++] = 0x01; //length
        //    packet[pos++] = 0x00; //SNMP error index

        //    //Start of variable bindings
        //    packet[pos++] = 0x30; //Start of variable bindings sequence

        //    packet[pos++] = Convert.ToByte(6 + miblen - 1); // Size of variable binding

        //    packet[pos++] = 0x30; //Start of first variable bindings sequence
        //    packet[pos++] = Convert.ToByte(6 + miblen - 1 - 2); // size
        //    packet[pos++] = 0x06; //Object type
        //    packet[pos++] = Convert.ToByte(miblen - 1); //length

        //    //Start of MIB
        //    packet[pos++] = 0x2b;
        //    //Place MIB array in packet
        //    for (i = 2; i < miblen; i++)
        //        packet[pos++] = Convert.ToByte(mib[i]);
        //    packet[pos++] = 0x05; //Null object value
        //    packet[pos++] = 0x00; //Null

        //    //Send packet to destination
        //    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
        //                     ProtocolType.Udp);
        //    sock.SetSocketOption(SocketOptionLevel.Socket,
        //                    SocketOptionName.ReceiveTimeout, 5000);
        //    IPHostEntry ihe = Dns.Resolve(host);
        //    IPEndPoint iep = new IPEndPoint(ihe.AddressList[0], 161);
        //    EndPoint ep = (EndPoint)iep;
        //    sock.SendTo(packet, snmplen, SocketFlags.None, iep);

        //    //Receive response from packet
        //    try
        //    {
        //        int recv = sock.ReceiveFrom(packet, ref ep);
        //    }
        //    catch (SocketException)
        //    {
        //        packet[0] = 0xff;
        //    }
        //    return packet;
        //}

        //public string getnextMIB(byte[] mibin)
        //{
        //    string output = "1.3";
        //    int commlength = mibin[6];
        //    int mibstart = 6 + commlength + 17; //find the start of the mib section
        //    //The MIB length is the length defined in the SNMP packet
        //    // minus 1 to remove the ending .0, which is not used
        //    int miblength = mibin[mibstart] - 1;
        //    mibstart += 2; //skip over the length and 0x2b values
        //    int mibvalue;

        //    for (int i = mibstart; i < mibstart + miblength; i++)
        //    {
        //        mibvalue = Convert.ToInt16(mibin[i]);
        //        if (mibvalue > 128)
        //        {
        //            mibvalue = (mibvalue / 128) * 128 + Convert.ToInt16(mibin[i + 1]);
        //            //ERROR here, it should be mibvalue = (mibvalue-128)*128 + Convert.ToInt16(mibin[i+1]);
        //            //for mib values greater than 128, the math is not adding up correctly   

        //            i++;
        //        }
        //        output += "." + mibvalue;
        //    }
        //    return output;
        //}
    }
}
