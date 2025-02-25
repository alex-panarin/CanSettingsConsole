﻿using CanSettingsConsole.Core;
using System.IO.Ports;

namespace CanSettingsConsole.ViewModel
{
    public class VMSerialPorts : ModelCollectionBase<SerialPort>
    {
        public VMSerialPorts()
        {
            CollectSerialPorts();
        }

        private void CollectSerialPorts()
        {
            using (LockChangedEvent())
            {
                var ports = SerialPort.GetPortNames();
                foreach (var port in ports)
                    Add(new SerialPort(port, 115200, Parity.None, 8, StopBits.One));
            }
        }

        public bool HasItem => this.Count > 0;

    }
}
