using Microsoft.Win32;
using System;
using System.Collections;
using System.Linq;
using System.Management;


namespace System_Data
{
    class hardware_details
    {
        public static string System_Data()
        {
            string data = "";      
            
            //OPERATING SYSTEM
            string installDate_os, lastBootTime_os, localeDateTime_os;
            string lang = "";
            uint a_os;
            string os = "======OPERATING SYSTEM======;";

            ManagementObjectSearcher osQuery = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection osCol = osQuery.Get();

            foreach (ManagementObject obj in osCol)
            {
                os += "Computer Name: " + obj["csname"] + ";";
                os += "User: " + obj["RegisteredUser"] + ";";
                os += obj["Caption"] + " " + obj["OSArchitecture"] + " Version " + obj["Version"] + " (Build " + obj["BuildNumber"] + ")" + ";";
                a_os = Convert.ToUInt32(obj["OSLanguage"]);
                switch (a_os)
                {
                    case 1033:
                        lang = "English-United States";
                        break;
                    case 9:
                        lang = "English";
                        break;
                    case 1081:
                        lang = "Hindi";
                        break;
                    case 2057:
                        lang = "English–United Kingdom";
                        break;
                    default:
                        lang = "none";
                        break;
                }
                os += "Installed Language: " + lang + ";";


                if (Convert.ToString(obj["Locale"]) == "0409")
                {
                    os += "System Locale: " + "en-US" + ";";
                }

                installDate_os = Convert.ToString(obj["InstallDate"]);
                {
                    os += "Installed: " + ConvertToDateTimeSeconds(installDate_os) + ";";
                }

                lastBootTime_os = Convert.ToString(obj["LastBootUpTime"]);
                {
                    os += "LastBootUp: " + ConvertToDateTimeSeconds(lastBootTime_os) + ";";
                }

                localeDateTime_os = Convert.ToString(obj["LocalDateTime"]);
                {
                    os += "LocalDate: " + ConvertToDateTimeSeconds(localeDateTime_os) + ";";
                }

            }

            string biosVersionDate = "";
            string releaseDate = "";

            //string bios_data = "======BIOS======;";

            ManagementObjectSearcher biosQuery = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            ManagementObjectCollection biosCol = biosQuery.Get();
            foreach (ManagementObject obj in biosCol)
            {
                releaseDate = Convert.ToString(obj["ReleaseDate"]);    
                biosVersionDate = Convert.ToString(obj["Manufacturer"]) + " " + Convert.ToString(obj["SMBIOSBIOSVersion"]) + ", " + ConvertToDateTime(releaseDate) + ";";           
                os += "BIOS Version/Date: " + biosVersionDate + ";";                
                os += "Embedded Controller Version: " + obj["EmbeddedControllerMinorVersion"] + "." + obj["EmbeddedControllerMajorVersion"] + ";";
            }

            data += os + "\t";

            //MODEL
            string sysmod = "======SYSTEM MODEL======;";

            ManagementObjectSearcher sysmodQuery = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectCollection sysmodCol = sysmodQuery.Get();
            foreach (ManagementObject obj in sysmodCol)
            {
                sysmod += obj["Name"] + ";";
                sysmod += obj["IdentifyingNumber"] + ";";
            }

            data += sysmod + "\t";

            //PROCESSOR
            string processor = "";
            string processor_data = "======PROCESSOR======;";
            string pfamily = "";

            ManagementObjectSearcher proQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection proCol = proQuery.Get();

            foreach (ManagementObject obj in proCol)
            {
                int processorF = Convert.ToUInt16(obj["Family"]);
                switch (processorF)
                {
                    case 185:
                        pfamily = "Intel(R) Pentium(R) M processor";
                        break;
                    case 186:
                        pfamily = "Intel(R) Celeron(R) D processor";
                        break;
                    case 187:
                        pfamily = "Intel(R) Pentium(R) D processor";
                        break;
                    case 188:
                        pfamily = "Intel(R) Pentium(R) Processor Extreme Edition";
                        break;
                    case 189:
                        pfamily = "Intel(R) Core(TM) Solo Processor";
                        break;
                    case 191:
                        pfamily = "Intel(R) Core(TM)2 Duo Processor";
                        break;
                    case 192:
                        pfamily = "Intel(R) Core(TM)2 Solo processor";
                        break;
                    case 193:
                        pfamily = "Intel(R) Core(TM)2 Extreme processor";
                        break;
                    case 194:
                        pfamily = "Intel(R) Core(TM)2 Quad processor";
                        break;
                    case 198:
                        pfamily = "Intel(R) Core(TM) i7 processor";
                        break;
                    case 199:
                        pfamily = "Dual-Core Intel(R) Celeron(R) Processor";
                        break;
                    case 205:
                        pfamily = "Intel(R) Core(TM) i5 processor";
                        break;
                    case 206:
                        pfamily = "Intel(R) Core(TM) i3 processor";
                        break;
                    default:
                        pfamily = "Other";
                        break;
                }
 
                processor_data += pfamily + ";";
                int arch = Convert.ToUInt16(obj["Architecture"]);
                switch (arch)
                {
                    case 0:
                        processor = "x86";
                        break;
                    case 1:
                        processor = "MIPS";
                        break;
                    case 2:
                        processor = "Alpha";
                        break;
                    case 3:
                        processor = "POWER PC";
                        break;
                    case 5:
                        processor = "ia64";
                        break;
                    case 6:
                        processor = "Itanium-based systems";
                        break;
                    case 9:
                        processor = "x64";
                        break;
                }

                processor_data += processor + ";";
                processor_data += "Current Clock Speed: " + Math.Round(((float)Convert.ToUInt32(obj["CurrentClockSpeed"]) / 1000), 2) + " gigahertz" + ";";
                processor_data += "Max Clock Speed: " + Math.Round(((float)Convert.ToUInt32(obj["MaxClockSpeed"]) / 1000), 2) + " gigahertz" + ";";
                processor_data += "Multi Core: " + obj["NumberOfCores"] + ";";
                processor_data += obj["L2CacheSize"] + " kilobyte primary memory cache" + ";";
                processor_data += obj["L3CacheSize"] + " kilobyte tertiary memory cache" + ";";
                processor_data += "Logical Processor: " + obj["NumberOfLogicalProcessors"] + ";";
                processor_data += "Bus Clock: " + obj["ExtClock"] + " megahertz" + ";";
            }

            data += processor_data + "\t";

            //MOTHERBOARD
            string board = "";
            string mboard = "======MOTHERBOARD======;";
            ManagementObjectSearcher mboardQuery = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection mboardCol = mboardQuery.Get();

            foreach (ManagementObject obj in mboardCol)
            {
                board = Convert.ToString(obj["Manufacturer"]) + " " + Convert.ToString(obj["Product"]);
                mboard += "Board: " + board + ";";
                mboard += "SerialNumber: " + obj["SerialNumber"] + ";";
            }
            data += mboard + "\t";


            //DISPLAY
            string dis = "======DISPLAY======;";
            ManagementObjectSearcher disQuery = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            ManagementObjectCollection disCol = disQuery.Get();
            foreach (ManagementObject obj in disCol)
            {
                dis += obj["Caption"] + ";";
            }

            ManagementObjectSearcher disSizeQuery = new ManagementObjectSearcher(@"\root\wmi", @"SELECT * FROM WmiMonitorBasicDisplayParams");

            //Calculate and output size for monitor  
            foreach (ManagementObject obj in disSizeQuery.Get())
            {
                //Calculate monitor size  
                double widatah = (byte)obj["MaxHorizontalImageSize"] / 2.54;
                double height = (byte)obj["MaxVerticalImageSize"] / 2.54;
                double diagonal = Math.Sqrt(widatah * widatah + height * height);
                dis += "Monitor Size: " + Math.Round(diagonal, 1) + ";";
            }

            data += dis + "\t";


            //BUS ADAPTER
            string ba = "======BUS ADAPTER======;";
            ManagementObjectSearcher baQuery = new ManagementObjectSearcher("SELECT * FROM Win32_SCSIController");
            ManagementObjectCollection baCol = baQuery.Get();
            foreach (ManagementObject obj in baCol)
            {
                ba += obj["Caption"] + ";";
            }

            ManagementObjectSearcher usbConQuery = new ManagementObjectSearcher("SELECT * FROM Win32_USBController");
            ManagementObjectCollection usbConCol = usbConQuery.Get();
            foreach (ManagementObject obj in usbConCol)
            {
                ba += obj["Caption"] + ";";
            }

            data += ba + "\t";


            //CONTROLLER
            string co = "======CONTROLLER======;";
            ManagementObjectSearcher coQuery = new ManagementObjectSearcher("SELECT * FROM Win32_IDEController");
            ManagementObjectCollection coCol = coQuery.Get();
            foreach (ManagementObject obj in coCol)
            {
                co += obj["Caption"] + ";";
            }
            data += co + "\t";


            //DISK DRIVE
            string dd = "======DISK DRIVE======;";
            ulong gb = 1024 * 1024 * 1024;

            ManagementObjectSearcher ddQuery = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            ManagementObjectCollection ddCol = ddQuery.Get();
            foreach (ManagementObject obj in ddCol)
            {
                dd += "Hard Drive: " + obj["Caption"] + ";";
                dd += "Serial No: " + Convert.ToString(obj["SerialNumber"]).Trim() + ";";
                float c = Convert.ToUInt64(obj["Size"]) / (float)gb;
                decimal d = Decimal.Floor(Convert.ToDecimal(c));
                dd += "Size: " + d + " GB" + ";";
            }

            ManagementObjectSearcher ldQuery = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectCollection ldCol = ldQuery.Get();
            decimal totalFree = 0;
            decimal totalAvailable = 0;
            string dd1;
            string dd2;
            foreach (ManagementObject obj in ldCol)
            {
                float c0 = Convert.ToUInt64(obj["FreeSpace"]) / (float)gb;
                decimal d0 = Decimal.Round(Convert.ToDecimal(c0), 2);
                totalFree = totalFree + d0;

                float c1 = Convert.ToUInt64(obj["Size"]) / (float)gb;
                decimal d1 = Decimal.Round(Convert.ToDecimal(c1));
                totalAvailable = totalAvailable + d1;

                dd1 = obj["Caption"] + "     " + obj["FileSystem"] + "   " + "Free: " + d0 + " GB" + "   " + "Available: " + d1 + " GB";
                dd += dd1 + ";";
            }

            dd2 = "Total Free: " + totalFree + "    " + "Total Available: " + totalAvailable + " GB";
            dd += dd2 + ";";

            string op;
            ManagementObjectSearcher cdromQuery = new ManagementObjectSearcher("SELECT * FROM Win32_CDROMDrive");
            ManagementObjectCollection cdromCol = cdromQuery.Get();
            foreach (ManagementObject obj in cdromCol)
            {
                op = "Optical Drive: " + obj["Caption"];
                dd += op + ";";
            }

            data += dd + "\t";


            //PHYSICAL MEMORY
            string pm = "======PHYSICAL MEMORY======;";
            UInt16 pmType;
            string type = "";
            ManagementObjectSearcher pmQuery = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectCollection pmCol = pmQuery.Get();
            foreach (ManagementObject obj in pmCol)
            {
                pm += "Serial Number: " + obj["SerialNumber"] + ";";

                float c = Convert.ToUInt64(obj["Capacity"]) / (float)gb;
                decimal d = Decimal.Round(Convert.ToDecimal(c), 2);
                pm += "Size: " + d + " GB" + ";";

                pm += "Location: " + obj["DeviceLocator"] + ";";
                pm += "Datawidth: " + obj["Datawidth"] + ";";
                pm += "Clock Speed: " + obj["ConfiguredClockSpeed"] + " Megahertz" + ";";
                pmType = Convert.ToUInt16(obj["TypeDetail"]);
                switch (pmType)
                {
                    case 1:
                        type = "Reserved";
                        break;
                    case 2:
                        type = "Other";
                        break;
                    case 4:
                        type = "Unknown";
                        break;
                    case 8:
                        type = "Fast-Paged";
                        break;
                    case 16:
                        type = "Static-Column";
                        break;
                    case 32:
                        type = "Psuedo-static";
                        break;
                    case 64:
                        type = "RAMBUS";
                        break;
                    case 128:
                        type = "Synchronous";
                        break;
                    case 256:
                        type = "CMOS";
                        break;
                    case 512:
                        type = "EDO";
                        break;
                    case 1024:
                        type = "Window DRAM";
                        break;
                    case 2048:
                        type = "Cache DRAM";
                        break;
                    case 4096:
                        type = "Non-volatile";
                        break;
                }
                pm += "Type: " + type + ";";
            }

            data += pm + "\t";


            //MULTI MEDIA
            string multim = "======MULTI MEDIA======;";
            ManagementObjectSearcher mmQuery = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");
            ManagementObjectCollection mmCol = mmQuery.Get();
            foreach (ManagementObject obj in mmCol)
            {
                multim += obj["Caption"] + ";";
            }
            data += multim + "\t";


            //PRINTER
            string printer = "======PRINTER======;";
            ManagementObjectSearcher pQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            ManagementObjectCollection pCol = pQuery.Get();
            foreach (ManagementObject obj in pCol)
            {
                printer += obj["Caption"] + ";";
                printer += obj["PortName"] + ";";
            }
            data += printer + "\t";


            //NETWORK
            string nd = "======NETWORK DETAILS======;";
            ManagementObjectSearcher netQuery = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled = \"True\" ");
            ManagementObjectCollection netCol = netQuery.Get();
            foreach (ManagementObject obj in netCol)
            {
                nd += "Description: " + obj["Description"] + ";";
                nd += "MAC Address: " + obj["MACAddress"] + ";";
                nd += "DNS HostName: " + obj["DNSHostName"] + ";";

                object anArray = obj["DefaultIPGateway"];
                IEnumerable enumerable = anArray as IEnumerable;
                if (enumerable != null)
                {
                    string s0 = "";
                    foreach (object element in enumerable)
                    {
                        s0 += element + ", ";
                    }
                    nd += "Default IPGateway: " + s0.TrimEnd().TrimEnd(',') + ";";
                }

                object anArray1 = obj["IPAddress"];
                IEnumerable enumerable1 = anArray1 as IEnumerable;
                if (enumerable1 != null)
                {
                    string s1 = "";
                    foreach (object element1 in enumerable1)
                    {
                        s1 += element1 + ", ";
                    }
                    nd += "IP Address: " + s1.TrimEnd().TrimEnd(',') + ";";
                }

                object anArray2 = obj["IPSubnet"];
                IEnumerable enumerable2 = anArray2 as IEnumerable;
                if (enumerable2 != null)
                {
                    string s2 = "";
                    foreach (object element2 in enumerable2)
                    {
                        s2 += element2 + ", ";
                    }
                    nd += "IP Subnet: " + s2.TrimEnd().TrimEnd(',') + ";";
                }

                object anArray3 = obj["DNSServerSearchOrder"];
                IEnumerable enumerable3 = anArray3 as IEnumerable;
                if (enumerable3 != null)
                {
                    string s3 = "";
                    foreach (object element3 in enumerable3)
                    {
                        s3 += element3 + ", ";
                    }
                    nd += "DNS Server: " + s3.TrimEnd().TrimEnd(',') + ";";
                }
                break;
            }
            data += nd + "\t";


