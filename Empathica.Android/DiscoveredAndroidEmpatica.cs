using System;
using Com.Empatica.Empalink;
using Empathica.Shared;

namespace Empathica.Android
{
    class DiscoveredAndroidEmpatica : IEmpaticaDevice
    {
        public string Adress { get; set; }
        public EmpaticaDevice ActualDevice { get; set; }
    }
}