using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityComponentSystemDemo.Components
{
    public class PackageComponent : Component
    {
        public int FromEntity;
        public int ToEntity;
        public PackageStatus Status;
    }

    public enum PackageStatus
    {
        Waiting, Moving, Delivered
    }
}
