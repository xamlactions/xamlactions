using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XamlActions.Crypto;

namespace XamlActions.Tests.Crypto {
    public class Md5Tests {

        [Test]
        public void HashString() {
            Assert.AreEqual("ACBD18DB4CC2F85CEDEF654FCCC4A4D8", Md5.GetHashString("foo"));
            Assert.AreEqual("37B51D194A7513E45B56F6524F2D51F2", Md5.GetHashString("bar"));
            Assert.AreEqual("81DC9BDB52D04DC20036DBD8313ED055", Md5.GetHashString("1234"));
            Assert.AreEqual("EF238EA00A26528DE40FF231E5A97F50", Md5.GetHashString("foo123"));
        }
    }
}