            //ANTIVIRUS
            string av = "======ANTIVIRUS======;";
            string wmipathstr = @"\Root\Microsoft\Windows\Defender";
            string antispy;

            ManagementObjectSearcher avQuery = new ManagementObjectSearcher(wmipathstr, "SELECT * FROM MSFT_MpComputerStatus");
            ManagementObjectCollection avCol = avQuery.Get();
            foreach (ManagementObject obj in avCol)
            {
                av += "Windows Defender Version: " + obj["AMProductVersion"] + ";";
                av += "Scan Engine Version: " + obj["AMEngineVersion"] + ";";
                av += "Windows Defender Enabled: " + obj["AMServiceEnabled"] + ";";
                av += "Virus Protection Enabled: " + obj["AntispywareEnabled"] + ";";
                antispy = ConvertToDateTime(Convert.ToString(obj["AntivirusSignatureLastUpdated"])) + "\\" + Convert.ToString(obj["AntivirusSignatureVersion"]);
                av += "Virus Defination Last Updated\\Version: " + antispy + ";";
                int a_av = Convert.ToInt16(obj["LastFullScanSource"]);
                string fullScan = "";
                switch (a_av)
                {
                    case 0:
                        fullScan = "Unknown";
                        break;
                    case 1:
                        fullScan = "User";
                        break;
                    case 2:
                        fullScan = "System";
                        break;
                    case 3:
                        fullScan = "Real-time";
                        break;
                    case 4:
                        fullScan = "IOAV";
                        break;
                }
                av += "LastFull Scan Source: " + fullScan + ";";
                int b_av = Convert.ToInt16(obj["LastQuickScanSource"]);
                string quickScan = "";
                switch (b_av)
                {
                    case 0:
                        quickScan = "Unknown";
                        break;
                    case 1:
                        quickScan = "User";
                        break;
                    case 2:
                        quickScan = "System";
                        break;
                    case 3:
                        quickScan = "Real-time";
                        break;
                    case 4:
                        quickScan = "IOAV";
                        break;
                }
                av += "LastQuick Scan Source: " + quickScan + ";";
                av += "QuickScan EndTime: " + ConvertToDateTimeSeconds(Convert.ToString(obj["QuickScanEndTime"])) + ";";
                av += "QuickScan StartTime: " + ConvertToDateTimeSeconds(Convert.ToString(obj["QuickScanStartTime"])) + ";";
                av += "RealTime Protection Enabled: " + obj["RealTimeProtectionEnabled"] + ";";
            }
            data += av + "\t";


