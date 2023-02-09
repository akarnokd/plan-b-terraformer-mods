﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatMultiplayer
{
    internal class MessageUpdateAltitude : MessageUpdate
    {
        const string messageCode = "UpdateAltitude";
        static readonly byte[] messageCodeBytes = Encoding.UTF8.GetBytes(messageCode);
        public override string MessageCode() => messageCode;
        public override byte[] MessageCodeBytes() => messageCodeBytes;

        internal float value;

        public override void GetSnapshot(int2 coords)
        {
            this.coords = coords;
            value = GHexes.altitude[coords.x, coords.y];
        }

        public override void ApplySnapshot()
        {
            GHexes.altitude[coords.x, coords.y] = value;
        }

        public override void Encode(BinaryWriter output)
        {
            output.Write(coords.x);
            output.Write(coords.y);
            output.Write(value);
        }

        public override bool TryDecode(BinaryReader input, out MessageBase message)
        {
            var msg = new MessageUpdateAltitude();
            msg.coords = new int2(input.ReadInt32(), input.ReadInt32());
            msg.value = input.ReadSingle();
            message = msg;
            return true;
        }
    }
}
