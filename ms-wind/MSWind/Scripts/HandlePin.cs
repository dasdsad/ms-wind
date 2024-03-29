﻿using MSWind.Connection;
using System.Windows.Forms;

namespace MSWind.Scripts
{
    class HandlePin
    {
        public HandlePin(Client client, byte[] packet)
        {
            PacketReader Reader = new PacketReader(packet);

            byte type = Reader.ReadByte();

            if (type == 0)
            { 
                PacketWriter Writer = new PacketWriter();

                Writer.WriteShort((short)PacketOpcodes.sWorldInfoRequest);
                client.SendPacket(Writer);
            }
            else if (type == 1)
            {
                MessageBox.Show("Please set a pin.");
                client.MapleConnect.Close();
                client.LoginThread.Abort();
                client.Account.LoggingIn = false;
                client.MForm.ChangedAccount();
            }
            else if (type == 2)
            {
                MessageBox.Show("Incorrect pin");
                client.MapleConnect.Close();
                client.LoginThread.Abort();
                client.Account.LoggingIn = false;
                client.MForm.ChangedAccount();
            }
        }
    }
}