            //OTHER DEVICES
            string od = "======OTHER DEVICES======;";
            ManagementObjectSearcher ooQuery = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity");
            ManagementObjectCollection ooCol = ooQuery.Get();
            foreach (ManagementObject obj in ooCol)
            {
                od += obj["Caption"] + ";";
            }

            data += od + "\t";


            //HOTFIX
            string hf = "======HOTFIX======;";
            ManagementObjectSearcher hfQuery = new ManagementObjectSearcher("SELECT * FROM Win32_QuickFixEngineering");
            ManagementObjectCollection hfCol = hfQuery.Get();
            foreach (ManagementObject obj in hfCol)
            {
                hf += obj["HotFixID"] + " ";
                if (obj["Description"] != null)
                    hf += obj["Description"] + ";";
                else
                    hf += "     " + ";";
            }

            data += hf + "\t";


            //SOFTWARE VERSION
            string sv = "======SOFTWARE======;";
            Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            foreach (var v in key.GetSubKeyNames())
            {
                string keyName = key + "\\" + v;
                string a_sv = (string)Registry.GetValue(keyName, "DisplayName", null);
                if (a_sv != null)
                {
                    try
                    {
                        sv += Registry.GetValue(keyName, "DisplayName", null) + ";";
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            data += sv + "\t";
            return data;

        }
        private static string ConvertToDateTime(string unconvertedataime)
        {
            string convertedataime = "";
            string year = unconvertedataime.Substring(0, 4);
            string month = unconvertedataime.Substring(4, 2);
            string date = unconvertedataime.Substring(6, 2);          
            convertedataime = date + "-" + month + "-" + year;
            return convertedataime;
        }

        private static string ConvertToDateTimeSeconds(string unconvertedataime)
        {
            string convertedataime = "";
            string year = unconvertedataime.Substring(0, 4);
            string month = unconvertedataime.Substring(4, 2);
            string date = unconvertedataime.Substring(6, 2);
            string hours = unconvertedataime.Substring(8, 2);
            string minutes = unconvertedataime.Substring(10, 2);
            string seconds = unconvertedataime.Substring(12, 2);
          
            convertedataime = date + "-" + month + "-" + year + " " + hours + ":" + minutes + ":" + seconds;
            return convertedataime;
        }
        static void Main(string[] args)
        {
            string[] str, details;
            string data = System_Data();
            details = data.Split('\t');
            for (int i = 0; i < details.Length; i++)
            {
                str = details[i].Split(';');
                str = str.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                for (int j = 0; j < str.Length; j++)
                {
                    Console.WriteLine(j + " " + str[j]);
                }
            }
        }
    }
}
